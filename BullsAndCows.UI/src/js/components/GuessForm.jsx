import React, { Component } from 'react';
import { authorizedRequest, getUserId } from '../utils';
import propTypes from 'prop-types';

class GuessForm extends Component {
  constructor(props) {
    super(props);

    this.onFormValueChange = this.onFormValueChange.bind(this);
    this.submitGuess = this.submitGuess.bind(this);

    this.state = {
      guess: ""
    };
  }

  submitGuess(e) {
    e.preventDefault();

    const { gameId } = this.props;
    const { guess } = this.state;

    return authorizedRequest(`/api/players/${getUserId()}/game/${gameId}/guessSecret/${guess}`, "POST")
      .then(response => response.json())
      .then(guessResults => this.props.onGuessResults(guessResults))
      .catch(error => console.error(error));
  }

  onFormValueChange({ target }) {
    this.setState({
      [target.id]: target.value
    });
  }

  render() {
    return (
      <form className="guess-form" onSubmit={this.submitGuess}>
        <div>
          <label htmlFor="guess">Guess Secret Number:</label>
          <input
            type="text"
            id="guess"
            value={this.state.guess}
            onChange={this.onFormValueChange}
            maxLength="4"
            minLength="4"
            required
          />
        </div>
        <button type="submit" disabled={this.props.disabled}>Guess</button>
      </form>
    );
  }
}

GuessForm.propTypes = {
  disabled: propTypes.bool.isRequired,
  onGuessResults: propTypes.func.isRequired,
  gameId: propTypes.number.isRequired
};

export default GuessForm;
