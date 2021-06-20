using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories
{
    public interface IReviewRepository
    {
        public IEnumerable<Reviews> GetReviews();
        public Reviews GetReviewById(long id);
        public Reviews NewReview(long articleId, Reviews newReview);
        public Reviews UpdateReview(long reviewId, Reviews updatedReview);
        public bool DeleteReview(long id);

    }
}
