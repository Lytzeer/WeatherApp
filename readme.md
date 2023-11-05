# Weather App en c# à l'aide d'Avalonia

<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/6/6e/JetBrains_Rider_Icon.svg/1200px-JetBrains_Rider_Icon.svg.png" width=200>
<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Logo_C_sharp.svg/1200px-Logo_C_sharp.svg.png" width=200>
<img src="https://avatars.githubusercontent.com/u/14075148?s=280&v=4" width=200>



## Getting Started

Here we created an application that retrieves data from openweathermap api then we displayed them on an c# avalonia application.

### Features availables

<ul>
    <li>Getting datas</li>
    <li>Display datas</li>
    <li>Search bar for search any cities of the world</li>
    <li>Set default city</li>
    <li>Set default language</li>
    <li>Icons with weather</li>
    <li>Weather forecast for the next 5 days</li>
</ul>

**You can search any cities around the Earth to know the weather.**

**You can also define a language and a city by fedaut so that as soon as the application is launched you know the weather forecasts in your language**

### Prerequisites

*You must have JetBrains Rider, dotnet 7.0 and Avalonia installed on your pc.*

**If you don't have them they are available right here [Dontnet](https://dotnet.microsoft.com/en-us/download), [Rider](https://www.jetbrains.com/fr-fr/rider/), [Avalonia](https://avaloniaui.net/)**

### Installation

_To use the project you should clone the repo._


Clone the repo
   ```sh
   git clone https://ytrack.learn.ynov.com/git/plukas/WeatherApp.git
   ```

Configuring your API key

```sh
    cd WeatherApp
    cd WeatherApp
```

You must then create an app.config.json file in this folder and give it the following content after you have created an api key on this [site](https://openweathermap.org/api):



```json
{
    "apiKey": "Your_Api_Key_Here"
}
```

Then you can start the application and find out whether the weather is rainy or not in your area.