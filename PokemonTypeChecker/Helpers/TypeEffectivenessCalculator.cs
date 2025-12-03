using PokemonTypeChecker.Models;

namespace PokemonTypeChecker.Helpers
{
    /// <summary>
    /// Helper class to calculate type effectiveness for Pokemon
    /// </summary>
    public class TypeEffectivenessCalculator
    {
        /// <summary>
        /// Calculates which types the given Pokemon types are strong against
        /// Strong = does double damage, takes no damage, or takes half damage
        /// </summary>
        public List<string> GetStrongAgainst(List<PokemonType> types)
        {
            var strongTypes = new HashSet<string>();

            foreach (var type in types)
            {
                // Offensive advantages: we do double damage to these types
                foreach (var target in type.DamageRelations.DoubleDamageTo)
                {
                    strongTypes.Add(target.Name);
                }

                // Defensive advantages: we take no damage from these types
                foreach (var source in type.DamageRelations.NoDamageFrom)
                {
                    strongTypes.Add(source.Name);
                }

                // Defensive advantages: we take half damage from these types
                foreach (var source in type.DamageRelations.HalfDamageFrom)
                {
                    strongTypes.Add(source.Name);
                }
            }

            return strongTypes.OrderBy(t => t).ToList();
        }

        /// <summary>
        /// Calculates which types the given Pokemon types are weak against
        /// Weak = does no damage, does half damage, or takes double damage
        /// </summary>
        public List<string> GetWeakAgainst(List<PokemonType> types)
        {
            var weakTypes = new HashSet<string>();

            foreach (var type in types)
            {
                // Offensive disadvantages: we do no damage to these types
                foreach (var target in type.DamageRelations.NoDamageTo)
                {
                    weakTypes.Add(target.Name);
                }

                // Offensive disadvantages: we do half damage to these types
                foreach (var target in type.DamageRelations.HalfDamageTo)
                {
                    weakTypes.Add(target.Name);
                }

                // Defensive disadvantages: we take double damage from these types
                foreach (var source in type.DamageRelations.DoubleDamageFrom)
                {
                    weakTypes.Add(source.Name);
                }
            }

            return weakTypes.OrderBy(t => t).ToList();
        }

        /// <summary>
        /// Formats a type name to be capitalized
        /// </summary>
        public string FormatTypeName(string typeName)
        {
            if (string.IsNullOrEmpty(typeName))
                return typeName;

            return char.ToUpper(typeName[0]) + typeName.Substring(1);
        }
    }
}
