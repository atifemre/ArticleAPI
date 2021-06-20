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
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly apiContext _context;
        private readonly ArticleRepository _articleRepo;

        public ArticlesController(apiContext context)
        {
            _context = context;
            _articleRepo = new ArticleRepository(_context);
        }

        // GET: api/Articles
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IEnumerable<Articles>>> GetArticles()
        {

            var articles = _articleRepo.GetArticles();

           // var articles = await _context.Articles.ToListAsync();

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
            var article =  _articleRepo.GetArticleById(id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticles(long id, Articles article)
        {
            var newArticle = _articleRepo.UpdateArticle(id, article);

            return CreatedAtAction("GetArticles", new {id}, newArticle);
        }

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Articles>> NewArticles(Articles articles)
        {
            var newArticle = _articleRepo.NewArticle(articles);

            return CreatedAtAction("GetArticles", new { id = newArticle.Id }, newArticle);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticles(long id)
        {
            bool isDeleted = _articleRepo.DeleteArticle(id);
            if (isDeleted == false)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
