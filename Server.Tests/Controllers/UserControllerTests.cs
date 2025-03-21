namespace Server.Tests
{
    public class UserControllerTests
    {
        private readonly BonsaiContext _dbContext;
        private readonly Mock<IPasswordHasher<User>> _mockPasswordHasher;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            var options = new DbContextOptionsBuilder<BonsaiContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
            _dbContext = new BonsaiContext(options);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();

            
            _mockPasswordHasher = new Mock<IPasswordHasher<User>>();
            _mockPasswordHasher
                .Setup(ph => ph.HashPassword(It.IsAny<User>(), It.IsAny<string>()))
                .Returns("hashed_password");

            
            _controller = new UserController(_dbContext, _mockPasswordHasher.Object);
        }

        [Fact]
        public async Task GetUser_ReturnsOk_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var testUser = new User { Id = userId, Username = "testuser", Email = "test@example.com", PasswordHash = "hashed_password" };
            _dbContext.Users.Add(testUser);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _controller.GetUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<UserDTO>(okResult.Value);
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
    }
}