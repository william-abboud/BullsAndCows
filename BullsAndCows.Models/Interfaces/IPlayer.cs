namespace BullsAndCows.Models.Interfaces
{
    public interface IPlayer
    {
        void CreateSecretNumber(int numberOfDigits);

        int SetGuess(int numberOfDigits);

        IGuessResult GuessSecretNumber(IPlayer oponent, int guess);

        IGuessResult CheckGuess(int guess);
    }
}
