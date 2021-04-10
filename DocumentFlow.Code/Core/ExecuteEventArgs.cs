//-----------------------------------------------------------------------
// Copyright © 2010-2021 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.10.2020
// Time: 13:43
//-----------------------------------------------------------------------

using System;

namespace DocumentFlow.Code.Core
{
    public class ExecuteEventArgs : EventArgs
    {
        private readonly IBrowser browser;
        private readonly IEditor editor;

        public ExecuteEventArgs(IBrowser browser) => this.browser = browser;
        public ExecuteEventArgs(IEditor editor) => this.editor = editor;

        public IBrowser Browser 
        { 
            get
            {
                if (browser == null)
                {
                    throw new Exception("Значение Browser не определено. Возможно необходимо использовать свойство Editor.");
                }

                return browser;
            }
        }
        public IEditor Editor 
        { 
            get
            {
                if (editor == null)
                {
                    throw new Exception("Значение Editor не определено. Возможно необходимо использовать свойство Browser.");
                }

                return editor;
            }
        }
    }
}
