namespace BookStore.Api.DTOs
{
    // This class is used as a Data Transfer Object (DTO) for both creating and updating a book via API.
    public class UpdateBookRequest
    {
        // Title of the book. Required and validated.
        public string Title { get; set; } = "";

        // Author of the book. Required and validated.
        public string Author { get; set; } = "";

        // Genre of the book. Required and validated.
        public string Genre { get; set; } = "";

        // Price of the book. Must be greater than zero.
        public decimal Price { get; set; }

        // Number of items in stock. Cannot be negative.
        public int Stock { get; set; }

        // Total number of pages. Must be greater than zero.
        public int PageCount { get; set; }

        // Date when the book was published. Must not be in the future.
        public DateTime PublishedDate { get; set; }
    }
}