using System;

namespace API.Helpers;

public class LikesParams : PagiantionParams
{
    public int UserId { get; set; }
    public  required string Predicate { get; set; } = "liked";

}
