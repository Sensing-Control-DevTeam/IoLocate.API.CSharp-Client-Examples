using IoLocate.Api.Client.ConsoleApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoLocate.Api.Client.ConsoleApp.Services
{
    public interface IIolocateService
    {
        Task<AccessTokenModel> GetAccessTokenAsync(string userName, string password);
        Task<IEnumerable<CompanyRepresentation>> GetCompaniesAsync(AccessTokenModel accessToken);
        Task<IEnumerable<DeviceRepresentation>> GetDevicesByCompanyIdAsync(AccessTokenModel accessToken, int companyId);
        Task<IEnumerable<HistoryRepresentation>> GetHistoryByDeviceIdAsync(AccessTokenModel accessToken, int companyId, int deviceId);
    }
}
