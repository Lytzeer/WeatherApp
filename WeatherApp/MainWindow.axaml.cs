using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace WeatherApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ValiderRecherche(object? sender, RoutedEventArgs e)
    {
        Button b = sender as Button;
        string search = SearchBar.Text.ToString();
        if (search == "")
        {
            Error.Content = "Entrez le nom d'une ville avant de valider";
        }
        else
        {
            SearchBar.Text = "";
            Error.Content = "";
            Ville.Content = search;
            Coord.Content = "Coordonées";
            Image.Content = "Image";
            Temp.Content = "Température";
            Desc.Content = "Description";
            Hum.Content = "Humidité";
        }
    }
}