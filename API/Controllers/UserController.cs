using DatingApp.API.Data;
using DatingApp.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Controllers;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using API.DTOs;
using AutoMapper;
using System.Security.Claims;
namespace API;

[Authorize]
public class UsersController(IUserRepository userRepository, IMapper mapper) : BaseApiController
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await userRepository.GetMembersAsync();

        return Ok(users);
    }

    [HttpGet("{username}")]  // /api/users/2
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await userRepository.GetMemberAsync(username);

        if (user == null) return NotFound();

        return user;
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(memberUpdateDTO memberUpdateDTO)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (username == null) return BadRequest("User not found");
        var user = await userRepository.GetUserByUsernameAsync(username);
        if (user == null) return NotFound("User not found");
        mapper.Map(memberUpdateDTO, user);
        if (await userRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to update user");
    }
}