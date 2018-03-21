namespace BullsAndCows.Web.ViewModels
{
    using System.Collections.Generic;

    public class GameViewModel
    {
        public int GameId { get; set; }

        public int? SecretNumber { get; set; }

        public bool HasFinished { get; set; }

        public ICollection<PlayerGuessResultViewModel> GuessResults { get; set; }
    }
}