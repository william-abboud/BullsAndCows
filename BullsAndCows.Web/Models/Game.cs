namespace BullsAndCows.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;

    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [ForeignKey("PlayerOne")]
        public string PlayerOneId { get; set; }

        [ForeignKey("PlayerTwo")]
        public string PlayerTwoId { get; set; }

        public virtual Player PlayerOne { get; set; }

        public virtual Player PlayerTwo { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }

        public virtual ICollection<SecretNumber> SecretNumbers { get; set; }

        public Game()
        {
            this.Rounds = new HashSet<Round>();
            this.SecretNumbers = new HashSet<SecretNumber>();
        }
    }
}
