namespace BullsAndCows.Web.ViewModels
{
    public class PlayerGuessResultViewModel
    {
        public string PlayerId { get; set; }

        public string Winner { get; set; }

        public int GameId { get; set; }

        public int Guess { get; set; }

        public int BullsGuessed { get; set; }

        public int CowsGuessed { get; set; }

        public bool HasGameFinished { get; set; }

        public bool IsAbandoned { get; set; }
    }
}