using System;

namespace API.Helpers;

public class MessageParams : PagiantionParams
{
public string? UserName { get; set; }
public required string Container { get; set; } = "Unread";
}
