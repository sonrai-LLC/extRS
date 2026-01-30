namespace Sonrai.ExtRS.Models.GIS
{
    public class GeoLocation
    {
        public string Query { get; set; } = ""; // IP address
        public string Country { get; set; } = "";
        public string RegionName { get; set; } = "";
        public string City { get; set; } = "";
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Status { get; set; } = "";
    }
}
