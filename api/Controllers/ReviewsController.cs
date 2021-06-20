using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;
using Microsoft.AspNet.OData;
using api.Repositories;

namespace api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Articles/{ArticleId:int}/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly apiContext _context;
        private readonly ReviewRepository _reviewRepo;

        public ReviewsController(apiContext context)
        {
            _context = context;
            _reviewRepo = new ReviewRepository(_context);
        }

        // GET: api/Reviews
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        [EnableQuery]
        [ActionName(nameof(GetReviews))]

        public async Task<ActionResult<Reviews>> GetReviews(long id)
        {
            var reviews = _reviewRepo.GetReviewById(id);

            if (reviews == null)
            {
                return NotFound();
            }

            return reviews;
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviews(long articleid, long id, Reviews reviews)
        {
            var newReview = _reviewRepo.UpdateReview(articleid,reviews);

            if (newReview == null)
            {
                return  NotFound();
            }

            return CreatedAtAction(nameof(GetReviews), new { Articleid = articleid, id = newReview.Id }, newReview);
        }

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reviews>> PostReviews(long articleid, Reviews review)
        {
            var newReview = _reviewRepo.NewReview(articleid,review);            

            return CreatedAtAction(nameof(GetReviews), new { Articleid = articleid, id = newReview.Id }, newReview);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviews(long id)
        {
            bool isDeleted = _reviewRepo.DeleteReview(id);
            if (isDeleted == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
