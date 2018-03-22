using System.Net;
using System.Net.Http;

namespace BullsAndCows.Web.Controllers
{
    using App_Data;
    using System.Web.Http;
    using Models;
    using System.Collections.Generic;
    using AutoMapper;
    using ViewModels;

    [Authorize]
    public class GameController : ApiController
    {
        private readonly ApplicationDbContext context;

        public GameController()
        {
            this.context = new ApplicationDbContext();
        }

        [HttpPost]
        [Route("api/players/{playerId}/newgame")]
        public IHttpActionResult NewGame(string playerId)
        {
            var computerPlayer = this.context.GetComputerPlayer();
            var player = this.context.GetPlayer(playerId);

            if (player == null)
            {
                return NotFound();
            }

            var currentGame = this.context.GetCurrentGameBeingPlayed(player.PlayerId);

            currentGame?.Abandon();

            var game = new Game { PlayerOne = player, PlayerTwo = computerPlayer };
            this.context.Games.Add(game);
            this.context.SaveChanges();

            var gameView = new GameViewModel
            {
                GameId = game.GameId,
                GuessResults = new List<PlayerGuessResultViewModel>()
            };

            return Ok(gameView);
        }

        [HttpPost]
        [Route("api/players/{playerId}/continueGame")]
        public IHttpActionResult ContinueGame(string playerId)
        {
            var player = this.context.GetPlayer(playerId);

            if (player == null)
            {
                return NotFound();
            }

            var game = this.context.GetCurrentGameBeingPlayed(player.PlayerId);

            if (game == null)
            {
                // There are no current games being played
                return NotFound();
            }

            var secretNumber = this.context.GetSecretNumber(playerId, game.GameId);
            var guessResults = this.context.GetGuessResultsForGame(game.GameId);

            var gameView = new GameViewModel()
            {
                HasFinished = false,
                SecretNumber = secretNumber?.Number,
                GameId = game.GameId,
                GuessResults = guessResults.Count > 0
                    ? Mapper.Map(guessResults, new List<PlayerGuessResultViewModel>())
                    : new List<PlayerGuessResultViewModel>()
            };

            return Ok(gameView);
        }

        [HttpGet]
        [Route("api/players/{playerId}/game/{gameId}")]
        public IHttpActionResult GetGameInfoForPlayer(string playerId, int gameId)
        {
            var player = this.context.GetPlayer(playerId);

            if (player == null)
            {
                return NotFound();
            }

            var game = this.context.GetGame(gameId);

            if (game == null)
            {
                return NotFound();
            }

            var secretNumber = this.context.GetSecretNumber(player.PlayerId, game.GameId);
            var guessResults = this.context.GetGuessResultsForGame(game.GameId);

            var gameView = new GameViewModel()
            {
                HasFinished = game.HasFinished,
                SecretNumber = secretNumber?.Number,
                GameId = game.GameId,
                GuessResults = guessResults.Count > 0
                    ? Mapper.Map(guessResults, new List<PlayerGuessResultViewModel>())
                    : new List<PlayerGuessResultViewModel>()
            };

            return Ok(gameView);
        }
    }
}
