using DSUgrupp2.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace DSUgrupp2.Data.Dto.Shot
{
    public class ShootingSessionDto
    {
        /// <summary>
        /// Model for response from API
        /// </summary>
        public string? Id { get; set; }
        public string? Shooter { get; set; }
        public DateTime Date { get; set; }
        public string? Location { get; set; }
        public string? IbuId { get; set; }
        public List<ResultseriesDto>? Results { get; set; }
        [NotMapped]
        public GeometryDto? Geometry { get; set; }
        #region Random Weather props for a session
        /// <summary>
        /// Test for simulating weather data
        /// </summary>
        public int? WindForce { get; set; }
        public int? Temperature { get; set; }
        public string? Condition { get; set; }
        [NotMapped]
        public List<string> ExistingContitions { get; set; } = new List<string> { "Clear", "Fog", "Rain", "Snow" };

        #endregion
        

        

    }
}
