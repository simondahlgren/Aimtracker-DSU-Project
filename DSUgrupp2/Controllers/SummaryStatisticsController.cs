using DSUgrupp2.Data;
using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.Repositories;
using DSUgrupp2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSUgrupp2.Controllers
{
    public class SummaryStatisticsController : Controller
    {
        private readonly IDbRepository _dbRepository;
        private readonly IApiRepository _apiRepository;
        private readonly AppDbContext _appDbContext;
        public decimal miss = 0;
        public decimal hit = 0;
        public decimal results { get; set; }

        public SummaryStatisticsController(IDbRepository dbRepository, IApiRepository apiRepository,AppDbContext appDbContext)
        {
            _apiRepository = apiRepository;
            _appDbContext = appDbContext;
            _dbRepository = dbRepository;

        }
        #region landingpage in controller
        /// <summary>
        /// Returns the user to the summary page with athletes in a list for selecting a specific athlete.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Summary()
        {
               var atletes = _appDbContext.AthleteDatas.ToList();                
               var model = new SummaryStatisticsViewModel(atletes);

            return View(model);
        }
        #endregion
        #region Suboptimal method for getting statistics and calculating avarages
        /// <summary>
        /// This works but is not optimal.
        /// </summary>
        /// <param name="IbuId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="maxTemp"></param>
        /// <param name="minTemp"></param>
        /// <param name="maxWind"></param>
        /// <param name="minWind"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetSummary(string IbuId, DateTime startDate, DateTime endDate, int maxTemp, int minTemp, int maxWind, int minWind)
        {
            var atletes = _appDbContext.AthleteDatas.ToList();
            string ibuid;
            AthleteDto currentAthlete;

            if (IbuId == null) // No crash when not chosing a athelete
            {
                var errorModel = new SummaryStatisticsViewModel(atletes);
                
                return View("Summary", errorModel);
            }
            Random random = new Random();
            List<ShootingSessionDto> finalList = new List<ShootingSessionDto>();

            var trainingHistory = await _apiRepository.GetAthletesHistoryAsync(IbuId, startDate, endDate);



            foreach (var session in trainingHistory)
            {
                if (!_appDbContext.ShootingSessions.Where(x => x.Id == session.Id).Any())
                {
                    //Sets randomly generated properties for the session (Weather props).
                    foreach (var item in trainingHistory)
                    {
                        item.WindForce = random.Next(1, 20);
                        int randomNumber = random.Next(0, 4);
                        item.Temperature = random.Next(-20, 1);
                        item.Condition = item.ExistingContitions[randomNumber].ToString();
                        var shootingSession = _appDbContext.ShootingSessions.Where(x => x.Id == item.Id).FirstOrDefault();
                        if (shootingSession == null)
                        {
                            _appDbContext.Add(item);
                        }
                    }
                    finalList = (List<ShootingSessionDto>)trainingHistory;
                    _appDbContext.SaveChanges();
                }

            }
            if (maxTemp == 0 && minTemp == 0 && maxWind == 0 & minWind == 0)
            {
                var MonthHistory = _appDbContext.ShootingSessions.Where(x => x.IbuId == IbuId && x.Date >= startDate && x.Date <= endDate).Include(x => x.Results).ThenInclude(s => s.Shots).ToList();
                finalList = MonthHistory;

            }
            else if (maxTemp != 0 && minTemp != 0 && maxWind == 0 & minWind == 0)
            {

                var MonthHistory = _appDbContext.ShootingSessions.Where(x => x.IbuId == IbuId && x.Date >= startDate && x.Date <= endDate && x.Temperature >= minTemp && x.Temperature <= maxTemp).Include(x => x.Results).ThenInclude(s => s.Shots).ToList();
                finalList = MonthHistory;
            }
            else if (maxWind != 0 && minWind != 0 && maxTemp == 0 && minTemp == 0)
            {
                var MonthHistory = _appDbContext.ShootingSessions.Where(x => x.IbuId == IbuId && x.Date >= startDate && x.Date <= endDate && x.WindForce <= maxWind && x.WindForce >= minWind).Include(x => x.Results).ThenInclude(s => s.Shots).ToList();
                finalList = MonthHistory;
            }
            else
            {
                var MonthHistory = _appDbContext.ShootingSessions.Where(x => x.IbuId == IbuId && x.Date >= startDate && x.Date <= endDate && x.Temperature >= minTemp && x.Temperature <= maxTemp && x.WindForce <= maxWind && x.WindForce >= minWind).Include(x => x.Results).ThenInclude(s => s.Shots).ToList();
                finalList = MonthHistory;
            }

            

            if (finalList.Count() != 0)
            {
                 ibuid = finalList[0].IbuId;
                currentAthlete = atletes.Where(x => x.IbuId == ibuid).FirstOrDefault();
                var model = new SummaryStatisticsViewModel(finalList, atletes, currentAthlete);
                return View("Summary", model);

            }
            else
            {
                var returnViewModel = new SummaryStatisticsViewModel(atletes);
                return View("Summary", returnViewModel);
            }
            

        }
        #endregion



    }


}
