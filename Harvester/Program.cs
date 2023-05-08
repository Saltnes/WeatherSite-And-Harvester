using System;
using System.IO;
using System.Net;
using System.Threading;
using DBLayer;
using Newtonsoft.Json.Linq;

namespace Harvester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int retryCount = 0;
            bool exitProgram = false;

            while (!exitProgram && retryCount < 2)
            {
                try
                {
                    GetWeather();
                    // If data is successfully retrieved, exit the loop
                    exitProgram = true;
                }
                catch (NoDataException)
                {
                    // If no data is available, wait for the specified delay before retrying
                    if (retryCount == 0)
                    {
                        Thread.Sleep(TimeSpan.FromMinutes(3));
                    }
                    else if (retryCount == 1)
                    {
                        Thread.Sleep(TimeSpan.FromMinutes(5));
                    }

                    retryCount++;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions here
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    // Wait for 5 minutes before retrying
                    Thread.Sleep(TimeSpan.FromMinutes(5));
                }
            }
        }

        private static void GetWeather()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(
                "https://api.met.no/weatherapi/nowcast/2.0/complete.json?altitude=37&lat=59.2796&lon=10.7849");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.UserAgent = "ithilien";

            using (var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                if (httpResponse.StatusCode == HttpStatusCode.NoContent)
                {
                    // If no data is available, throw a custom exception
                    throw new NoDataException("No data available in the API.");
                }

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject jObj = JObject.Parse(result);

                    JToken details = jObj.SelectToken("properties.timeseries[0].data.instant.details");
                    JToken summary = jObj.SelectToken("properties.timeseries[0].data.next_1_hours.summary");
                    string Rain = summary.Value<string>("symbol_code");
                    double AirTemperature = details.Value<double>("air_temperature");
                    double Humidity = details.Value<double>("relative_humidity");
                    double WindSpeed = details.Value<double>("wind_speed");
                    double WindGust = details.Value<double>("wind_speed_of_gust");
                    int WindDirection = details.Value<int>("wind_from_direction");

                    DbLayer dbl = new DbLayer();
                    dbl.InsertWeatherValues(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour,
                        DateTime.Now,
                        AirTemperature, Humidity, WindSpeed, WindGust, WindDirection, Rain);
                }
            }
        }
    }

    // Custom exception class for indicating no data available
    public class NoDataException : Exception
    {
        public NoDataException(string message) : base(message)
        {
        }
    }
}