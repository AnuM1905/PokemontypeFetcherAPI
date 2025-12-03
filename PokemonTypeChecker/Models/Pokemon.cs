using System.Text.Json.Serialization;

namespace PokemonTypeChecker.Models
{
    /// <summary>
    /// Represents a Pokemon from the PokeAPI
    /// </summary>
    public class Pokemon
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("types")]
        public List<PokemonTypeSlot> Types { get; set; } = new List<PokemonTypeSlot>();
    }

    /// <summary>
    /// Represents a type slot in a Pokemon's type list
    /// </summary>
    public class PokemonTypeSlot
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public NamedApiResource Type { get; set; } = new NamedApiResource();
    }

    /// <summary>
    /// Represents a named resource from the API (like a type or ability)
    /// </summary>
    public class NamedApiResource
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
