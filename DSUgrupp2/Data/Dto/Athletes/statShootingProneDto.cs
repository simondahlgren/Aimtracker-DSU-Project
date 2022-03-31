using System.ComponentModel.DataAnnotations.Schema;

namespace DSUgrupp2.Data.Dto.Athletes
{
    public class StatShootingProneDto
    {
        public int Id { get; set; }
        [ForeignKey("AthleteDatas")]
        public int? AtheleteId { get; set; }
        public string StatShootingProne { get; set; }
    }
}
