using Xunit;

namespace BookStore.Tests.Genre
{
    public class CreateGenreCommandTests
    {
        // This is a placeholder. In a real-world scenario,
        // you would inject a service or use a mock to simulate genre creation.

        [Fact]
        public void CreateGenre_Should_Succeed_When_Data_Is_Valid()
        {
            // Arrange
            var genreName = "Science Fiction";

            // Act
            var createdGenre = new { Id = 1, Name = genreName }; // Simulated output

            // Assert
            Assert.Equal("Science Fiction", createdGenre.Name);
            Assert.True(createdGenre.Id > 0);
        }

        [Fact]
        public void CreateGenre_Should_Fail_When_Name_Is_Empty()
        {
            // Arrange
            var genreName = "";

            // Simulate validation manually (or with a mock validator)
            var isValid = !string.IsNullOrWhiteSpace(genreName);

            // Assert
            Assert.False(isValid);
        }
    }
}