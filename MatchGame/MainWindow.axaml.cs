using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace MatchGame;

public partial class MainWindow : Window
{
    TextBlock lastTextBlock;
    bool findingMatch = false;
    
    public MainWindow()
    {
        InitializeComponent();

        SetUpGame();
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
            int index = random.Next(animalEmoji.Count);
            string nextEmoji = animalEmoji[index];
            textBlock.Text = nextEmoji;
            animalEmoji.RemoveAt(index);
        }
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
        else if (textBlock.Text == lastTextBlock.Text)
        {
            lastTextBlock.IsVisible = false;
            textBlock.IsVisible = false;
            findingMatch = false;
        }
        else
        {
            lastTextBlock.Foreground = textBlock.Foreground;
            findingMatch = false;
        }
    }
}