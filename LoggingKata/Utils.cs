using System;
using System.Diagnostics.Metrics;

namespace LoggingKata;

public static class Utils
{
    private const double MetersPerMile = 1609.344;
    
    public static double ConvertMetersToMiles(double meters)
    {
        return meters / MetersPerMile;
    }
}
