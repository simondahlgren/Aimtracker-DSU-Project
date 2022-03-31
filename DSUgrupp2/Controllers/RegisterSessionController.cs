using DSUgrupp2.Data;
using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.Repositories;
using DSUgrupp2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSUgrupp2.Controllers
{
    
    public class RegisterSessionController : Controller
    {

        
        private readonly ILogger<RegisterSessionController> _logger;
        private readonly IDbRepository _dbRepository;
        private readonly IApiRepository _apiRepository;
        private readonly AppDbContext _appDbContext;
        #region Constructor
        public RegisterSessionController(ILogger<RegisterSessionController> logger, IDbRepository dbRepository, IApiRepository apiRepository, AppDbContext appDbContext)
        {
            _logger = logger;
            _dbRepository = dbRepository;
            _apiRepository = apiRepository;
            _appDbContext = appDbContext;
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }
        #region Save Session from API
        /// <summary>
        /// Method for saving the API-shooting session.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(SessionViewModel model)
        {
            ShootingSessionDto session = GlobalRepository.Session; // Gets the session to a static property, not the best sulution...
            var athlete = _appDbContext.AthleteDatas.Where(x => x.IbuId == session.IbuId).FirstOrDefault(); //Gets athlets from DB.
            SessionViewModel sessionViewModel = new SessionViewModel { DateAndTime = session.Date, IbuId = session.IbuId, Location = session.Location, Athlete = athlete, ShootingSession = session }; // Creates a new viewmodel for updating the fields on the page.
            
            _appDbContext.ShootingSessions.Add(session); // Adds the session to the DB.
            _appDbContext.SaveChanges(); // Save the changes in DB.
            GlobalRepository.Session = null; // Reset static property.
            return View("RegistrationComplete", sessionViewModel); // Return view with new viewmodel.

            
        }
        #endregion
        #region Get Single session from API
        /// <summary>
        /// Gets a session from the API and sets random weather conditions on that session. We are not wealthy enough to buy historical weather data. If we were we would use that API-call to set accurate data.
        /// </summary>
        /// <param name="regmod"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSession(SessionViewModel regmod) 
        {           
            Random random = new Random();
            var session = await _apiRepository.GetShootingAsync(); //Gets session from simulated data
            session.Id = Guid.NewGuid().ToString(); //Sets a new guid for the session so we can save it to DB, uniquie ID.
            var athlete = _dbRepository.GetRandomAthlete(); //Gets random athlete.
            session.IbuId = athlete.IbuId;//Overrides the ibuid on api-session to a existing ibuid
            session = _dbRepository.SetWeatherConditions(session); // Sets random weather conditions for the session
            session = await _dbRepository.SetConditionsFromApiAsync(session); // Sets weatherconditions from API.
            SessionViewModel model = new SessionViewModel //Updates the viewmodels fields with the current data when returning to the view.
            {
                DateAndTime = session.Date,
                Location = session.Location,
                IbuId = athlete.IbuId
            };
            GlobalRepository.Session = session;
            
            return View("Index", model);

        }
        #endregion


    }
}
