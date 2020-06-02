//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 23.03.2019
// Time: 22:06
//-----------------------------------------------------------------------

namespace DocumentFlow.DataSchema.Types
{
    using DocumentFlow.Controls;
    using DocumentFlow.DataSchema.Types.Core;

    [Tag("Image")]
    public class F_ImageBox : BindingEditorControl<L_ImageViewerBox, string>
    {
        protected override string GetDefaultValue() => string.Empty;

        protected override object GetValue() => Control.Base64Image;

        protected override void SetValue(object value) => Control.Base64Image = value == null ? DefaultValue : value.ToString();
    }
}
