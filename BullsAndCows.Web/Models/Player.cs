namespace BullsAndCows.Web.Models
{
    using Utils;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Player : IPlayer
    {
        public ISecretNumberProvider SecretNumberProvider { private get; set; }

        [Key]
        [ForeignKey("User")]
        public string PlayerId { get; set; }

        [Required]
        public string Name { get; set; }

        public int Score { get; set; }

        public virtual ICollection<Game> GamesAsPlayerOne { get; set; }

        public virtual ICollection<Game> GamesAsPlayerTwo { get; set; }

        public virtual ApplicationUser User { get; set; }

        public Player()
        {
            this.GamesAsPlayerOne = new HashSet<Game>();
            this.GamesAsPlayerTwo = new HashSet<Game>();
        }

        public Player(ISecretNumberProvider provider)
        {
            this.SecretNumberProvider = provider;
        }

        public IGuessResult CheckGuess(int guess)
        {
            var guessResult = new GuessResult();
            var guessList = NumToListConverter.Convert(guess);
            var secretNumberList = NumToListConverter.Convert(this.SecretNumberProvider.GetSecretNumber());

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
    }
}
