namespace BullsAndCows.Models
{
    using System;
    using System.Collections.Generic;

    public static class SecretNumberGenerator
    {
        private static readonly Random RandomGenerator = new Random();

        // Generates unique N digit number where
        // each digit must be different from the other
        public static int GenerateUniqueSecretNumber(int numberOfDigits)
        {
            var digits = new HashSet<int>();

            for (int i = 0; i < numberOfDigits; i++)
            {
                var randomDigit = RandomGenerator.Next(0, 9);

                while (randomDigit == 0 || digits.Contains(randomDigit))
                {
                    randomDigit = RandomGenerator.Next(0, 9);
                }

                digits.Add(randomDigit);
            }

            return int.Parse(String.Join("", digits));
        }
    }
}
