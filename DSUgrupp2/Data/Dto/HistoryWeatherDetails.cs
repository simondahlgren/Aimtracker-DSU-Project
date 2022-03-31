namespace DSUgrupp2.Data.Dto
{
    public class HistoryWeatherDetails
    {
        /// <summary>
        /// Model for response from API
        /// </summary>
        public string Date { get; set; }
        public List<HourDto> Hour { get; set; }
        public DayDto Day { get; set; }
    }
}
