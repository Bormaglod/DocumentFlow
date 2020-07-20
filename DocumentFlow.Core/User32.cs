//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 20.07.2020
// Time: 19:44
//-----------------------------------------------------------------------

namespace DocumentFlow.Core
{
    using System;
    using System.Runtime.InteropServices;

    public class User32
    {
        [Flags]
        public enum WS : uint
        {
            BORDER       = 0x00800000,
            CAPTION      = 0x00C00000,
            CHILD        = 0x40000000,
            CLIPCHILDREN = 0x02000000,
            CLIPSIBLINGS = 0x04000000,
            DISABLED     = 0x08000000,
            DLGFRAME     = 0x00400000,
            GROUP        = 0x00020000,
            HSCROLL      = 0x00100000,
            ICONIC       = 0x20000000,
            MAXIMIZE     = 0x01000000,
            MAXIMIZEBOX  = 0x00010000,
            MINIMIZE     = 0x20000000,
            MINIMIZEBOX  = 0x00020000,
            OVERLAPPED   = 0x00000000,
            POPUP        = 0x80000000,
            SIZEBOX      = 0x00040000,
            SYSMENU      = 0x00080000,
            TABSTOP      = 0x00010000,
            THICKFRAME   = 0x00040000,
            TILED        = 0x00000000,
            VISIBLE      = 0x10000000,
            WS_VSCROLL   = 0x00200000,

            OVERLAPPEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX,
            POPUPWINDOW = POPUP | BORDER | SYSMENU,
            WS_TILEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX
        }

        public enum GWL
        {
            WNDPROC    = -4,
            HINSTANCE  = -6,
            HWNDPARENT = -8,
            ID         = -12,
            STYLE      = -16,
            EXSTYLE    = -20,
            USERDATA   = -21
        }

        [Flags]
        public enum WSEX : uint
        {
            ACCEPTFILES         = 0x00000010,
            APPWINDOW           = 0x00040000,
            CLIENTEDGE          = 0x00000200,
            COMPOSITED          = 0x02000000,
            CONTEXTHELP         = 0x00000400,
            CONTROLPARENT       = 0x00010000,
            DLGMODALFRAME       = 0x00000001,
            LAYERED             = 0x00080000,
            LAYOUTRTL           = 0x00400000,
            LEFT                = 0x00000000,
            LEFTSCROLLBAR       = 0x00004000,
            LTRREADING          = 0x00000000,
            MDICHILD            = 0x00000040,
            NOACTIVATE          = 0x08000000,
            NOINHERITLAYOUT     = 0x00100000,
            NOPARENTNOTIFY      = 0x00000004,
            NOREDIRECTIONBITMAP = 0x00200000,
            RIGHT               = 0x00001000,
            RIGHTSCROLLBAR      = 0x00000000,
            RTLREADING          = 0x00002000,
            STATICEDGE          = 0x00020000,
            TOOLWINDOW          = 0x00000080,
            TOPMOST             = 0x00000008,
            TRANSPARENT         = 0x00000020,
            WINDOWEDGE          = 0x00000100,
            TOP                 = 0x00000000,

            OVERLAPPEDWINDOW = WINDOWEDGE | CLIENTEDGE,
            PALETTEWINDOW = WINDOWEDGE | TOOLWINDOW | TOPMOST
        }

        [Flags]
        public enum MF : uint
        {
            BYCOMMAND  = 0x00000000,
            BYPOSITION = 0x00000400,
            DISABLED   = 0x00000002,
            ENABLED    = 0x00000000,
            GRAYED     = 0x00000001
        }

        public enum SC
        {
            MAXIMIZE = 0xF030,
            RESTORE = 0xF120
        }

        [Flags]
        public enum TPM : uint
        {
            CENTERALIGN  = 0x0004,
            LEFTALIGN    = 0x0000,
            RIGHTALIGN   = 0x0008,

            BOTTOMALIGN  = 0x0020,
            TOPALIGN     = 0x0000,
            VCENTERALIGN = 0x0010,

            NONOTIFY     = 0x0080,
            RETURNCMD    = 0x0100,

            LEFTBUTTON   = 0x0000,
            RIGHTBUTTON  = 0x0002,

            HORNEGANIMATION = 0x0800,
            HORPOSANIMATION = 0x0400,
            NOANIMATION     = 0x4000,
            VERNEGANIMATION = 0x2000,
            VERPOSANIMATION = 0x1000,

            HORIZONTAL = 0x0000,
            VERTICAL   = 0x0040
        }

        public enum WM : uint
        {
            SYSCOMMAND = 0x112
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowLong(IntPtr hWnd, GWL gwlIndex, uint dwNewLong);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetWindowLong(IntPtr hWnd, GWL gwlIndex);
    }
}
