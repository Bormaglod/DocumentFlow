//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Settings;

namespace DocumentFlow.Controls;

public partial class CardPanel : UserControl
{
    private readonly ICard card;

    public CardPanel(ICard card, StartPageSettings settings)
    {
        InitializeComponent();

        labelCaption.Text = card.Title;

        if (card is Control control)
        {
            control.Dock = DockStyle.Fill;
            Controls.Add(control);

            control.BringToFront();
        }

        Size = new Size(
            settings.CardSize.Width * card.Size.Width + settings.CardPadding * (card.Size.Width - 1),
            settings.CardSize.Height * card.Size.Height + settings.CardPadding * (card.Size.Height - 1));

        this.card = card;
    }

    public ICard Card => card;
}
