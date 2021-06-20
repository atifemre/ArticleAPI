using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories
{
    interface IReviewRepository
    {
        IEnumerable<Reviews> GetReviews();
        Reviews GetReviewById(int id);
        Reviews NewReview(Reviews employee);
        Reviews UpdateReview(Reviews employee);
        void DeleteReview(int id);
        void Save();
    }
}
