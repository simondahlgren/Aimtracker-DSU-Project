using DSUgrupp2.Data.Dto.Athletes;

namespace DSUgrupp2.Infrastructure
{
    public interface IApiClient
    {
        /// <summary>
        /// Methods that can run when you implement this interface is other classes.
        /// </summary>
        /// <typeparam name="WeatherDto"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        Task<WeatherDto> GetHistoryForecastAsync<WeatherDto>(string endpoint);
        Task<WeatherDto> GetHistorForecastAsync<WeatherDto>(string endpoint);
        Task<WeatherDto> GetForecastAsync<WeatherDto>(string endpoint);
        Task<ShootingSessionDto> GetShootingAsync<ShootingSessionDto>(string endpoint);
        Task<T> GetAthletesAsync<T>(string v);

        Task<ShootingSessionDto> GetAthletesHistoryAsync<ShootingSessionDto>(string endpoint);

    }
}