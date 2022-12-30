//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Controls.PageContents;

public partial class StartPage : UserControl, IPage
{
    public StartPage(IEnumerable<ICard> cards)
    {
        InitializeComponent();

        foreach(var card in cards) 
        { 

        }
    }

    public void OnPageClosing()
    {
        throw new NotImplementedException();
    }

    public void RefreshPage()
    {
        throw new NotImplementedException();
    }
}
