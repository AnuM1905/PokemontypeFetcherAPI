using System.Text.Json;
using PokemonTypeChecker.Models;

namespace PokemonTypeChecker.Services
{
    /// <summary>
    /// Service for interacting with the PokeAPI
    /// </summary>
    public class PokeApiService : IPokeApiService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://pokeapi.co/api/v2/";

        public PokeApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }

        /// <summary>
        /// Gets a Pokemon by name from the API
        /// </summary>
        public async Task<Pokemon?> GetPokemonAsync(string pokemonName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"pokemon/{pokemonName.ToLower()}");
                
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var pokemon = JsonSerializer.Deserialize<Pokemon>(content);
                
                return pokemon;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing response: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets type information including damage relations
        /// </summary>
        public async Task<PokemonType?> GetTypeAsync(string typeName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"type/{typeName.ToLower()}");
                
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var pokemonType = JsonSerializer.Deserialize<PokemonType>(content);
                
                return pokemonType;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Error parsing response: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return null;
            }
        }
    }
}
