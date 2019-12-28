using System.Collections.Generic;
using System.Linq;

namespace MyScore
{
    public static class Utility
    {
        public static List<string> MissingElements(IList<string> origin, IList<string> addition)
        {
            return addition.Where(e => !origin.Contains(e)).ToList();
        }
    }
}
