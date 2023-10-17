using System.Text.Json;
using Newtonsoft.Json;
namespace WeatherApp;

public class WeatherActual
{
    [JsonProperty("coord")] public Coord Coord { get; set; }
    [JsonProperty("name")] public string Name { set; get; }
    [JsonProperty("weather")] public Weather[] Weathers { set; get; }
    [JsonProperty("main")] public MainWeather MainWeather { get; set; }
    [JsonProperty("sys")] public SysWeather SysWeather { set; get; }
}

public class Coord
{
    [JsonProperty("lat")] public string Lat { set; get; }
    [JsonProperty("lon")] public string Lon { set; get; }
}

public class Weather
{
    [JsonProperty("main")] public string Main { set; get; }
    [JsonProperty("description")] public string Description { set; get; }
    [JsonProperty("icon")] public string Icon { get; set; }
}

public class MainWeather
{
    [JsonProperty("temp")] public float Temp { set; get; }
    [JsonProperty("feels_like")] public float Feels_like { set; get; }
    [JsonProperty("temp_min")] public float Temp_min { set; get; }
    [JsonProperty("temp_max")] public float Temp_max { set; get; }
    [JsonProperty("humidity")] public int Humidity { set; get; }
}

public class SysWeather
{
    [JsonProperty("country")] public string Country { set; get; }
}