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
    public class ReviewUnitTests
    {
        private Articles article = new()
        {
            Title = "Article Unit Test for Reviews",
            Author = "John Loke Lost",
            ArticleContent = "This test shall pass.",
            StarCount = 5,
        };

        [Fact]
        public void Create_Review_ValidTest()
        {
            Reviews review = new()
            {
                Reviewer = "Add Review Unit Test",
                ReviewerContent = "This test shall pass."
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());
            Articles newArticle = articleRepo.NewArticle(article);

            IReviewRepository reviewRepo = new ReviewRepository(GetInMemoryDB());
            Reviews newReview = reviewRepo.NewReview(newArticle.Id,review);

            Assert.Equal(review.ReviewerContent, newReview.ReviewerContent);
        }

        [Fact]
        public void Get_ReviewById_ValidTest()
        {
            Reviews review = new()
            {
                Reviewer = "Get Review Unit Test",
                ReviewerContent = "This test shall pass."
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());
            Articles newArticle = articleRepo.NewArticle(article);

            IReviewRepository reviewRepo = new ReviewRepository(GetInMemoryDB());

            Reviews newReview = reviewRepo.NewReview(newArticle.Id,review);
            Reviews returnedReview = reviewRepo.GetReviewById(review.Id);

            Assert.Equal(newReview, returnedReview);
        }

        [Fact]
        public void Update_Review_ValidTest()
        {
            Reviews review = new()
            {
                Reviewer = "Update Review Unit Test",
                ReviewerContent = "This test shall pass."
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());
            Articles newArticle = articleRepo.NewArticle(article);

            IReviewRepository reviewRepo = new ReviewRepository(GetInMemoryDB());
            Reviews newReview = reviewRepo.NewReview(newArticle.Id,review);

            string newReviewContent = "Updated Article Unit Test";
            newReview.ReviewerContent = newReviewContent;

            Reviews updatedReview = reviewRepo.UpdateReview(newReview.Id, newReview);

            Assert.Equal(newReviewContent, updatedReview.ReviewerContent);
        }

        [Fact]
        public void Delete_Review_ValidTest()
        {
            Reviews review = new()
            {
                Reviewer = "Delete Review Unit Test",
                ReviewerContent = "This test shall pass."
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());
            Articles newArticle = articleRepo.NewArticle(article);

            IReviewRepository reviewRepo = new ReviewRepository(GetInMemoryDB());
            Reviews newReview = reviewRepo.NewReview(article.Id, review);

            Assert.True(reviewRepo.DeleteReview(newReview.Id));
        }

        [Fact]
        public void Get_Reviews_ValidTest()
        {
            Reviews review = new()
            {
                Reviewer = "0",
                ReviewerContent = "This test shall pass."
            };
            Reviews review1 = new()
            {
                Reviewer = "1",
                ReviewerContent = "This test shall pass."
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB("inMemDB"));
            Articles newArticle = articleRepo.NewArticle(article);

            IReviewRepository reviewRepo = new ReviewRepository(GetInMemoryDB("inMemDB"));
            reviewRepo.NewReview(article.Id,review);
            reviewRepo.NewReview(article.Id,review1);

            List<Reviews> reviews = reviewRepo.GetReviews().ToList();

            Assert.True(reviews.Count > 1 && reviews.First().Reviewer == "0" && reviews.Skip(1).First().Reviewer == "1");
        }

        //Better to be mockdb but encountered some problems while contructing mock architeture
        public apiContext GetInMemoryDB(string dbName = "UnitTests")
        {
            var options = new DbContextOptionsBuilder<apiContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            var context = new apiContext(options);
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void Create_Review_NotExistArticle_NotValidTest()
        {
            Reviews review = new()
            {
                Reviewer = "Add Review Unit Test",
                ReviewerContent = "This test shall pass."
            };

            IReviewRepository reviewRepo = new ReviewRepository(GetInMemoryDB());
            Reviews newReview = reviewRepo.NewReview(article.Id, review);

            Assert.Null(newReview);
        }

        [Fact]
        public void Create_Review_ReviwerEmpty_NotValidTest()
        {
            Reviews review = new()
            {
              //  Reviewer = "Add Review Unit Test",
                ReviewerContent = "This test shall pass."
            };
            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());
            Articles newArticle = articleRepo.NewArticle(article);

            IReviewRepository reviewRepo = new ReviewRepository(GetInMemoryDB());
            Reviews newReview = reviewRepo.NewReview(newArticle.Id, review);

            Assert.Null(newReview);
        }

        [Fact]
        public void Update_Review_ReviwerDeleted_NotValidTest()
        {
            Reviews review = new()
            {
                Reviewer = "Update Review Unit Test",
                ReviewerContent = "This test shall pass."
            };

            IArticleRepository articleRepo = new ArticleRepository(GetInMemoryDB());
            Articles newArticle = articleRepo.NewArticle(article);

            IReviewRepository reviewRepo = new ReviewRepository(GetInMemoryDB());
            Reviews newReview = reviewRepo.NewReview(newArticle.Id, review);

            newReview.Reviewer = null;

            Reviews updatedReview = reviewRepo.UpdateReview(newReview.Id, newReview);

            Assert.Null(updatedReview);
        }

    }
}
