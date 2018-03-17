using BullsAndCows.Web.Models;

namespace BullsAndCows.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using App_Data;
    using Dtos;
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
            var players = Mapper.Map(context.Players.ToList(), new List<PlayerDto>());

            return Ok(players);
        }

        public IHttpActionResult GetPlayer(string id)
        {
            var playerInDb = context.Players.FirstOrDefault(p => p.PlayerId == id);

            if (playerInDb == null)
            {
                return NotFound();
            }

            var player = Mapper.Map(playerInDb, new PlayerDto());

            return Ok(player);
        }

        [HttpPost]
        // TODO: QueryParams for secret and gameId
        [Route("api/players/{playerId}/createSecret/{secret}/game/{gameId}")]
        public IHttpActionResult CreateSecret(string playerId, int secret, int gameId)
        {
            var playerInDb = context.Players.FirstOrDefault(p => p.PlayerId == playerId);

            if (playerInDb == null)
            {
                return NotFound();
            }

            var secretNumber = new SecretNumber
            {
                GameId = gameId,
                PlayerId = playerInDb.PlayerId,
                Number = secret
            };

            context.SecretNumbers.Add(secretNumber);
            context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        // TODO: QueryParams for secret and gameId
        [Route("api/players/{playerId}/guessSecret/{secret}/{gameId}")]
        public IHttpActionResult GuessSecret(string playerId, int guess, int gameId)
        {
            var playerInDb = context.Players.FirstOrDefault(p => p.PlayerId == playerId);
            var gameInDb = context.Games.FirstOrDefault(g => g.GameId == gameId);

            if (playerInDb == null || gameInDb == null)
            {
                return NotFound();
            }

            var opponent = gameInDb.PlayerOneId == playerId ? gameInDb.PlayerTwo : gameInDb.PlayerOne;

            opponent.SecretNumberProvider = new DbSecretNumberProvider(gameId, opponent.PlayerId);

            var result = opponent.CheckGuess(guess);
            var round = new Round
            {
                GameId = gameId,
                PlayerId = playerId,
                BullsGuessed = result.Bulls,
                CowsGuessed = result.Cows
            };

            this.context.Rounds.Add(round);
            context.SaveChanges();

            return Ok(result);
        }

        [Route("api/players/top")]
        [AllowAnonymous]
        public IHttpActionResult GetTopPlayers(int limit = 25)
        {
            var topPlayers = context.Players
                .OrderByDescending(p => p.Score)
                .Take(limit)
                .ToList();

            var players = Mapper.Map(topPlayers, new List<PlayerDto>());

            return Ok(players);
        }
    }
}
