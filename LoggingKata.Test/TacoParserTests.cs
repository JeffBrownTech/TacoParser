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
        [InlineData("33.59453,-86.694742,Taco Bell Birmingham...", -86.694742)]
        [InlineData("34.271508,-84.798907,Taco Bell Cartersville...", -84.798907)]
        [InlineData("30.448367,-86.638431,Taco Bell Fort Walton Beach...", -86.638431)]
        [InlineData("34.518362,-87.7164,Taco Bell Russellvill...", -87.7164)]
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
        [InlineData("33.59453,-86.694742,Taco Bell Birmingham...", 33.59453)]
        [InlineData("34.271508,-84.798907,Taco Bell Cartersville...", 34.271508)]
        [InlineData("30.448367,-86.638431,Taco Bell Fort Walton Beach...", 30.448367)]
        [InlineData("34.518362,-87.7164,Taco Bell Russellvill...", 34.518362)]
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
        [InlineData("33.59453,-86.694742,Taco Bell Birmingham...", "Taco Bell Birmingham...")]
        [InlineData("34.271508,-84.798907,Taco Bell Cartersville...", "Taco Bell Cartersville...")]
        [InlineData("30.448367,-86.638431,Taco Bell Fort Walton Beach...", "Taco Bell Fort Walton Beach...")]
        [InlineData("34.518362,-87.7164,Taco Bell Russellvill...", "Taco Bell Russellvill...")]
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
        [InlineData(1, 0.00062137119223733392)]
        [InlineData(1609344, 1000)]
        [InlineData(5489.35, 3.4109239541080094)]
        public void ConvertMetersToMiles(double meters, double expected)
        {
            //Act
            var actual = Utils.ConvertMetersToMiles(meters);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
