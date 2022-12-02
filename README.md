# HTTP Client for IoLocate API 

## :warning: Request credentials to access our IoLocate API at **support@iolocate.io**

**Swagger UI**: https://app.iolocate.io/api/swagger-ui/#/b2b

## How it works?
You can use [IoLocateService](https://github.com/Sensing-Control-DevTeam/IoLocate.API.CSharp-Client-Examples/blob/main/IoLocate.Api.Client/IoLocate.Api.Client.ConsoleApp/Services/IolocateService.cs) for access our data.

**1. Initilize and configure IoLocate Service:**

```csharp

var options = new IoLocateApiOption
{
    BaseAddress = new Uri("https://app.iolocate.io")
};
var iolocateService = new IolocateService(options);

```

**2. Get Access Token:**

```csharp

var userName = "Request credentials to support@iolocate.io.";
var password = "Request credentials to support@iolocate.io.";

var getAccessTokenResponse = await iolocateService.GetAccessTokenAsync(userName, password);
if (!getAccessTokenResponse.IsSuccess)
    throw new AggregateException(getAccessTokenResponse.Errors);

var accessToken = getAccessTokenResponse.Data;

```

**3. Get Companies:** 

```csharp

var getCompaniesResponse = await iolocateService.GetCompaniesAsync(accessToken);
if (!getCompaniesResponse.IsSuccess)
    throw new AggregateException(getCompaniesResponse.Errors);

var companies = getCompaniesResponse.Data;

```

**4. Get Devices by company id:**

```csharp

var getDevicesByCompanyIdResponse = await iolocateService.GetDevicesByCompanyIdAsync(accessToken, company.Id, page: 1);
if (!getDevicesByCompanyIdResponse.IsSuccess)
    throw new AggregateException(getDevicesByCompanyIdResponse.Errors);

var devicesPaginated = getDevicesByCompanyIdResponse.Data;

```

**5. Get History by device id:**

```csharp

var getHistoryByDeviceIdResponse = await iolocateService.GetHistoryByDeviceIdAsync(accessToken, company.Id, device.Id, page: 1);
if (!getHistoryByDeviceIdResponse.IsSuccess)
    throw new AggregateException(getHistoryByDeviceIdResponse.Errors);

var historyPaginated = getHistoryByDeviceIdResponse.Data;
    
```

**6. Get History by device id and date range:**

```csharp
var from = new DateTime(2022, 11, 1);
var to = new DateTime(2022, 11, 30).AddDays(1).AddMilliseconds(-1);

var getHistoryByDeviceIdAndDateRangeResponse = await iolocateService.GetHistoryByDeviceIdAndDateRangeAsync(accessToken, company.Id, device.Id, from, to, page: 1);
if (!getHistoryByDeviceIdAndDateRangeResponse.IsSuccess)
    throw new AggregateException(getHistoryByDeviceIdAndDateRangeResponse.Errors);

var historyPaginated = getHistoryByDeviceIdAndDateRangeResponse.Data;
    
```

### NOTE:  
  
**Json Settings:**

- **Pascal Case**
- **UTC** DateTime Assumption
- **ISO8601** Date Format
- Exclude Type Info
- Include Null Values

## Commercial Information  
**Asset Monitoring**  
  
Whether you want to monitor machines, equipment, vehicles, locate containers, track lost goods, or detect changes in the environment, our IoT asset tracking solution provides the tools necessary to substantially improve efficiencies in a wide variety of business sectors.

**Main Website**: http://www.iolocate.io/en/  
**Application Website**: https://app.iolocate.io/

