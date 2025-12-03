using PokemonTypeChecker.Helpers;
using PokemonTypeChecker.Models;
using Xunit;

namespace PokemonTypeChecker.Tests
{
    public class TypeEffectivenessCalculatorTests
    {
        private readonly TypeEffectivenessCalculator _calculator;

        public TypeEffectivenessCalculatorTests()
        {
            _calculator = new TypeEffectivenessCalculator();
        }

        [Fact]
        public void GetStrongAgainst_WithDoubleDamageTo_ReturnsCorrectTypes()
        {
            // Arrange - Create a mock electric type that does double damage to water and flying
            var electricType = new PokemonType
            {
                Name = "electric",
                DamageRelations = new TypeRelations
                {
                    DoubleDamageTo = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "water" },
                        new NamedApiResource { Name = "flying" }
                    }
                }
            };

            var types = new List<PokemonType> { electricType };

            // Act
            var result = _calculator.GetStrongAgainst(types);

            // Assert
            Assert.Contains("water", result);
            Assert.Contains("flying", result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetStrongAgainst_WithNoDamageFrom_ReturnsCorrectTypes()
        {
            // Arrange - Create a mock ghost type that takes no damage from normal and fighting
            var ghostType = new PokemonType
            {
                Name = "ghost",
                DamageRelations = new TypeRelations
                {
                    NoDamageFrom = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "normal" },
                        new NamedApiResource { Name = "fighting" }
                    }
                }
            };

            var types = new List<PokemonType> { ghostType };

            // Act
            var result = _calculator.GetStrongAgainst(types);

            // Assert
            Assert.Contains("normal", result);
            Assert.Contains("fighting", result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetStrongAgainst_WithHalfDamageFrom_ReturnsCorrectTypes()
        {
            // Arrange - Create a mock steel type that takes half damage from grass
            var steelType = new PokemonType
            {
                Name = "steel",
                DamageRelations = new TypeRelations
                {
                    HalfDamageFrom = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "grass" },
                        new NamedApiResource { Name = "ice" }
                    }
                }
            };

            var types = new List<PokemonType> { steelType };

            // Act
            var result = _calculator.GetStrongAgainst(types);

            // Assert
            Assert.Contains("grass", result);
            Assert.Contains("ice", result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetWeakAgainst_WithNoDamageTo_ReturnsCorrectTypes()
        {
            // Arrange - Create a mock normal type that does no damage to ghost
            var normalType = new PokemonType
            {
                Name = "normal",
                DamageRelations = new TypeRelations
                {
                    NoDamageTo = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "ghost" }
                    }
                }
            };

            var types = new List<PokemonType> { normalType };

            // Act
            var result = _calculator.GetWeakAgainst(types);

            // Assert
            Assert.Contains("ghost", result);
        }

        [Fact]
        public void GetWeakAgainst_WithHalfDamageTo_ReturnsCorrectTypes()
        {
            // Arrange - Create a mock fire type that does half damage to water
            var fireType = new PokemonType
            {
                Name = "fire",
                DamageRelations = new TypeRelations
                {
                    HalfDamageTo = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "water" },
                        new NamedApiResource { Name = "rock" }
                    }
                }
            };

            var types = new List<PokemonType> { fireType };

            // Act
            var result = _calculator.GetWeakAgainst(types);

            // Assert
            Assert.Contains("water", result);
            Assert.Contains("rock", result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetWeakAgainst_WithDoubleDamageFrom_ReturnsCorrectTypes()
        {
            // Arrange - Create a mock grass type that takes double damage from fire
            var grassType = new PokemonType
            {
                Name = "grass",
                DamageRelations = new TypeRelations
                {
                    DoubleDamageFrom = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "fire" },
                        new NamedApiResource { Name = "ice" }
                    }
                }
            };

            var types = new List<PokemonType> { grassType };

            // Act
            var result = _calculator.GetWeakAgainst(types);

            // Assert
            Assert.Contains("fire", result);
            Assert.Contains("ice", result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetStrongAgainst_WithMultipleTypes_CombinesResults()
        {
            // Arrange - Create mock types with overlapping strengths
            var type1 = new PokemonType
            {
                Name = "type1",
                DamageRelations = new TypeRelations
                {
                    DoubleDamageTo = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "water" }
                    }
                }
            };

            var type2 = new PokemonType
            {
                Name = "type2",
                DamageRelations = new TypeRelations
                {
                    DoubleDamageTo = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "grass" }
                    }
                }
            };

            var types = new List<PokemonType> { type1, type2 };

            // Act
            var result = _calculator.GetStrongAgainst(types);

            // Assert
            Assert.Contains("water", result);
            Assert.Contains("grass", result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void GetStrongAgainst_WithDuplicates_ReturnsUniqueTypes()
        {
            // Arrange - Create types with duplicate strong types
            var type1 = new PokemonType
            {
                Name = "type1",
                DamageRelations = new TypeRelations
                {
                    DoubleDamageTo = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "water" }
                    }
                }
            };

            var type2 = new PokemonType
            {
                Name = "type2",
                DamageRelations = new TypeRelations
                {
                    DoubleDamageTo = new List<NamedApiResource>
                    {
                        new NamedApiResource { Name = "water" }
                    }
                }
            };

            var types = new List<PokemonType> { type1, type2 };

            // Act
            var result = _calculator.GetStrongAgainst(types);

            // Assert
            Assert.Single(result);
            Assert.Contains("water", result);
        }

        [Fact]
        public void FormatTypeName_WithLowercaseInput_ReturnsCapitalized()
        {
            // Arrange
            var input = "electric";

            // Act
            var result = _calculator.FormatTypeName(input);

            // Assert
            Assert.Equal("Electric", result);
        }

        [Fact]
        public void FormatTypeName_WithEmptyString_ReturnsEmptyString()
        {
            // Arrange
            var input = "";

            // Act
            var result = _calculator.FormatTypeName(input);

            // Assert
            Assert.Equal("", result);
        }

        [Fact]
        public void GetStrongAgainst_WithEmptyTypeList_ReturnsEmptyList()
        {
            // Arrange
            var types = new List<PokemonType>();

            // Act
            var result = _calculator.GetStrongAgainst(types);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetWeakAgainst_WithEmptyTypeList_ReturnsEmptyList()
        {
            // Arrange
            var types = new List<PokemonType>();

            // Act
            var result = _calculator.GetWeakAgainst(types);

            // Assert
            Assert.Empty(result);
        }
    }
}
