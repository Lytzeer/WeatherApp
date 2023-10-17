using System.Collections.Generic;
using System.Security.Authentication;

namespace WeatherApp
{
    public class WeatherDaily
    {
        public City City{get;set;}
        public ListsDaily[] ListsDailys{get;set;}
    }

    public class City
    {
        public string Name{get;set;}
        public string Country{get;set;}
        public Coord Coord{get;set;}
        public int Population{get;set;}
        public int Timezone{get;set;}
        public int Sunrise{get;set;}
        public int Sunset{get;set;}
    }

    public class ListsDaily
    {
        public int Dt{get;set;}
        public MainWeather Main{get;set;}
        public List<WeatherDailyInfos> Weather{get;set;}
        public string Dt_txt{get;set;}
    }

    public class WeatherDailyInfos
    {
        public int Id{get;set;}
        public MainWeather Main{get;set;}
        public string Description{get;set;}
        public string Icon{get;set;}
    }
}