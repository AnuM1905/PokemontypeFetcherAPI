using System.Text.Json.Serialization;

namespace PokemonTypeChecker.Models
{
    /// <summary>
    /// Represents the damage relations for a Pokemon type
    /// </summary>
    public class TypeRelations
    {
        /// <summary>
        /// Types that this type does double damage to
        /// </summary>
        [JsonPropertyName("double_damage_to")]
        public List<NamedApiResource> DoubleDamageTo { get; set; } = new List<NamedApiResource>();

        /// <summary>
        /// Types that this type does half damage to
        /// </summary>
        [JsonPropertyName("half_damage_to")]
        public List<NamedApiResource> HalfDamageTo { get; set; } = new List<NamedApiResource>();

        /// <summary>
        /// Types that this type does no damage to
        /// </summary>
        [JsonPropertyName("no_damage_to")]
        public List<NamedApiResource> NoDamageTo { get; set; } = new List<NamedApiResource>();

        /// <summary>
        /// Types that do double damage to this type
        /// </summary>
        [JsonPropertyName("double_damage_from")]
        public List<NamedApiResource> DoubleDamageFrom { get; set; } = new List<NamedApiResource>();

        /// <summary>
        /// Types that do half damage to this type
        /// </summary>
        [JsonPropertyName("half_damage_from")]
        public List<NamedApiResource> HalfDamageFrom { get; set; } = new List<NamedApiResource>();

        /// <summary>
        /// Types that do no damage to this type
        /// </summary>
        [JsonPropertyName("no_damage_from")]
        public List<NamedApiResource> NoDamageFrom { get; set; } = new List<NamedApiResource>();
    }
}
