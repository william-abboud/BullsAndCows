import React from 'react';
import { Link } from 'react-router-dom';
import utils from '../utils';

function SiteHeader() {
  return (
    <header className="site-header">
      <nav className="main-nav">
        <ul>
          <li><Link to="/">Home</Link></li>
          <li><Link to="/rules">Rules</Link></li>
          <li><Link to="/ranking">Top Players</Link></li>
        </ul>
      </nav>

      {
        utils.isLoggedIn()
          ?
            <div>{`Welcome ${utils.getUser()} !`}</div>
          :
            <nav className="login-register-nav">
              <ul>
                <li><Link to="/login">Login</Link></li>
                <li><Link to="/register">Register</Link></li>
              </ul>
            </nav>
      }
    </header>
  );
}

export default SiteHeader;
