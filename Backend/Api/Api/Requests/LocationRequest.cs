namespace Api.Requests
{
    public class LocationRequest
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Address { get; set; }
    }
}
