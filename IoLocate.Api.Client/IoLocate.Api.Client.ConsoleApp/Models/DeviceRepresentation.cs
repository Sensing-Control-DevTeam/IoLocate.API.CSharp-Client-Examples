using System;
using System.Collections.Generic;

namespace IoLocate.Api.Client.ConsoleApp.Models
{
    public class DeviceRepresentation
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public DateTime LastUpdate { get; set; }
        public double? InternalTemperature { get; set; }
        public List<string> Tags { get; set; }
        public string LocationType { get; set; }
        public double? Accuracy { get; set; }
        public string Status { get; set; }
        public string DeviceType { get; set; }
        public double? RunHours { get; set; }
        public double? BatteryPercentage { get; set; }
    }
}
