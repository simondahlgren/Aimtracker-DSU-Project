using DSUgrupp2.Data.Dto.Shot;
using DSUgrupp2.ViewModels;
using Newtonsoft.Json;

namespace DSUgrupp2.Repositories
{
    public static class GlobalRepository
    {
        /// <summary>
        /// Static shooting session used for transporting data between controllers (havent fixed this, should send this through constructor instead).
        /// </summary>
        public static ShootingSessionDto Session { get; set; }
        /// <summary>
        /// Method for getting JSON from anywhere in the application.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static ShootingSessionDto GetFileData(string input) { return JsonConvert.DeserializeObject<ShootingSessionDto>(File.ReadAllText(input)); }


    }
}
