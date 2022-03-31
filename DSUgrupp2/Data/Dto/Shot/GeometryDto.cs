using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace DSUgrupp2.Data.Dto.Shot
{
    /// <summary>
    /// Model for response from API
    /// </summary>
    [Keyless] // Couldnt get List<Double> to save to DB so we just dont track it. only used for API-calls.
    public class GeometryDto
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        
        public List<decimal> Coordinates { get; set; }
    }
}
