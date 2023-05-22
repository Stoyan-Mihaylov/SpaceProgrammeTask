using SpaceProgramme.Models;
using System.Collections.Generic;

namespace SpaceProgramme.Services.Weather
{
    public interface IWeatherService
    {
        ///<summary>
        /// This method creates new csv file and populate weather data and best day for launch on it.
        ///</summary>
        void CreateNewCsvFileWithWeatherData(Dictionary<int, WeatherData> data, WeatherData? bestDay);

        ///<summary>
        /// This method determines best day for launch.
        ///</summary>
        WeatherData? FindBestLaunchDay(Dictionary<int, WeatherData> data);

        ///<summary>
        /// This method reads csv file and populate weather data.
        ///</summary>
        Dictionary<int, WeatherData> ReadWeatherDataFromFile(string fileName);
    }
}