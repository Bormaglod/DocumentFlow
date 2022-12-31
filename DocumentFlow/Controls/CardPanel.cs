//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Controls;

public partial class CardPanel : UserControl
{
    private readonly ICard card;

    public CardPanel(ICard card)
    {
        InitializeComponent();

        labelCaption.Text = card.Title;

        if (card is Control control)
        {
            control.Dock = DockStyle.Fill;
            Controls.Add(control);

            control.BringToFront();
        }

        this.card = card;
    }

    public ICard Card => card;

    public void RefreshPanel() => card.RefreshCard();
}
