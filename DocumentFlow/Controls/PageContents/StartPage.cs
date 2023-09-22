//-----------------------------------------------------------------------
// Copyright © 2010-2023 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@yandex.ru>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 30.12.2022
//-----------------------------------------------------------------------

using DocumentFlow.Controls.Interfaces;
using DocumentFlow.Data.Exceptions;
using DocumentFlow.Settings;

using Microsoft.Extensions.Options;

namespace DocumentFlow.Controls.PageContents;

public partial class StartPage : UserControl, IStartPage
{
    private readonly IEnumerable<ICard> cards;
    private readonly StartPageSettings settings;

    public StartPage(IEnumerable<ICard> cards, IOptions<LocalSettings> options)
    {
        InitializeComponent();

        this.cards = cards;
        settings = options.Value.StartPage;
        Text = "Начальная страница";

        foreach (var card in cards.OrderBy(x => x.Index))
        {
            try
            {
                panelCards.Controls.Add(new CardPanel(card, settings));
            }
            catch (RepositoryException)
            {
            }
        }

        UpdateCardControlLayout();
    }


    public void NotifyPageClosing()
    {
    }

    public void RefreshPage()
    {
        foreach (var card in cards)
        {
            card.RefreshCard();
        }
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        RefreshPage();
    }

    private void UpdateCardControlLayout()
    {
        Point pos = new(settings.CardPadding, settings.CardPadding);

        List<CardPanel> placed = new();
        foreach (var card in cards.OrderBy(x => x.Index))
        {
            if (card is Control cardControl && cardControl.Parent is CardPanel cardPanel)
            {
                if (pos.X != settings.CardPadding)
                {
                    if (pos.X + cardPanel.Width > panelCards.Width)
                    {
                        pos = new(settings.CardPadding, pos.Y + settings.CardSize.Height + settings.CardPadding);
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
        int xPos = p.X + settings.CardSize.Width + settings.CardPadding;
        int yPos = p.Y;
        if (xPos + panel.Width > panelCards.Width)
        {
            xPos = settings.CardPadding;
            yPos += settings.CardSize.Height + settings.CardPadding;
        }

        return new Point(xPos, yPos);
    }

    private void StartPage_SizeChanged(object sender, EventArgs e) => UpdateCardControlLayout();

    private void ButtonRefresh_Click(object sender, EventArgs e) => RefreshPage();
}
