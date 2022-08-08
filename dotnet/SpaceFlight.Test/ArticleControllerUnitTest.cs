using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Moq;
using SpaceFlight.API.Api.Controllers;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Application.Model;
using SpaceFlight.API.Core.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SpaceFlight.Test
{
    public class ArticleControllerUnitTest
    {
        private readonly CancellationToken token = new();

        [Fact(DisplayName = "Create")]
        public async void CreateArticle_ReturnSuccess()
        {
            // Arrange
            var dto = new ArticleDTO()
            {
                ImageUrl = "UNIT.TEST",
                NewsSite = "UNIT.TEST",
                Title = "UNIT.TEST",
                PublishedAt = DateTime.Now,
                UpdatedAt = DateTime.Now.AddDays(1),
                Summary = "UNIT.TEST",
                Url = "UNIT.TEST",
                Featured = true,
            };

            Mock<IArticleRepository> mockRepository = new();
            var sut = new ArticleController(mockRepository.Object);

            // Act
            var result = await sut.InsertAsync(dto, token);
            var httpResult = (CreatedResult)result;

            // Assert
            mockRepository.Verify(m => m.AddAsync(It.IsAny<ArticleDTO>(), token), Times.Once());
            httpResult.StatusCode.Should().Be(201);
        }

        [Fact(DisplayName = "Get by id")]
        public async void GetArticleById_ReturnSuccess()
        {
            // Arrange
            var article = new Article()
            {
                Id = 1,
                ImageUrl = "UNIT.TEST",
                NewsSite = "UNIT.TEST",
                Title = "UNIT.TEST",
                PublishedAt = DateTime.Now,
                UpdatedAt = DateTime.Now.AddDays(1),
                Summary = "UNIT.TEST",
                Url = "UNIT.TEST",
                Featured = true,
                Events = new List<Events>(),
                Launches = new List<Launches>()
            };

            Mock<IArticleRepository> mockRepository = new();
            mockRepository.Setup(r => r.GetByIdAsync(article.Id, token)).ReturnsAsync(article);

            var sut = new ArticleController(mockRepository.Object);

            // Act
            var result = await sut.GetByIdAsync(article.Id, token);
            var httpResult = (OkObjectResult)result;

            // Assert
            mockRepository.Verify(m => m.GetByIdAsync(It.IsAny<int>(), token), Times.Once());
            httpResult.StatusCode.Should().Be(200);
            httpResult.Value.Should().Be(article);
        }

        [Fact(DisplayName = "Get All")]
        public async void GetArticles_ReturnSuccess()
        {
            // Arrange
            int limit = 5;
            var articles = new List<Article>
            {
                new Article()
                {
                    Id = 1
                },
                new Article()
                {
                    Id = 2
                },
                new Article()
                {
                    Id = 3
                },
                new Article()
                {
                    Id = 4
                }
                ,new Article()
                {
                    Id = 5
                }
            };

            Mock<IArticleRepository> mockRepository = new();
            mockRepository.Setup(r => r.GetAsync(limit, token)).ReturnsAsync(articles);

            var sut = new ArticleController(mockRepository.Object);

            // Act
            var result = await sut.GetAsync(token);
            var httpResult = (OkObjectResult)result;

            // Assert
            mockRepository.Verify(m => m.GetAsync(It.IsAny<int>(), token), Times.Once());
            httpResult.StatusCode.Should().Be(200);
            httpResult.Value.Should().Be(articles);
        }

        [Fact(DisplayName = "Update")]
        public async void UpdateArticle_ReturnSuccess()
        {
            // Arrange
            var article = new Article()
            {
                Id = 1,
                ImageUrl = "UNIT.TEST",
                NewsSite = "UNIT.TEST",
                Title = "UNIT.TEST",
                PublishedAt = DateTime.Now,
                UpdatedAt = DateTime.Now.AddDays(1),
                Summary = "UNIT.TEST",
                Url = "UNIT.TEST",
                Featured = true,
                Events = new List<Events>(),
                Launches = new List<Launches>()
            };

            var dto = new ArticleDTO()
            {
                ImageUrl = "UNIT.TEST-UPDATED",
                NewsSite = "UNIT.TEST-UPDATED",
                Title = "UNIT.TEST-UPDATED",
                PublishedAt = DateTime.Now,
                UpdatedAt = DateTime.Now.AddDays(1),
                Summary = "UNIT.TEST-UPDATED",
                Url = "UNIT.TEST-UPDATED",
                Featured = true,
                Events = new List<EventsDTO>(),
                Launches = new List<LaunchesDTO>()
            };

            Mock<IArticleRepository> mockRepository = new();
            mockRepository.Setup(r => r.UpdateAsync(article.Id, dto, token)).Returns(Task.CompletedTask);

            var sut = new ArticleController(mockRepository.Object);

            // Act
            var result = await sut.UpdateAsync(article.Id, dto, token);
            var httpResult = (NoContentResult)result;

            // Assert
            mockRepository.Verify(m => m.UpdateAsync(It.IsAny<int>(), It.IsAny<ArticleDTO>(), token), Times.Once());
            httpResult.StatusCode.Should().Be(204);
        }

        [Fact(DisplayName = "Delete")]
        public async void DeleteArticle_ReturnSuccess()
        {
            // Arrange
            var article = new Article()
            {
                Id = 1
            };

            Mock<IArticleRepository> mockRepository = new();
            mockRepository.Setup(r => r.DeleteAsync(article.Id, token)).Returns(Task.CompletedTask);

            var sut = new ArticleController(mockRepository.Object);

            // Act
            var result = await sut.DeleteAsync(article.Id, token);
            var httpResult = (NoContentResult)result;

            // Assert
            mockRepository.Verify(m => m.DeleteAsync(It.IsAny<int>(), token), Times.Once());
            httpResult.StatusCode.Should().Be(204);
        }
    }
}