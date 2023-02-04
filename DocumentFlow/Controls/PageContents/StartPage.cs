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
// Версия 2023.1.29
//  - реализована логика для размещения карточек произвольного размера
// Версия 2023.2.4
//  - добавлена обработка исключения DbAccessException - если карточка
//    генерирует его (у пользователя нет каких-то прав), то она не 
//    будет добавлятся
//
//-----------------------------------------------------------------------

using DocumentFlow.Core.Exceptions;
using DocumentFlow.Infrastructure.Controls;

using Windows.Media.Devices;

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

        foreach (var card in cards)
        {
            try
            {
                panelCards.Controls.Add(new CardPanel(card, cardSize));
            }
            catch (DbAccessException)
            {
            }
        }

        UpdateCardControlLayout();
    }

    public void OnPageClosing() { }

    public void RefreshPage()
    {
        foreach (var card in cards)
        {
            card.RefreshCard();
        }
    }

    private void UpdateCardControlLayout()
    {
        Point pos = new(cardPadding, cardPadding);

        List<CardPanel> placed = new();
        foreach (var card in cards.OrderBy(x => x.Index))
        {
            if (card is Control cardControl && cardControl.Parent is CardPanel cardPanel) 
            {
                if (pos.X != cardPadding)
                {
                    if (pos.X + cardPanel.Width > panelCards.Width)
                    {
                        pos = new(cardPadding, pos.Y + cardSize.Height + cardPadding);
                    }
                }

                // проверим, не перекрывает ли карточка другие
                // если есть перекрытие, выберем другую точку размещения
                // и еще раз попробуем проверить
                while (true)
                {
                    bool notIntersects = true;
                    Rectangle cur = new(pos, cardPanel.Size);
                    foreach (var p in placed)
                    {
                        Rectangle pRect = new(p.Location, p.Size);
                        if (cur.IntersectsWith(pRect))
                        {
                            pos = NextPosition(cardPanel, pos);

                            notIntersects = false;
                            break;
                        }
                    }

                    if (notIntersects)
                        break;
                }

                cardPanel.Location = pos;
                placed.Add(cardPanel);

                pos = NextPosition(cardPanel, pos);
            }
        }
    }

    private Point NextPosition(CardPanel panel, Point p)
    {
        int xPos = p.X + cardSize.Width + cardPadding;
        int yPos = p.Y;
        if (xPos + panel.Width > panelCards.Width)
        {
            xPos = cardPadding;
            yPos += cardSize.Height + cardPadding;
        }

        return new Point(xPos, yPos);
    }

    private void StartPage_SizeChanged(object sender, EventArgs e) => UpdateCardControlLayout();

    private void ButtonRefresh_Click(object sender, EventArgs e) => RefreshPage();
}
