using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Google.Protobuf.Collections;
using MySql.Data.MySqlClient;

namespace Project3.StoryDeal;

public partial class AddDealWndw : Window {
    public string _connString =
        "server = sql8.freesqldatabase.com;database = sql8665106; port = 3306; User = sql8665106; password = iQZp6r1cIn";

    private MySqlConnection _connection;

    public AddDealWndw() {
        InitializeComponent();
        InitComboBoxSobstv();
        InitComboBoxObject();
        InitComboBoxPokyp();
        InitComboBoxDovLic();
    }

    private void AddBtn_OnClick(object? sender, RoutedEventArgs e) {
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        string sql = "insert into StoryDeal (Object, Sobstvennik, Pokupatel, DoverennoeLico)" +
                     "values (@obj, @sobst, @pokup, @dovLic); select LAST_INSERT_ID()";
        int? id = null;
        using (MySqlCommand command = new MySqlCommand(sql, _connection)) {
            command.Parameters.AddWithValue("@obj", ObjCMB.SelectedValue);
            command.Parameters.AddWithValue("@sobst", SobCMB.SelectedValue);
            command.Parameters.AddWithValue("@pokup", PokCMB.SelectedValue);
            command.Parameters.AddWithValue("@dovLic", DowCMB.SelectedValue);
            id = Convert.ToInt32(command.ExecuteScalar());
        }

        _connection.Close();
        if (id is null) {
            Result = null;
        }
        else {
            Result = new StoryDeal(
                id.Value,
                (ObjCMB.SelectedItem as Object.Object).Nazvanie,
                (SobCMB.SelectedItem as Sobstvennik.Sobstvennik).FIO,
                (PokCMB.SelectedItem as Pokupatel.Pokupatel).PokFIO,
                (DowCMB.SelectedItem as DoverennoeLico.DoverennoeLico).DovFio
            );
        }

        OnAdd();
        this.Close();
    }

    public event Action AddEvent;

    private void OnAdd() {
        AddEvent?.Invoke();
    }
    public StoryDeal? Result { get; private set; }

    private void BackBtn_OnClick(object? sender, RoutedEventArgs e) 
    {
        this.Close();
    }

    public void InitComboBoxObject() {
        string sql = """
                     select ObjectId, N.Nazvanie from sql8665106.Object join sql8665106.Nazvanie N on Object.Nazvanie = N.NazvanieID
                     """;
        var objects = new List<Object.Object>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows) {
            var current = new Object.Object() {
                ObjectId = reader.GetInt32(column: "ObjectID"),
                Nazvanie = reader.GetString(column: "Nazvanie"),
            };
            objects.Add(current);
        }

        _connection.Close();
        ObjCMB.ItemsSource = objects;
    }

    private void InitComboBoxSobstv()
    {
        string sql = " select SobsvennikID, FIO " +
                     "from Sobstvennik";
        var sobstvs = new List<Sobstvennik.Sobstvennik>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            sobstvs.Add(new Sobstvennik.Sobstvennik(
                reader.GetInt32(column: "SobsvennikID"),
                reader.GetString(column: "FIO"),
                reader.GetString("Phone")
        ));
    }
    _connection.Close();
    SobCMB.ItemsSource = sobstvs;
    }

    private void InitComboBoxPokyp() {
        string sql = " select PokupatelID, PokFIO " +
                     "from Pokupatel";
        var poks = new List<Pokupatel.Pokupatel>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows) {
            var current = new Pokupatel.Pokupatel() {
                PokupatelID = reader.GetInt32(column: "PokupatelID"),
                PokFIO = reader.GetString(column: "PokFIO"),
            };
            poks.Add(current);
        }

        _connection.Close();
        PokCMB.ItemsSource = poks;
    }

    private void InitComboBoxDovLic() {
        string sql = " select LicoID, DovFIO " +
                     "from DoverennoeLico";
        var lics = new List<DoverennoeLico.DoverennoeLico>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows) {
            var current = new DoverennoeLico.DoverennoeLico() {
                LicoID = reader.GetInt32(column: "LicoID"),
                DovFio = reader.GetString(column: "DovFIO"),
            };
            lics.Add(current);
        }

        _connection.Close();
        DowCMB.ItemsSource = lics;
    }
}