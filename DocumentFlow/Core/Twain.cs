//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.03.2021
// Time: 20:37
//-----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using NTwain;
using NTwain.Data;
using DocumentFlow.Interfaces;

namespace DocumentFlow.Core
{
    public class Twain : ITwain
    {
        private readonly IntPtr windowHandle;
        private TwainSession twain;
        private DataSource currentDataSource;
        private Guid destinationId;

        public Twain(IntPtr windowHandle)
        {
            this.windowHandle = windowHandle;

            Setup();
        }

        public event EventHandler<CupturingImageEventArgs> CapturingImage;

        public int State => twain.State;

        public void Cleanup()
        {
            switch (twain.State)
            {
                case 4:
                    twain.CurrentSource.Close();
                    break;
                case 3:
                    twain.Close();
                    break;
                case > 2:
                    // normal close down didn't work, do hard kill
                    twain.ForceStepDown(2);
                    break;
            }
        }

        void ITwain.Capture(Guid destinationId)
        {
            if (this.destinationId != Guid.Empty)
                return;

            this.destinationId = destinationId;

            if (twain.State > 4)
            {
                return;
            }

            if (twain.State == 4)
            {
                twain.CurrentSource.Close();
            }

            if (currentDataSource == null)
            {
                currentDataSource = SelectTwainForm.ShowDialog(twain, Properties.Settings.Default.TwainDataSource);
            }

            if (currentDataSource != null)
            {
                Properties.Settings.Default.TwainDataSource = currentDataSource.Name;
                currentDataSource.Open();
            }
            else
            {
                return;
            }

            if (twain.State == 4)
            {
                //_twain.CurrentSource.CapXferCount.Set(4);

                if (twain.CurrentSource.Capabilities.CapUIControllable.IsSupported)
                {
                    PlatformInfo.Current.Log.Info("CapUIControllable is supported on thread " + Thread.CurrentThread.ManagedThreadId);
                    // hide scanner ui if possible
                    if (twain.CurrentSource.Enable(SourceEnableMode.ShowUI, false, windowHandle) == ReturnCode.Success)
                    {
                        PlatformInfo.Current.Log.Info("twain.CurrentSource.Enable() on thread " + Thread.CurrentThread.ManagedThreadId);
                    }
                }
                else
                {
                    PlatformInfo.Current.Log.Info("CapUIControllable is not supported on thread " + Thread.CurrentThread.ManagedThreadId);
                    if (twain.CurrentSource.Enable(SourceEnableMode.NoUI, true, windowHandle) == ReturnCode.Success)
                    {
                        PlatformInfo.Current.Log.Info("twain.CurrentSource.Enable() on thread " + Thread.CurrentThread.ManagedThreadId);
                    }
                }
            }
        }

        private void Setup()
        {
            var appId = TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetEntryAssembly());
            twain = new TwainSession(appId);

            twain.StateChanged += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("State changed to " + twain.State + " on thread " + Thread.CurrentThread.ManagedThreadId);
            };

            twain.TransferError += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Got xfer error on thread " + Thread.CurrentThread.ManagedThreadId);
            };

            twain.DataTransferred += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Transferred data event on thread " + Thread.CurrentThread.ManagedThreadId);

                // example on getting ext image info
                var infos = e.GetExtImageInfo(ExtendedImageInfo.Camera).Where(it => it.ReturnCode == ReturnCode.Success);
                foreach (var it in infos)
                {
                    var values = it.ReadValues();
                    PlatformInfo.Current.Log.Info(string.Format("{0} = {1}", it.InfoID, values.FirstOrDefault()));
                    break;
                }

                // handle image data
                Image image = null;
                if (e.NativeData != IntPtr.Zero)
                {
                    var stream = e.GetNativeImageStream();
                    if (stream != null)
                    {
                        image = Image.FromStream(stream);
                    }
                }
                else if (!string.IsNullOrEmpty(e.FileDataPath))
                {
                    image = new Bitmap(e.FileDataPath);
                }

                OnCapturingImage(destinationId, image);
            };

            twain.SourceDisabled += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Source disabled event on thread " + Thread.CurrentThread.ManagedThreadId);
                ClearDestination();
            };

            twain.TransferReady += (s, e) =>
            {
                PlatformInfo.Current.Log.Info("Transferr ready event on thread " + Thread.CurrentThread.ManagedThreadId);
            };

            // either set sync context and don't worry about threads during events,
            // or don't and use control.invoke during the events yourself
            PlatformInfo.Current.Log.Info("Setup thread = " + Thread.CurrentThread.ManagedThreadId);
            twain.SynchronizationContext = SynchronizationContext.Current;
            if (twain.State < 3)
            {
                // use this for internal msg loop
                twain.Open();
                // use this to hook into current app loop
                //_twain.Open(new WindowsFormsMessageLoopHook(this.Handle));
            }
        }

        private void OnCapturingImage(Guid id, Image image)
        {
            if (CapturingImage != null)
            {
                CapturingImage.Invoke(this, new CupturingImageEventArgs(id, image));
            }
        }

        private void ClearDestination() => destinationId = Guid.Empty;
    }
}
