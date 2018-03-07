namespace BullsAndCows.Models.Interfaces
{
    public interface IGuessResult
    {
        int Cows { get; }

        int Bulls { get; }
    }
}
