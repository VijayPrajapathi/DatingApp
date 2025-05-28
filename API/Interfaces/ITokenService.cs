using System;
using DatingApp.API.Entities;

namespace API.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(AppUser user);

}
