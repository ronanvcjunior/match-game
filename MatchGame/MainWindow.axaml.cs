using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;

namespace MatchGame;

public partial class MainWindow : Window
{
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
}