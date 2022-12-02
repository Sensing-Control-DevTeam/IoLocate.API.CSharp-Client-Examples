using IoLocate.Api.Client.ConsoleApp.Models;
using IoLocate.Api.Client.ConsoleApp.Models.Responses;
using System;
using System.Threading.Tasks;

namespace IoLocate.Api.Client.ConsoleApp.Services
{
    public interface IIolocateService
    {
        Task<B2BLoginResponse> GetAccessTokenAsync(string userName, string password);
        Task<GetB2BCompaniesResponse> GetCompaniesAsync(AccessTokenModel accessToken);
        Task<GetB2BDevicesResponse> GetDevicesByCompanyIdAsync(AccessTokenModel accessToken, int companyId, int page = 1);
        Task<GetB2BDeviceHistoryByDateResponse> GetHistoryByDeviceIdAndDateRangeAsync(AccessTokenModel accessToken, int companyId, string deviceId, DateTime from, DateTime to, int page = 1);
        Task<GetB2BDeviceHistoryResponse> GetHistoryByDeviceIdAsync(AccessTokenModel accessToken, int companyId, string deviceId, int page = 1);
    }
}
