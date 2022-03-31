namespace DSUgrupp2.Data.Dto.Shot
{
    public class ResultseriesDto
    {
        /// <summary>
        /// Model for response from API
        /// </summary>
        public int Id { get; set; }
        
        public int? Seriesid { get; set; }
        public string? stance { get; set; }
        public DateTime dateTime { get; set; }
        public List<shotsDto>? Shots { get; set; }


    }
}
