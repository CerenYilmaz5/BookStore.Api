namespace BookStore.Api.DTOs
{
    // DTO returned when querying books
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}