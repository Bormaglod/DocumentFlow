//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.10.2020
// Time: 18:47
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DocumentFlow.Code.Implementation
{
    public class ControlData : IControl
    {
        public ControlData(Control control)
        {
            Owner = control;
        }

        int IControl.Left 
        {
            get => Owner.Left;
            set => Owner.Left = value;
        }

        int IControl.Top 
        {
            get => Owner.Top;
            set => Owner.Top = value;
        }

        int IControl.Width 
        {
            get => Owner.Width;
            set => Owner.Width = value;
        }

        int IControl.Height 
        {
            get => Owner.Height;
            set => Owner.Height = value;
        }

        bool IControl.Enabled 
        {
            get => Owner.Enabled;
            set => Owner.Enabled = value;
        }

        bool IControl.Visible 
        {
            get => Owner.Visible;
            set => Owner.Visible = value;
        }

        DockStyle IControl.Dock 
        {
            get => Owner.Dock;
            set => Owner.Dock = value;
        }

        IList<int> IControl.Margin 
        {
            get => new int[] { Owner.Margin.Left, Owner.Margin.Top, Owner.Margin.Right, Owner.Margin.Bottom };
            set => Owner.Margin = new Padding(value[0], value[1], value[2], value[3]);
        }

        IList<int> IControl.Padding 
        {
            get => new int[] { Owner.Padding.Left, Owner.Padding.Top, Owner.Padding.Right, Owner.Padding.Bottom };
            set => Owner.Padding = new Padding(value[0], value[1], value[2], value[3]);
        }

        bool IControl.FitToSize
        {
            get
            {
                if (Owner is IEditControl edit)
                {
                    return edit.FitToSize;
                }

                return false;
            }

            set
            {
                if (Owner is IEditControl edit)
                {
                    edit.FitToSize = value;
                }
            }
        }

        IControl IControl.SetLeft(int left)
        {
            IControl control = this;
            control.Left = left;
            return control;
        }

        IControl IControl.SetTop(int top)
        {
            IControl control = this;
            control.Top = top;
            return control;
        }

        IControl IControl.SetWidth(int width)
        {
            IControl control = this;
            control.Width = width;
            return control;
        }

        IControl IControl.SetHeight(int height)
        {
            IControl control = this;
            control.Height = height;
            return control;
        }

        IControl IControl.SetLocation(Point location)
        {
            Owner.Location = location;
            return this;
        }

        IControl IControl.SetSize(Size size)
        {
            Owner.Size = size;
            return this;
        }

        IControl IControl.SetEnabled(bool enabled)
        {
            IControl control = this;
            control.Enabled = enabled;
            return control;
        }

        IControl IControl.SetVisible(bool visible)
        {
            IControl control = this;
            control.Visible = visible;
            return control;
        }

        IControl IControl.SetDock(DockStyle dock)
        {
            IControl control = this;
            control.Dock = dock;
            return control;
        }

        IControl IControl.SetMargin(int left, int top, int right, int bottom)
        {
            IControl control = this;
            control.Margin = new int[] { left, top, right, bottom }; ;
            return control;
        }

        IControl IControl.SetMargin(int all)
        {
            IControl control = this;
            control.Margin = new int[] { all, all, all, all };
            return control;
        }

        IControl IControl.SetPadding(int left, int top, int right, int bottom)
        {
            IControl control = this;
            control.Padding = new int[] { left, top, right, bottom };
            return control;
        }

        IControl IControl.SetPadding(int all)
        {
            IControl control = this;
            control.Padding = new int[] { all, all, all, all };
            return control;
        }

        IControl IControl.SetFitToSize(bool fitToSize)
        {
            IControl control = this;
            control.FitToSize = fitToSize;
            return control;
        }

        public Control Owner { get; }
    }
}
