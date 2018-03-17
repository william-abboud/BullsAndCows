import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import logo from '../../assets/images/bull.png';
import utils from '../utils';

class Home extends Component {
  constructor(props) {
    super(props);

    this.startGame = this.startGame.bind(this);
  }

  startGame(e) {
    e.preventDefault();
    const { history } = this.props;

    if (!utils.isLoggedIn()) {
      history.push('/login');
    }

    utils.authorizedRequest("/api/game/startGameAgainstComputer")
      .then(response => response.json())
      .then(({ GameId }) => history.push({
        pathname: "/game",
        search: `?gameId=${GameId}`,
      }))
      .catch(err => console.error(err));
  }

  render() {
    return (
      <div className="home-view">
        <img src={logo} alt="Cows and Bulls Logo" />
        <h2>Cows and Bulls</h2>
        <Link to="/game" onClick={this.startGame}>Play</Link>
      </div>
    );
  }
}

export default Home;
