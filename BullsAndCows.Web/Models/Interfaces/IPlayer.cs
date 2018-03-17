namespace BullsAndCows.Web.Models.Interfaces
{
    public interface IPlayer
    {
        ISecretNumberProvider SecretNumberProvider { set; }

        IGuessResult CheckGuess(int guess);
    }
}
