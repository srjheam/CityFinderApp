using Newtonsoft.Json;

namespace CityFinder.Models
{
    public class Place
    {
        [JsonProperty("place name")]
        public string PlaceName { get; }
        public float Longitude { get; }
        public string State { get; }
        [JsonProperty("state abbreviation")]
        public string StateAbbreviation { get; }
        public float Latitude { get; }

        public Place(string placeName, float longitude, string state, string stateAbbreviation, float latitude)
        {
            PlaceName = placeName;
            Longitude = longitude;
            State = state;
            StateAbbreviation = stateAbbreviation;
            Latitude = latitude;
        }
    }
}