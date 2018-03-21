import React from 'react';

function Rules() {
  return (
    <div className="rules-view view">
      <h2>Bulls and Cows Rules:</h2>

      <div className="rules-container">
        <p>
          In Bulls and Cows you play against the computer trying to guess
          a secret number it created while at the same time the computer
          tries to guess your secret number.
        </p>

        <p>
          A secret number is a 4 digit number where each digit is a unique non repeatable digit.

          <br />

          E.g <span className="example-secret">1234</span>

          <br />

          This is not a correct secret number:
          <span className="incorrect-secret-example">2234</span>
          because the number 2 is repeated
        </p>

        <p>
          Your guess result is expressed in terms of the number of bulls and cows
          you found. A bull is found when a digit in your guess is in
          the exact sample place in the secret number of your opponent secret.
          A cow is found when a digit in your guess exist in your opponent secret
          but it did not match the exact location of your guess.
        </p>

        <p>
          E.g Opponent secret: 1234 - Guess: 1256 (2 Bulls and 0 Cows)

          <br />

          E.g Opponent secret: 8974 - Guess: 9874 (2 Cows and 2 Bulls)
        </p>

        <p>
          The winner is the first to guess the opponent secret !
        </p>
      </div>
    </div>
  );
}

export default Rules;
