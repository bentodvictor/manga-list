using AutoMapper;
using MangaList.Application.DTOs;
using MangaList.Domain.Entities;

namespace MangaList.Application.Mapper;
public class MangaProfile : Profile
{
    public MangaProfile()
    {
        CreateMap<MangaRequest, Manga>().ReverseMap();
        CreateMap<MangaDetailsRequest, Manga>().ReverseMap();
        CreateMap<MangaListRequest, MangaCollection>().ReverseMap();
    }
}