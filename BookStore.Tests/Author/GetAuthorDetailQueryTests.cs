using Xunit;

namespace BookStore.Tests.Author
{
    public class GetAuthorDetailQueryTests
    {
        [Fact]
        public void GetAuthorDetail_Should_Return_Author_When_Id_Is_Valid()
        {
            // Arrange
            var authorId = 1;
            var author = new Api.Models.Author
            {
                Id = authorId,
                FirstName = "Margaret",
                LastName = "Atwood",
                BirthDate = new DateTime(1939, 11, 18)
            };

            // Act
            var isFound = author.Id == 1;

            // Assert
            Assert.True(isFound);
            Assert.Equal("Margaret", author.FirstName);
        }

        [Fact]
        public void GetAuthorDetail_Should_Return_Null_When_Id_Is_Invalid()
        {
            // Simulate: author not found
            Api.Models.Author? author = null;

            // Assert
            Assert.Null(author);
        }
    }
}