# Application Architecture & Flow

## High-Level Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                         USER                                 │
│                  (Console Interface)                         │
└─────────────────────┬───────────────────────────────────────┘
                      │
                      │ Enters Pokemon Name
                      ▼
┌─────────────────────────────────────────────────────────────┐
│                      Program.cs                              │
│                   (Entry Point + UI)                         │
│  • Dependency Injection Setup                                │
│  • User Input Handling                                       │
│  • Output Formatting                                         │
└─────────────┬───────────────────────┬───────────────────────┘
              │                       │
              │                       │
              ▼                       ▼
┌─────────────────────────┐  ┌──────────────────────────────┐
│   IPokeApiService       │  │ TypeEffectivenessCalculator  │
│   (Service Interface)   │  │      (Helper Class)          │
└─────────────┬───────────┘  └──────────────┬───────────────┘
              │                              │
              │ Implemented by               │ Uses
              ▼                              │
┌─────────────────────────┐                 │
│    PokeApiService       │                 │
│  (API Communication)    │                 │
│  • HttpClient           │                 │
│  • JSON Parsing         │                 │
│  • Error Handling       │                 │
└─────────────┬───────────┘                 │
              │                             │
              │ Uses                        │
              ▼                             ▼
┌─────────────────────────────────────────────────────────────┐
│                       MODELS                                 │
│  • Pokemon.cs          - Pokemon entity                      │
│  • PokemonType.cs      - Type entity                         │
│  • TypeRelations.cs    - Damage relations                    │
└─────────────┬───────────────────────────────────────────────┘
              │
              │ HTTP GET Requests
              ▼
┌─────────────────────────────────────────────────────────────┐
│                   PokéAPI (External)                         │
│  • https://pokeapi.co/api/v2/pokemon/{name}                  │
│  • https://pokeapi.co/api/v2/type/{name}                     │
└─────────────────────────────────────────────────────────────┘
```

## Application Flow

### Step-by-Step Execution

```
1. APPLICATION START
   │
   ├─> Setup Dependency Injection Container
   │   ├─> Register HttpClient
   │   ├─> Register IPokeApiService -> PokeApiService
   │   └─> Register TypeEffectivenessCalculator
   │
   └─> Display Welcome Message

2. USER INPUT LOOP
   │
   ├─> Prompt user for Pokemon name
   │
   ├─> Validate input
   │   ├─> If "exit" -> Terminate program
   │   └─> If valid -> Continue
   │
   └─> Call ProcessPokemon()

3. FETCH POKEMON DATA
   │
   ├─> Call PokeApiService.GetPokemonAsync(name)
   │   │
   │   ├─> HTTP GET to /api/v2/pokemon/{name}
   │   │
   │   ├─> Parse JSON response
   │   │
   │   └─> Return Pokemon object (or null if error)
   │
   └─> Handle errors with user-friendly messages

4. FETCH TYPE DATA
   │
   ├─> For each type the Pokemon has:
   │   │
   │   ├─> Call PokeApiService.GetTypeAsync(typeName)
   │   │   │
   │   │   ├─> HTTP GET to /api/v2/type/{name}
   │   │   │
   │   │   ├─> Parse JSON response
   │   │   │
   │   │   └─> Return PokemonType object
   │   │
   │   └─> Add to types list
   │
   └─> Build complete type information

5. CALCULATE EFFECTIVENESS
   │
   ├─> Call TypeEffectivenessCalculator.GetStrongAgainst(types)
   │   │
   │   ├─> Analyze DoubleDamageTo (offensive advantage)
   │   ├─> Analyze NoDamageFrom (defensive advantage)
   │   ├─> Analyze HalfDamageFrom (defensive advantage)
   │   │
   │   └─> Return unique list of type names
   │
   └─> Call TypeEffectivenessCalculator.GetWeakAgainst(types)
       │
       ├─> Analyze NoDamageTo (offensive disadvantage)
       ├─> Analyze HalfDamageTo (offensive disadvantage)
       ├─> Analyze DoubleDamageFrom (defensive disadvantage)
       │
       └─> Return unique list of type names

6. DISPLAY RESULTS
   │
   ├─> Format Pokemon name and types
   │
   ├─> Display "STRONG AGAINST" list
   │
   ├─> Display "WEAK AGAINST" list
   │
   └─> Return to Step 2 (Input Loop)
```

## Data Flow Example: Pikachu

```
User Input: "pikachu"
    │
    ▼
GET /api/v2/pokemon/pikachu
    │
    ▼
