using AutoMapper;
using AutoMapper.Internal.Mappers;
using MangaList.Application.DTOs;
using MangaList.Domain.Interfaces;

namespace MangaList.Application.Services;

public class MangaService
{
  private readonly IMangaRepository _mangaRepository;
  private readonly IMapper _mapper;

  public MangaService(IMangaRepository mangaRepository, IMapper mapper)
  {
    _mangaRepository = mangaRepository;
    _mapper = mapper;
  }

  public async Task<MangaListRequest> GetMangaAsync(string? page, string? key, string? order, string search)
  {
    int pagination = string.IsNullOrWhiteSpace(page) ? 1 : int.Parse(page);
    var mangas = await _mangaRepository.GetMangaAsync(pagination, key, order, search);
    return _mapper.Map<MangaListRequest>(mangas);
  }

  public async Task<MangaRequest?> GetMangaByIdAsync(Guid Id)
  {
    var manga = await _mangaRepository.GetMangaByIdAsync(Id);
    return _mapper.Map<MangaRequest>(manga);

  }
}