using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Threading;

namespace MatchGame;

public partial class MainWindow : Window
{
    DispatcherTimer timer = new();
    int tenthsOfSecondsElapsed = 0;
    int matchesFound = 0;
    
    TextBlock lastTextBlock;
    bool findingMatch = false;
    
    public MainWindow()
    {
        InitializeComponent();

        timer.Interval = TimeSpan.FromSeconds(.1);
        timer.Tick += Timer_Tick;
        SetUpGame();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        tenthsOfSecondsElapsed++;
        TimeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
        if (matchesFound == 8)
        {
            timer.Stop();
            TimeTextBlock.Text += " - Play again?";
        }
    }

    private void SetUpGame()
    {
        List<string> animalEmoji = new()
        {
            "🐶", "🐶",
            "🐱", "🐱",
            "🐭", "🐭",
            "🐹", "🐹",
            "🐰", "🐰",
            "🦊", "🦊",
            "🐻", "🐻",
            "🐼", "🐼"
        };

        Random random = new Random();

        foreach (TextBlock textBlock in MainGrid.Children.OfType<TextBlock>())
        {
            if (textBlock.Name != "TimeTextBlock")
            {
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                textBlock.IsVisible = true;
                animalEmoji.RemoveAt(index);
            }
        }
        
        timer.Start();
        tenthsOfSecondsElapsed = 0;
        matchesFound = 0;
    }

    private void TextBlock_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        TextBlock textBlock = sender as TextBlock;

        if (!findingMatch)
        {
            textBlock.Foreground = Brushes.Crimson;
            lastTextBlock = textBlock;
            findingMatch = true;
        }
        else if (textBlock != lastTextBlock && textBlock.Text == lastTextBlock.Text)
        {
            matchesFound++;
            lastTextBlock.ClearValue(TextBlock.ForegroundProperty);
            lastTextBlock.IsVisible = false;
            textBlock.IsVisible = false;
            findingMatch = false;
        }
        else
        {
            lastTextBlock.ClearValue(TextBlock.ForegroundProperty);
            findingMatch = false;
        }
    }

    
    private void TimeTextBlock_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (matchesFound == 8)
        {
            SetUpGame();
        }
    }
}