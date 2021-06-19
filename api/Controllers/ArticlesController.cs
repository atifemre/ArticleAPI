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
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly apiContext _context;

        public ArticlesController(apiContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Articles>>> GetArticles()
        {
            var articles = await _context.Articles.ToListAsync();

            if (articles == null)
            {
                return NotFound();
            }

            return await _context.Articles.Include(t => t.Reviews).ToListAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Articles>> GetArticles(long id)
        {
            var article = await _context.Articles.Include(t => t.Reviews).FirstOrDefaultAsync(t => t.Id == id);

            if (article == null)
            {
                return NotFound();
            }
            else {
                //article.Reviews = _context.Reviews.Where(b => b.ArticlesId == id)
                //                       .OrderBy(b => b.Reviewer).ToList();
            }

            return article;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticles(long id, Articles articles)
        {

            if (id != articles.Id)
            {
                return BadRequest();
            }

            //Remember Publish Date. there may be better approach, to be investaged later(like one time programmable)
            var old = await _context.Articles.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
            articles.PublishDate = old.PublishDate; 

            articles.UpdateDate = DateTime.Now;
            _context.Entry(articles).State = EntityState.Modified;

            try
            { 
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticlesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetArticles", new {id}, articles);
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Articles>> PostArticles(Articles articles)
        {
            articles.PublishDate = DateTime.Now;
            _context.Articles.Add(articles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticles", new { id = articles.Id }, articles);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticles(long id)
        {
            var articles = await _context.Articles.Include(t => t.Reviews).FirstOrDefaultAsync(t => t.Id == id);
            if (articles == null)
            {
                return NotFound();
            }
            
            _context.Articles.Remove(articles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticlesExists(long id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
    }
}
