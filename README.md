# Unity Tic Tac Toe Multiplayer

## Overview

This project is a simple Tic Tac Toe game implemented in Unity with a focus on online Player versus Player (PvP) multiplayer functionality using a basic Reliable UDP (RUDP) library. It is designed as a study project to explore the implementation of a dedicated server for online gaming.

*Please note that this project is not intended for production use, and certain concepts may have been implemented in a naive or simplified manner.*

## Features

- **Tic Tac Toe Gameplay:** Classic Tic Tac Toe game mechanics with a simple and intuitive user interface.

- **Online PvP Multiplayer:** Engage in multiplayer matches with friends or other players online.

- **Dedicated Server:** The project includes a basic dedicated server written in .NET Core to facilitate online gameplay.

## Dependencies

This project relies on the following external libraries:

- **LiteNetLibe**
- **VContainer**
- **UniTask**
- **MessagePipe**
- **TriInspector**
- **ParrelSync**
- **FluentAssertions**
- **DOTween**

## Usage

By default, the game is set up to work on localhost. However, you can build and host it on any location of your choice. To get started:

1. Clone the repository to your local machine.
2. Open the Unity project in the `TTT.Client` folder.
3. Build the project for your desired platform.
4. Host the server on a machine accessible to players using the server code in the `TTT.Server` folder.

## Project Structure

- **TTT.Client:** Unity project containing scripts, assets, and other resources for the client-side implementation.

- **TTT.Server:** Contains the server code, written in .NET Core, responsible for managing game sessions.

- **TTT.Shared:** A shared library containing shared data, including message types and helper functions common to both the client and server.

## Important Note

This project is a learning endeavor, and certain aspects may not adhere to best practices for production environments. Feel free to explore the code, experiment, and improve upon it for your own learning purposes.

## Contributions

Contributions are welcome! If you find issues or have suggestions for enhancements, please submit a pull request or open an issue.

## License

This project is licensed under the [MIT License](LICENSE), granting you the freedom to use, modify, and distribute the code.

---

*Enjoy exploring the world of online multiplayer gaming with this Unity Tic Tac Toe project!*
