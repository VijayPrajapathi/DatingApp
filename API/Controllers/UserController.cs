using DatingApp.API.Data;
 using DatingApp.API.Entities;
using Microsoft.AspNetCore.Mvc;
 using Microsoft.EntityFrameworkCore;
using API.Controllers;
using Microsoft.AspNetCore.Authorization;
using API.Interfaces;
using API.DTOs;
using AutoMapper;
namespace API;
 
[Authorize]
 public class UsersController(IUserRepository userRepository) : BaseApiController
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
 }