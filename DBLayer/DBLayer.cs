using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BusinessObjects;

namespace DBLayer 
{
    public class DBLayer
    {
        public void InsertWeatherValues(int year, int month, int day, int hour, DateTime DateAndTime,
            double AirTemperature, double Humidity, double WindSpeed, double WindGust,
            int WindDirection, string Rain)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnAir"].ConnectionString;
            SqlParameter param;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd =
                    new SqlCommand(
                        "insert into dbo.KjerreWeather values(@year,@month,@day,@hour,@DateAndTime,@AirTemperature,@Humidity,@WindSpeed,@WindGust,@WindDirection,@Rain)",
                        conn);
                cmd.CommandType = CommandType.Text;

                param = new SqlParameter("@year", SqlDbType.Int);
                param.Value = year;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@month", SqlDbType.Int);
                param.Value = month;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@day", SqlDbType.Int);
                param.Value = day; //(object)wpd.Temp ?? DBNull.Value;//wpd.Temp;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@AirTemperature", SqlDbType.Float);
                param.Value = AirTemperature; //(object)wpd.Temp ?? DBNull.Value;//wpd.Temp;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Humidity", SqlDbType.Float);
                param.Value = Humidity; //(object)wpd.Temp ?? DBNull.Value;//wpd.Temp;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@WindSpeed", SqlDbType.Float);
                param.Value = WindSpeed; //(object)wpd.Temp ?? DBNull.Value;//wpd.Temp;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@WindGust", SqlDbType.Float);
                param.Value = WindGust; //(object)wpd.Temp ?? DBNull.Value;//wpd.Temp;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@WindDirection", SqlDbType.Int);
                param.Value = WindDirection; //(object)wpd.Temp ?? DBNull.Value;//wpd.Temp;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@Rain", SqlDbType.VarChar);
                param.Value = Rain; //(object)wpd.Temp ?? DBNull.Value;//wpd.Temp;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@hour", SqlDbType.Int);
                param.Value = hour; //(object)wpd.Temp ?? DBNull.Value;//wpd.Temp;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@DateAndTime", SqlDbType.DateTime);
                param.Value = DateAndTime; //(object)wpd.Temp ?? DBNull.Value;//wpd.Temp;
                cmd.Parameters.Add(param);

                int rows = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public DataTable GetByDate(string name)
        {
            SqlParameter param;
            var connectionString = ConfigurationManager.ConnectionStrings["ConnAir"].ConnectionString;
            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("=@name", conn);
                cmd.CommandType = CommandType.Text;

                //params here
                param = new SqlParameter("@name", SqlDbType.NVarChar);
                param.Value = name;
                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();

                dt.Load(reader);

                reader.Close();
                conn.Close();
            }

            return dt;
        }

        public List<Weather> GetCurrentTemp()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnAir"].ConnectionString;
            DataTable dt = new DataTable();
            List<Weather> weathers = new List<Weather>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd =
                    new SqlCommand("SELECT TOP 1 AirTemperature, rain FROM dbo.KjerreWeather ORDER BY Id DESC", conn);
                cmd.CommandType = CommandType.Text;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Weather w = new Weather();
                    w.AirTemperature = (double)reader["AirTemperature"];
                    w.Rain = (string)reader["rain"];
                    weathers.Add(w);
                }

                dt.Load(reader);

                reader.Close();
                conn.Close();
            }

            return weathers;
        }

        public List<Weather> GetDailyWeatherData(int day, int month, int year)
        {
            SqlParameter param;
            var connectionString = ConfigurationManager.ConnectionStrings["ConnAir"].ConnectionString;
            DataTable dt = new DataTable();
            List<Weather> weathers = new List<Weather>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd =
                    new SqlCommand("SELECT * FROM KjerreWeather WHERE year =@Year AND month =@Month AND day =@Day", conn);
                cmd.CommandType = CommandType.Text;

                param = new SqlParameter("@day", SqlDbType.Int);
                param.Value = day;
                cmd.Parameters.Add(param);

                param = new SqlParameter("@month", SqlDbType.Int);
                param.Value = month;
                cmd.Parameters.Add(param);
                
                param = new SqlParameter("@year", SqlDbType.Int);
                param.Value = year;
                cmd.Parameters.Add(param);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Weather w = new Weather();
                    w.Year = (int)reader["year"];
                    w.Month = (int)reader["month"];
                    w.Day = (int)reader["day"];
                    w.Hour = (int)reader["hour"];
                    w.AirTemperature = (double)reader["AirTemperature"];
                    w.Humidity = (double)reader["Humidity"];
                    w.WindSpeed = (double)reader["WindSpeed"];
                    w.WindGust = (double)reader["WindGust"];
                    w.WindDirection = (int)reader["WindDirection"];
                    w.Rain = (string)reader["rain"];

                    weathers.Add(w);
                }

                dt.Load(reader);

                reader.Close();
                conn.Close();
            }

            return weathers;
        }
    }
}