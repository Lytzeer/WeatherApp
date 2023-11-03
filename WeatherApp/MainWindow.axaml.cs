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
    public WeatherDaily wD = new WeatherDaily();
    public WeatherActual wA = new WeatherActual();
    public WeatherApp w = new WeatherApp();
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
        ApplicationSettings config = GetSettings();
        SelectBarCity.Text = config.defaultCity;
        defaultLangBox.SelectedIndex = countriesCodeList.FindIndex(la=> la.Contains(config.lang));
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
            using  (StreamWriter w = File.AppendText(("../../../options.json")))
            using (JsonTextWriter writerJson = new JsonTextWriter(w))
            {
                defaultsSettings.WriteTo(writerJson);
            }
            
        }
    }

    async public void DisplayDefaultWeather()
    {
        ApplicationSettings config = GetSettings();
        City = config.defaultCity;

        if (config.defaultCity != "")
        {
            Console.WriteLine("ccccccc");
            wA = await w.CityWeatherActual(City);
            wD = await w.CityWeatherDaily(City);
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
            wA = await w.CityWeatherActual(City);
            wD = await w.CityWeatherDaily(City);
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
        if (wA.Error == "")
        {
            SearchBar.Text = "";
            Error.Content = "";
            Ville.Content = City[0].ToString().ToUpper()+City.Substring(1).ToString()  +", "+wA.SysWeather.Country;
            Coord.Content = wA.Coord.Lat+"; "+wA.Coord.Lon;
            Image.Source = new Bitmap($"../../../img/{wA.Weathers[0].Icon}.png");
            Temp.Content = "Température : "+Math.Round(wA.MainWeather.Temp,1)+"°";
            Desc.Content = "Description : "+wA.Weathers[0].Description;
            Hum.Content = "Humidité : "+wA.MainWeather.Humidity+"%";
        }
        else
        {
            Error.Content = wA.Error;
        }
    }

    private void DisplayDailyWeather()
    {
        if (wD.Error == "")
        {
            Error2.Content = "";
            Ville2.Content = City[0].ToString().ToUpper()+City.Substring(1).ToString()  +", "+wD.CityDaily.Country;
            Coord2.Content = "Coordonées: "+wD.CityDaily.Coord.Lat+"; "+wD.CityDaily.Coord.Lon;
            //On commence j à 2 car les labels sont nommés à partir de 2.
            int j = 2;
            for (int i = 0; i < wD.ListsDailys.Length; i++)
            {
                DateTime date = DateTime.ParseExact(wD.ListsDailys[i].DtTxt, "yyyy-MM-dd HH:mm:ss", null);
                if (date.Hour == 12)
                {
                    var timeLabel = this.FindControl<Label>("Time" + j);
                    var dateLabel = this.FindControl<Label>("Date" + j);
                    var image = this.FindControl<Image>("Image" + j);
                    var temp=this.FindControl<Label>("Temp" + j);
                    var desc = this.FindControl<Label>("Desc" + j);
                    var hum = this.FindControl<Label>("Hum" + j);
                    temp.Content = Math.Round(wD.ListsDailys[i].Main.Temp,1)+"°";
                    image.Source = new Bitmap($"../../../img/{wD.ListsDailys[i].Weather[0].Icon}.png");
                    desc.Content = wD.ListsDailys[i].Weather[0].Description;
                    hum.Content = wD.ListsDailys[i].Main.Humidity+"%";
                    dateLabel.Content = date.ToString("D");
                    timeLabel.Content = date.ToString("HH:mm");
                    j++;
                }
            }
        }
        else
        {
            Error2.Content = wD.Error;
            ClearAll();
        }
    }
    


    async private void DefaultCity_OnClick(object? sender, RoutedEventArgs e)
    {
        string defaultCityValue = SelectBarCity.Text.ToString();
        wA = await w.CityWeatherActual(defaultCityValue);
        if (wA.Error == "" && IsConnectedToInternet())
        {
            SelectBarCity.Text = "";
            ApplicationSettings actualConfig = GetSettings();
            JObject changedSettings = new JObject(
                new JProperty("defaultCity", defaultCityValue),
                new JProperty("lang",actualConfig.lang)
            );
            using  (StreamWriter w = File.CreateText("../../../options.json"))
            using (JsonTextWriter writerJson = new JsonTextWriter(w))
            {
                changedSettings.WriteTo(writerJson);
            }

            ErrorDefaultCity.Content = "";
        }
        else if (!IsConnectedToInternet())
        {
            ErrorDefaultCity.Content = "Pas de connexion internet veuillez vous connecter à internet et relancer l'application";
        }
        else
        {
            ErrorDefaultCity.Content = wA.Error;
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
        ApplicationSettings actualConfig = GetSettings();
        JObject changedSettings = new JObject(
            new JProperty("defaultCity", actualConfig.defaultCity),
            new JProperty("lang",lang)
        );
        using  (StreamWriter w = File.CreateText("../../../options.json"))
        using (JsonTextWriter writerJson = new JsonTextWriter(w))
        {
            changedSettings.WriteTo(writerJson);
        }
    }

    public void ClearAll()
    {
        //Méthode pour afficher les erreurs
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
    
    public ApplicationSettings GetSettings()
    {
        string configSettings = File.ReadAllText("../../../options.json");
        ApplicationSettings config = JsonConvert.DeserializeObject<ApplicationSettings>(configSettings);
        return config;
    }
}