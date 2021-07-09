using System.Collections.Generic;

namespace TheCuriousReaders.Models.Helpers
{
    public static class GenresValidator
    {
        public static bool IsGenreValid(string genreName)
        {
            var list = new List<string>
            {
                "Fantasy","Mystery",
                "Thriller","Romance",
                "Dystopian","Sci-Fi",
                "Horror","Crime",
            };

            return list.Contains(genreName);
        }
    }
}