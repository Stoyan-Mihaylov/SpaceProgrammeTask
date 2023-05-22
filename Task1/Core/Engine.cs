using SpaceProgramme.Core.Contracts;
using SpaceProgramme.Exceptions;
using SpaceProgramme.Helpers;
using SpaceProgramme.Models;
using SpaceProgramme.Services;
using SpaceProgramme.Services.Weather;
using System;
using System.Collections.Generic;
using System.IO;

namespace SpaceProgramme.Core
{
    public class Engine : IEngine
    {
        private readonly IWeatherService _weatherService;

        public Engine()
        {
            _weatherService = new WeatherService();
        }

        public void Run(string[] args)
        {
            if (args.Length != 4)
            {
                throw new ArgumentException("Invalid parameters!");
            }

            string fileName = args[0];
            string senderEmail = args[1];
            string password = args[2];
            string receiverEmail = args[3];

            if (!File.Exists(fileName))
            {
                throw new FileIsNotFoundException();
            }

            Dictionary<int, WeatherData> data = _weatherService.ReadWeatherDataFromFile(fileName);

            WeatherData bestDay = _weatherService.FindBestLaunchDay(data);

            if (bestDay == null)
            {
                throw new BestDayForLaunchIsNullException();
            }

            Console.WriteLine($"Best day for launch is: {bestDay.Day}");

            _weatherService.CreateNewCsvFileWithWeatherData(data, bestDay);

            EmailSenderHelper.TrySendEmail(
                senderEmail,
                password,
                receiverEmail,
                subject: "Weather report",
                body: "",
                "./WeatherReport.csv");
        }
    }
}
