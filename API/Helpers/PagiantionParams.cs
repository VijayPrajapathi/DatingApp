using System;

namespace API.Helpers;

public class PagiantionParams
{
     private const int MaxSize = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxSize) ? MaxSize : value;
    }

}
