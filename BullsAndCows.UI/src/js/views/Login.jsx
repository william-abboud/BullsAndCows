import React, { Component } from 'react';
import { Redirect } from 'react-router';
import utils from '../utils';

class Login extends Component {
  constructor(props) {
    super(props);

    this.login = this.login.bind(this);
    this.onFormValueChange = this.onFormValueChange.bind(this);

    this.state = {
      email: '',
      password: '',
      loggedIn: false,
    };
  }

  componentDidMount() {
    this.setState({ loggedIn: utils.isLoggedIn() });
  }

  login(e) {
    e.preventDefault();
    const { email, password } = this.state;

    fetch("/token", {
      method: "POST",
      Headers: {
        "Content-Type": "application/x-www-form-urlencoded"
      },
      body: `grant_type=password&username=${email}&password=${password}`
    })
    .then(response => response.json())
    .then(res => {
      utils.setAccessToken(res["access_token"]);
      window.localStorage.setItem("user", res.FullName);
    })
    .then(() => this.setState({ loggedIn: true }))
    .catch(error => console.error(error));
  }

  onFormValueChange({ target }) {
    this.setState({
      [target.id]: target.value
    });
  }

  render() {
    if (this.state.loggedIn) {
      return <Redirect to="/" />;
    }

    return (
      <div className="login-view">
        <h2>Login:</h2>

        <form onSubmit={this.login}>
          <div>
            <label htmlFor="email">Email:</label>
            <input type="email" id="email" onChange={this.onFormValueChange} />
          </div>

          <div>
            <label htmlFor="password">Password:</label>
            <input type="password" id="password" onChange={this.onFormValueChange} />
          </div>

          <button type="submit">Submit</button>
        </form>
      </div>
    );
  }
}

export default Login;
