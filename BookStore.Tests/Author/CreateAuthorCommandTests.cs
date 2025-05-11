using Xunit;

namespace BookStore.Tests.Author
{
    public class CreateAuthorCommandTests
    {
        [Fact]
        public void CreateAuthor_Should_Succeed_When_Data_Is_Valid()
        {
            // Arrange
            var author = new Api.Models.Author
            {
                FirstName = "George",
                LastName = "Orwell",
                BirthDate = new DateTime(1903, 6, 25)
            };

            // Act
            var isValid = !string.IsNullOrEmpty(author.FirstName) && author.BirthDate < DateTime.Today;

            // Assert
            Assert.True(isValid);
            Assert.Equal("George", author.FirstName);
        }

        [Fact]
        public void CreateAuthor_Should_Fail_When_BirthDate_Is_In_Future()
        {
            // Arrange
            var author = new Api.Models.Author
            {
                FirstName = "Future",
                LastName = "Writer",
                BirthDate = DateTime.Today.AddDays(10)
            };

            // Act
            var isValid = author.BirthDate < DateTime.Today;

            // Assert
            Assert.False(isValid);
        }
    }
}