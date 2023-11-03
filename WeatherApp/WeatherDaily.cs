using System.Collections.Generic;
using System.Security.Authentication;
using Newtonsoft.Json;

namespace WeatherApp
{
    public class WeatherDaily
    {
        public string Error { get; set; }
        [JsonProperty("city")] public CityDaily CityDaily{get;set;}
        [JsonProperty("list")] public ListsDaily[] ListsDailys{get;set;}
    }

    public class CityDaily
    {
        [JsonProperty("name")] public string Name{get;set;}
        [JsonProperty("country")] public string Country{get;set;}
        [JsonProperty("coord")] public Coord Coord{get;set;}
        [JsonProperty("population")] public int Population{get;set;}
        [JsonProperty("timezone")] public int Timezone{get;set;}
        [JsonProperty("sunrise")] public int Sunrise{get;set;}
        [JsonProperty("sunset")] public int Sunset{get;set;}
    }

    public class ListsDaily
    {
        [JsonProperty("dt")] public int Dt{get;set;}
        [JsonProperty("main")] public MainWeatherDaily Main{get;set;}
        [JsonProperty("weather")] public List<WeatherDailyInfos> Weather{get;set;}
        [JsonProperty("dt_txt")] public string DtTxt{get;set;}
    }

    public class MainWeatherDaily
    {
        [JsonProperty("temp")] public float Temp { set; get; }
        [JsonProperty("feels_like")] public float Feels_like { set; get; }
        [JsonProperty("temp_min")] public float Temp_min { set; get; }
        [JsonProperty("temp_max")] public float Temp_max { set; get; }
        [JsonProperty("humidity")] public int Humidity { set; get; }
    }

    public class WeatherDailyInfos
    {
        [JsonProperty("id")]public int Id{get;set;}
        [JsonProperty("main")]public string Main{get;set;}
        [JsonProperty("description")]public string Description{get;set;}
        [JsonProperty("icon")]public string Icon{get;set;}
    }
}