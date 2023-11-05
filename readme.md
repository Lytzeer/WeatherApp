# Weather App en c# à l'aide d'Avalonia

<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/6/6e/JetBrains_Rider_Icon.svg/1200px-JetBrains_Rider_Icon.svg.png" width=200>
<img src="https://upload.wikimedia.org/wikipedia/commons/thumb/b/bd/Logo_C_sharp.svg/1200px-Logo_C_sharp.svg.png" width=200>
<img src="https://avatars.githubusercontent.com/u/14075148?s=280&v=4" width=200>



## Getting Started

Here we created an application that retrieves data from openweathermap api then we displayed them on an c# avalonia application.

### Features availables

<ul>
    <li>Getting data</li>
    <li>Displaying data</li>
    <li>Search bar to search any city in the world</li>
    <li>Set default city</li>
    <li>Set default language</li>
    <li>Weather icons</li>
    <li>Weather forecast for the next 5 days</li>
</ul>

**You can search any city around the world to know the weather.**

**You can also define a language and a city by default so that as soon as the application is launched you know the weather forecast in your language**

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

Installing Weather Icons

You must then download the img folder at this link [site](https://auvencecom-my.sharepoint.com/:f:/g/personal/lucas_hanson_ynov_com/EoZhySjkuJtPiQbglW6Bl9gBNxl0Hptl0ITHAOz7Pj13Eg?e=3OPt4y) and then place the folder in the same location as the app.config.json file

Then you may start the application and find out whether the weather is rainy or not in your area.