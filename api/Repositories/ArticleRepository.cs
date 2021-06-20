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
    public  class ArticleRepository : IArticleRepository
    {
        private readonly apiContext _context;

       public ArticleRepository()
       {
           _context = new apiContext();
       }

        public ArticleRepository(apiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Deletes Article by Id
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns>Bool:IsRemoved</returns>
        public bool DeleteArticle(long articleId)
        {
            var articles =  _context.Articles.Include(t => t.Reviews).FirstOrDefault(t => t.Id == articleId);
            if (articles == null)
            {
                return false;
            }

            _context.Articles.Remove(articles);
            Save();

            return true;
        }

        public Articles GetArticleById(long id)
        {
            var article = _context.Articles.Include(t => t.Reviews).FirstOrDefault(t => t.Id == id);

            if (article == null)
            {
                return null;
            }

            return article;
        }

        public IEnumerable<Articles> GetArticles()
        {
            var articles = _context.Articles.Include(t => t.Reviews).ToList();

            if (articles == null)
            {
                return null;
            }

            return articles;
        }

        public Articles NewArticle(Articles newArticle)
        {
            newArticle.PublishDate = DateTime.Now;
            _context.Articles.Add(newArticle);
            Save();
            //_context.SaveChangesAsync();

            return newArticle;
        }

        /// <summary>
        /// Updates Article by Id
        /// </summary>
        /// <param name="articleId"></param>
        /// <param name="updatedArticle"></param>
        /// <returns></returns>
        public Articles UpdateArticle(long articleId,Articles updatedArticle)
        {
            if (articleId != updatedArticle.Id)
            {
                return null;
            }

            //Remember Publish Date. there may be better approach, to be investaged later(like one time programmable)
            var old = _context.Articles.AsNoTracking().FirstOrDefault(t => t.Id == articleId);
            updatedArticle.PublishDate = old.PublishDate;

            updatedArticle.UpdateDate = DateTime.Now;
            _context.Entry(updatedArticle).State = EntityState.Modified;

            try
            {
                Save();
             //    _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticlesExists(articleId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return updatedArticle;
        }

        private bool ArticlesExists(long id)
        {
            return _context.Articles.Any(e => e.Id == id);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
