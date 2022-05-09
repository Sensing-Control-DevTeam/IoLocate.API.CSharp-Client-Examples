using System;

namespace IoLocate.Api.Client.ConsoleApp.Models
{
    public class HistoryRepresentation
    {
        public string Asset { get; set; }

        public DateTime? DataLogged { get; set; }

        public string LocationType { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string PositionAccuracy { get; set; }

        public string LogReason { get; set; }

        public double? Battery { get; set; }
        public double? BatteryPercentage { get; set; }

        public double? InternalTemperature { get; set; }
        public double? ExternalTemperature { get; set; }

        public double? Speed { get; set; }

        public double? SpeedAccuracy { get; set; }

        public double? Altitude { get; set; }

        public double? HeadingDegrees { get; set; }

        public string Alarm { get; set; }

        public double? Angle { get; set; }

        public double? RunHours { get; set; }
    }
}
