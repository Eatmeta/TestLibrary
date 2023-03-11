using Application.Common.Mappings;
using Application.Requests.Commands.CreateBook;
using AutoMapper;
using Domain.Models;

namespace WebApi.Models;

public class CreateBookDto : IMapWith<CreateBookCommand>
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public GenreEnum Genre { get; set; }
    public List<Author> Authors { get; set; } = new();
    public string? Description { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateBookDto, CreateBookCommand>().ReverseMap();
    }
}