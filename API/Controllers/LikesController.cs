using System;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace API.Controllers;

public class LikesController(ILikesRepository likesRepository) : BaseApiController
{
    [HttpPost("{targetUserid}")]
    public async Task<ActionResult> ToggleLike(int targetUserId)
    {
        var sourceUserId = User.GetUserId();
        if (sourceUserId == targetUserId) return BadRequest("You cannot like yourself");

        var existingLike = await likesRepository.GetUserLike(sourceUserId, targetUserId);

        if (existingLike == null)
        {
            var like = new UserLike
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };

            likesRepository.AddLike(like);

        }
        else
        {
            likesRepository.RemoveLike(existingLike);
        }

        if (await likesRepository.SaveChanges()) return Ok();

        return BadRequest("Failed to save like");
    }

    [HttpGet("list")]

    public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserLikeId()
    {
        return Ok(await likesRepository.GetCurrentUserLikeId(User.GetUserId()));
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerator<MemberDto>>> GetUserLikes([FromQuery] LikesParams likeParams)
    {
        likeParams.UserId = User.GetUserId();
        var users = await likesRepository.GetUserLikes(likeParams);

        Response.AddPaginationheader(users);

        return Ok(users);
    }

}
