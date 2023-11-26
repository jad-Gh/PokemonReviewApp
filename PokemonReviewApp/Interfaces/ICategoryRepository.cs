using PokemonReviewApp.Models;
using System.ComponentModel;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCatgeories();
        Category GetCategory(int id);

        ICollection<Pokemon> GetPokemonByCategory(int categoryId);

        bool CategoryExists(int id);

        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);

        bool Save();


    }
}
