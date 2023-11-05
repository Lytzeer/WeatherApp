namespace WeatherApp;
using System.IO;
using Newtonsoft.Json;

public class ApplicationSettings
{
    public string defaultCity { get; set; }
    public string lang { get; set; }
    
    
    public ApplicationSettings GetSettings()
    {
        string configSettings = File.ReadAllText("../../../options.json");
        ApplicationSettings config = JsonConvert.DeserializeObject<ApplicationSettings>(configSettings);
        return config;
    }
}