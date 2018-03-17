namespace BullsAndCows.Web.Controllers
{
    using System.Linq;
    using App_Data;
    using Microsoft.AspNet.Identity;
    using System.Web.Http;
    using Models;

    [Authorize]
    public class GameController : ApiController
    {
        private readonly ApplicationDbContext context;

        public GameController()
        {
            this.context = new ApplicationDbContext();
        }

        [HttpGet]
        [Route("api/players/{playerId}/newgame")]
        public IHttpActionResult StartGameAgainstComputer(string playerId)
        {
            var computerPlayer = this.context.GetComputerPlayer();
            var player = this.context.Players.FirstOrDefault(p => p.PlayerId == playerId);

            if (player == null)
            {
                return NotFound();
            }

            var game = new Game
            {
                PlayerOne = player,
                PlayerTwo = computerPlayer
            };

            context.Games.Add(game);
            context.SaveChanges();

            return Ok(new { GameId = game.GameId });
        }
    }
}
