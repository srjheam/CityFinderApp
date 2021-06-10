using CityFinder.Models;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;

namespace CityFinder.Tests.Models
{
    public class ZipCodeTests
    {
        #region MemberData
        public static IEnumerable<object[]> TryParseZipCode_ValidZipCodesData
        {
            get => new[]
            {
                new object[] { "US 10001", new ZipCode("US", "10001") },
                new object[] { "BR 29730-000", new ZipCode("BR", "29730-000") },
                new object[] { "AD AD100", new ZipCode("AD", "AD100") },
                new object[] { "CA A0A", new ZipCode("CA", "A0A") },
                new object[] { "CZ 100 00", new ZipCode("CZ", "100 00") },
                new object[] { "LK *", new ZipCode("LK", "*") }
            };
        }

        public static IEnumerable<object[]> TryParseZipCode_InvalidZipCodesData
        {
            get => new[]
            {
                new object[] { "" },
                new object[] { "BR " },
                new object[] { "ADAD100" },
                new object[] { "CAA 0A" },
                new object[] { "S1 1000" }
            };
        }
        #endregion

        [Theory]
        [InlineData("US 10001")]
        [InlineData("BR 29730-000")]
        [InlineData("AD AD100")]
        [InlineData("CA A0A")]
        [InlineData("CZ 100 00")]
        [InlineData("LK *")]
        public void IsZipCodeValid_ValidZipCodes_ReturnsTrue(string input)
        {
            var actual = ZipCode.IsZipCodeValid(input);

            Assert.True(actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("BR ")]
        [InlineData("ADAD100")]
        [InlineData("CAA 0A")]
        [InlineData("S1 1000")]
        public void IsZipCodeValid_InvalidZipCodes_ReturnsFalse(string input)
        {
            var actual = ZipCode.IsZipCodeValid(input);

            Assert.False(actual);
        }

        [Fact]
        public void IsZipCodeValid_Null_ReturnsFalse()
        {
            string input = null;

            var actual = ZipCode.IsZipCodeValid(input);

            Assert.False(actual);
        }

        [Theory, MemberData(nameof(TryParseZipCode_ValidZipCodesData))]
        public void TryParseZipCode_ValidZipCodes_ReturnsCorrectParse(string input, ZipCode expected)
        {
            var isSucceeded = ZipCode.TryParseZipCode(input, out ZipCode actual);

            Assert.True(isSucceeded);
            Assert.Equal(expected, actual);
        }

        [Theory, MemberData(nameof(TryParseZipCode_InvalidZipCodesData))]
        public void TryParseZipCode_InvalidZipCodes_ReturnsFalse(string input)
        {
            var isSucceeded = ZipCode.TryParseZipCode(input, out ZipCode actual);

            Assert.False(isSucceeded);
            Assert.Null(actual);
        }

    }
}
