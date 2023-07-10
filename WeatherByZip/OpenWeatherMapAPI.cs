using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherByZip
{
    public class OpenWeatherMapAPI
    {


        public static void GetWeatherData()
        {
            var apiKeyObj = File.ReadAllText("appsettings.json");
            var apiKey = JObject.Parse(apiKeyObj).GetValue("apiKey").ToString();

            Console.Write("Enter Your Zip Code:");
            var zip = Console.ReadLine();

            string weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zip}&appid={apiKey}&units=imperial";
            var client = new HttpClient();
            var weatherResponse = client.GetStringAsync(weatherURL).Result;
            var weatherData = JObject.Parse(weatherResponse);

            var temp = (double)weatherData["main"]["temp"];

            var sunriseUnixTime = (long)weatherData["sys"]["sunrise"];
            DateTimeOffset sunriseDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(sunriseUnixTime);
            DateTime sunriseTime = sunriseDateTimeOffset.LocalDateTime;
            string sunrise = sunriseTime.ToString("hh:mm tt");

            var sunsetUnixTime = (long)weatherData["sys"]["sunset"];
            DateTimeOffset sunsetDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(sunsetUnixTime);
            DateTime sunsetTime = sunsetDateTimeOffset.LocalDateTime;
            string sunset = sunsetTime.ToString("hh:mm tt");

            Console.WriteLine($"Current Temperature: {temp}");
            Console.WriteLine($"Sunrise: {sunrise} ");
            Console.WriteLine($"Sunset: {sunset} ");


        }
    }
}
