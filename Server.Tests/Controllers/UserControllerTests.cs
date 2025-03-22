using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Moq;
using Server.Endpoints;
using Server.Models;
using Server.Data;
using Server.DTOs;
using Xunit;

namespace Server.Tests
{
    public class UserControllerTests
    {
        private readonly BonsaiContext _dbContext;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            var options = new DbContextOptionsBuilder<BonsaiContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new BonsaiContext(options);
            _dbContext.Database.EnsureCreated();

            _controller = new UserController(_dbContext);
        }

        [Fact]
        public async Task GetUser_ReturnsOk_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var testUser = new User
            {
                Id = userId,
                Username = "testuser",
                Email = "test@example.com",
                PasswordHash = "hashed_password"
            };
            _dbContext.Users.Add(testUser);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _controller.GetUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("testuser", returnedUser.Username);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Act
            var result = await _controller.GetUser(Guid.NewGuid());

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreated_WhenUserIsCreated()
        {
            // Arrange
            var dto = new UserRegistrationDTO
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Something123"
            };

            // Act
            var result = await _controller.CreateUser(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdUser = Assert.IsType<User>(createdResult.Value);

            Assert.Equal(dto.Username, createdUser.Username);
            Assert.Equal(dto.Email, createdUser.Email);
            Assert.NotNull(createdUser.PasswordHash);
            Assert.NotEqual(dto.Password, createdUser.PasswordHash);
        }
    }
}