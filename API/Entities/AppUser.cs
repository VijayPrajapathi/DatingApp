using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace DatingApp.API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public DateOnly DateOfBirth { get; set; }
        public required string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime LastActive { get; set; } = DateTime.UtcNow;
        public required string Gender { get; set; }
        public string? Introduction { get; set; }
        public string? Intrests { get; set; }
        public string? LookingFor { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();

        public List<UserLike> LikedBytheUsers { get; set; } = [];
        public List<UserLike> LikedUsers { get; set; } = [];

        public List<Message> MessageSent { get; set; } = [];
        public List<Message> MessagesReceived { get; set; } = [];
        public ICollection<AppUserRole> UserRoles { get; set; } = []   ;
    }
}
