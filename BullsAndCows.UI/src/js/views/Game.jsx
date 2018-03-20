import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { getAccessToken, authorizedRequest, getUserId } from '../utils';
import queryString from 'query-string';

class SecretNumberForm extends Component {
  constructor(props) {
    super(props);

    this.submitSecret = this.submitSecret.bind(this);
    this.onFormValueChange = this.onFormValueChange.bind(this);

    this.state = {
      secret: 0
    };
  }

  submitSecret(e) {
    e.preventDefault();

    const { gameId } = this.props;
    const { secret } = this.state;

    authorizedRequest(`/api/players/${getUserId()}/game/${gameId}/createSecret/${secret}`, "POST")
      .then(() => this.props.onSecretSubmit());
  }

  onFormValueChange({ target }) {
    this.setState({
      [target.id]: target.value
    });
  }

  render() {
    const { playerName } = this.props;

    return (
      <form onSubmit={this.submitSecret}>
        <div>
          <label htmlFor="secret">Secret Number:</label>
          <input type="text" id="secret" value={this.state.secret} onChange={this.onFormValueChange} />
        </div>
        <button type="submit">Submit Secret</button>
      </form>
    );
  }
}

class GuessForm extends Component {
  constructor(props) {
    super(props);

    this.onFormValueChange = this.onFormValueChange.bind(this);
    this.submitGuess = this.submitGuess.bind(this);

    this.state = {
      guess: 0
    };
  }

  submitGuess(e) {
    e.preventDefault();

    const { gameId } = this.props;
    const { guess } = this.state;

    authorizedRequest(`/api/players/${getUserId()}/game/${gameId}/guessSecret/${guess}`, "POST")
      .then(response => response.json())
      .then(guessResult => console.dir(guessResult))
      .catch(error => console.error(error));
  }

  onFormValueChange({ target }) {
    this.setState({
      [target.id]: target.value
    });
  }

  render() {
    return (
      <form onSubmit={this.submitGuess}>
        <div>
          <label htmlFor="guess">Guess Secret Number:</label>
          <input type="text" id="guess" value={this.state.guess} onChange={this.onFormValueChange} />
        </div>
        <button type="submit">Guess</button>
      </form>
    );
  }
}

/* ТODO: Fix getting game Id */
class Game extends Component {
  constructor(props) {
    super(props);

    this.showGuessForm = this.showGuessForm.bind(this);
    this.state = {
      gameId: queryString.parse(this.props.location.search).gameId,
      guessFormHidden: true,
    };
  }

  showGuessForm() {
    this.setState({ guessFormHidden: false });
  }

  render() {
    if (!getAccessToken()) {
      return <Redirect to="/login" />;
    }

    return (
      <div className="game-view">
        {
          this.state.guessFormHidden
            ?
              <SecretNumberForm gameId={this.state.gameId} onSecretSubmit={this.showGuessForm} />
            :
              <GuessForm gameId={this.state.gameId} />
        }
      </div>
    );
  }
}

export default Game;
