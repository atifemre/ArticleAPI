using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories
{
    public interface IArticleRepository
    {
        public IEnumerable<Articles> GetArticles();
        public Articles GetArticleById(long id);
        public Articles NewArticle(Articles article);
        public Articles UpdateArticle(long articleId, Articles article);
        public bool DeleteArticle(long articleId);
    }
}
