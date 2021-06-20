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

namespace apiTest
{
    public class UnitTestMain
    {
        [Fact]
        public void Add_ArticleTest()
        {
            var options = new DbContextOptionsBuilder<apiContext>()
            .UseInMemoryDatabase(databaseName: "UnitTests")
            .Options;

            var context = new apiContext(options);
            context.SaveChanges();

            Articles article = new()
            {
                Id = 1,
                Title = "Unit Test0",
                ArticleContent = "This test shall pass.",
                StarCount = 5,
            };

            IArticleRepository articleRepo = new ArticleRepository(context);

            Articles newArticle = articleRepo.NewArticle(article);

            Assert.Equal(article.ArticleContent, newArticle.ArticleContent);
        }
    }

}
