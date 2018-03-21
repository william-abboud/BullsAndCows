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

        public bool HasFinished { get; private set; }

        public bool IsAbandoned { get; private set; }

        public virtual Player PlayerOne { get; set; }

        public virtual Player PlayerTwo { get; set; }

        public virtual ICollection<PlayerGuessResult> PlayerGuessResults { get; set; }

        public virtual ICollection<SecretNumber> SecretNumbers { get; set; }

        public Game()
        {
            this.PlayerGuessResults = new HashSet<PlayerGuessResult>();
            this.SecretNumbers = new HashSet<SecretNumber>();
        }

        public void Finish()
        {
            this.HasFinished = true;
        }

        public void Abandon()
        {
            this.IsAbandoned = true;
        }
    }
}
