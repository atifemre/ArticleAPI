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

namespace api.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/Articles/{ArticleId:int}/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly apiContext _context;

        public ReviewsController(apiContext context)
        {
            _context = context;
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
            var reviews = await _context.Reviews.FindAsync(id);

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
            if (id != reviews.Id)
            {
                return BadRequest();
            }

            //Remember Publish Date. there may be better approach, to be investaged later(like one time programmable)
            var old = await _context.Reviews.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            reviews.ArticlesId = old.ArticlesId;
            reviews.PublishDate = old.PublishDate;

            reviews.UpdateDate = DateTime.Now;
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

            return CreatedAtAction(nameof(GetReviews), new { Articleid = articleid, id = reviews.Id }, reviews);
        }

        // POST: api/Reviews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reviews>> PostReviews(long articleid, Reviews reviews)
        {
            reviews.ArticlesId = articleid;
            _context.Reviews.Add(reviews);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReviews), new { Articleid = articleid, id = reviews.Id }, reviews);
        }

        // DELETE: api/Reviews/5
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
