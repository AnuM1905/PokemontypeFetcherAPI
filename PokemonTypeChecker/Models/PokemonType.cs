using System.Text.Json.Serialization;

namespace PokemonTypeChecker.Models
{
    /// <summary>
    /// Represents a Pokemon type with its damage relations
    /// </summary>
    public class PokemonType
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("damage_relations")]
        public TypeRelations DamageRelations { get; set; } = new TypeRelations();
    }
}
