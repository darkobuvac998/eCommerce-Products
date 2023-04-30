using Newtonsoft.Json;

namespace eCommerce.Products.Domain.Shared;

public sealed class PaginateRequest
{
    private const int _maxPageSize = 100;
    private int _pageNumber = 1;
    private int _pageSize = 100;

    public bool PaginatioRequired { get; set; }

    public int PageNumber
    {
        get => _pageNumber;
        set { _pageNumber = value < 1 ? _pageNumber : value; }
    }

    public int PageSize
    {
        get => _pageSize;
        set { _pageSize = value > _maxPageSize ? _maxPageSize : value; }
    }

    public override string ToString()
    {
        return $"PageNumber:{_pageNumber} PageSize: {_pageSize}";
    }
}
