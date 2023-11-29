using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Project3.StoryDeal;

public partial class AddDealWndw : Window
{
    public string _connString = "server = sql8.freesqldatabase.com;database = sql8665106; port = 3306; User = sql8665106; password = iQZp6r1cIn";
    private MySqlConnection _connection;
    public AddDealWndw()
    {
        InitializeComponent();
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }

    private void BackBtn_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }

    public InitComboBoxObject()
    {
        string sql = "select ObjectId, N.Nazvanie " +
                     "from sql8665106.Objeсt" +
                     " join Nazvanie N on Objeсt.Nazvanie = N.NazvanieID";
        var objects = new List<Object.Object>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new Object.Object()
            {
                ObjectId = reader.GetInt32(column:"ObjectID"),
                Nazvanie = reader.GetString(column: "Nazvanie"),
            };
            objects.Add(current);
        }
        _connection.Close();
        ObjCMB.ItemsSource = objects;
    }
}