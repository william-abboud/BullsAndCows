namespace BullsAndCows.Web.Models.Utils
{
    using System.Collections.Generic;
    using System.Linq;

    public static class NumToListConverter
    {
        public static List<int> Convert(int number)
        {
            return number.ToString()
                .Select(digit => (int)char.GetNumericValue(digit))
                .ToList();
        }
    }
}