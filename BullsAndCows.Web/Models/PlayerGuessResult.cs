namespace BullsAndCows.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class PlayerGuessResult
    {
        [Key]
        public int Id { get; set; }

        public int Guess { get; set; }

        public int BullsGuessed { get; set; }

        public int CowsGuessed { get; set; }

        [ForeignKey("Player")]
        public string PlayerId { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Player Player { get; set; }

        public virtual Game Game { get; set; }
    }
}