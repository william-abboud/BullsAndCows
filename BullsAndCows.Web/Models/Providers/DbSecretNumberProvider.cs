namespace BullsAndCows.Web.Models.Providers
{
    using System.Linq;
    using Interfaces;
    using App_Data;

    public class DbSecretNumberProvider : ISecretNumberProvider
    {
        private ApplicationDbContext context;
        private string PlayerId;
        private int GameId;

        public DbSecretNumberProvider(int gameId, string playerId)
        {
            this.context = new ApplicationDbContext();
            this.PlayerId = playerId;
            this.GameId = gameId;
        }

        public int GetSecretNumber()
        {
            var secretNumber = this.context.SecretNumbers.FirstOrDefault(sn => 
                sn.GameId == this.GameId &&
                sn.Player.PlayerId == this.PlayerId);

            if (secretNumber != null)
            {
                return secretNumber.Number;
            }

            return -1;
        }
    }
}