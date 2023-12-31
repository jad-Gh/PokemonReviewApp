﻿using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int id);
        bool ReviewerExists(int id);
        ICollection<Review> GetReviewsByReviewer(int reviewerId);

        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);

        bool Save();

    }
}