Response: 
{
  "name": "pikachu",
  "types": [
    {
      "slot": 1,
      "type": {
        "name": "electric"
      }
    }
  ]
}
    │
    ▼
GET /api/v2/type/electric
    │
    ▼
Response:
{
  "name": "electric",
  "damage_relations": {
    "double_damage_to": ["water", "flying"],
    "half_damage_to": ["grass", "electric", "dragon"],
    "no_damage_to": [],
    "double_damage_from": ["ground"],
    "half_damage_from": ["flying", "steel", "electric"],
    "no_damage_from": []
  }
}
    │
    ▼
Calculate:
Strong Against = ["water", "flying", "steel", "electric"]
Weak Against = ["ground", "grass", "electric", "dragon"]
    │
    ▼
Display formatted results
```

## Class Relationships

```
┌──────────────────┐
│   Program.cs     │
│   (Main Class)   │
└────────┬─────────┘
         │ uses
         │
    ┌────▼─────────────────┐
    │                      │
┌───▼──────────┐  ┌────────▼─────────────┐
│ IPokeApiSvc  │  │  TypeEffectiveness   │
│ (Interface)  │  │    Calculator        │
└───┬──────────┘  └──────────────────────┘
    │                      │
    │ implemented by       │ uses
    │                      │
┌───▼──────────┐  ┌────────▼─────────────┐
│ PokeApiSvc   │  │      Models:         │
│ (Class)      │  │  • Pokemon           │
└───┬──────────┘  │  • PokemonType       │
    │             │  • TypeRelations     │
    │ uses        │  • NamedApiResource  │
    └─────────────▼──────────────────────┘
```

## Dependency Injection Container

```
ServiceCollection
    │
    ├─> AddHttpClient()
    │   └─> Provides HttpClient for API calls
    │
    ├─> AddSingleton<IPokeApiService, PokeApiService>()
    │   └─> Service for API communication
    │
    └─> AddSingleton<TypeEffectivenessCalculator>()
        └─> Helper for calculations

ServiceProvider
    │
    └─> Resolves dependencies at runtime
```

## Error Handling Strategy

```
┌─────────────────────┐
│   User Action       │
└──────────┬──────────┘
           │
     ┌─────▼─────┐
     │  Try API  │
     │   Call    │
     └─────┬─────┘
           │
    ┌──────▼──────────┐
    │  Success?       │
    └──┬──────────┬───┘
       │ Yes      │ No
       │          │
       ▼          ▼
┌──────────┐  ┌────────────────┐
│ Process  │  │ Catch Exception│
│  Data    │  └────────┬───────┘
└──────────┘           │
                       ├─> HttpRequestException
                       │   → "Network error"
                       │
                       ├─> JsonException
                       │   → "Error parsing response"
                       │
                       └─> Exception
                           → "Unexpected error"
                       │
                       ▼
              ┌────────────────────┐
              │ Display friendly   │
              │ error message      │
              └────────────────────┘
```

## Type Effectiveness Logic

```
For each Pokemon type, gather:

OFFENSIVE CAPABILITIES:
├─> double_damage_to    → Strong offensive matchup
├─> half_damage_to      → Weak offensive matchup
└─> no_damage_to        → Weak offensive matchup

DEFENSIVE CAPABILITIES:
├─> double_damage_from  → Weak defensive matchup
├─> half_damage_from    → Strong defensive matchup
└─> no_damage_from      → Strong defensive matchup

Combine all types:
├─> STRONG AGAINST = double_damage_to ∪ half_damage_from ∪ no_damage_from
└─> WEAK AGAINST = no_damage_to ∪ half_damage_to ∪ double_damage_from

Note: Using Set (HashSet) to eliminate duplicates
```

## Testing Structure

```
TypeEffectivenessCalculatorTests
│
├─> Test GetStrongAgainst()
│   ├─> With DoubleDamageTo
│   ├─> With NoDamageFrom
│   ├─> With HalfDamageFrom
│   ├─> With Multiple Types
│   ├─> With Duplicates
│   └─> With Empty List
│
├─> Test GetWeakAgainst()
│   ├─> With NoDamageTo
│   ├─> With HalfDamageTo
│   ├─> With DoubleDamageFrom
│   └─> With Empty List
│
└─> Test FormatTypeName()
    ├─> With Lowercase
    └─> With Empty String
```

This architecture ensures:
- **Separation of Concerns**: Each class has a single responsibility
- **Testability**: Interfaces enable easy mocking
- **Maintainability**: Clear structure and documentation
- **Extensibility**: Easy to add new features
- **Error Resilience**: Comprehensive error handling
