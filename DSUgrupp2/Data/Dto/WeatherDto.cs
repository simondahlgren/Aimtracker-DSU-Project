namespace DSUgrupp2.Data.Dto
{
    public class WeatherDto
    {
        /// <summary>
        /// Model for response from API
        /// </summary>
        public LocationDto Location { get; set; }
        public HistoryWeatherDto Forecast { get; set; }

    }
}
