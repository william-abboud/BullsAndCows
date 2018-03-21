import React from 'react';
import { Link } from 'react-router-dom';
import logo from '../../assets/images/bull.png';

function Home() {
  return (
    <div className="home-view">
      <h1 className="game-title">Cows and Bulls</h1>
      <img src={logo} alt="Cows and Bulls Logo" className="bull-logo" />
      <Link to={{ pathname: '/game', search: '?newGame=true' }} className="start-game-trigger">
          Start new Game
      </Link>
      <Link to={{ pathname: '/game', search: '?newGame=false' }} className="continue-game-trigger">
          Continue
      </Link>
    </div>
  );
}

export default Home;
