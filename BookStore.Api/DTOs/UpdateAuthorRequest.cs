namespace BookStore.Api.DTOs
{
    // DTO used for creating and updating author records through the API
    public class UpdateAuthorRequest
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime BirthDate { get; set; }
    }
}