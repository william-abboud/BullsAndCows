namespace BullsAndCows.Models
{
    using System;

    public class Startup
    {
        public static void Main()
        {
            var p1 = new ComputerPlayer { Name = "Computer one"};
            var p2 = new ComputerPlayer { Name = "Computer two" };

            var game = new BullsAndCowsGame(p1, p2);

            game.Start(p1);

            Console.ReadLine();
        }
    }
}
