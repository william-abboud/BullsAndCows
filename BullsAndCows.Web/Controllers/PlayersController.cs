namespace BullsAndCows.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using App_Data;
    using Dtos;
    using Models;
    using Models.Providers;

    [Authorize]
    public class PlayersController : ApiController
    {
        private readonly ApplicationDbContext context;

        public PlayersController()
        {
            context = new ApplicationDbContext();
        }
        
        public IHttpActionResult GetPlayers()
        {
            var players = Mapper.Map(this.context.Players.ToList(), new List<PlayerDto>());

            return Ok(players);
        }

        public IHttpActionResult GetPlayer(string id)
        {
            var player = this.context.GetPlayer(id);

            if (player == null)
            {
                return NotFound();
            }

            var result = Mapper.Map(player, new PlayerDto());

            return Ok(result);
        }

        [HttpPost]
        [Route("api/players/{playerId}/game/{gameId}/createSecret/{secret}")]
        public IHttpActionResult CreateSecret(string playerId, int gameId, int secret)
        {
            var player = this.context.GetPlayer(playerId);
            var game = this.context.GetGame(gameId);

            if (player == null || game == null)
            {
                return NotFound();
            }

            var secretNumber = new SecretNumber
            {
                Game = game,
                Player = player,
                Number = secret
            };

            context.SecretNumbers.Add(secretNumber);
            context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("api/players/{playerId}/game/{gameId}/guessSecret/{guess}")]
        public IHttpActionResult GuessSecret(string playerId, int gameId, int guess)
        {
            var playerInDb = this.context.GetPlayer(playerId);
            var gameInDb = this.context.GetGame(gameId);

            if (playerInDb == null || gameInDb == null)
            {
                return NotFound();
            }

            var opponent = this.context.GetOpponentPlayer(playerInDb, gameInDb);
            opponent.SecretNumberProvider = new DbSecretNumberProvider(gameId, opponent.PlayerId);

            var result = opponent.CheckGuess(guess);
            var guessResult = new GuessResult
            {
                Bulls = result.Bulls,
                Cows = result.Cows
            };

            var round = new Round
            {
                Game = gameInDb,
                Player = playerInDb,
                BullsGuessed = guessResult.Bulls,
                CowsGuessed = guessResult.Cows
            };

            this.context.Rounds.Add(round);
            context.SaveChanges();

            return Ok(guessResult);
        }

        [Route("api/players/top")]
        [AllowAnonymous]
        public IHttpActionResult GetTopPlayers(int limit = 25)
        {
            var topPlayers = this.context.Players
                .OrderByDescending(p => p.Score)
                .Take(limit)
                .ToList();

            var players = Mapper.Map(topPlayers, new List<PlayerDto>());

            return Ok(players);
        }
    }
}
