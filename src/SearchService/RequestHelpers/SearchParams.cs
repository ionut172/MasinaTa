using System.Runtime.InteropServices;

namespace SearchService.RequestHelpers;

public class SearchParams {

    public string SearchTerm { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 4;
    public string Vanzator { get; set; }
    public string Castigator { get; set; }
    public string OrderBy { get; set; }
    public string FilterBy { get; set; }

}