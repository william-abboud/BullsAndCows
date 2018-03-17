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

        public DbSet<Round> Rounds { get; set; }

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

            modelBuilder.Entity<Round>()
                .HasRequired(r => r.Game)
                .WithMany(g => g.Rounds)
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
            return this.Players.FirstOrDefault();
        }
    }
}