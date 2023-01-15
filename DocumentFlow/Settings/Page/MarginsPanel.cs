//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.07.2022
//
// Версия 2023.1.8
//  - отключено
//
//-----------------------------------------------------------------------

/*using DocumentFlow.Controls.Infrastructure;
using DocumentFlow.Settings.Infrastructure;*/

namespace DocumentFlow.Settings.Page;

public partial class MarginsPanel : UserControl//, ISettingsPage
{
    public MarginsPanel(/*IMarginsSettings settings*/)
    {
        InitializeComponent();
/*
        textBoxLeft.IntegerValue = settings.Left;
        textBoxTop.IntegerValue = settings.Top;
        textBoxRight.IntegerValue = settings.Right;
        textBoxBottom.IntegerValue = settings.Bottom;*/
    }

    /*public string Path => "/Печать/Поля";

    public Control Control => this;*/
}
