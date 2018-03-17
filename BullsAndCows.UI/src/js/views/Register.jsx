import React, { Component } from 'react';

class Register extends Component {
  constructor(props) {
    super(props);

    this.registerUser = this.registerUser.bind(this);
    this.prepareFormDataForSubmission = this.prepareFormDataForSubmission.bind(this);
    this.onChange = this.onChange.bind(this);

    this.state = {
      username: "",
      fullName: "",
      email: "",
      password: "",
      confirmPassword: ""
    };
  }

  onChange({ target }) {
    this.setState({
      [target.id]: target.value
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

  registerUser(e) {
    e.preventDefault();

    const data = this.prepareFormDataForSubmission();

    fetch('/api/account/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    })
    .then(() => console.log("Success !"))
    .catch(error => console.error(error));
  }

  render() {
    const { fullName, email, password, confirmPassword } = this.state;

    return (
      <div className="register-view">
        <h2>Register Account:</h2>

        <form onSubmit={this.registerUser}>
          <div>
            <label htmlFor="fullName">Full Name:</label>
            <input type="text" id="fullName" value={fullName} onChange={this.onChange} />
          </div>

          <div>
            <label htmlFor="email">Email:</label>
            <input type="email" id="email" value={email} onChange={this.onChange} />
          </div>

          <div>
            <label htmlFor="password">Password:</label>
            <input type="password" id="password" value={password} onChange={this.onChange} />
          </div>

           <div>
            <label htmlFor="confirmPassword">Confirm Password:</label>
            <input type="password" id="confirmPassword" value={confirmPassword} onChange={this.onChange} />
          </div>

          <button type="submit">Register</button>
        </form>
      </div>
    );
  }
}

export default Register;
