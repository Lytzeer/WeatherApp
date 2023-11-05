namespace WeatherApp;
using System.IO;
using Newtonsoft.Json;

public class Api
{
    public string apiKey { get; set; }
    
    public string getApiKey()
    {
        string content = File.ReadAllText("../../../app.config.json");
        Api config = JsonConvert.DeserializeObject<Api>(content);
        return config.apiKey;
    }
}