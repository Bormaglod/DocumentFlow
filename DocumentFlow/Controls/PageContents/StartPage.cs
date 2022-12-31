//-----------------------------------------------------------------------
// Copyright © 2010-2022 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//
// Версия 2022.12.31
//  - наследование от IPage заменено на IStartPage
//  - реализован весь функционал
//
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Infrastructure;

namespace DocumentFlow.Controls.PageContents;

public partial class StartPage : UserControl, IStartPage
{
    private readonly Size cardSize;
    private readonly IEnumerable<ICard> cards;

    public StartPage(IEnumerable<ICard> cards)
    {
        InitializeComponent();

        this.cards = cards;

        Text = "Начальная страница";
        cardSize = Properties.Settings.Default.CardSize;

        SetCardControlLayout();
    }

    public void OnPageClosing() { }

    public void RefreshPage()
    {
        foreach (var cardPanel in Controls.OfType<CardPanel>())
        {
            cardPanel.RefreshPanel();
        }
    }

    private void SetCardControlLayout()
    {
        foreach (var card in cards)
        {
            //calc visible column count
            int columnCount = Width / cardSize.Width;

            var cardPanel = new CardPanel(card)
            {
                Width = cardSize.Width,
                Height = cardSize.Height
            };

            SetCardLocation(cardPanel, columnCount);

            Controls.Add(cardPanel);
        }
    }

    private void UpdateCardControlLayout()
    {
        int columnCount = Width / (cardSize.Width + 10);

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
            int xPos = cardPanel.Card.Index % columnCount * (cardSize.Width + 10) + 10;
            int yPos = cardPanel.Card.Index / columnCount * (cardSize.Height + 10) + 10;

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
}
