using System;
using API.DTOs;
using API.Entities;
using API.Helpers;
using AutoMapper.Execution;

namespace API.Interfaces;

public interface ILikesRepository
{

    Task<UserLike?> GetUserLike(int sourceUserId, int targetUserId);
    Task<PagedList<MemberDto>> GetUserLikes(LikesParams likesParams);
    Task<IEnumerable<int>> GetCurrentUserLikeId(int currentUserId);

    void AddLike(UserLike userLike);
    void RemoveLike(UserLike userLike);

    Task<bool> SaveChanges();

}
