namespace BullsAndCows.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SecretNumber
    {
        [Key, Column(Order = 0), ForeignKey("Game")]
        public int GameId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Player")]
        public string PlayerId { get; set; }

        public int Number { get; set; }

        public virtual Game Game { get; set; }

        public virtual Player Player { get; set; }
    }
}