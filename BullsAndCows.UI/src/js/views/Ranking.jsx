import React, { Component } from 'react';
import { string, number } from 'prop-types';

function PlayerTabularInfo({ rank, name, score }) {
  return (
    <tr>
      <th>{rank}</th>
      <th>{name}</th>
      <th>{score}</th>
    </tr>
  );
}

PlayerTabularInfo.propTypes = {
  rank: number.isRequired,
  name: string.isRequired,
  score: number.isRequired,
};

class Ranking extends Component {
  constructor(props) {
    super(props);

    this.state = {
      topPlayers: []
    };
  }

  componentDidMount() {
    fetch("/api/players/top")
      .then(response => response.json())
      .then(topPlayers => this.setState({ topPlayers }))
      .catch(error => console.error(error));
  }

  render() {
    const { topPlayers } = this.state;

    return (
      <div className="ranking-view">
        <h2>Top 25 players</h2>
        <table>
          <thead>
            <tr>
              <th>#</th>
              <th>Player</th>
              <th>Score</th>
            </tr>
          </thead>
          <tbody>
            { topPlayers.map((player, i) => (
              <PlayerTabularInfo
                key={player.Name + player.Score}
                name={player.Name}
                score={player.Score}
                rank={i + 1}
              />
            ))}
          </tbody>
        </table>
      </div>
    );
  }
}

export default Ranking;
