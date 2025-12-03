# Pokémon Type Checker

A console application that determines a Pokémon's type effectiveness against other Pokémon types using the PokéAPI.

## What This App Does

Enter a Pokémon name, and the app will show you:
- What types your Pokémon is **strong against** (good offensive and defensive matchups)
- What types your Pokémon is **weak against** (bad offensive and defensive matchups)

## Requirements

- .NET 6.0 SDK or higher
- Internet connection (to access PokéAPI)

## How to Run

1. **Clone or download this repository**

2. **Navigate to the project directory**
   ```bash
   cd PokemonTypeChecker/PokemonTypeChecker
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

## How to Use

1. When prompted, enter a Pokémon name (e.g., `pikachu`, `charizard`, `ditto`)
2. The app will display the Pokémon's type(s) and effectiveness analysis
3. Type `exit` or `quit` to close the application

## Example Usage

```
===========================================
   Pokémon Type Effectiveness Checker
===========================================

Enter a Pokémon name (or 'exit' to quit): pikachu

Fetching data for 'pikachu'...

===========================================
Pokémon: Pikachu
Type(s): Electric
===========================================

STRONG AGAINST (Offensive & Defensive Advantages):
  - Flying
  - Water
  - Steel

WEAK AGAINST (Offensive & Defensive Disadvantages):
  - Ground
  - Grass
  - Electric
  - Dragon

===========================================
```

## Running Tests

To run the unit tests:

```bash
cd PokemonTypeChecker.Tests
dotnet test
```

## Project Structure

- **Program.cs** - Main entry point and user interface
- **Models/** - Data models for Pokémon and Type information
- **Services/** - API service for communicating with PokéAPI
- **Helpers/** - Logic for calculating type effectiveness
- **Tests/** - Unit tests for the calculator

## How Type Effectiveness Works

A Pokémon type is **STRONG** against another type if:
- It does double damage to that type
- It takes no damage from that type
- It takes half damage from that type

A Pokémon type is **WEAK** against another type if:
- It does no damage to that type
- It does half damage to that type
- It takes double damage from that type

## Technologies Used

- C# / .NET 6+
- System.Net.Http for API calls
- System.Text.Json for JSON parsing
- xUnit for unit testing

## API Reference

This application uses [PokéAPI](https://pokeapi.co/) - a free RESTful Pokémon API.
