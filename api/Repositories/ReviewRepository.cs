using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace api.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly apiContext _context;

        public ReviewRepository()
        {
            _context = new apiContext();
        }
        public ReviewRepository(apiContext context)
        {
            _context = context;
        }
        public Reviews NewReview(long articleId, Reviews newReview)
        {
            newReview.ArticlesId = articleId;
            newReview.PublishDate = DateTime.Now;
            _context.Reviews.Add(newReview);
            Save();
            _context.SaveChangesAsync();

            return newReview;
        }
        public IEnumerable<Reviews> GetReviews()
        {
            throw new NotImplementedException();
        }
        public Reviews GetReviewById(long id)
        {
            var review = _context.Reviews.FirstOrDefault(t => t.Id == id);

            if (review == null)
            {
                return null;
            }

            return review;
        }

        public Reviews UpdateReview(long reviewId, Reviews review)
        {
            if (reviewId != review.Id)
            {
                return null;
            }

            //Remember Publish Date. there may be better approach, to be investaged later(like one time programmable)
            var old =  _context.Reviews.AsNoTracking().FirstOrDefault(t => t.Id == reviewId);
            review.ArticlesId = old.ArticlesId;
            review.PublishDate = old.PublishDate;

            review.UpdateDate = DateTime.Now;
            _context.Entry(review).State = EntityState.Modified;

            try
            {
                Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewsExists(reviewId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return review;
        }

        public bool DeleteReview(long id)
        {
            var reviews = _context.Reviews.FirstOrDefault(t => t.Id == id);
            if (reviews == null)
            {
                return false;
            }

            _context.Reviews.Remove(reviews);
            Save();
            return true;
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
        private bool ReviewsExists(long id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }

    }
}
