import React from 'react';

function Player() {
  return (
    <div className="human-player">
      <h3>William</h3>

      <form>
        <div>
          <label htmlFor="secretNumber">Secret Number:</label>
          <input type="text" id="secretNumber" />
        </div>

        <div>
          <label htmlFor="guess">Guess:</label>
          <input type="text" id="guess" />
        </div>

        <button>Guess</button>
      </form>

      <Results />
      <Results />
      <Results />
      <Results />
      <Results />
      <Results />
      <Results />
    </div>
  );
}

function OpponentPlayer() {
  return (
    <div className="opponent-player">
      <h3>Computer player</h3>
    </div>
  );
}

function Results() {
  return (
    <div className="results">
      <p>Guess: 1234</p>
      <p>1 cow and 2 bulls</p>
    </div>
  );
}

function Game() {
  return (
    <div className="game-view">
      <h2>Turn: 8</h2>

      <div className="player-game-statistics">
        <Player />
        <OpponentPlayer />
      </div>
    </div>
  );
}

export default Game;
