import React from 'react';

function Register() {
  return (
    <div className="register-view">
      <h2>Register Account:</h2>

      <form>
        <div>
          <label htmlFor="username">Username:</label>
          <input type="text" id="username" />
        </div>

        <div>
          <label htmlFor="fullName">Full Name:</label>
          <input type="text" id="fullName" />
        </div>

        <div>
          <label htmlFor="email">Email:</label>
          <input type="email" id="email" />
        </div>

        <div>
          <label htmlFor="password">Password:</label>
          <input type="password" id="password" />
        </div>

         <div>
          <label htmlFor="confirmPassword">Confirm Password:</label>
          <input type="password" id="confirmPassword" />
        </div>
      </form>
    </div>
  );
}

export default Register;
