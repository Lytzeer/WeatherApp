using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;

namespace WeatherApp
{
    public class WeatherApp
    {

        public async Task<string> CityWeatherActual(string city)
        {
            string content = File.ReadAllText("app.config.json");
            ConfigurationSettings config = JsonConvert.DeserializeObject<ConfigurationSettings>(content);
            string apiKey = config.apiKey;

            Console.WriteLine(apiKey);



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
                        Console.WriteLine(responseBody);
                        Console.WriteLine(Wa.Name);
                        return Wa.Coord.Lat;
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }

        public async Task<string> CityWeatherDaily(string city)
        {
            string content = File.ReadAllText("app.config.json");
            ConfigurationSettings config = JsonConvert.DeserializeObject<ConfigurationSettings>(content);
            string apiKey = config.apiKey;

            string apiUrl = $"api.openweathermap.org/data/2.5/forecast?lat=44.9167311&lon=-0.4271005&appid={apiKey}&lang=fr&units=metric";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        WeatherDaily Wa = JsonConvert.DeserializeObject<WeatherDaily>(responseBody);
                        Console.WriteLine(responseBody);
                        Console.WriteLine(Wa.City.Name);
                        return Wa.City.Name;
                    }
                    return "";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }

    public class ConfigurationSettings
    {
        public string apiKey { get; set; }
    }
}

