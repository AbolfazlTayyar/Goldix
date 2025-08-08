namespace Goldix.Application.Models.Pagination;

public class PagedRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }

    public void Validate()
    {
        if (Page < 1) Page = 1;
        if (PageSize < 1) PageSize = 10;
        if (PageSize > 100) PageSize = 100;
    }
}
