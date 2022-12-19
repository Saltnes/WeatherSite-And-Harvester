﻿using System;
using System.Collections.Generic;
using System.Data;
using BusinessObjects;
using ClassLib;
using System.Linq;

namespace BusinessLayer
{
    public class BLayer
    {
        public List<Weather> CurrentTemperature()
        {
            var dbl = new DBLayer();
            return dbl.GetCurrentTemp();
        }

        public List<Weather> GetDailyWeather()
        {
            var dbl = new DBLayer();
            return dbl.GetDailyWeatherData(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
        }

        public List<Weather> GetTempReadingsByMonthAndDay(int year, int month, int day)
        {
            DBLayer dbl = new DBLayer();
            return dbl.GetDailyWeatherData(day, month, year);
        }

        public double GetAverageAirTemperatureDay(int day, int month, int year)
        {
            DBLayer dbl = new DBLayer();
            List<Weather> weathers = dbl.GetDailyWeatherData(day, month, year);
            double averageAirTemperature = weathers.Average(w => w.AirTemperature);

            return averageAirTemperature;
        }
        public double GetMaxAirTemperatureDay(int day, int month, int year)
        {
            DBLayer dbl = new DBLayer();
            List<Weather> weathers = dbl.GetDailyWeatherData(day, month, year);
            double averageAirTemperature = weathers.Max(w => w.AirTemperature);

            return averageAirTemperature;
        }
        public double GetMinAirTemperatureDay(int day, int month, int year)
        {
            DBLayer dbl = new DBLayer();
            List<Weather> weathers = dbl.GetDailyWeatherData(day, month, year);
            double averageAirTemperature = weathers.Min(w => w.AirTemperature);

            return averageAirTemperature;
        }
        
        

        // public List<Weather> GetWeeklyReport()
        // {
        //     var dbl = new DBLayer();
        //     List<Weather> list = new List<Weather>();
        //     return GetWeeklyReport();
        // }
    }
}