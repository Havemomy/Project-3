using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Project3.Sobstvennik;

public partial class SobstvWndw : Window
{
    public string _connString =
        "server = localhost;database = мразь; port = 3306; User = root; password = Bebra2281337";

    private List<Sobstvennik> _sobstvenniks;
    private MySqlConnection _connection;
    public SobstvWndw()
    {
        InitializeComponent();
        ShowTable();
    }

    private void ShowTable()
    {
        string sql = "select SobstvennikID, FIO, Phone from sobstvennik";
        _sobstvenniks = new List<Sobstvennik>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            _sobstvenniks.Add(new Sobstvennik(
                reader.GetInt32("SobstvennikID"),
                reader.GetString("FIO"),
                reader.GetString("Phone")
            ));
        }
        _connection.Close();
        SobstvenniksGrid.ItemsSource = _sobstvenniks;
    }

    private void AddSobstv_OnClick(object? sender, RoutedEventArgs e)
    {
        AddSobstvWndw addSobstvWndw = new AddSobstvWndw();
        addSobstvWndw.AddEvent += () => {
            var newList = SobstvenniksGrid.ItemsSource.Cast<Sobstvennik>().ToList();
            newList.Add(addSobstvWndw.Result);
            SobstvenniksGrid.ItemsSource = newList;
            Show();
        };
        this.Hide();
        addSobstvWndw.Show();
    }

    private void DelSobstv_OnClick(object? sender, RoutedEventArgs e)
    {
        var selectedItem = SobstvenniksGrid.SelectedItem as Sobstvennik;
        if (selectedItem is null) {
            return;
        }

        _connection.Open();
        string sql = "delete from StoryDeal where DealID = @id";
        MySqlCommand command = new MySqlCommand(sql, _connection);
        command.Parameters.AddWithValue("@id", selectedItem.SobsvennikID);
        command.ExecuteNonQuery();
        _connection.Close();

        var newList = SobstvenniksGrid.ItemsSource.Cast<Sobstvennik>().ToList();
        var itemToRemove = newList.FirstOrDefault(x => x.SobsvennikID == selectedItem.SobsvennikID);
        newList.Remove(itemToRemove);

        SobstvenniksGrid.ItemsSource = newList;
    }

    private void BackBTN_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close();
    }
}