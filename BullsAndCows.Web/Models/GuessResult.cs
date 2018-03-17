namespace BullsAndCows.Web.Models
{
    using Interfaces;

    public class GuessResult : IGuessResult
    {
        public int Bulls { get; set; }
        public int Cows { get; set; }
    }
}
