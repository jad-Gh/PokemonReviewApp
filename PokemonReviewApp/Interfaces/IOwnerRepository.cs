using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners();
        Owner GetOwner(int ownerId);
        bool OwnerExists(int ownerId);
        ICollection<Pokemon> GetOwnerPokemons(int ownerId);

        bool CreateOwner(Owner owner);

        bool Save();

    }
}
