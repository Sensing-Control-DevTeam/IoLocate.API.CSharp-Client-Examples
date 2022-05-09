using System;

namespace IoLocate.Api.Client.ConsoleApp.Models
{
    public class AccessTokenModel
    {
        public string AccessToken { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}
