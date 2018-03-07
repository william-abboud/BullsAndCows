import React from 'react';

function Ranking() {
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
          <tr>
            <th>1</th>
            <th>Emil Mladenov</th>
            <th>400</th>
          </tr>
          <tr>
            <th>2</th>
            <th>Emil Mladenov</th>
            <th>400</th>
          </tr>
          <tr>
            <th>3</th>
            <th>Emil Mladenov</th>
            <th>400</th>
          </tr>
          <tr>
            <th>4</th>
            <th>Emil Mladenov</th>
            <th>400</th>
          </tr>
        </tbody>
      </table>
    </div>
  );
}

export default Ranking;
