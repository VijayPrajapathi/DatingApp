using System;
using DatingApp.API.Data;
using DatingApp.API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class BuggyController(DataContext dbContext) : BaseApiController
{
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetAuth()
    {
        return "secret tests";
    }

    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        var res = dbContext.Users.Find(-1);
        if (res == null) return NotFound();
        return res;
    }

    [HttpGet("server-error")]
    public ActionResult<AppUser> GetServerError()
    {
        var res = dbContext.Users.Find(-1) ?? throw new Exception("Server error");
        return res;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was not a good request");
    }
}
