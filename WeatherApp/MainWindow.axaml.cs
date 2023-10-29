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

    async private void ValiderRecherche(object? sender, RoutedEventArgs e)
    {
        Button b = sender as Button;
        string search = SearchBar.Text.ToString();
        if (search == "")
        {
            Error.Content = "Entrez le nom d'une ville avant de valider";
        }
        else
        {
            WeatherActual wa = new WeatherActual();
            WeatherApp w = new WeatherApp();
            //Console.WriteLine(await w.CityWeatherActual(search));
            wa = await w.CityWeatherActual(search);
            SearchBar.Text = "";
            Error.Content = "";
            Ville.Content = search[0].ToString().ToUpper()+search.Substring(1).ToString()  +", "+wa.SysWeather.Country;
            Coord.Content = wa.Coord.Lat+"; "+wa.Coord.Lon;
            //Dl les images en local et test comme ça
            // Image.Source =  "https://openweathermap.org/img/wn/10"+wa.Weathers[0].Icon.ToString()+".png";
            Temp.Content = "Température : "+wa.MainWeather.Temp;
            Desc.Content = "Description : "+wa.Weathers[0].Description;
            Hum.Content = "Humidité : "+wa.MainWeather.Humidity+"%";
        }
    }
}