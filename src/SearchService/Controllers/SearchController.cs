using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Controllers;
[ApiController]
[Route("/api/search")]
public class SearchController : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParams searchParams)
    {

        var query = DB.PagedSearch<Item, Item>();
        query.Sort(x => x.Ascending(a => a.Make));
        if (!string.IsNullOrEmpty(searchParams.SearchTerm))
        {
            query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
        }
        query = searchParams.OrderBy switch
        {
            "producator" => query.Sort(x => x.Ascending(a => a.Make)),
            "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
            _ => query.Sort(x => x.Ascending(a => a.LicitatieEnd))

        };
        query = searchParams.FilterBy switch
        {
            "finished" => query.Match(x => x.LicitatieEnd < DateTime.UtcNow),
            "endingSoon" => query.Match(x => x.LicitatieEnd < DateTime.UtcNow.AddHours(5) && x.LicitatieEnd > DateTime.UtcNow),
            _ => query.Match(x => x.LicitatieEnd > DateTime.UtcNow)
        };
        if (!string.IsNullOrEmpty(searchParams.Vanzator))
        {
            query.Match(x => x.Vanzator == searchParams.Vanzator);
        }
        if (!string.IsNullOrEmpty(searchParams.Castigator))
        {
            query.Match(x => x.Castigator == searchParams.Castigator);
        }
        if (!string.IsNullOrEmpty(searchParams.make))
        {
            query.Match(x => x.Make == searchParams.make);
        }

        query.PageNumber(searchParams.PageNumber);
        query.PageSize(searchParams.PageSize);
        var result = await query.ExecuteAsync();
        return Ok(new
        {
            result = result.Results,
            pageCount = result.PageCount,
            totalCount = result.TotalCount
        });
    }
}