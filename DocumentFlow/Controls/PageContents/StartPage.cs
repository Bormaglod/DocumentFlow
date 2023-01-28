//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//
// Версия 2022.12.31
//  - наследование от IPage заменено на IStartPage
//  - реализован весь функционал
// Версия 2023.1.19
//  - добавлена кнопка "Обновить"
//  - карты переместились на panelCards
//  - RefreshPage обновляет карты напрямую, а не через CardPanel
// Версия 2023.1.22
//  - DocumentFlow.Controls.Infrastructure перемещено в DocumentFlow.Infrastructure.Controls
// Версия 2023.1.28
//  - немного оптимизации
//
//-----------------------------------------------------------------------

using DocumentFlow.Infrastructure.Controls;

namespace DocumentFlow.Controls.PageContents;

public partial class StartPage : UserControl, IStartPage
{
    private readonly Size cardSize;
    private readonly IEnumerable<ICard> cards;
    private readonly int cardPadding;

    public StartPage(IEnumerable<ICard> cards)
    {
        InitializeComponent();

        this.cards = cards;

        Text = "Начальная страница";
        cardSize = Properties.Settings.Default.CardSize;
        cardPadding = Properties.Settings.Default.CardPadding;

        SetCardControlLayout();
    }

    public void OnPageClosing() { }

    public void RefreshPage()
    {
        foreach (var card in cards)
        {
            card.RefreshCard();
        }
    }

    private void SetCardControlLayout()
    {
        //calc visible column count
        int columnCount = Width / (cardSize.Width + cardPadding);

        foreach (var card in cards)
        {
            var cardPanel = new CardPanel(card, cardSize);
            SetCardLocation(cardPanel, columnCount);

            panelCards.Controls.Add(cardPanel);
        }
    }

    private void UpdateCardControlLayout()
    {
        int columnCount = Width / (cardSize.Width + cardPadding);

        foreach (var cardPanel in Controls.OfType<CardPanel>())
        {
            SetCardLocation(cardPanel, columnCount);
        }
    }

    private void SetCardLocation(CardPanel cardPanel, int columnCount)
    {
        if (columnCount > 0)
        {
            //calc the x index and y index.
            int xPos = cardPanel.Card.Index % columnCount * (cardSize.Width + cardPadding) + cardPadding;
            int yPos = cardPanel.Card.Index / columnCount * (cardSize.Height + cardPadding) + cardPadding;

            cardPanel.Location = new Point(xPos, yPos);
        }
        else
        {
            cardPanel.Location = new Point(0, 0);
        }

        cardPanel.Visible = columnCount > 0;
    }

    private void StartPage_SizeChanged(object sender, EventArgs e)
    {
        UpdateCardControlLayout();
    }

    private void ButtonRefresh_Click(object sender, EventArgs e) => RefreshPage();
}
