using System;
using System.Net.Http;
using Xunit;
using api.Data;
using api.Models;
using api.Repositories;
using Moq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace apiTest
{
    public class UnitTestMain
    {
        [Fact]
        public void Create_Article_ValidTest()
        {
            Articles article = new()
            {
                Title = "Add Article Unit Test",
                ArticleContent = "This test shall pass.",
                StarCount = 5,
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());

            Articles newArticle = articleRepo.NewArticle(article);

            Assert.Equal(article.ArticleContent, newArticle.ArticleContent);
        }

        [Fact]
        public void Get_ArticleById_ValidTest()
        {
            Articles article = new()
            {
                Title = "Get Article Unit Test",
                ArticleContent = "This test shall pass.",
                StarCount = 5,
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());

            Articles newArticle = articleRepo.NewArticle(article);
            Articles returnedArticle = articleRepo.GetArticleById(newArticle.Id);

            Assert.Equal(newArticle, returnedArticle);
        }

        [Fact]
        public void Update_Article_ValidTest()
        {
            Articles article = new()
            {
                Title = "Article Unit Test",
                ArticleContent = "This test shall pass.",
                StarCount = 5,
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());

            Articles newArticle = articleRepo.NewArticle(article);

            string newTitle = "Updated Article Unit Test";
            newArticle.Title = newTitle;

            Articles updatedArticle = articleRepo.UpdateArticle(newArticle.Id, newArticle);

            Assert.Equal(newTitle, updatedArticle.Title);
        }

        [Fact]
        public void Delete_Article_ValidTest()
        {
            Articles article = new()
            {
                Title = "Delete Unit Test",
                ArticleContent = "This test shall pass.",
                StarCount = 5,
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());

            Articles newArticle = articleRepo.NewArticle(article);

            Assert.True(articleRepo.DeleteArticle(newArticle.Id));
        }

        [Fact]
        public void Get_Articles_ValidTest()
        {
            Articles article = new()
            {
                Title = "0",
                ArticleContent = "This test shall pass.",
                StarCount = 5,
            };
            Articles article1 = new()
            {
                Title = "1",
                ArticleContent = "This test shall pass.",
                StarCount = 5,
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB("inMemDB"));

            articleRepo.NewArticle(article);
            articleRepo.NewArticle(article1);
        
            List<Articles> articles = articleRepo.GetArticles().ToList();

            Assert.True(articles.Count > 1 && articles.First().Title == "0" && articles.Skip(1).First().Title == "1");
        }

        //Better to be mockdb but encountered some problems while contructing mock architeture
        public apiContext GetInMemoryDB(string dbName = "UnitTests") {

            var options = new DbContextOptionsBuilder<apiContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            var context = new apiContext(options);
                           context.SaveChanges();

            return context;
        }
    
    }

}
