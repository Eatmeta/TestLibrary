using Application.Common.Mappings;
using AutoMapper;
using Domain.Models;

namespace Application.Requests.Queries.GetBookDetails;

public class BookDetailsDto : IMapWith<Book>
{
    public Guid Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public List<Genre> Genres { get; set; } = new();
    public List<Author> Authors { get; set; } = new();
    public string? Description { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ReturnDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Book, BookDetailsDto>()
            .ForMember(bookVm => bookVm.Id,
                options => options.MapFrom(book => book.Id))
            .ForMember(bookVm => bookVm.Isbn,
                options => options.MapFrom(book => book.Isbn))
            .ForMember(bookVm => bookVm.Title,
                options => options.MapFrom(book => book.Title))
            .ForMember(bookVm => bookVm.Genres,
                options => options.MapFrom(book => book.Genres))
            .ForMember(bookVm => bookVm.Authors,
                options => options.MapFrom(book => book.Authors))
            .ForMember(bookVm => bookVm.Description,
                options => options.MapFrom(book => book.Description))
            .ForMember(bookVm => bookVm.IssueDate,
                options => options.MapFrom(book => book.IssueDate))
            .ForMember(bookVm => bookVm.ReturnDate,
                options => options.MapFrom(book => book.ReturnDate));
    }
}