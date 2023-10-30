using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;

namespace WeatherApp;

public partial class MainWindow : Window
{
    public string City = "";
    public WeatherDaily wD = new WeatherDaily();
    public WeatherActual wA = new WeatherActual();
    public WeatherApp w = new WeatherApp();
    public MainWindow()
    {
        InitializeComponent();
    }

    async private void ValiderRecherche(object? sender, RoutedEventArgs e)
    {
        Button b = sender as Button;
        City= SearchBar.Text.ToString();
        if (City == "")
        {
            Error.Content = "Entrez le nom d'une ville avant de valider";
        }
        else
        {
            wA = await w.CityWeatherActual(City);
            wD = await w.CityWeatherDaily(City);
            Console.WriteLine(wD.ListsDailys[0].DtTxt);
            DisplayCurrentWeather();
            DisplayDailyWeather();
        }
    }

    private void DisplayCurrentWeather()
    {
        SearchBar.Text = "";
        Error.Content = "";
        Ville.Content = City[0].ToString().ToUpper()+City.Substring(1).ToString()  +", "+wA.SysWeather.Country;
        Coord.Content = wA.Coord.Lat+"; "+wA.Coord.Lon;
        Image.Source = new Bitmap($"../../../img/{wA.Weathers[0].Icon}.png");
        Temp.Content = "Température : "+wA.MainWeather.Temp;
        Desc.Content = "Description : "+wA.Weathers[0].Description;
        Hum.Content = "Humidité : "+wA.MainWeather.Humidity+"%";
    }

    private void DisplayDailyWeather()
    {
        SearchBar2.Text = "";
        Error2.Content = "";
        Ville2.Content = City[0].ToString().ToUpper()+City.Substring(1).ToString()  +", "+wD.CityDaily.Country;
        Coord2.Content = "Coordonées";
        //On commence j à 2 car les labels sont nommés à partir de 2.
        int j = 2;
        for (int i = 0; i < wD.ListsDailys.Length; i++)
        {
            Console.WriteLine(wD.ListsDailys[i].Weather[0].Icon);
            DateTime date = DateTime.ParseExact(wD.ListsDailys[i].DtTxt, "yyyy-MM-dd HH:mm:ss", null);
            if (date.Hour == 12)
            {
                Image = this.FindControl<Image>("Image" + j);
                Temp=this.FindControl<Label>("Temp" + j);
                Desc = this.FindControl<Label>("Desc" + j);
                Hum = this.FindControl<Label>("Hum" + j);
                Temp.Content = wD.ListsDailys[i].Main.Temp.ToString();
                Image.Source = new Bitmap($"../../../img/{wD.ListsDailys[i].Weather[0].Icon}.png");
                Desc.Content = wD.ListsDailys[i].Weather[0].Description;
                Hum.Content = wD.ListsDailys[i].Main.Humidity+"%";
                j++;
            }
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
    }
}