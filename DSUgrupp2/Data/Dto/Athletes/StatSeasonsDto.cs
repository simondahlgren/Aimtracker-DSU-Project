using System.ComponentModel.DataAnnotations.Schema;

namespace DSUgrupp2.Data.Dto.Athletes
{
    public class StatSeasonsDto
    {
        public int id { get; set; }

        [ForeignKey("AthleteDatas")]
        public int? AtheleteId { get; set; }
        public string? StatSeasons { get; set; }
    }
}
