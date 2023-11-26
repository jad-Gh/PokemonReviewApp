using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _dataContext;

        public ReviewRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool createReview(Review review)
        {
            _dataContext.Add(review);
            return Save();
        }

        public ICollection<Review> GetPokemonReviews(int pokeId)
        {
            return _dataContext.Reviews.Where(r=>r.Pokemon.Id==pokeId).ToList();
        }

        public Review GetReview(int id)
        {
            return _dataContext.Reviews.Where(r => r.Id == id).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _dataContext.Reviews.ToList();
        }

        public bool ReviewExists(int id)
        {
            return _dataContext.Reviews.Any(review => review.Id == id);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
