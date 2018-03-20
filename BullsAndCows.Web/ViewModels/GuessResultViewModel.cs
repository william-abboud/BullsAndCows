namespace BullsAndCows.Web.ViewModels
{
    using System.Collections.Generic;

    public class GuessResultViewModel
    {
        public List<PlayerGuessResultViewModel> GuessResults { get; set; }

        public GuessResultViewModel(params PlayerGuessResultViewModel[] guessResults)
        {
            GuessResults = new List<PlayerGuessResultViewModel>(guessResults);
        }
    }
}