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
            logger.LogInfo("Log initialized");

            // Read input file and check for entries
            var lines = File.ReadAllLines(csvPath);

            if (lines.Length == 0) { logger.LogError("No data found in file!"); }
            if (lines.Length == 1) { logger.LogWarning("Only one entry found in data file"); }

            logger.LogInfo($"Lines: {lines[0]}");

            // Parse the file into locations
            var parser = new TacoParser();
            var locations = lines.Select(parser.Parse).ToArray();

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
