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
            ConfigurationSettings config = JsonConvert.DeserializeObject<ConfigurationSettings>(content);
            string apiKey = config.apiKey;



            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang=fr";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        WeatherActual Wa = JsonConvert.DeserializeObject<WeatherActual>(responseBody);
                        return Wa;
                    }

                    return new WeatherActual();
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
            ConfigurationSettings config = JsonConvert.DeserializeObject<ConfigurationSettings>(content);
            string apiKey = config.apiKey;

            string apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&lang=fr&units=metric";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        WeatherDaily wD = JsonConvert.DeserializeObject<WeatherDaily>(responseBody);
                        Console.WriteLine(responseBody);
                        return wD;
                    }
                    return new WeatherDaily();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new WeatherDaily();
                }
            }
        }
    }

    public class ConfigurationSettings
    {
        public string apiKey { get; set; }
    }
}

