namespace SpaceProgramme.Models
{
    public class WeatherData
    {
        public WeatherData(){}
        public WeatherData(int day)
        {
            Day = day;
        }

        public int Day { get; set; }
        public int Temperature { get; set; }
        public int WindSpeed { get; set; }
        public int Humidity { get; set; }
        public int Precipitation { get; set; }
        public bool Lightning { get; set; }
        public string CloudType { get; set; }
    }
}
