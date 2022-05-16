using IoLocate.Api.Client.ConsoleApp.Options;
using IoLocate.Api.Client.ConsoleApp.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IoLocate.Api.Client.ConsoleApp
{
    internal class Program
    {
        private static IIolocateService _iolocateService;

        static void Main(string[] args)
        {
            var options = new IoLocateApiOption
            {
                BaseAddress = new Uri("https://dev.iolocate.io")
            };
            _iolocateService = new IolocateService(options);

            MainAsync(args).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            var userName = "Request credentials to support@iolocate.io.";
            var password = "Request credentials to support@iolocate.io.";

            try
            {
                var getAccessTokenResponse = await _iolocateService.GetAccessTokenAsync(userName, password);
                if (!getAccessTokenResponse.IsSuccess)
                    throw new AggregateException(getAccessTokenResponse.Errors);

                var getCompaniesResponse = await _iolocateService.GetCompaniesAsync(getAccessTokenResponse.Data);
                if (!getCompaniesResponse.IsSuccess)
                    throw new AggregateException(getAccessTokenResponse.Errors);

                foreach (var company in getCompaniesResponse.Data)
                {
                    Console.WriteLine("------------------- Company -------------------");

                    Console.WriteLine($"CompanyName: {company.Name}");

                    Console.WriteLine("Devices:");

                    var getDevicesResponse = await _iolocateService.GetDevicesByCompanyIdAsync(getAccessTokenResponse.Data, company.Id);
                    if (!getDevicesResponse.IsSuccess)
                        throw new AggregateException(getDevicesResponse.Errors);

                    foreach (var device in getDevicesResponse.Data)
                    {
                        Console.WriteLine("******************   Device  *********************");

                        Console.WriteLine($"Device Name: {device.Name}");
                        Console.WriteLine($"Longitude: {device.Longitude}");
                        Console.WriteLine($"Latitude: {device.Latitude}");
                        Console.WriteLine($"Last Update: {device.LastUpdate}");
                        Console.WriteLine($"Internal Temperature: {device.InternalTemperature}");
                        Console.WriteLine($"Location Type: {device.LocationType}");
                        Console.WriteLine($"Accuracy: {device.Accuracy}");
                        Console.WriteLine($"Status: {device.Status}");
                        Console.WriteLine($"Device Type: {device.DeviceType}");
                        Console.WriteLine($"Run Hours: {device.RunHours}");
                        Console.WriteLine($"Battery Percentage: {device.BatteryPercentage}");

                        Console.WriteLine("******************   History  *********************");

                        var getHistoryResponse = await _iolocateService.GetHistoryByDeviceIdAsync(getAccessTokenResponse.Data, company.Id, device.Id);
                        if (!getHistoryResponse.IsSuccess)
                            throw new AggregateException(getHistoryResponse.Errors);

                        foreach (var history in getHistoryResponse.Data.Source.OrderByDescending(c => c.DataLogged))
                        {
                            Console.WriteLine($"Asset: {history.Asset}");
                            Console.WriteLine($"Data Logged: {history.DataLogged}");
                            Console.WriteLine($"Longitude: {history.Longitude}");
                            Console.WriteLine($"Latitude: {history.Latitude}");
                            Console.WriteLine($"Location Type: {history.LocationType}");
                            Console.WriteLine($"Position Accuracy: {history.PositionAccuracy}");
                            Console.WriteLine($"Log Reason: {history.LogReason}");
                            Console.WriteLine($"Battery: {history.Battery}");
                            Console.WriteLine($"Battery Percentage: {history.BatteryPercentage}");
                            Console.WriteLine($"Internal Temperature: {history.InternalTemperature}");
                            Console.WriteLine($"External Temperature: {history.ExternalTemperature}");
                            Console.WriteLine($"Speed: {history.Speed}");
                            Console.WriteLine($"Speed Accuracy: {history.SpeedAccuracy}");
                            Console.WriteLine($"Altitude: {history.Altitude}");
                            Console.WriteLine($"Heading Degrees: {history.HeadingDegrees}");
                            Console.WriteLine($"Alarm: {history.Alarm}");
                            Console.WriteLine($"Angle: {history.Angle}");
                            Console.WriteLine($"Run Hours: {history.RunHours}");
                        }

                        Console.WriteLine("***********************************************");

                        Console.WriteLine("***********************************************");
                    }

                    Console.WriteLine("----------------------------------------------");
                }
            }
            catch (AggregateException aex)
            {
                foreach (var ex in aex.InnerExceptions)
                    Console.WriteLine(ex.Message);

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
