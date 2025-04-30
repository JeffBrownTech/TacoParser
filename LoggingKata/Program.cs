using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;
using System.Reflection.PortableExecutable;
using System.ComponentModel;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // Objective: Find the two Taco Bells that are the farthest apart from one another.
            logger.LogInfo("Log initialized");

            // Use File.ReadAllLines(path) to grab all the lines from your csv file. 
            // Optional: Log an error if you get 0 lines and a warning if you get 1 line
            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0) { logger.LogError("No data found in file!"); }
            if (lines.Length == 1) { logger.LogWarning("Only one entry found in data file"); }

            // This will display the first item in your lines array
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            var parser = new TacoParser();

            // Use the Select LINQ method to parse every line in lines collection
            var locations = lines.Select(parser.Parse).ToArray();

            // TODO: Create two `ITrackable` variables with initial values of `null`. 
            // These will be used to store your two Taco Bells that are the farthest from each other.
            // TODO: Create a `double` variable to store the distance
            ITrackable farStoreOne = null;
            ITrackable farStoreTwo = null;            
            double longestDistance = 0;

            ITrackable closestStoreOne = null;
            ITrackable closestStoreTwo = null;
            double shortestDistance = Double.MaxValue;

            foreach (var locA in locations)
            {
                var cordA = new GeoCoordinate();
                cordA.Latitude = locA.Location.Latitude;
                cordA.Longitude = locA.Location.Longitude;

                foreach (var locB in locations)
                {
                    var cordB = new GeoCoordinate();
                    cordB.Latitude = locB.Location.Latitude;
                    cordB.Longitude = locB.Location.Longitude;

                    double distanceAB = cordA.GetDistanceTo(cordB);

                    // Check if stores are the furthest apart
                    if (distanceAB > longestDistance)
                    {
                        farStoreOne = locA;
                        farStoreTwo = locB;
                        longestDistance = distanceAB;
                    }

                    // Check if stores are the closest together
                    if (distanceAB < shortestDistance && distanceAB != 0)
                    {
                        closestStoreOne = locA;
                        closestStoreTwo = locB;
                        shortestDistance = distanceAB;
                    }
                }
            }

            logger.LogInfo($"{farStoreOne.Name} and {farStoreTwo.Name} are the furthest apart at {longestDistance:F0} meters.");
            logger.LogInfo($"{farStoreOne.Name} and {farStoreTwo.Name} are the furthest apart at {(Utils.ConvertMetersToMiles(longestDistance)):F1} miles.");

            logger.LogInfo($"{closestStoreOne.Name} and {closestStoreTwo.Name} are the closest together at {shortestDistance:F0} meters.");
            logger.LogInfo($"{closestStoreOne.Name} and {closestStoreTwo.Name} are the closest together at {(Utils.ConvertMetersToMiles(shortestDistance)):F1} miles.");
        }
    }
}
