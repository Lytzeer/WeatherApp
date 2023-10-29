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