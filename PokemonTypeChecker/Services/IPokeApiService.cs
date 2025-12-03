using PokemonTypeChecker.Models;

namespace PokemonTypeChecker.Services
{
    /// <summary>
    /// Interface for interacting with the PokeAPI
    /// </summary>
    public interface IPokeApiService
    {
        /// <summary>
        /// Gets a Pokemon by name from the API
        /// </summary>
        /// <param name="pokemonName">The name of the Pokemon</param>
        /// <returns>The Pokemon object, or null if not found</returns>
        Task<Pokemon?> GetPokemonAsync(string pokemonName);

        /// <summary>
        /// Gets type information including damage relations
        /// </summary>
        /// <param name="typeName">The name of the type</param>
        /// <returns>The PokemonType object, or null if not found</returns>
        Task<PokemonType?> GetTypeAsync(string typeName);
    }
}
