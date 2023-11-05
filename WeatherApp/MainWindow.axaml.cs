using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WeatherApp;

public partial class MainWindow : Window
{
    public string City = "";
    public WeatherDaily Wd = new WeatherDaily();
    public WeatherActual Wa = new WeatherActual();
    public WeatherApp W = new WeatherApp();
    public MainWindow()
    {
        CreateOptionsFile();
        InitializeComponent();
        if (IsConnectedToInternet())
        {
            DisplayDefaultWeather();
            InitializeSettings();
        }
        else
        {
            this.Error.Content = "Pas de connexion internet veuillez vous connecter à internet et relancer l'application";
            this.Error2.Content = "Pas de connexion internet veuillez vous connecter à internet et relancer l'application";
        }
    }

    public void InitializeSettings()
    {
        List<string> countriesCodeList = new List<string>
        {
            "af", "al", "ar", "az", "bg", "ca", "cz", "da", "de", "el", "en", "eu", "fa", "fi", "fr",
            "gl", "he", "hi", "hr", "hu", "id", "it", "ja", "kr", "la", "lt", "mk", "no", "nl", "pl",
            "pt", "pt_br", "ro", "ru", "sv", "sk", "sl", "sp", "sr", "th", "tr", "ua", "vi", "zh_cn", "zh_tw"
        };
        SelectBarCity.Text = new ApplicationSettings().GetSettings().defaultCity;
        defaultLangBox.SelectedIndex = countriesCodeList.FindIndex(la=> la.Contains(new ApplicationSettings().GetSettings().lang));
    }

    public bool IsConnectedToInternet()
    {
        Ping p = new Ping();
        PingReply reply = p.Send("8.8.8.8");
        return reply.Status == IPStatus.Success;
    }

    public void CreateOptionsFile()
    {
        JObject defaultsSettings = new JObject(
            new JProperty("defaultCity", ""),
            new JProperty("lang","fr")
        );
        if (!File.Exists("../../../options.json"))
        {
            string formattedJson = JsonConvert.SerializeObject(defaultsSettings, Formatting.Indented);
            File.WriteAllText("../../../options.json", formattedJson);
        }
    }

    async public void DisplayDefaultWeather()
    {
        City = new ApplicationSettings().GetSettings().defaultCity;

        if (City != "")
        {
            Wa = await W.CityWeatherActual(City);
            Wd = await W.CityWeatherDaily(City);
            DisplayCurrentWeather();
            DisplayDailyWeather();
        }
    }

    async private void ValiderRecherche(object? sender, RoutedEventArgs e)
    {
        Button b = sender as Button;
        City= SearchBar.Text.ToString();
        if (City == "")
        {
            Error.Content = "Entrez le nom d'une ville avant de valider";
        }
        else if(IsConnectedToInternet())
        {
            Wa = await W.CityWeatherActual(City);
            Wd = await W.CityWeatherDaily(City);
            DisplayCurrentWeather();
            DisplayDailyWeather();
        }
        else
        {
            Error.Content = "Pas de connexion internet veuillez vous connecter à internet et relancer l'application";
            Error2.Content = "Pas de connexion internet veuillez vous connecter à internet et relancer l'application";
            ClearAll();
        }
    }

    private void DisplayCurrentWeather()
    {
        if (Wa.Error == "")
        {
            SearchBar.Text = "";
            Error.Content = "";
            Ville.Content = City[0].ToString().ToUpper()+City.Substring(1).ToString()  +", "+Wa.SysWeather.Country;
            Coord.Content = Wa.Coord.Lat+"; "+Wa.Coord.Lon;
            Image.Source = new Bitmap($"../../../img/{Wa.Weathers[0].Icon}.png");
            Temp.Content = "Température : "+Math.Round(Wa.MainWeather.Temp,1)+"°";
            Desc.Content = "Description : "+Wa.Weathers[0].Description;
            Hum.Content = "Humidité : "+Wa.MainWeather.Humidity+"%";
        }
        else
        {
            Error.Content = Wa.Error;
        }
    }

