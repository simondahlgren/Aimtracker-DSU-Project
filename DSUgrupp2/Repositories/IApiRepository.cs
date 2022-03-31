using DSUgrupp2.Data.Dto;
using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Data.Dto.Shot;

namespace DSUgrupp2.Repositories
{
    public interface IApiRepository
    {
        /// <summary>
        /// Methods that you can use when implementing this interface in other classes.
        /// </summary>
        /// <param name="date"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        Task<WeatherDto> GetForecastAsync(DateTime date, string location);
        Task<WeatherDto> GetHistoryForecastAsync(DateTime date, decimal lat, decimal lng);
        Task<WeatherDto> GetHistorForecastAsync(DateTime date, string lat, string lng);
        Task<ShootingSessionDto> GetShootingAsync();

        Task<IEnumerable<AthleteDto>> GetAthletesAsync();
        Task<IEnumerable<ShootingSessionDto>> GetAthletesHistoryAsync(string ibuld, DateTime startDate, DateTime endDate);
    }
}