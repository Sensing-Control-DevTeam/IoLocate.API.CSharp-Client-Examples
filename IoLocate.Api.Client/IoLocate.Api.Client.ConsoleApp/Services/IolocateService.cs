using IoLocate.Api.Client.ConsoleApp.Models;
using IoLocate.Api.Client.ConsoleApp.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tracker.Services.BusinessToBusiness.Responses;

namespace IoLocate.Api.Client.ConsoleApp.Services
{
    public class IolocateService : IIolocateService
    {
        private readonly IoLocateApiOption _options;

        public IolocateService(IoLocateApiOption options)
        {
            _options = options;
        }

        /// <summary>
        /// Get Access Token By UserName and Password
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="password">Password</param>
        /// <returns>The response may be a successfull or failed response. If it is successfully response returns an Access Token</returns>
        public async Task<B2BLoginResponse> GetAccessTokenAsync(string userName, string password)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _options.BaseAddress;
                var payload = new
                {
                    UserName = userName,
                    Password = password
                };
                var content = new StringContent(JsonConvert.SerializeObject(payload));
                using (var httpResponse = await httpClient.PostAsync("/api/b2b/login", content))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<B2BLoginResponse>(responseString);
                    return response;
                }
            }
        }

        public async Task<GetB2BCompaniesResponse> GetCompaniesAsync(AccessTokenModel accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _options.BaseAddress;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("X-ApiToken", accessToken.AccessToken);
                using (var httpResponse = await httpClient.GetAsync("/api/b2b/companies"))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GetB2BCompaniesResponse>(responseString);
                    if (!response.IsSuccess)
                        throw new AggregateException(response.Errors);

                    return response;
                }
            }
        }

        public async Task<GetB2BDevicesResponse> GetDevicesByCompanyIdAsync(AccessTokenModel accessToken, int companyId, int page = 1)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _options.BaseAddress;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("X-ApiToken", accessToken.AccessToken);
                using (var httpResponse = await httpClient.GetAsync($"/api/b2b/companies/{companyId}/devices?page={(page <= 0 ? 1 : page)}"))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GetB2BDevicesResponse>(responseString);
                    if (!response.IsSuccess)
                        throw new AggregateException(response.Errors);

                    return response;
                }
            }
        }

        public async Task<GetB2BDeviceHistoryResponse> GetHistoryByDeviceIdAsync(AccessTokenModel accessToken, int companyId, string deviceId, int page = 1)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _options.BaseAddress;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Add("X-ApiToken", accessToken.AccessToken);
                using (var httpResponse = await httpClient.GetAsync($"/api/b2b/companies/{companyId}/devices/{deviceId}/logs?page={(page <= 0 ?  1 : page)}"))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GetB2BDeviceHistoryResponse>(responseString);
                    if (!response.IsSuccess)
                        throw new AggregateException(response.Errors);

                    return response;
                }
            }
        }
    }
}
