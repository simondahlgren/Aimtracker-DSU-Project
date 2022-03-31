using DSUgrupp2.Data.Dto;
using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.Infrastructure;
using System.Globalization;

namespace DSUgrupp2.Repositories
{
    public class ApiRepository : IApiRepository
    {
        private readonly IApiClient _apiClient;
        private readonly IConfiguration _configuration;
        
        public ApiRepository(IApiClient apiClient, IConfiguration configuration)
        {
            _apiClient = apiClient;
            _configuration = configuration;
        }

        public async Task<ShootingSessionDto> GetShootingAsync() => await _apiClient.GetShootingAsync<ShootingSessionDto>($"{_configuration["ApiConnectionStrings:EndpointShootingG7"]}");
     //   public async Task<AthletesDto> GetAthletesAsync() => await _apiClient.GetAthletesAsync<AthletesDto>($"{EndpointShooting2}/athletes");
        public async Task<IEnumerable<ShootingSessionDto>> GetAthletesHistoryAsync(string ibuld, DateTime startDate, DateTime endDate) => await _apiClient.GetAthletesHistoryAsync<IEnumerable<ShootingSessionDto>>($"{_configuration["ApiConnectionStrings:EndpointShootingG8"]}/history/date/{ibuld}/?startDate={startDate.ToString("yyMMdd")}&endDate={endDate.ToString("yyMMdd")}");
        public async Task<WeatherDto> GetForecastAsync(DateTime date,  string location) => await _apiClient.GetForecastAsync<WeatherDto>($"{_configuration["ApiConnectionStrings:EndpointWeather"]}{date.ToShortDateString()}&hour={date.Hour}&q={location}");
        public async Task<WeatherDto> GetHistoryForecastAsync(DateTime date, decimal lat, decimal lng) => await _apiClient.GetHistoryForecastAsync<WeatherDto>($"{_configuration["ApiConnectionStrings:EndpointWeather"]}{date.Date}&hour={date.Hour}&q={lat.ToString(CultureInfo.InvariantCulture)},{lng.ToString(CultureInfo.InvariantCulture)}");
        public async Task<WeatherDto> GetHistorForecastAsync(DateTime date, string lat, string lng) => await _apiClient.GetHistorForecastAsync<WeatherDto>($"{_configuration["ApiConnectionStrings:EndpointWeather"]}{date.Date}&hour={date.Hour}&q={lat},{lng}");

        public async Task<IEnumerable<AthleteDto>> GetAthletesAsync() => await _apiClient.GetAthletesAsync<IEnumerable<AthleteDto>>($"{_configuration["ApiConnectionStrings:EndpointShootingG8"]}/athletes");

        
    }
}
