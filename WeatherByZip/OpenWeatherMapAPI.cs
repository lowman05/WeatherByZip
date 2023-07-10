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
            try
            {
                var apiKeyObj = File.ReadAllText("appsettings.json");
                var apiKey = JObject.Parse(apiKeyObj).GetValue("apiKey").ToString();

                Console.Write("Enter Your Zip Code:");
                var zip = Console.ReadLine();

                string weatherURL = $"https://api.openweathermap.org/data/2.5/weather?zip={zip}&appid={apiKey}&units=imperial";
                var client = new HttpClient();
                var weatherResponse = client.GetStringAsync(weatherURL).Result;
                var weatherData = JObject.Parse(weatherResponse);

                var description = weatherData["weather"][0]["description"];
                var icon = weatherData["weather"][0]["icon"];

                var temp = (double)weatherData["main"]["temp"];
                var roundedTemp = Math.Round(temp);

                var feelsLike = (double)weatherData["main"]["feels_like"];
                var roundedFeelsLike = Math.Round(feelsLike);

                var high = weatherData["main"]["temp_max"];
                var roundedHigh = Math.Round((double)high);

                var low = weatherData["main"]["temp_min"];
                var roundedLow = Math.Round((double)low);

                var sunriseUnixTime = (long)weatherData["sys"]["sunrise"];
                DateTimeOffset sunriseDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(sunriseUnixTime);
                DateTime sunriseTime = sunriseDateTimeOffset.LocalDateTime;
                string sunrise = sunriseTime.ToString("hh:mm tt");

                var sunsetUnixTime = (long)weatherData["sys"]["sunset"];
                DateTimeOffset sunsetDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(sunsetUnixTime);
                DateTime sunsetTime = sunsetDateTimeOffset.LocalDateTime;
                string sunset = sunsetTime.ToString("hh:mm tt");


                Console.WriteLine($"{description}");
                Console.WriteLine($"Current Temperature: {roundedTemp}");
                Console.WriteLine($"Feels Like: {roundedFeelsLike}");
                Console.WriteLine($"Today's High: {roundedHigh}");
                Console.WriteLine($"Today's Low: {roundedLow}");
                Console.WriteLine($"Sunrise: {sunrise} ");
                Console.WriteLine($"Sunset: {sunset} ");
            }
            catch ( Exception ex ) 
            {
                Console.WriteLine("The zipcode you entered is invalid");
            }


        }
    }
}
