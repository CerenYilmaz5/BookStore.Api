﻿namespace BookStore.Api.DTOs
{
    // DTO used to return author details in API responses
    public class AuthorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime BirthDate { get; set; }
    }
}