using System;

namespace Fintech_App.Util
{
    public class AccountNumberGenerator
    {
        public static long GenerateAccountNumber(int min = 1, int max = 50)
        {
            var numbers = new HashSet<int>();

            // Keep generating until the set contains exactly 7 unique items
            while (numbers.Count < 7)
            {
                numbers.Add(Random.Shared.Next(min, max + 1));
            }

            return long.Parse(string.Concat(numbers.OrderBy(x => x)));
        }
    }

}
