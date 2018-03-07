namespace BullsAndCows.Models.Abstract
{
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Player : IPlayer
    {
        private static List<int> ConvertNumToList(int number)
        {
            return number.ToString()
                .Select(digit => (int)char.GetNumericValue(digit))
                .ToList();
        }

        protected int SecretNumber;

        public string Name { get; set; }

        public abstract void CreateSecretNumber(int numberOfDigits);

        public abstract int SetGuess(int numberOfDigits);

        public IGuessResult CheckGuess(int guess)
        {
            var guessResult = new GuessResult();
            var guessList = ConvertNumToList(guess);
            var secretNumberList = ConvertNumToList(this.SecretNumber);

            for (int i = 0; i < secretNumberList.Count; i++)
            {
                var guessDigit = guessList[i];

                if (secretNumberList.Contains(guessDigit))
                {
                    if (secretNumberList.IndexOf(guessDigit) == i)
                    {
                        guessResult.Bulls++;
                    }
                    else
                    {
                        guessResult.Cows++;
                    }
                }
            }

            return guessResult;
        }

        public IGuessResult GuessSecretNumber(IPlayer oponent, int guess)
        {
            return oponent.CheckGuess(guess);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
