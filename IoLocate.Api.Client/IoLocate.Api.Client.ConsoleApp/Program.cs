using IoLocate.Api.Client.ConsoleApp.Options;
using IoLocate.Api.Client.ConsoleApp.Services;
using System;
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
                BaseAddress = new Uri("https://dev.iolocate.io"),
                UseName = "Request credentials to support@iolocate.io.",
                Password = "Request credentials to support@iolocate.io."
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
                var token = await _iolocateService.GetAccessTokenAsync(userName, password);

                var companies = await _iolocateService.GetCompaniesAsync(token);
                foreach (var company in companies)
                {
                    Console.WriteLine("----------------------------------------------");

                    Console.WriteLine($"CompanyName: {company.Name}");

                    Console.WriteLine("Devices:");

                    var devices = await _iolocateService.GetDevicesByCompanyIdAsync(token, company.Id);
                    foreach (var device in devices)
                    {
                        Console.WriteLine("***********************************************" );

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
