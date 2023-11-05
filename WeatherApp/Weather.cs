using System.Net.Http;
using System.Threading.Tasks;
using System;
using Newtonsoft.Json;

namespace WeatherApp
{
    public class WeatherApp
    {

        public async Task<WeatherActual> CityWeatherActual(string city)
        {
            string apiKey = new Api().getApiKey();
            string lang = new ApplicationSettings().GetSettings().lang;

            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric&lang={lang}";

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
            string apiKey = new Api().getApiKey();
            string lang = new ApplicationSettings().GetSettings().lang;

            string apiUrl = $"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&lang={lang}&units=metric";
            
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
                    return new WeatherDaily();
                }
            }
        }
    }
}

