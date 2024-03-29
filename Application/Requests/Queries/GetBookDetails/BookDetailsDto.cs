﻿using Application.Common.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Requests.Queries.GetBookDetails;

public class BookDetailsDto : IMapWith<Book>
{
    public Guid Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public GenreEnum Genre { get; set; }
    public List<Author> Authors { get; set; } = new();
    public string? Description { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ExpireDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BookDetailsDto>().ReverseMap();
    }
}