using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Location
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string? Address { get; private set; }

        private Location() { } 

        [JsonConstructor]
        public Location(double latitude, double longitude, string? address)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
        }
    }
}
