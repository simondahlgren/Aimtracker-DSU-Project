using DSUgrupp2.Data;
using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.Models;
using DSUgrupp2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DSUgrupp2.Repositories
{
    public class DbRepository : IDbRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IApiRepository _apiRepository;

        public DbRepository(AppDbContext appDbContext, IApiRepository apiRepository)
        {
            _appDbContext = appDbContext;
            _apiRepository = apiRepository;
            
        }
        
        
        /// <summary>
        /// Registers a session to the DB.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public ShootingSessionDto RegisterSession(ShootingSessionDto session)
        {
           session.Id = Guid.NewGuid().ToString();
           var ses = _appDbContext.Add(session);
            _appDbContext.SaveChanges();
            return session;
        }

        /// <summary>
        /// Gets all sessions from DB for a specific athlete.
        /// </summary>
        /// <param name="ibuid"></param>
        /// <returns></returns>
        public List<ShootingSessionDto> GetSessions(string? ibuid)
        {
            var result =  _appDbContext.ShootingSessions.Where(o => o.IbuId == ibuid).Include(z=>z.Results).ThenInclude(x=>x.Shots).ToList();
            return result;
        }
        /// <summary>
        /// Get single shooting session from DB based on PK.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ShootingSessionDto GetSingleSession(string id)
        {
            var result = _appDbContext.ShootingSessions.Where(o => o.Id == id).Include(z => z.Results).ThenInclude(x => x.Shots).FirstOrDefault();
            return result;
        }
        /// <summary>
        /// Sets Random weathercontitions on a session. (From the new API).
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public ShootingSessionDto SetWeatherConditions(ShootingSessionDto session)
        {
            Random random = new Random();
            session.WindForce = random.Next(1, 20);
            int randomNumber = random.Next(0, 4);
            session.Temperature = random.Next(-20, 1);
            session.Condition = session.ExistingContitions[randomNumber].ToString();
            return session;
        }
        /// <summary>
        /// Gets random athlete, used for setting ibuid on OLD api
        /// </summary>
        /// <returns></returns>
        public AthleteDto GetRandomAthlete()
        {
            Random random = new Random();
            var athletes = _appDbContext.AthleteDatas.ToList();
            var athleteSelector = random.Next(0, athletes.Count);
            var athlete = athletes[athleteSelector];
            return athlete;
        }
        /// <summary>
        /// Gets weatherconditions from session (OLD API).
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public async Task<ShootingSessionDto> SetConditionsFromApiAsync(ShootingSessionDto session)
        {
            var weather = await _apiRepository.GetHistoryForecastAsync(session.Date, session.Geometry.Coordinates[0], session.Geometry.Coordinates[1]); // Gets weather data from Weather API.
            session.WindForce = (int)weather.Forecast.Forecastday[0].Hour[0].Gust_kph; // Sets windforce from API.
            session.Condition = weather.Forecast.Forecastday[0].Hour[0].Condition.text; // Sets the condition from API.
            session.Temperature = (int)weather.Forecast.Forecastday[0].Hour[0].Temp_c; // Sets Temp from API
            return session;
        }

    }
}
