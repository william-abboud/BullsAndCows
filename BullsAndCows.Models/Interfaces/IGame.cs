namespace BullsAndCows.Models.Interfaces
{
    public interface IGame
    {
        void Start(IPlayer firstToGo);

        IGuessResult GiveTurnTo(IPlayer player);

        bool IsGuessCorrect(IGuessResult guessResult, IPlayer guesser);

        void End(IPlayer winner);
    }
}
