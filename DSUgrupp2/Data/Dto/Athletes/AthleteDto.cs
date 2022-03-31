using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSUgrupp2.Data.Dto.Athletes
{
    public class AthleteDto
    {

        /// <summary>
        /// Model for athlete from API
        /// </summary>
        public int? Id { get; set; }


        public string IbuId { get; set; }
        public string FullName { get; set; }
        public string FamilyName { get; set; }
        public string GivenName { get; set; }
        public string Country { get; set; }
        public string Nat { get; set; }
        public string GenderId { get; set; }
        public int MaxHeartRate { get; set; }
        public string? Image { get; set; }



        #region StatsProne

        [ForeignKey("AtheleteId")]
        public List<StatShootingProneDto> StatShootingProneDto { get { return PrivateStatShootingPrones; } }
        private List<StatShootingProneDto> PrivateStatShootingPrones;

        [NotMapped]
        public List<string>? StatShootingProne { set { SetStatsProne(value); } }
        private void SetStatsProne(List<string> s)
        {
            PrivateStatShootingPrones = new List<StatShootingProneDto>();
            foreach (var item in s)
            {
                PrivateStatShootingPrones.Add(new StatShootingProneDto() { StatShootingProne = item });

            }
        }
        #endregion

        #region StatShootingStanding

        [ForeignKey("AtheleteId")]
        public List<StatShootingStandingDto>? statShootingStandingsDto { get { return PrivateStatShootingStandings; } }

        private List<StatShootingStandingDto> PrivateStatShootingStandings;

        [NotMapped]
        public List<string>? StatShootingStanding { set { SetStatsStanding(value); } }
        private void SetStatsStanding(List<string> s)
        {
            PrivateStatShootingStandings = new List<StatShootingStandingDto>();
            foreach (var item in s)
            {
                PrivateStatShootingStandings.Add(new StatShootingStandingDto() { StatShootingStanding = item });

            }
        }
        #endregion


        #region StatSeasonR

        [ForeignKey("AtheleteId")]
        public List<StatSeasonsDto>? statSeasonsDto { get { return privateStat; } }

        private List<StatSeasonsDto> privateStat;

        [NotMapped]
        public List<string>? StatSeasons { set {  setStats(value); } }
        private void setStats(List<string> asd)
        {
            privateStat = new List<StatSeasonsDto>();
            foreach (var item in asd)
            {
                privateStat.Add(new StatSeasonsDto() { StatSeasons = item });

            }
        }
        #endregion

    }
}
