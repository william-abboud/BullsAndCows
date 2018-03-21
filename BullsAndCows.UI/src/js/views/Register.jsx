import React, { Component } from 'react';
import { object } from 'prop-types';
import { request } from '../utils';

class Register extends Component {
  constructor(props) {
    super(props);

    this.registerUser = this.registerUser.bind(this);
    this.prepareFormDataForSubmission = this.prepareFormDataForSubmission.bind(this);
    this.onChange = this.onChange.bind(this);

    this.state = {
      fullName: "",
      email: "",
      password: "",
      confirmPassword: "",
      errors: [],
    };
  }

  onChange({ target }) {
    this.setState({
      [target.id]: target.value,
      errors: []
    });
  }

  prepareFormDataForSubmission(data = this.state) {
    return {
      FullName: data.fullName,
      Email: data.email,
      Password: data.password,
      ConfirmPassword: data.confirmPassword
    };
  }

  registerUser(event) {
    event.preventDefault();

    const data = this.prepareFormDataForSubmission();

    request("/api/account/register", "POST", data)
      .then((response) => {
        if (!response.ok) {
          throw new Error(response.status);
        }
      })
      .then(() => this.props.history.push("/login"))
      .catch(error => console.error(error));
  }

  render() {
    const { fullName, email, password, confirmPassword, errors } = this.state;

    return (
      <div className="register-view">
        <h2>Register Account:</h2>

        <form onSubmit={this.registerUser}>
          <div>
            <label htmlFor="fullName">Full Name:</label>
            <input
              type="text"
              id="fullName"
              value={fullName}
              onChange={this.onChange}
              minLength="2"
              required
            />
          </div>

          <div>
            <label htmlFor="email">Email:</label>
            <input
              type="email"
              id="email"
              value={email}
              onChange={this.onChange}
              required
            />
          </div>

          <div>
            <label htmlFor="password">Password:</label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={this.onChange}
              minLength="5"
              required
            />
          </div>

          <div>
            <label htmlFor="confirmPassword">Confirm Password:</label>
            <input
              type="password"
              id="confirmPassword"
              value={confirmPassword}
              onChange={this.onChange}
              pattern={password}
              minLength="5"
              required
            />
          </div>

          <button type="submit">Register</button>
        </form>

        {
          Array.isArray(errors)
            ? errors.map(error => <div className="error-message" key={error}>{error}</div>)
            : null
        }
      </div>
    );
  }
}

Register.propTypes = {
  history: object.isRequired,
};

export default Register;
