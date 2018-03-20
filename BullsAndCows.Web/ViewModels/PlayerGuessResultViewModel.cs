namespace BullsAndCows.Web.ViewModels
{
    public class PlayerGuessResultViewModel
    {
        public string PlayerId { get; set; }

        public int Guess { get; set; }

        public int BullsGuessed { get; set; }

        public int CowsGuessed { get; set; }
    }
}