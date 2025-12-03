# Quick Start Guide

## Prerequisites

1. Install .NET 6.0 SDK or higher from: https://dotnet.microsoft.com/download
2. Verify installation by running: `dotnet --version`

## Setup Steps

### 1. Download/Clone the Project
```bash
# If using git
git clone <repository-url>
cd PokemonTypeChecker

# Or extract the ZIP file and navigate to the folder
```

### 2. Restore Dependencies
```bash
cd PokemonTypeChecker
dotnet restore
```

### 3. Build the Project
```bash
dotnet build
```

### 4. Run the Application
```bash
dotnet run
```

## Testing the Application

Try these Pokémon names:
- `pikachu` - Electric type
- `charizard` - Fire/Flying type
- `bulbasaur` - Grass/Poison type
- `ditto` - Normal type
- `gengar` - Ghost/Poison type
- `mewtwo` - Psychic type

## Running Unit Tests

```bash
cd PokemonTypeChecker.Tests
dotnet test
```

## Common Issues

### "Pokemon not found" error
- Make sure you spell the Pokémon name correctly (all lowercase works best)
- The API contains Pokémon from all generations

### Network errors
- Ensure you have an active internet connection
- The app needs to connect to https://pokeapi.co

### Build errors
- Make sure you have .NET 6.0 SDK or higher installed
- Run `dotnet restore` to ensure all packages are downloaded

## Project Structure Explained

```
PokemonTypeChecker/
├── Models/           # Data classes for Pokemon and Types
├── Services/         # API communication logic
├── Helpers/          # Type effectiveness calculations
└── Program.cs        # Main application entry point
```

## How It Works

1. User enters a Pokémon name
2. App fetches Pokemon data from PokéAPI
3. App fetches type information for each of the Pokemon's types
4. Calculator determines strengths and weaknesses
5. Results are displayed in a formatted output
