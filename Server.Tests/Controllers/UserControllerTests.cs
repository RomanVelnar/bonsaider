using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Moq;
using Server.Endpoints;
using Server.Models;
using Server.Data;
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
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unique per run
                .Options;

            _dbContext = new BonsaiContext(options);
            _dbContext.Database.EnsureCreated();

            _controller = new UserController(_dbContext);
        }

        [Fact]
        public async Task GetUser_ReturnsOk_WhenUserExists()
        {
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

            var result = await _controller.GetUser(userId);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("testuser", returnedUser.Username);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
        {
            var result = await _controller.GetUser(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result.Result);
        }
    }
}
