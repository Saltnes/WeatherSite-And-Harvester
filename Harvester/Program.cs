using System;
using System.IO;
using System.Net;
using DBLayer;
using Newtonsoft.Json.Linq;

namespace Harvester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GetWeather();
        }

        private static void GetWeather()
        {
            //http://jsonviewer.stack.hu/
            //59.202752, 10.953535
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(
                    "https://api.met.no/weatherapi/nowcast/2.0/complete.json?altitude=37&lat=59.2796&lon=10.7849");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.UserAgent = "ithilien";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

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
            catch (Exception ex)
            {
            }
        }

        public int GetStuff()
        {
            //http://jsonviewer.stack.hu/
            //https://peterdaugaardrasmussen.com/2022/01/18/how-to-get-a-property-from-json-using-selecttoken/
            //create the httpwebrequest
            var httpWebRequest =
                (HttpWebRequest)WebRequest.Create(
                    "https://api.met.no/weatherapi/nowcast/2.0/complete.json?altitude=37&lat=59.2796&lon=10.7849");

            //the usual stuff. run the request, parse your json
            try
            {
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.UserAgent = "";
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    JObject jObj = JObject.Parse(result);
                    //JToken data = jObj.SelectToken("path");
                    //int valuepm1 = data.Value<int>("keyname");//key name - getting key.value
                    //int valuepm25 = data.Value<int>("pm25");
                    //int radonValue = data.Value<int>("radonShortTermAvg");
                    // inn i db
                }

                return 0;
            }
            catch
            {
                Exception ex;
            }

            return 0;
        }
    }
}