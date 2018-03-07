namespace BullsAndCows.Models
{
    using System;
    using System.Collections.Generic;
    using Enums;
    using Interfaces;

    public class BullsAndCowsGame : IGame
    {
        public const int MinPlayers = 2;
        public const int MinNumberOfDigits = 3;
        public const int MaxNumberOfDigits = 9;

        private int secretNumberLength;

        public TurnTakingStategy Strategy { get; } = TurnTakingStategy.Rotational;

        public List<IPlayer> Players { get; }

        public int SecretNumberLength
        {
            get { return secretNumberLength; }
            private set
            {
                if (value < MinNumberOfDigits)
                {
                    throw new ArgumentOutOfRangeException(
                        $"The unique secret number must be at least {MinNumberOfDigits} digits long.");
                }

                if (value > MaxNumberOfDigits)
                {
                    throw new ArgumentOutOfRangeException(
                        $"The unique secret number must be at most {MaxNumberOfDigits} digits long.");
                }

                this.secretNumberLength = value;
            }
        }

        public BullsAndCowsGame(int secretNumberLength, TurnTakingStategy stategy, params IPlayer[] players)
        {
            if (players.Length < MinPlayers)
            {
                throw new ArgumentOutOfRangeException($"You need at least {MinPlayers} players to play this game.");
            }

            this.Players = new List<IPlayer>(players);
            this.SecretNumberLength = secretNumberLength;
            this.Strategy = stategy;
        }

        public BullsAndCowsGame(params IPlayer[] players) : this(4, TurnTakingStategy.Rotational, players)
        {
        }

        private IPlayer GetOpponentOf(IPlayer guesser)
        {
            // TODO: Enumerator pattern
            if (this.Strategy == TurnTakingStategy.Rotational)
            {
                var guesserIdx = this.Players.IndexOf(guesser);
                var isGuesserLastPlayer = guesserIdx == (this.Players.Count - 1);

                return isGuesserLastPlayer ? this.Players[0] : this.Players[guesserIdx + 1];
            }

            return null;
        }

        public void Start(IPlayer firstToGoPlayer)
        {
            foreach (var player in this.Players)
            {
                player.CreateSecretNumber(this.SecretNumberLength);
            }

            var guesser = firstToGoPlayer;
            var guessResult = this.GiveTurnTo(guesser);

            while (!this.IsGuessCorrect(guessResult, guesser))
            {
                guesser = this.GetOpponentOf(guesser);
                guessResult = this.GiveTurnTo(guesser);
            }

            this.End(guesser);
        }

        public bool IsGuessCorrect(IGuessResult guessResult, IPlayer guesser)
        {
            return guessResult.Bulls == SecretNumberLength;
        }

        public IGuessResult GiveTurnTo(IPlayer player)
        {
            return player.GuessSecretNumber(this.GetOpponentOf(player), player.SetGuess(this.SecretNumberLength));
        }

        public void End(IPlayer guesser)
        {
            Console.WriteLine(guesser);
        }
    }
}
