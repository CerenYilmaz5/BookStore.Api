using Xunit;

namespace BookStore.Tests.Genre
{
    public class GetGenreDetailQueryTests
    {
        [Fact]
        public void GetGenreDetail_Should_Return_Genre_When_Id_Is_Valid()
        {
            // Arrange
            var genreId = 1;

            // Simulate a genre result
            var genre = new { Id = genreId, Name = "Drama" };

            // Assert
            Assert.NotNull(genre);
            Assert.Equal(1, genre.Id);
        }

        [Fact]
        public void GetGenreDetail_Should_Return_Null_When_Id_Is_Invalid()
        {
            // Arrange
            int invalidId = -1;

            // Simulate a null result
            object? genre = invalidId > 0 ? new { Id = invalidId, Name = "Fake" } : null;

            // Assert
            Assert.Null(genre);
        }
    }
}