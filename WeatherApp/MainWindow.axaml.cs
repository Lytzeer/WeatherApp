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
    
    private void ValiderRecherche2(object? sender, RoutedEventArgs e)
    {
        Button b = sender as Button;
        string search = SearchBar2.Text.ToString();
        if (search == "")
        {
            Error2.Content = "Entrez le nom d'une ville avant de valider";
        }
        else
        {
            SearchBar2.Text = "";
            Error2.Content = "";
            Ville2.Content = search;
            Coord2.Content = "Coordonées";
            Image2.Content = "Image";
            Temp2.Content = "Température";
            Desc2.Content = "Description";
            Hum2.Content = "Humidité";
            Image3.Content = "Image";
            Temp3.Content = "Température";
            Desc3.Content = "Description";
            Hum3.Content = "Humidité";
            Image4.Content = "Image";
            Temp4.Content = "Température";
            Desc4.Content = "Description";
            Hum4.Content = "Humidité";
            Image5.Content = "Image";
            Temp5.Content = "Température";
            Desc5.Content = "Description";
            Hum5.Content = "Humidité";
            Image6.Content = "Image";
            Temp6.Content = "Température";
            Desc6.Content = "Description";
            Hum6.Content = "Humidité";
        }
    }
}