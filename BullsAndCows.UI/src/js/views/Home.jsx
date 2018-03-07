import React from 'react';
import { Link } from 'react-router-dom';
import logo from '../../assets/images/bull.png';

function Home() {
  return (
    <div className="home-view">
      <img src={logo} alt="Cows and Bulls Logo" />
      <h2>Cows and Bulls</h2>
      <Link to="/game">Play</Link>
    </div>
  );
}

export default Home;
