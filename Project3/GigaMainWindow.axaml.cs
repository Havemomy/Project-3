using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Project3.Sobstvennik;

namespace Project3;

public partial class GigaMainWindow : Window
{
    public GigaMainWindow()
    {
        InitializeComponent();
    }

    private void SdelkiiBTN_OnClick(object? sender, RoutedEventArgs e)
    {
        SdelkaWNDW sdelkaWndw = new SdelkaWNDW();
        sdelkaWndw.Show();
    }
    private void SobstvennikiBTN_OnClick(object? sender, RoutedEventArgs e)
    {
        SobstvWndw sobstvWndw = new SobstvWndw();
        sobstvWndw.Show();
    }
}