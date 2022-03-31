using DSUgrupp2.Data.Dto;
using Newtonsoft.Json;
using System.Net;

namespace DSUgrupp2.Infrastructure
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient client = new HttpClient();
        /// <summary>
        /// Heart of the API-client. This is what runs when you make a get/post/put etc to an API.
        /// </summary>
        /// <typeparam name="WeatherDto"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<WeatherDto> GetForecastAsync<WeatherDto>(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            try
            {
                using var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<WeatherDto>(responseJson);

                    return data;
                }
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new Exception("To many requests");
                }
                throw new Exception("Bad request");

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<WeatherDto> GetHistoryForecastAsync<WeatherDto>(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            try
            {
                using var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<WeatherDto>(responseJson);

                    return data;
                }
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new Exception("To many requests");
                }
                throw new Exception("Bad request");

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<WeatherDto> GetHistorForecastAsync<WeatherDto>(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            try
            {
                using var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<WeatherDto>(responseJson);

                    return data;
                }
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new Exception("Fick ingen kontakt med sidan.");
                }
                throw new Exception("Felaktikgt API-anrop");

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ShootingSessionDto> GetShootingAsync<ShootingSessionDto>(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            try
            {
                using var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ShootingSessionDto>(responseJson);

                    return data;
                }
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new Exception("Fick ingen kontakt med sidan.");
                }
                throw new Exception("Felaktikgt API-anrop");

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<T> GetAthletesAsync<T>(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            try
            {
                using var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<T>(responseJson);

                    return data;
                }
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new Exception("Fick ingen kontakt med sidan.");
                }
                throw new Exception("Felaktikgt API-anrop");

            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<T> GetAthletesHistoryAsync<T>(string endpoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            try
            {
                using var response = await client.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<T>(responseJson);

                    return data;
                }
                if (response.StatusCode == HttpStatusCode.TooManyRequests)
                {
                    throw new Exception("Fick ingen kontakt med sidan.");
                }
                throw new Exception("Felaktikgt API-anrop");

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
