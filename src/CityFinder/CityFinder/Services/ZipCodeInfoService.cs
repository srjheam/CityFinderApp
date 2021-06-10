using CityFinder.Models;
using System.IO;
using System.Threading.Tasks;

namespace CityFinder.Services
{
    static class ZipCodeInfoService
    {
        const string BASE_URI = "https://api.zippopotam.us/";

        public static async Task<ZipCodeInfo> GetAsync(string countryAbbreviation, string postCode)
        {
            var endpoint = Path.Combine(BASE_URI, countryAbbreviation, postCode);

            var result = await HttpDataService.GetAsync<ZipCodeInfo>(endpoint);
            return result;
        }
    }
}
