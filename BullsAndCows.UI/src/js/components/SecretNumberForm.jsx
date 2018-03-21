import React, { Component } from 'react';
import { authorizedRequest, getUserId } from '../utils';
import { number, func } from 'prop-types';

class SecretNumberForm extends Component {
  constructor(props) {
    super(props);

    this.submitSecret = this.submitSecret.bind(this);
    this.onFormValueChange = this.onFormValueChange.bind(this);

    this.state = {
      secret: ""
    };
  }

  submitSecret(e) {
    e.preventDefault();

    if (!e.target.checkValidity()) {
      return;
    }

    const { gameId } = this.props;
    const { secret } = this.state;

    authorizedRequest(`/api/players/${getUserId()}/game/${gameId}/createSecret/${secret}`, "POST")
      .then(this.props.onSecretSubmit);
  }

  onFormValueChange({ target }) {
    this.setState({
      [target.id]: target.value
    });
  }

  render() {
    return (
      <form className="secret-number-form" onSubmit={this.submitSecret}>
        <div>
          <label htmlFor="secret">Secret Number:</label>
          <input
            type="text"
            id="secret"
            value={this.state.secret}
            onChange={this.onFormValueChange}
            minLength="4"
            maxLength="4"
            required
          />
        </div>
        <button type="submit">Create Secret</button>
      </form>
    );
  }
}

SecretNumberForm.propTypes = {
  onSecretSubmit: func.isRequired,
  gameId: number.isRequired
};

export default SecretNumberForm;
