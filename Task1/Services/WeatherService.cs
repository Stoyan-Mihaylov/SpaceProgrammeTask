using SpaceProgramme.Models;
using SpaceProgramme.Services.Weather;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpaceProgramme.Services
{
    public class WeatherService : IWeatherService
    {
        public Dictionary<int, WeatherData> ReadWeatherDataFromFile(string fileName)
        {
            var daysMap = new Dictionary<int, WeatherData>();
            using var reader = new StreamReader(fileName);

            var header = reader.ReadLine().Split(',');

            for (int i = 1; i < header.Length; i++)
            {
                daysMap[int.Parse(header[i])] = new WeatherData { Day = int.Parse(header[i]) };
            }

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                var split = line.Split(',');
                var parameter = split[0];

                for (int i = 1; i < split.Length; i++)
                {
                    switch (parameter)
                    {
                        case "Temperature (C)":
                            daysMap[i].Temperature = int.Parse(split[i]);
                            break;
                        case "Wind (m/s)":
                            daysMap[i].WindSpeed = int.Parse(split[i]);
                            break;
                        case "Humidity (%)":
                            daysMap[i].Humidity = int.Parse(split[i]);
                            break;
                        case "Precipitation (%)":
                            daysMap[i].Precipitation = int.Parse(split[i]);
                            break;
                        case "Lightning":
                            daysMap[i].Lightning = split[i] == "Yes";
                            break;
                        case "Clouds":
                            daysMap[i].CloudType = split[i];
                            break;
                    }
                }
            }


            return daysMap;
        }

        public WeatherData FindBestLaunchDay(Dictionary<int, WeatherData> data)
        {
            return data.Values.Where(d =>
                            d.Temperature >= 2 && d.Temperature <= 31 &&
                            d.WindSpeed <= 10 &&
                            d.Humidity < 60 &&
                            d.Precipitation == 0 &&
                            d.Lightning == false &&
                            d.CloudType != "Cumulus" && d.CloudType != "Nimbus")
                            .OrderBy(d => d.WindSpeed).ThenBy(d => d.Humidity)
                            .FirstOrDefault()!;
        }

        public void CreateNewCsvFileWithWeatherData(Dictionary<int, WeatherData> data, WeatherData bestDay)
        {
            var numericProperties = typeof(WeatherData)
                            .GetProperties()
                            .Where(p => p.PropertyType == typeof(int));

            using (var writer = new StreamWriter("WeatherReport.csv"))
            {
                writer.WriteLine("Parameter,Average,Max,Min,Median,Best Day");

                foreach (var property in numericProperties)
                {
                    var values = data.Values.Select(d => (int)property.GetValue(d)).ToList();
                    var average = values.Average();
                    var max = values.Max();
                    var min = values.Min();
                    var median = values.OrderBy(v => v).ElementAt(values.Count / 2);

                    writer.WriteLine($"{property.Name},{average},{max},{min},{median},{property.GetValue(bestDay)}");
                }

                writer.WriteLine($"Lightning,,,,,{bestDay.Lightning}");
                writer.WriteLine($"CloudType,,,,,{bestDay.CloudType}");
            }
        }
    }
}
