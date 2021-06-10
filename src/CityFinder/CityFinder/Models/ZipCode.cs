using System;
using System.Linq;

namespace CityFinder.Models
{
    public class ZipCode
    {
        public string CountryAbbreviation { get; }
        public string PostCode { get; }

        public ZipCode(string countryAbbreviation, string postCode)
        {
            CountryAbbreviation = countryAbbreviation;
            PostCode = postCode;
        }

        public static bool IsZipCodeValid(string zipCode)
        {
            // Default pattern:
            //   CA zip
            //     - CA: Country abbreviation, eg US for United States
            //     - zip: Post code, eg 10001 for New York
            //     Example: US 10001

            if (zipCode is null
                || zipCode.Length < 4
                || !zipCode.Contains(" ")
                || zipCode[2] != ' '
                || !zipCode.ToCharArray(0, 2).All(x => Char.IsLetter(x)))
            {
                return false;
            }

            return true;
        }

        public static bool TryParseZipCode(string str, out ZipCode zipCode)
        {
            zipCode = null;

            if (!IsZipCodeValid(str))
            {
                return false;
            }

            var ca = str.Substring(0, 2);
            var zip = String.Concat(str.Skip(3));
            zipCode = new ZipCode(ca, zip);

            return true;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj is null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            var zipCode = (ZipCode)obj;
            return CountryAbbreviation == zipCode.CountryAbbreviation && PostCode == zipCode.PostCode;
        }
    }
}
