import React, { Component } from 'react';
import { object } from 'prop-types';
import { authorizedRequest, getUserId, isLoggedIn } from '../utils';
import queryString from 'query-string';
import SecretNumberForm from '../components/SecretNumberForm';
import GuessForm from '../components/GuessForm';

function PlayerGuessResult({ guessResult }) {
  return (
    <div className="guess-result">
      <div className="guess">Guess: {guessResult.Guess}</div>
      <div className="cows">Cows: {guessResult.CowsGuessed}</div>
      <div className="bulls">Bulls: {guessResult.BullsGuessed}</div>
      {
        guessResult.HasGameFinished
          ? <div className="winner">Winner: {guessResult.Winner} !</div>
          : null
      }
    </div>
  );
}

PlayerGuessResult.propTypes = {
  guessResult: object.isRequired
};

class Game extends Component {
  constructor(props) {
    super(props);

    this.storeGuessResults = this.storeGuessResults.bind(this);
    this.getGameInfo = this.getGameInfo.bind(this);
    this.startGame = this.startGame.bind(this);
    this.continueGame = this.continueGame.bind(this);

    this.state = {
      game: {
        GameId: "",
        SecretNumber: "",
        HasFinished: "",
        GuessResults: [],
      }
    };
  }

  componentDidMount() {
    const { history, location } = this.props;
    const { newGame } = queryString.parse(location.search);

    if (!isLoggedIn()) {
      history.push("/login");
    }

    if (newGame === "true") {
      this.startGame();
    } else {
      this.continueGame();
    }
  }

  startGame() {
    return authorizedRequest(`/api/players/${getUserId()}/newgame`, "POST")
      .then(response => response.json())
      .then(game => this.setState({ game }))
      .then(() => this.props.history.push({
        pathname: '/game',
        search: "?newGame=false"
      }))
      .catch(err => console.error(err));
  }

  continueGame() {
    return authorizedRequest(`/api/players/${getUserId()}/continueGame`, "POST")
      .then(response => response.json())
      .then(game => this.setState({ game }))
      .catch(error => console.error(error));
  }

  getGameInfo() {
    return authorizedRequest(`/api/players/${getUserId()}/game/${this.state.game.GameId}`)
      .then(response => response.json())
      .then(game => this.setState({ game }))
      .catch(error => console.error(error));
  }

  storeGuessResults(newGuessResults) {
    const existingGame = this.state.game;
    const existingResults = existingGame.GuessResults;
    const newGameState = {
      ...existingGame,
      GuessResults: [...existingResults, ...newGuessResults]
    };

    if (newGuessResults.filter(result => result.HasGameFinished).length) {
      newGameState.HasFinished = true;
    }

    this.setState({ game: newGameState });
  }

  render() {
    const { game } = this.state;

    return (
      <div className="game-view">
        {
          !Boolean(game.SecretNumber)
            ?
              <SecretNumberForm gameId={game.GameId} onSecretSubmit={this.getGameInfo} />
            :
              <div className="players-summary">
                <div className="player-summary">
                  <h2>Your Secret Number: {game.SecretNumber}</h2>
                  <h2>Your Guesses:</h2>
                  <GuessForm
                    gameId={game.GameId}
                    onGuessResults={this.storeGuessResults}
                    disabled={game.HasFinished}
                  />
                </div>
                <div className="opponent-player-summary">
                  <h2>{"Opponent's Guesses:"}</h2>
                </div>
              </div>
        }

        <hr />

        {
          Array.isArray(game.GuessResults)
            ?
              <div className="player-results-wrapper">
                <div className="player-results">
                  {
                    game.GuessResults
                      .filter(gs => gs.PlayerId === getUserId())
                      .map(guessResult => (
                        <PlayerGuessResult
                          key={guessResult.PlayerId + guessResult.Guess}
                          guessResult={guessResult}
                        />
                      ))
                  }
                </div>
                <div className="player-results">
                  {
                    game.GuessResults
                      .filter(gs => gs.PlayerId !== getUserId())
                      .map(guessResult => (
                        <PlayerGuessResult
                          key={guessResult.PlayerId + guessResult.Guess}
                          guessResult={guessResult}
                        />
                      ))
                  }
                </div>
              </div>
            : null
        }
      </div>
    );
  }
}

Game.propTypes = {
  location: object.isRequired,
  history: object.isRequired
};

export default Game;
