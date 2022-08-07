using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using NSubstitute;
using SpaceFlight.API.Api.Controllers;
using SpaceFlight.API.Application.DTO.ViewModel;
using SpaceFlight.API.Application.Model;
using SpaceFlight.API.Core.Contracts.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using Xunit;

namespace SpaceFlight.Test
{
    public class ArticleControllerUnitTest
    {
        private readonly CancellationToken token = new();
        private readonly IMongoCollection<Article> mockCollection = Mock.Of<IMongoCollection<Article>>();

        [Fact(DisplayName = "Create")]
        public async void CreateArticle_ReturnsSuccess()
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
            var mockContext = new Mock<IContext>();
            mockContext.Setup(ctx => ctx.Collection).Returns(mockCollection);
            var sut = new ArticleController(mockContext.Object);

            // Act
            var result = await sut.InsertAsync(dto, token);
            var httpResult = (CreatedResult)result;

            // Assert
            httpResult.StatusCode.Should().Be(201);
            mockContext.Verify(m => m.Collection.InsertOneAsync(It.IsAny<Article>(), null, token), Times.Once());
            mockContext.Verify(m => m.GetNewIdAsync(token), Times.Once());
        }
    }
}