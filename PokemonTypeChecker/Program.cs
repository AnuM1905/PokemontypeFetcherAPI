using Microsoft.Extensions.DependencyInjection;
using PokemonTypeChecker.Helpers;
using PokemonTypeChecker.Models;
using PokemonTypeChecker.Services;

namespace PokemonTypeChecker
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Setup dependency injection
            var serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .AddSingleton<IPokeApiService, PokeApiService>()
                .AddSingleton<TypeEffectivenessCalculator>()
                .BuildServiceProvider();

            var pokeApiService = serviceProvider.GetRequiredService<IPokeApiService>();
            var calculator = serviceProvider.GetRequiredService<TypeEffectivenessCalculator>();

            // Display welcome message
            DisplayWelcome();

            // Main loop
            while (true)
            {
                Console.Write("\nEnter a Pokémon name (or 'exit' to quit): ");
                var input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a valid Pokémon name.");
                    continue;
                }

                if (input == "exit" || input == "quit")
                {
                    Console.WriteLine("\nThank you for using Pokémon Type Checker! Goodbye!");
                    break;
                }

                await ProcessPokemon(input, pokeApiService, calculator);
            }
        }

        /// <summary>
        /// Displays the welcome message
        /// </summary>
        static void DisplayWelcome()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("   Pokémon Type Effectiveness Checker");
            Console.WriteLine("===========================================");
        }

        /// <summary>
        /// Processes a Pokemon lookup and displays results
        /// </summary>
        static async Task ProcessPokemon(string pokemonName, IPokeApiService pokeApiService, TypeEffectivenessCalculator calculator)
        {
            Console.WriteLine($"\nFetching data for '{pokemonName}'...");

            // Get Pokemon data
            var pokemon = await pokeApiService.GetPokemonAsync(pokemonName);

            if (pokemon == null)
            {
                Console.WriteLine($"\n❌ Pokemon '{pokemonName}' not found. Please check the spelling and try again.");
                return;
            }

            // Get type information for each of the Pokemon's types
            var pokemonTypes = new List<PokemonType>();
            
            foreach (var typeSlot in pokemon.Types)
            {
                var typeInfo = await pokeApiService.GetTypeAsync(typeSlot.Type.Name);
                if (typeInfo != null)
                {
                    pokemonTypes.Add(typeInfo);
                }
            }

            if (pokemonTypes.Count == 0)
            {
                Console.WriteLine("\n❌ Could not retrieve type information for this Pokémon.");
                return;
            }

            // Calculate effectiveness
            var strongAgainst = calculator.GetStrongAgainst(pokemonTypes);
            var weakAgainst = calculator.GetWeakAgainst(pokemonTypes);

            // Display results
            DisplayResults(pokemon, pokemonTypes, strongAgainst, weakAgainst, calculator);
        }

        /// <summary>
        /// Displays the results in a formatted manner
        /// </summary>
        static void DisplayResults(
            Pokemon pokemon, 
            List<PokemonType> types, 
            List<string> strongAgainst, 
            List<string> weakAgainst,
            TypeEffectivenessCalculator calculator)
        {
            Console.WriteLine("\n===========================================");
            Console.WriteLine($"Pokémon: {calculator.FormatTypeName(pokemon.Name)}");
            
            var typeNames = string.Join(", ", types.Select(t => calculator.FormatTypeName(t.Name)));
            Console.WriteLine($"Type(s): {typeNames}");
            Console.WriteLine("===========================================");

            // Display strengths
            Console.WriteLine("\n✓ STRONG AGAINST (Offensive & Defensive Advantages):");
            if (strongAgainst.Count > 0)
            {
                foreach (var type in strongAgainst)
                {
                    Console.WriteLine($"  - {calculator.FormatTypeName(type)}");
                }
            }
            else
            {
                Console.WriteLine("  - None");
            }

            // Display weaknesses
            Console.WriteLine("\n✗ WEAK AGAINST (Offensive & Defensive Disadvantages):");
            if (weakAgainst.Count > 0)
            {
                foreach (var type in weakAgainst)
                {
                    Console.WriteLine($"  - {calculator.FormatTypeName(type)}");
                }
            }
            else
            {
                Console.WriteLine("  - None");
            }

            Console.WriteLine("\n===========================================");
        }
    }
}
