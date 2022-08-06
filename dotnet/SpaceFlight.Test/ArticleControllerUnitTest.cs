using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SpaceFlight.API.Api.Controllers;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Application.Model;
using SpaceFlight.API.Core.Contracts.Infrastructure;
using System;
using Xunit;

namespace SpaceFlight.Test
{
    public class ArticleControllerUnitTest
    {
        private readonly ArticleController _sut;
        private readonly IDatabase _db = Substitute.For<IDatabase>();

        public ArticleControllerUnitTest()
        {
            _sut = new ArticleController(_db);
        }

        [Fact]
        public async void GetArticleById_ReturnsArticle()
        {
            // Arrange
            var token = new System.Threading.CancellationToken();
            var id = 1;
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
            Article entity = dto.ToEntity(id);
            await _db.Collection.InsertOneAsync(entity);

            // Act
            var result = await _sut.GetById(id, token);
            var httpResult = (OkObjectResult)result;

            // Assert
            httpResult.StatusCode.Should().Be(200);
        }
    }
}