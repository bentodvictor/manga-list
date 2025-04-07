using Microsoft.AspNetCore.Mvc;
using MangaList.Application.Services;

namespace MangaList.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MangaController : Controller
{
    private readonly ILogger<MangaController> _logger;
    private readonly MangaService _mangaService;

    public MangaController(ILogger<MangaController> logger, MangaService mangaService)
    {
        _logger = logger;
        _mangaService = mangaService;
    }

    // Get all manga list considering pagination (because of a large quantity of mangas) 
    [HttpGet("all")]
    public async Task<IActionResult> GetMangas([FromQuery] string? page, [FromQuery] string? key, [FromQuery] string? order, [FromQuery] string? search)
    {
        _logger.LogInformation("Calling API GetMangas Method.");
        var mangas = await _mangaService.GetMangaAsync(page, key, order, search);
        return Ok(mangas);
    }


    // Get detail information about the manga
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMangaDetails(Guid Id)
    {
        _logger.LogInformation("Calling API GetMangaDetails Method.");
        var mangaDetails = await _mangaService.GetMangaByIdAsync(Id);
        return Ok(mangaDetails);
    }
}