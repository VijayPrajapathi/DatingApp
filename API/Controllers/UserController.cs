using DatingApp.API.Data;
 using DatingApp.API.Entities;
 using System.Collections.Generic;
 using System.Threading.Tasks;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 using Microsoft.EntityFrameworkCore;
using API.Controllers;
using Microsoft.AspNetCore.Authorization;
namespace API;
 
[Authorize]
 public class UsersController(DataContext context) : BaseApiController
 {

    [AllowAnonymous]
     [HttpGet]
     public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
     {
         var users = await context.Users.ToListAsync();
 
         return users;
     }
    
    [Authorize]
     [HttpGet("{id:int}")]  // /api/users/2
     public async Task<ActionResult<AppUser>> GetUser(int id)
     {
         var user = await context.Users.FindAsync(id);
 
         if (user == null) return NotFound();
 
         return user;
     }
 }