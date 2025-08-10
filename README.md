# MazeGame

MazeGame is a simple Windows Forms application written in C# that lets you generate and solve mazes interactively. The game provides randomly generated mazes of varying sizes and allows you to navigate from a starting point to the exit using your keyboard.

## Features

- **Random Maze Generation:** Each game creates a new maze using the depth-first search algorithm.
- **Multiple Sizes:** Choose from different maze sizes for varying difficulty.
- **Keyboard Navigation:** Use arrow keys to move your player through the maze.
- **Game Progress:** Reach the red square to win!

## Getting Started

### Prerequisites

- .NET Framework 4.7.2 or later
- Windows OS

### Setup

1. Clone this repository:
   ```bash
   git clone https://github.com/serkankahveci/MazeGame.git
   ```
2. Open the project in Visual Studio.
3. Build and run the solution.

## How to Play

1. Choose the maze size from the dropdown menu.
2. Click "New Game" to generate a new maze.
3. Use the **arrow keys** to move your player (blue square) through the maze.
4. Reach the **red square** (exit) to win the game.

## Project Structure

- `MazeGame/Program.cs` – Application entry point.
- `MazeGame/MainForm.cs` – Main game logic, maze generation, rendering, and input handling.
- `MazeGame/Form1.cs` – (Template form, not used for main gameplay.)

## License

This project is licensed under the MIT License - see the LICENSE file for details.
