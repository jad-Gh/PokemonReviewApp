using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(String name);
        decimal GetPokemonRating(int pokeId);
        bool PokemonExists(int pokeId);

        bool CreatePokemon(Pokemon pokemon, int ownerId, int categoryId);
        bool UpdatePokemon(Pokemon pokemon, int ownerId, int categoryId);
        bool Save();
    }
}
