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
    public class CodeContent
    {
        public CodeContent(IBrowserCode browser, IEditorCode editor, bool compiled)
        {
            Browser = browser;
            Editor = editor;
            Compiled = compiled;
        }

        public IBrowserCode Browser { get; set; }
        public IEditorCode Editor { get; set; }
        public bool Compiled { get; set; }

        public override bool Equals(object obj)
        {
            return obj is CodeContent other &&
                   EqualityComparer<IBrowserCode>.Default.Equals(Browser, other.Browser) &&
                   EqualityComparer<IEditorCode>.Default.Equals(Editor, other.Editor);
        }

        public override int GetHashCode()
        {
            int hashCode = 480819551;
            hashCode = hashCode * -1521134295 + EqualityComparer<IBrowserCode>.Default.GetHashCode(Browser);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEditorCode>.Default.GetHashCode(Editor);
            return hashCode;
        }

        public void Deconstruct(out IBrowserCode browser, out IEditorCode editor, out bool compiled)
        {
            browser = Browser;
            editor = Editor;
            compiled = Compiled;
        }

        public void Reset() => Compiled = false;

        public static implicit operator (IBrowserCode browser, IEditorCode editor, bool compiled)(CodeContent value)
        {
            return (value.Browser, value.Editor, value.Compiled);
        }

        public static implicit operator CodeContent((IBrowserCode browser, IEditorCode editor, bool compiled) value)
        {
            return new CodeContent(value.browser, value.editor, value.compiled);
        }

        public static implicit operator CodeContent((IBrowserCode browser, IEditorCode editor) value)
        {
            return new CodeContent(value.browser, value.editor, true);
        }
    }
}
