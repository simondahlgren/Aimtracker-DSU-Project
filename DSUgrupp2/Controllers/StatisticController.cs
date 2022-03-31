using DSUgrupp2.Data;
using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Repositories;
using DSUgrupp2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;

namespace DSUgrupp2.Controllers
{
    public class StatisticController : Controller
    {

        private readonly IDbRepository _dbRepository;
        private readonly IApiRepository _apiRepository;
        private readonly AppDbContext _appDbContext;

        public StatisticController(IDbRepository dbRepository, IApiRepository apiRepository, AppDbContext appDbContext)
        {
            _dbRepository = dbRepository;
            _apiRepository = apiRepository;
            _appDbContext = appDbContext;
        }
        /// <summary>
        /// Sends the user to a view with detailed info about a single session.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> ShootingSession(string id)
        {
            var session = _appDbContext.ShootingSessions.Where(x=>x.Id == id).Include(x=>x.Results).ThenInclude(x=>x.Shots).ThenInclude(x=>x.FiringCoords).FirstOrDefault();
            ShootingSessionViewModel model = new ShootingSessionViewModel(session);            
            return View(model);
        }
    }
}
