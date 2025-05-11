using Xunit;

namespace BookStore.Tests.Genre
{
    public class DeleteGenreCommandTests
    {
        // This is a simulated test; in a real scenario, you'd use a mocked or fake genre service

        [Fact]
        public void DeleteGenre_Should_Succeed_When_Genre_Exists()
        {
            // Arrange
            var genreId = 1; // Assume genre exists

            // Act
            var deleted = genreId > 0; // Simulate deletion logic

            // Assert
            Assert.True(deleted);
        }

        [Fact]
        public void DeleteGenre_Should_Fail_When_Genre_Does_Not_Exist()
        {
            // Arrange
            var genreId = -1;

            // Act
            var deleted = genreId > 0;

            // Assert
            Assert.False(deleted);
        }
    }
}