    private void DisplayDailyWeather()
    {
        if (Wd.Error == "")
        {
            Error2.Content = "";
            Ville2.Content = City[0].ToString().ToUpper()+City.Substring(1).ToString()  +", "+Wd.CityDaily.Country;
            Coord2.Content = "Coordonées: "+Wd.CityDaily.Coord.Lat+"; "+Wd.CityDaily.Coord.Lon;
            //On commence j à 2 car les labels sont nommés à partir de 2.
            int j = 2;
            for (int i = 0; i < Wd.ListsDailys.Length; i++)
            {
                DateTime date = DateTime.ParseExact(Wd.ListsDailys[i].DtTxt, "yyyy-MM-dd HH:mm:ss", null);
                if (date.Hour == 12)
                {
                    var timeLabel = this.FindControl<Label>("Time" + j);
                    var dateLabel = this.FindControl<Label>("Date" + j);
                    var image = this.FindControl<Image>("Image" + j);
                    var temp=this.FindControl<Label>("Temp" + j);
                    var desc = this.FindControl<Label>("Desc" + j);
                    var hum = this.FindControl<Label>("Hum" + j);
                    temp.Content = Math.Round(Wd.ListsDailys[i].Main.Temp,1)+"°";
                    image.Source = new Bitmap($"../../../img/{Wd.ListsDailys[i].Weather[0].Icon}.png");
                    desc.Content = Wd.ListsDailys[i].Weather[0].Description;
                    hum.Content = Wd.ListsDailys[i].Main.Humidity+"%";
                    dateLabel.Content = date.ToString("D");
                    timeLabel.Content = date.ToString("HH:mm");
                    j++;
                }
            }
        }
        else
        {
            Error2.Content = Wd.Error;
            ClearAll();
        }
    }
    


    async private void DefaultCity_OnClick(object? sender, RoutedEventArgs e)
    {
        string defaultCityValue = SelectBarCity.Text.ToString();
        Wa = await W.CityWeatherActual(defaultCityValue);
        if (Wa.Error == "" && IsConnectedToInternet())
        {
            string lang = new ApplicationSettings().GetSettings().lang;
            JObject changedSettings = new JObject(
                new JProperty("defaultCity", defaultCityValue),
                new JProperty("lang",lang)
            );
            string formattedJson = JsonConvert.SerializeObject(changedSettings, Formatting.Indented);
            File.WriteAllText("../../../options.json", formattedJson);
            ErrorDefaultCity.Content = $"{defaultCityValue} sera désormais la ville affichée par défaut dès le prochain lancement de l'application";
        }
        else if (!IsConnectedToInternet())
        {
            ErrorDefaultCity.Content = "Pas de connexion internet veuillez vous connecter à internet et relancer l'application";
        }
        else
        {
            ErrorDefaultCity.Content = Wa.Error;
        }
        
    }

    private void DefaultLang_OnClick(object? sender, RoutedEventArgs e)
    {
        List<string> countriesCodeList = new List<string>
        {
            "af", "al", "ar", "az", "bg", "ca", "cz", "da", "de", "el", "en", "eu", "fa", "fi", "fr",
            "gl", "he", "hi", "hr", "hu", "id", "it", "ja", "kr", "la", "lt", "mk", "no", "nl", "pl",
            "pt", "pt_br", "ro", "ru", "sv", "sk", "sl", "sp", "sr", "th", "tr", "ua", "vi", "zh_cn", "zh_tw"
        };
        string lang = countriesCodeList[defaultLangBox.SelectedIndex];
        string defaultCity = new ApplicationSettings().GetSettings().defaultCity;
        JObject changedSettings = new JObject(
            new JProperty("defaultCity", defaultCity),
            new JProperty("lang",lang)
        );
        string formattedJson = JsonConvert.SerializeObject(changedSettings, Formatting.Indented);
        File.WriteAllText("../../../options.json", formattedJson);
        ErrorLang.Content = $"{lang} sera désormais la langue par défaut dès le prochain lancement de l'application";
    }

    public void ClearAll()
    {
        //Méthode utilisée pour afficher les erreurs
        //Clear les entrées de la page Recherche
        Ville.Content = "";
        Coord.Content = "";
        Image.Source = null;
        Temp.Content = "";
        Desc.Content = "";
        Hum.Content = "";
        
        //Clear les entrées de la page Prévisions
        Ville2.Content = "";
        Coord2.Content = "";
        for (int j = 2; j<=6;j++)
        {
                var timeLabel = this.FindControl<Label>("Time" + j);
                var dateLabel = this.FindControl<Label>("Date" + j);
                var image = this.FindControl<Image>("Image" + j);
                var temp=this.FindControl<Label>("Temp" + j);
                var desc = this.FindControl<Label>("Desc" + j);
                var hum = this.FindControl<Label>("Hum" + j);
                temp.Content = "";
                image.Source = null;
                desc.Content = "";
                hum.Content = "";
                dateLabel.Content = "";
                timeLabel.Content = "";
        }
    }
}