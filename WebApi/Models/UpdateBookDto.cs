using Application.Common.Mappings;
using Application.Requests.Commands.UpdateBook;
using AutoMapper;
using Domain.Models;

namespace WebApi.Models;

public class UpdateBookDto : IMapWith<UpdateBookCommand>
{
    public Guid Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public GenreEnum Genre { get; set; }
    public string? Description { get; set; }
    public DateOnly? IssueDate { get; set; }
    public DateOnly? ExpireDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateBookDto, UpdateBookCommand>().ReverseMap();
    }
}