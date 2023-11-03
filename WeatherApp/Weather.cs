using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Linq;

namespace WeatherApp
{
    public class WeatherApp
    {

        public async Task<WeatherActual> CityWeatherActual(string city)
        {
            string content = File.ReadAllText("../../../app.config.json");
            ApiSettings config = JsonConvert.DeserializeObject<ApiSettings>(content);
            string apiKey = config.apiKey;
            
            ApplicationSettings settings = GetSettings();

            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang={settings.lang}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        WeatherActual wA = JsonConvert.DeserializeObject<WeatherActual>(responseBody);
                        wA.Error = "";
                        return wA;
                    }
                    else
                    {
                        WeatherActual wA = new WeatherActual();
                        wA.Error = "Veuillez entrez une ville qui existe s'il vous plaît";
                        Console.WriteLine(response.StatusCode.ToString()=="NotFound");
                        Console.WriteLine(response.StatusCode);
                        return wA;
                    }

                }
                catch (Exception ex)
                {
                    return new WeatherActual();
                }
            }
        }

        public async Task<WeatherDaily> CityWeatherDaily(string city)
        {
            string content = File.ReadAllText("../../../app.config.json");
            ApiSettings config = JsonConvert.DeserializeObject<ApiSettings>(content);
            string apiKey = config.apiKey;

            ApplicationSettings settings = GetSettings();
            
            Console.WriteLine(settings.lang);

            string apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&lang={settings.lang.ToString()}&units=metric";
            
            //lang à fixer car cpt
            

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        WeatherDaily wD = JsonConvert.DeserializeObject<WeatherDaily>(responseBody);
                        wD.Error = "";
                        return wD;
                    }
                    else
                    {
                        WeatherDaily wD = new WeatherDaily();
                        wD.Error = "Veuillez entrez une ville qui existe s'il vous plaît";
                        return wD;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new WeatherDaily();
                }
            }
        }
        
        public ApplicationSettings GetSettings()
        {
            string configSettings = File.ReadAllText("../../../options.json");
            ApplicationSettings config = JsonConvert.DeserializeObject<ApplicationSettings>(configSettings);
            return config;
        }
    }

    public class ApiSettings
    {
        public string apiKey { get; set; }
    }
    
    public class ApplicationSettings
    {
        public string defaultCity { get; set; }
        public string lang { get; set; }
    }
}

