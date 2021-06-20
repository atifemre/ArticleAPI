using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddReviewController : ControllerBase
    {
        private readonly apiContext _context;

        public AddReviewController(apiContext context)
        {
            _context = context;
        }

        // GET: api/AddReview
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reviews>>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        // GET: api/AddReview/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reviews>> GetReviews(long id)
        {
            var reviews = await _context.Reviews.FindAsync(id);

            if (reviews == null)
            {
                return NotFound();
            }

            return reviews;
        }

        // PUT: api/AddReview/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReviews(long id, Reviews reviews)
        {
            if (id != reviews.Id)
            {
                return BadRequest();
            }

            _context.Entry(reviews).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReviewsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AddReview
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reviews>> PostReviews(Reviews reviews)
        {
            _context.Reviews.Add(reviews);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReviews", new { id = reviews.Id }, reviews);
        }

        // DELETE: api/AddReview/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviews(long id)
        {
            var reviews = await _context.Reviews.FindAsync(id);
            if (reviews == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(reviews);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewsExists(long id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
