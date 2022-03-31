using DSUgrupp2.Data;
using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.Repositories;

namespace DSUgrupp2.ViewModels
{
    public class SessionViewModel 
    {
        /// <summary>
        /// ViewModel for register session page.
        /// </summary>
        public ShootingSessionDto? ShootingSession { get; set; }  
        public DateTime? DateAndTime { get; set; } = DateTime.Now;
        public string? Location { get; set; }
        public string? IbuId { get; set; }
        public AthleteDto? Athlete { get; set; }
       
        public SessionViewModel()
        {

        }
        
        
    }
}
