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
    using Models.Utils;
    using ViewModels;

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
            var secretLen = NumToListConverter.Convert(secret).Count;

            if (player == null || game == null)
            {
                return NotFound();
            }

            var opponent = this.context.GetOpponentPlayer(player, game);

            if (opponent == this.context.GetComputerPlayer())
            {
                var secretNumberForComputer = new SecretNumber()
                {
                    Game = game,
                    Player = opponent,
                    Number = SecretNumberGenerator.GenerateUniqueSecretNumber(secretLen)
                };

                this.context.SecretNumbers.Add(secretNumberForComputer);
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
            var player = this.context.GetPlayer(playerId);
            var game = this.context.GetGame(gameId);
            var guessLen = NumToListConverter.Convert(guess).Count;

            if (player == null || game == null)
            {
                return NotFound();
            }

            var opponent = this.context.GetOpponentPlayer(player, game);
            opponent.SecretNumberProvider = new DbSecretNumberProvider(gameId, opponent.PlayerId);

            var playerGuessResult = opponent.CheckGuess(guess);
            var hasFoundSecret = playerGuessResult.Bulls == guessLen;

            if (hasFoundSecret)
            {
                game.Finish();
                player.Score += 100;
            }

            var playerGuessViewModel = new PlayerGuessResultViewModel()
            {
                PlayerId = player.PlayerId,
                BullsGuessed = playerGuessResult.Bulls,
                CowsGuessed = playerGuessResult.Cows,
                Guess = guess,
                HasGameFinished = hasFoundSecret,
                Winner = hasFoundSecret ? player.Name : null
            };
            this.context.PlayerGuessResults.Add(new PlayerGuessResult()
            {
                Game = game,
                Player = player,
                BullsGuessed = playerGuessResult.Bulls,
                CowsGuessed = playerGuessResult.Cows,
                Guess = guess,
            });

            PlayerGuessResultViewModel computerGuessResultViewModel = null;

            if (opponent == this.context.GetComputerPlayer())
            {
                if (!hasFoundSecret)
                {
                    player.SecretNumberProvider = new DbSecretNumberProvider(gameId, playerId);
                    var computerGuess = SecretNumberGenerator.GenerateUniqueSecretNumber(guessLen);
                    var opponentGuessResult = player.CheckGuess(computerGuess);
                    var hasGuessedSecret = playerGuessResult.Bulls == guessLen;

                    if (hasGuessedSecret)
                    {
                        opponent.Score += 100;
                        game.Finish();
                    }

                    computerGuessResultViewModel = new PlayerGuessResultViewModel()
                    {
                        PlayerId = opponent.PlayerId,
                        BullsGuessed = opponentGuessResult.Bulls,
                        CowsGuessed = opponentGuessResult.Cows,
                        Guess = computerGuess,
                        HasGameFinished = hasGuessedSecret,
                        Winner = hasGuessedSecret ? opponent.Name : null
                    };

                    this.context.PlayerGuessResults.Add(new PlayerGuessResult()
                    {
                        Game = game,
                        Player = opponent,
                        BullsGuessed = opponentGuessResult.Bulls,
                        CowsGuessed = opponentGuessResult.Cows,
                        Guess = computerGuess
                    });
                }
            }

            this.context.SaveChanges();

            var guessResultViewModel = new List<PlayerGuessResultViewModel>();

            guessResultViewModel.Add(playerGuessViewModel);

            if (computerGuessResultViewModel != null)
            {
                guessResultViewModel.Add(computerGuessResultViewModel);
            }

            return Ok(guessResultViewModel);
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
