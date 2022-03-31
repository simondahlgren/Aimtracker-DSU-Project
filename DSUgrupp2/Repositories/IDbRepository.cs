using DSUgrupp2.Data.Dto.Athletes;
using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.Models;
using DSUgrupp2.ViewModels;

namespace DSUgrupp2.Repositories
{
    public interface IDbRepository
    {
        /// <summary>
        /// Methods you can use when implementing this interface in other classes.
        /// </summary>
        /// <returns></returns>
        AthleteDto GetRandomAthlete();
        List<ShootingSessionDto> GetSessions(string? ibuid);
        ShootingSessionDto GetSingleSession(string id);
        ShootingSessionDto RegisterSession(ShootingSessionDto session);
        Task<ShootingSessionDto> SetConditionsFromApiAsync(ShootingSessionDto session);
        ShootingSessionDto SetWeatherConditions(ShootingSessionDto session);
    }
}