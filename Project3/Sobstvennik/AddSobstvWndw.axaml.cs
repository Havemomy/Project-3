using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Project3.Sobstvennik;

public partial class AddSobstvWndw : Window
{
    public AddSobstvWndw()
    {
        InitializeComponent();
    }
    
    
    
    public event Action AddEvent;
    private void OnAdd() 
    {
        AddEvent?.Invoke();
    }
    public Sobstvennik? Result { get; private set; }
}