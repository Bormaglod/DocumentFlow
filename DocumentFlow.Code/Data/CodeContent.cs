//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.12.2020
// Time: 19:58
//-----------------------------------------------------------------------

using System.Collections.Generic;

namespace DocumentFlow.Code.Data
{
    public struct CodeContent
    {
        public IBrowserCode browser;
        public IEditorCode editor;

        public CodeContent(IBrowserCode browser, IEditorCode editor)
        {
            this.browser = browser;
            this.editor = editor;
        }

        public override bool Equals(object obj)
        {
            return obj is CodeContent other &&
                   EqualityComparer<IBrowserCode>.Default.Equals(browser, other.browser) &&
                   EqualityComparer<IEditorCode>.Default.Equals(editor, other.editor);
        }

        public override int GetHashCode()
        {
            int hashCode = 480819551;
            hashCode = hashCode * -1521134295 + EqualityComparer<IBrowserCode>.Default.GetHashCode(browser);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEditorCode>.Default.GetHashCode(editor);
            return hashCode;
        }

        public void Deconstruct(out IBrowserCode browser, out IEditorCode editor)
        {
            browser = this.browser;
            editor = this.editor;
        }

        public static implicit operator (IBrowserCode browser, IEditorCode editor)(CodeContent value)
        {
            return (value.browser, value.editor);
        }

        public static implicit operator CodeContent((IBrowserCode browser, IEditorCode editor) value)
        {
            return new CodeContent(value.browser, value.editor);
        }
    }
}
