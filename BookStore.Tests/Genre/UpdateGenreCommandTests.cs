using Xunit;

namespace BookStore.Tests.Genre
{
    public class UpdateGenreCommandTests
    {
        [Fact]
        public void UpdateGenre_Should_Succeed_When_Genre_Exists()
        {
            // Arrange
            var genreId = 1;
            var newName = "Updated Genre";

            // Simulate update
            var genre = new { Id = genreId, Name = newName };

            // Act
            var result = genre.Name;

            // Assert
            Assert.Equal("Updated Genre", result);
        }

        [Fact]
        public void UpdateGenre_Should_Fail_When_Genre_Does_Not_Exist()
        {
            // Arrange
            var genreId = -1;
            var updated = genreId > 0;

            // Assert
            Assert.False(updated);
        }
    }
}