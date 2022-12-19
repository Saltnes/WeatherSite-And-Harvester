using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using BusinessObjects;


namespace BegbyWeatherWP
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Displays current temperature & weather
                BLayer bl = new BLayer();
                var currentTemperature = bl.CurrentTemperature();
                //linq for å finne max etc. feks labels
                GridView1.DataSource = currentTemperature;
                GridView1.DataBind();

                var avgDailyAirTemperature =
                    bl.GetAverageAirTemperatureDay(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                avgDailyAirTemperatureLabel.Text =
                    "Average air temperature today: " + avgDailyAirTemperature.ToString("N2") + "°C";

                var dailyMaxTemperature =
                    bl.GetMaxAirTemperatureDay(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                DailyMaxTemperatureLabel.Text =
                    "Highest air temperature today: " + dailyMaxTemperature.ToString("N1") + "°C";

                var dailyMinTemperature =
                    bl.GetMinAirTemperatureDay(DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                DailyMinTemperatureLabel.Text =
                    "Lowest air temperature today: " + dailyMinTemperature.ToString("N1") + "°C";

                var getDailyReport = bl.GetDailyWeather(); // Paging on click - current = AddDays(-1)
                GridViewgetDailyW.DataSource = getDailyReport;
                GridViewgetDailyW.DataBind();
                BindChart();
            }
        }


        public void BindChart() //paging?
        {
            BLayer bl = new BLayer();
            List<Weather> tempsForToday =
                bl.GetTempReadingsByMonthAndDay(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var tempsToday = tempsForToday.Where(y =>
                y.Year == DateTime.Now.Year && y.Month == DateTime.Now.Month && y.Day == DateTime.Now.Day);
            var maxToday = tempsToday.Max(m => m.AirTemperature);


            Chart1.Series[0].XValueMember = "Hour";
            Chart1.Series[0].XValueType = ChartValueType.Int32; //optional
            Chart1.Series[0].YValueMembers = "AirTemperature";
            Chart1.Series[0].ChartType = SeriesChartType.FastLine;
            

            // //to serier=2 grafer/bars etc
            // Chart1.Series[1].XValueMember = "Hour";
            // Chart1.Series[1].XValueType = ChartValueType.Int32; //optional
            // Chart1.Series[1].YValueMembers = "Humid";
            // Chart1.Series[1].ChartType = SeriesChartType.Line;

            //Chart1.DataBindTable(temps,"Hour");//using just DataBind() below
            Chart1.ChartAreas[0].AxisX.Minimum = 0;
            Chart1.ChartAreas[0].AxisX.Maximum = 23;
            Chart1.ChartAreas[0].AxisX.Interval = 1;
            Chart1.ChartAreas[0].AxisX.IsMarginVisible = false;

            Chart1.DataSource = tempsForToday;
            Chart1.DataBind();

            for (var i = 0; i < tempsForToday.Count; i++)
                Chart1.Series[0].Points[i].ToolTip = tempsForToday[i].AirTemperature.ToString();
        }
    }
}