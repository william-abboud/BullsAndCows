namespace BullsAndCows.Models
{
    using Abstract;

    public class ComputerPlayer : Player
    {
        public override int SetGuess(int numberOfDigits)
        {
            return SecretNumberGenerator.GenerateUniqueSecretNumber(numberOfDigits);
        }

        public override void CreateSecretNumber(int numberOfDigits)
        {
            if (this.SecretNumber == 0)
            {
                this.SecretNumber = SecretNumberGenerator.GenerateUniqueSecretNumber(numberOfDigits);
            }
        }
    }
}
