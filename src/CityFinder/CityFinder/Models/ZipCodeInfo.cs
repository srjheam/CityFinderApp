using System.Collections.Generic;
using Newtonsoft.Json;

namespace CityFinder.Models
{
    public class ZipCodeInfo
    {
        [JsonProperty("post code")]
        public string PostCode { get; }
        public string Country { get; }
        [JsonProperty("country abbreviation")]
        public string CountryAbbreviation { get; }
        public ICollection<Place> Places { get; } = new List<Place>();

        public ZipCodeInfo(string postCode, string country, string countryAbbreviation, ICollection<Place> places)
        {
            PostCode = postCode;
            Country = country;
            CountryAbbreviation = countryAbbreviation;
            Places = places;
        }
    }
}
