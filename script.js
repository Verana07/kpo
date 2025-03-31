const choices = ["rock", "paper", "scissors"];
let playerScore = 0;
let computerScore = 0;

function computerPlay() {
  return choices[Math.floor(Math.random() * choices.length)];
}

function playRound(playerSelection, computerSelection) {
  if (playerSelection === computerSelection) {
    return "Döntetlen! 🤝";
  } else if (
    (playerSelection === "rock" && computerSelection === "scissors") ||
    (playerSelection === "paper" && computerSelection === "rock") ||
    (playerSelection === "scissors" && computerSelection === "paper")
  ) {
    playerScore++;
    return "Nyertél! 😎";
  } else {
    computerScore++;
    return "Vesztettél! 😢";
  }
}
function updateScore() {
  document.querySelector("#player-score").textContent = playerScore;
  document.querySelector("#computer-score").textContent = computerScore;
}

function game() {
  const buttons = document.querySelectorAll("button");
  let lastButton = null;
  const choices = {
    rock: "rock.png",
    paper: "paper.png",
    scissors: "scissors.png",
  };

  buttons.forEach((button) => {
    if (button.id === "") {
      return;
    }
    button.addEventListener("click", () => {
      if (lastButton !== null) {
        const prevComputerChoiceImg = document.querySelector(
          "#computer-choice img"
        );
        prevComputerChoiceImg.parentNode.removeChild(prevComputerChoiceImg);

        lastButton.classList.remove("last-choice");
      }
      button.classList.add("last-choice");
      lastButton = button;
      const playerSelection = button.id;
      const computerSelection = computerPlay();
      const computerChoiceImg = document.createElement("img");
      computerChoiceImg.src = choices[computerSelection];
      computerChoiceImg.width = 50;
      const result = playRound(playerSelection, computerSelection);
      document.querySelector("#computer-choice").appendChild(computerChoiceImg);
      document.querySelector("#result").textContent = result;
      updateScore();
    });
  });
}
game();
