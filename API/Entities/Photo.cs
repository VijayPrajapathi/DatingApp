using System;
using System.ComponentModel.DataAnnotations.Schema;
using DatingApp.API.Entities;

namespace API.Entities;

[Table("Photos")] 
public class Photo
{
public int Id { get; set; }
public required string Url { get; set; }
public bool IsMain { get; set; }
public string? PublicId { get; set; }
//Navigation property to AppUser
public int AppUserId { get; set; }

public AppUser AppUser { get; set; }  = null!;// Foreign key to AppUser
}
