using System.Collections.Generic;

namespace BullsAndCows.Web.App_Data
{
    using System.Linq;
    using System.Data.Entity;
    using Models;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Player> Players { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<PlayerGuessResult> PlayerGuessResults { get; set; }

        public DbSet<SecretNumber> SecretNumbers { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasRequired(g => g.PlayerOne)
                .WithMany(p => p.GamesAsPlayerOne)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Game>()
                .HasRequired(g => g.PlayerTwo)
                .WithMany(p => p.GamesAsPlayerTwo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PlayerGuessResult>()
                .HasRequired(r => r.Game)
                .WithMany(g => g.PlayerGuessResults)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecretNumber>()
                .HasRequired(sn => sn.Game)
                .WithMany(g => g.SecretNumbers)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecretNumber>()
                .HasRequired(sn => sn.Player);

            base.OnModelCreating(modelBuilder);
        }

        public Player GetComputerPlayer()
        {
            return this.Players.FirstOrDefault(p => p.Name == "Computer");
        }

        public Player GetPlayer(string id)
        {
            return this.Players.FirstOrDefault(p => p.PlayerId == id);
        }

        public Player GetOpponentPlayer(Player otherPlayer, Game game)
        {
            return game.PlayerOne == otherPlayer ? game.PlayerTwo : game.PlayerOne;
        }

        public Game GetGame(int id)
        {
            return this.Games.FirstOrDefault(g => g.GameId == id);
        }

        public Game GetCurrentGameBeingPlayed(string playerId)
        {
            return this.Games.FirstOrDefault(g => !g.HasFinished && !g.IsAbandoned);
        }

        public SecretNumber GetSecretNumber(string playerId, int gameId)
        {
            return this.SecretNumbers.FirstOrDefault(sn => sn.PlayerId == playerId && sn.GameId == gameId);
        }

        public ICollection<PlayerGuessResult> GetGuessResultsForGame(int gameId)
        {
            return this.Games.FirstOrDefault(g => g.GameId == gameId).PlayerGuessResults;
        }
    }
}