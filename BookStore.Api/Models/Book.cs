namespace BookStore.Api.Models
{
    // This class represents the main domain model (Entity) for a Book in the system.
    public class Book
    {
        // Unique identifier for the book.
        public int Id { get; set; }

        // Title of the book.
        public string Title { get; set; } = "";

        // Author of the book.
        public string Author { get; set; } = "";

        // Price of the book.
        public decimal Price { get; set; }

        // Number of books available in stock.
        public int Stock { get; set; }

        // Total number of pages in the book.
        public int PageCount { get; set; }

        // Date when the book was published.
        public DateTime PublishedDate { get; set; }
    }
}