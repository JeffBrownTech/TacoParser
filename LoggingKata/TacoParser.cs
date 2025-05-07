namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            // Split incoming line from input file into Array of strings
            var cells = line.Split(',');

            // If array's Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                logger.LogWarning("Less than three items contained in the data");
                return null; 
            }

            // Latitude from your array at index 0
            double latitude = double.Parse(cells[0]);            
            
            // Longitude from your array at index 1
            double longitude = double.Parse(cells[1]);            
            
            // Store name at index 2
            var name = cells[2];            

            // Set the values of the point (Latitude and Longitude)
            var point = new Point(latitude, longitude);

            // Set the values of the store (Name and Location)
            var store = new TacoBell(name, point);

            return store;
        }
    }
}
