using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638,-84.677017,Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);
        }

        [Theory]
        [InlineData("34.073638,-84.677017,Taco Bell Acwort...", -84.677017)]
        [InlineData("34.996237,-85.291147,Taco Bell Chattanooga...", -85.291147)]
        [InlineData("32.425341,-84.948505,Taco Bell Columbus/1...", -84.948505)]
        public void ShouldParseLongitude(string line, double expected)
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse(line);

            //Assert
            Assert.Equal(expected, actual.Location.Longitude);
        }

        [Theory]
        [InlineData("34.073638,-84.677017,Taco Bell Acwort...", 34.073638)]
        [InlineData("34.996237,-85.291147,Taco Bell Chattanooga...", 34.996237)]
        [InlineData("32.425341,-84.948505,Taco Bell Columbus/1...", 32.425341)]
        public void ShouldParseLatitude(string line, double expected)
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse(line);

            //Assert
            Assert.Equal(expected, actual.Location.Latitude);
        }

        [Theory]
        [InlineData("34.073638,-84.677017,Taco Bell Acwort...", "Taco Bell Acwort...")]
        [InlineData("34.996237,-85.291147,Taco Bell Chattanooga...", "Taco Bell Chattanooga...")]
        [InlineData("32.425341,-84.948505,Taco Bell Columbus/1...", "Taco Bell Columbus/1...")]
        public void ShouldParseName(string line, string expected)
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse(line);

            //Assert
            Assert.Equal(expected, actual.Name);
        }

        [Theory]
        [InlineData(50000, 31.068559611866696)]
        [InlineData(12345, 7.6708273681698875)]
        [InlineData(0, 0)]
        public void ConvertMetersToMiles(double meters, double expected)
        {
            //Act
            var actual = Utils.ConvertMetersToMiles(meters);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
