using System.ComponentModel.DataAnnotations.Schema;

namespace DSUgrupp2.Data.Dto.Shot
{
    public class ResultShootingsDto
    {
        /// <summary>
        /// Model for response from API
        /// </summary>
        public int? Id { get; set; }
        public int SeriesId { get; set; }
        public string? ShootingSessionId { get; set; }
        public List<ResultseriesDto>? Series { get; set; }
    }
}
