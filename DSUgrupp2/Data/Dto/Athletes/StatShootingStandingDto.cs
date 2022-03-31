using System.ComponentModel.DataAnnotations.Schema;

namespace DSUgrupp2.Data.Dto.Athletes
{
    public class StatShootingStandingDto
    {
        public int Id { get; set; }

        [ForeignKey("AthleteDatas")]
        public int? AtheleteId { get; set; }
        public string StatShootingStanding { get; set; }
    }
}
