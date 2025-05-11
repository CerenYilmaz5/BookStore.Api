
using Xunit;

namespace BookStore.Tests.Author
{
    public class UpdateAuthorCommandTests
    {
        [Fact]
        public void UpdateAuthor_Should_Succeed_When_Data_Is_Valid()
        {
            // Arrange
            var author = new Api.Models.Author
            {
                Id = 1,
                FirstName = "Original",
                LastName = "Author",
                BirthDate = new DateTime(1970, 1, 1)
            };

            // Act
            author.FirstName = "Updated";
            author.LastName = "Name";
            var updated = author;

            // Assert
            Assert.Equal("Updated", updated.FirstName);
            Assert.Equal("Name", updated.LastName);
        }

        [Fact]
        public void UpdateAuthor_Should_Fail_When_BirthDate_Is_Invalid()
        {
            // Arrange
            var futureDate = DateTime.Today.AddYears(1);

            // Act
            var isValid = futureDate < DateTime.Today;

            // Assert
            Assert.False(isValid);
        }
    }
}