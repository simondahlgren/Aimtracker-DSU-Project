using DSUgrupp2.Data;
using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.Models;
using DSUgrupp2.Repositories;
using DSUgrupp2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace DSUgrupp2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDbRepository _dbRepository;
        private readonly IApiRepository _apiRepository;
        private readonly AppDbContext _appDbContext;

        //private readonly MockRepository _mockRepository;
        #region Constructor
        /// <summary>
        /// Injection to the controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="dbRepository"></param>
        /// <param name="apiRepository"></param>
        /// <param name="appDbContext"></param>
        public HomeController(ILogger<HomeController> logger, IDbRepository dbRepository, IApiRepository apiRepository, AppDbContext appDbContext)
        {
            _logger = logger;
            _dbRepository = dbRepository;
            _apiRepository = apiRepository;
            _appDbContext = appDbContext;
           
        }
        #endregion
        #region Home Index
        /// <summary>
        /// Checks if there are athletes in the database, if not then seed from API. Home page when loged in
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            if (_appDbContext.AthleteDatas.Count() == 0)
            {
                var athletes = await _apiRepository.GetAthletesAsync(); // Hämtas direkt från api
                _appDbContext.AddRange(athletes);
                _appDbContext.SaveChanges();

            }

            return View();
        }
        #endregion
        #region Error method.
        /// <summary>
        /// Returns this view if there is an error.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}