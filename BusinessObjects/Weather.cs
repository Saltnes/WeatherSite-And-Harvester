using System;

namespace BusinessObjects
{
    public class Weather
    {
        public int Id { get; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public DateTime DateAndTime { get; set; }
        public double AirTemperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double WindGust { get; set; }
        public int WindDirection { get; set; }
        public string Rain { get; set; }
    }
}