using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public static class PanelCardLayout
{
    public static Panel AddCardsToPanel<T>(
        Panel panel,
        IEnumerable<T> cards,
        Func<T, Control> createCardControl,
        int columns = 1,
        int horizontalSpacing = 5,
        int verticalSpacing = 5,
        Padding? cardMargin = null)
    {
        if (panel == null || cards == null || createCardControl == null)
            return panel ?? new Panel();

        var margin = cardMargin ?? new Padding(5);
        panel.Controls.Clear();

        int availableWidth = CalculateAvailableWidth(panel);

        int cardWidth = availableWidth - (margin.Horizontal + horizontalSpacing * 2 - 5);

        ProcessSingleColumnCardsRecursive(
            cards.ToList(),
            0,
            panel,
            createCardControl,
            cardWidth,
            horizontalSpacing,
            verticalSpacing,
            margin,
            0);

        SetupPanelAutoScroll(panel);

        return panel;
    }

    private static void ProcessSingleColumnCardsRecursive<T>(
        IReadOnlyList<T> cards,
        int index,
        Panel panel,
        Func<T, Control> createCardControl,
        int cardWidth,
        int horizontalSpacing,
        int verticalSpacing,
        Padding margin,
        int currentY)
    {
        if (index >= cards.Count)
            return;

        var cardControl = createCardControl(cards[index]);

        cardControl.Width = cardWidth;

        cardControl.Height = CalculateAutoHeight(cardControl);
        cardControl.Margin = margin;

        int x = horizontalSpacing + margin.Left;
        cardControl.Location = new Point(x, currentY);

        panel.InvokeIfRequired(() => panel.Controls.Add(cardControl));

        ProcessSingleColumnCardsRecursive(
            cards,
            index + 1,
            panel,
            createCardControl,
            cardWidth,
            horizontalSpacing,
            verticalSpacing,
            margin,
            currentY + cardControl.Height + verticalSpacing + margin.Vertical);
    }

    private static int CalculateAvailableWidth(Panel panel)
    {
        int scrollBarWidth = SystemInformation.VerticalScrollBarWidth;
        int availableWidth = panel.ClientSize.Width;

        if (panel.AutoScroll)
        {
            availableWidth -= scrollBarWidth - 3;
        }

        return Math.Max(20, availableWidth);
    }

    private static int CalculateAutoHeight(Control card)
    {
        int preferredHeight = card.PreferredSize.Height;

        if (card is Button button && button.Text.Contains("Услуга:"))
        {
            using (Graphics g = button.CreateGraphics())
            {
                SizeF textSize = g.MeasureString(button.Text, button.Font, card.Width - 20);
                return Math.Max((int)textSize.Height + 20, 120);
            }
        }

        return Math.Max(preferredHeight, 60);
    }

    private static void SetupPanelAutoScroll(Panel panel)
    {
        panel.AutoScroll = false;

        if (panel.Controls.Count > 0)
        {
            int maxY = panel.Controls.Cast<Control>().Max(c => c.Bottom);

            panel.AutoScroll = maxY > panel.ClientSize.Height;

            if (panel.AutoScroll)
            {
                panel.PerformLayout();
            }
        }
        else
        {
            panel.AutoScroll = false;
        }
    }

    private static void InvokeIfRequired(this Control control, Action action)
    {
        if (control.InvokeRequired)
            control.Invoke(action);
        else
            action();
    }

    public static Func<IEnumerable<T>, Panel> CreateSingleColumnCardLayout<T>(
        Func<T, Control> cardFactory,
        int horizontalSpacing = 5,
        int verticalSpacing = 5)
    {
        return cards =>
        {
            var panel = new Panel
            {
                AutoScroll = false,
                Dock = DockStyle.Fill,
                Padding = new Padding(0)
            };

            return AddCardsToPanel(
                panel,
                cards,
                cardFactory,
                columns: 1,
                horizontalSpacing,
                verticalSpacing,
                cardMargin: new Padding(5));
        };
    }

    public static void AdjustCardWidthsOnResize(Panel panel)
    {
        if (panel == null || panel.Controls.Count == 0) return;

        int availableWidth = CalculateAvailableWidth(panel);

        var firstCard = panel.Controls[0];
        int horizontalSpacing = 5;
        var margin = firstCard.Margin;

        int newCardWidth = availableWidth - (margin.Horizontal + horizontalSpacing * 2);

        UpdateCardsWidthAndPositionRecursive(
            panel.Controls,
            newCardWidth,
            horizontalSpacing,
            margin,
            0,
            0);

        SetupPanelAutoScroll(panel);
    }

    private static void UpdateCardsWidthAndPositionRecursive(
        Control.ControlCollection controls,
        int newWidth,
        int horizontalSpacing,
        Padding margin,
        int currentIndex,
        int currentY)
    {
        if (currentIndex >= controls.Count) return;

        var card = controls[currentIndex];

        card.Width = newWidth;

        card.Height = CalculateAutoHeight(card);

        int x = horizontalSpacing + margin.Left;
        card.Location = new Point(x, currentY);

        UpdateCardsWidthAndPositionRecursive(
            controls,
            newWidth,
            horizontalSpacing,
            margin,
            currentIndex + 1,
            currentY + card.Height + 5 + margin.Vertical);
    }
}