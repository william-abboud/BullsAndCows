import React from 'react';

function Login() {
  return (
    <div className="login-view">
      <h2>Login:</h2>

      <form>
        <div>
          <label htmlFor="username">Username:</label>
          <input type="text" name="username" />
        </div>

        <div>
          <label htmlFor="password">Password:</label>
          <input type="password" name="password" />
        </div>
      </form>
    </div>
  );
}

export default Login;
