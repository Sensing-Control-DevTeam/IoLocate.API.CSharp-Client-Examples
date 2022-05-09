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

        public async Task<AccessTokenModel> GetAccessTokenAsync(string userName, string password)
        {
            AccessTokenModel accessTokenModel = null;
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
                    if (!response.IsSuccess)
                        throw new AggregateException(response.Errors);

                    accessTokenModel = response.Data;
                }
            }
            return accessTokenModel;
        }

        public async Task<IEnumerable<CompanyRepresentation>> GetCompaniesAsync(AccessTokenModel accessToken)
        {
            var companies = new List<CompanyRepresentation>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _options.BaseAddress;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);
                using (var httpResponse = await httpClient.GetAsync("/api/b2b/companies"))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GetB2BCompaniesResponse>(responseString);
                    if (!response.IsSuccess)
                        throw new AggregateException(response.Errors);

                    companies = response.Data.ToList();
                }
            }
            return companies;
        }

        public async Task<IEnumerable<DeviceRepresentation>> GetDevicesByCompanyIdAsync(AccessTokenModel accessToken, int companyId)
        {
            var devices = new List<DeviceRepresentation>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _options.BaseAddress;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);
                using (var httpResponse = await httpClient.GetAsync($"/api/b2b/companies/{companyId}/devices"))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GetB2BDevicesResponse>(responseString);
                    if (!response.IsSuccess)
                        throw new AggregateException(response.Errors);

                    devices = response.Data.ToList();
                }
            }
            return devices;
        }

        public async Task<IEnumerable<HistoryRepresentation>> GetHistoryByDeviceIdAsync(AccessTokenModel accessToken, int companyId, int deviceId)
        {
            var historyList = new List<HistoryRepresentation>();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = _options.BaseAddress;
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.AccessToken);
                using (var httpResponse = await httpClient.GetAsync($"/api/b2b/companies/{companyId}/devices/{deviceId}/logs"))
                {
                    httpResponse.EnsureSuccessStatusCode();
                    var responseString = await httpResponse.Content.ReadAsStringAsync();
                    var response = JsonConvert.DeserializeObject<GetB2BDeviceHistoryResponse>(responseString);
                    if (!response.IsSuccess)
                        throw new AggregateException(response.Errors);

                    historyList = response.Data.ToList();
                }
            }
            return historyList;
        }
    }
}
