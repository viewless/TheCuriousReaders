using System.Collections.Generic;

namespace TheCuriousReaders.Models.Helpers
{
    public static class CountriesValidator
    {
        public static bool IsCountryValid(string countryName)
        {
            var list = new List<string>
            {
                "Bulgaria", "Romania",
                "Germany", "Greece",
                "Turkey", "Serbia",
                "North Macedonia", "Moldova",
                "Croatia", "Montenegro"
            };

            return list.Contains(countryName);
        }
    }
}
