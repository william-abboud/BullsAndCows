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

    authorizedRequest(`/api/players/${getUserId()}/game/${gameId}/createSecret/${secret}`, "POST");
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

/* ТODO: Fix getting game Id */
class Game extends Component {
  constructor(props) {
    super(props);

    this.state = {
      gameId: queryString.parse(this.props.location.search).gameId
    };
  }

  render() {
    if (!getAccessToken()) {
      return <Redirect to="/login" />;
    }

    return (
      <div className="game-view">
        <h2>Create a secret number:</h2>
        <SecretNumberForm gameId={this.state.gameId} />
      </div>
    );
  }
}

export default Game;
