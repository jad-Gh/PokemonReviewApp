using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int id);

        bool ReviewExists(int id);

        ICollection<Review> GetPokemonReviews(int pokeId);

        bool createReview(Review review);

        bool Save();
    }
}
