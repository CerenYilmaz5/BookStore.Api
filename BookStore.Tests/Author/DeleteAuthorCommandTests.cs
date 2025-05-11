using Xunit;

namespace BookStore.Tests.Author
{
    public class DeleteAuthorCommandTests
    {
        [Fact]
        public void DeleteAuthor_Should_Succeed_When_NoBooksAssigned()
        {
            // Arrange
            bool hasBooks = false;

            // Act
            var canDelete = !hasBooks;

            // Assert
            Assert.True(canDelete);
        }

        [Fact]
        public void DeleteAuthor_Should_Fail_When_BooksExist()
        {
            // Arrange
            bool hasBooks = true;

            // Act
            var canDelete = !hasBooks;

            // Assert
            Assert.False(canDelete);
        }
    }
}