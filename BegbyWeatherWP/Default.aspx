<%@ Page Title="Weather" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BegbyWeatherWP._Default" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI.DataVisualization.Charting" Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <div class="table table-responsive">
            <h2>Manstad</h2>
            <asp:GridView AutoGenerateColumns="False" Class="table table-bordered table-condensed table-hover table-striped" ID="GridView1" AllowPaging="True" runat="server">
                <Columns>
                    <asp:BoundField DataField="AirTemperature" HeaderText="Current temperature°C" SortExpression="Temperature"/>
                    <asp:BoundField DataField="rain" HeaderText="Weather condition"
                                    SortExpression="Weather"/>
                </Columns>
            </asp:GridView>
            <div style="richness: initial">
                <asp:Label ID="avgDailyAirTemperatureLabel" runat="server"/>
                <br>
                <asp:Label ID="DailyMaxTemperatureLabel" runat="server"/>
                <br>
                <asp:Label ID="DailyMinTemperatureLabel" runat="server"/>
            </div>

            <%--<asp:Label ID="avgDailyAirTemperatureLabel" Text="Average daily air temperature: <%# Eval("AirTemperature", "{0:N2}°C") %>" runat="server"></asp:Label>--%>
        </div>
        <div class="table table-responsive">
            <h3>Weather Information</h3>
            <asp:GridView AutoGenerateColumns="False" Class="table table-bordered table-condensed table-hover table-striped" ID="GridViewgetDailyW" AllowPaging="False" runat="server">
                <Columns>
                    <asp:BoundField DataField="year" HeaderText="Year" SortExpression="Year"/>
                    <asp:BoundField DataField="month" HeaderText="Month"
                                    SortExpression="Month"/>
                    <asp:BoundField DataField="day" HeaderText="Day"
                                    SortExpression="Day"/>
                    <asp:BoundField DataField="hour" HeaderText="Hour"
                                    SortExpression="Time"/>
                    <asp:BoundField DataField="AirTemperature" HeaderText="Temperature °C"
                                    SortExpression="Temperature"/>
                    <asp:BoundField DataField="Humidity" HeaderText="Humidity"
                                    SortExpression="Humidity"/>
                    <asp:BoundField DataField="WindSpeed" HeaderText="Wind speed"
                                    SortExpression="Wind speed"/>
                    <asp:BoundField DataField="WindGust" HeaderText="Wind gust"
                                    SortExpression="Wind gust"/>
                    <asp:BoundField DataField="WindDirection" HeaderText="Wind direction"
                                    SortExpression="Wind Direction"/>
                    <asp:BoundField DataField="rain" HeaderText="Weather condition"
                                    SortExpression="Weather"/>

                </Columns>
            </asp:GridView>

            <div>


                <asp:Chart ID="Chart1" runat="server" Width="1050px">
                    <Series>
                        <asp:Series Name="Series0"></asp:Series>
                    </Series>
                    <%--<Series>
                                        <asp:Series Name="Series2"></asp:Series>
                                    </Series>--%>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
        </div>
    </div>
</asp:Content>