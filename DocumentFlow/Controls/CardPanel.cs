//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 31.12.2022
//
// Версия 2023.1.19
//  - удалён метод RefreshPanel
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls;

public partial class CardPanel : UserControl
{
    private readonly ICard card;

    public CardPanel(ICard card, Size cardSize)
    {
        InitializeComponent();

        labelCaption.Text = card.Title;

        if (card is Control control)
        {
            var cardPadding = Properties.Settings.Default.CardPadding;

            control.Dock = DockStyle.Fill;
            control.Size = new Size(
                cardSize.Width * card.Size.Width + cardPadding * (card.Size.Width - 1), 
                cardSize.Height * card.Size.Height + cardPadding * (card.Size.Height - 1));
            Controls.Add(control);

            control.BringToFront();
        }

        this.card = card;
    }

    public ICard Card => card;
}
