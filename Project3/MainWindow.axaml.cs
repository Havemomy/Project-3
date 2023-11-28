using System.Collections.Generic;
using Avalonia.Controls;
using MySql.Data.MySqlClient;


namespace Project3;

public partial class MainWindow : Window
{
    public string _connString = "server = sql8.freesqldatabase.com;database = sql8665106; port = 3306; User = sql8665106; password = iQZp6r1cIn";
    private List<StoryDeal.StoryDeal> _storyDeals;
    private MySqlConnection _connection;
    public MainWindow()
    {
        InitializeComponent();
        ShowTable();
    }

    private void ShowTable()
    {
        string sql = "select DealID, Nazvanie, FIO, PokFIO, DovFIO from StoryDeal " +
                     " join sql8665106.Nazvanie n on StoryDeal.Object = n.NazvanieID" +
                     " join sql8665106.Sobstvennik s on StoryDeal.Sobstvennik = s.SobsvennikID" +
                     " join sql8665106.Pokupatel p on StoryDeal.Pokupatel = p.PokupatelID" +
                     " join sql8665106.DoverennoeLico d on StoryDeal.DoverennoeLico = d.LicoID";
        _storyDeals = new List<StoryDeal.StoryDeal>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            _storyDeals.Add(new StoryDeal.StoryDeal(
                reader.GetInt32("DealID"),
                reader.GetString("Nazvanie"),
                reader.GetString("FIO"),
                reader.GetString("PokFIO"),
                reader.GetString("DovFIO")
                ));
        }
        _connection.Close();
        StoryDealGrid.ItemsSource = _storyDeals;
    }
}