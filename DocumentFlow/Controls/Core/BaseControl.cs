//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 04.11.2021
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Controls.Core;

public partial class BaseControl : UserControl
{
    public BaseControl(string property)
    {
        InitializeComponent();

        AllowSaving = true;
        Dock = DockStyle.Top;
        PropertyName = property;
        DefaultAsNull = true;
    }

    public bool AllowSaving { get; set; }

    public string PropertyName { get; set; }

    public bool DefaultAsNull { get; set; }

    public IEditorPage? EditorPage { get; set; }

    protected override void SetVisibleCore(bool value)
    {
        base.SetVisibleCore(value);
        AllowSaving = value;
    }
}
