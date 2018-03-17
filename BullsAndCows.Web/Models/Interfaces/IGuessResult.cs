namespace BullsAndCows.Web.Models.Interfaces
{
    public interface IGuessResult
    {
        int Cows { get; }

        int Bulls { get; }
    }
}
