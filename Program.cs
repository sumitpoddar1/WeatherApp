using System;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    // API_KEY
    private const string ApiKey = "your_openweathermap_api_key";

    static async Task Main(string[] args)
    {
        Console.WriteLine("Welcome to the Weather App!");

        // Get City Name
        Console.Write("Enter the city name: ");
        string cityName = Console.ReadLine();

        // Fetch Weather data from Api
        string weatherData = await GetWeatherDataAsync(cityName);

        // Parse and display the weather information
        if (weatherData != null)
        {
            ParseAndDisplayWeather(weatherData);
        }
        else
        {
            Console.WriteLine("Could not retrieve weather information. Please try again.");
        }
    }

// Function to fetch weather data using API
private static async Task<string> GetWeatherDataAsync(string cityName)
{
    string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={ApiKey}&units=metric";

    using (HttpClient client = new HttpClient())
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occured: " + ex.Message);
        }
    }
    return null;
}

// Function to parse amd display weather data
private static void ParseAndDisplayWeather(string jsonData)
{
    try
    {
        // Parse the JSON data
        dynamic weather = JsonConvert.DeserializeObject(jsonData);

        // Extract and display relevant information
        Console.WriteLine("\n--- Weather Information ---");
        Console.WriteLine($"City: {weather.name}");
        Console.WriteLine($"Temperature: {weather.main.temp}°C");
        Console.WriteLine($"Weather: {weather.weather[0].description}");
        Console.WriteLine($"----------------------");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error parsing weather data: " + ex.Message);
    }
}
}
