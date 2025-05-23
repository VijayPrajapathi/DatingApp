using System;
using API.DTOs;
using API.Entities;
using AutoMapper;
using DatingApp.API.Entities;

namespace API.Helpers;

public class AutoMapperProfiles : Profile
{
  public AutoMapperProfiles()
  {
    CreateMap<AppUser, MemberDto>()
    .ForMember(d => d.Age, o => o.MapFrom(s => s.DateOfBirth.ToDateTime(TimeOnly.MinValue).CalculateAge()))
    .ForMember(d => d.PhotoUrl, o => o.MapFrom(s => s.Photos.FirstOrDefault(x => x.IsMain)!.Url));
    CreateMap<Photo, PhotoDto>();
    CreateMap<memberUpdateDTO, AppUser>();
    CreateMap<RegisterDto, AppUser>();
    CreateMap<string, DateOnly>().ConvertUsing(s => DateOnly.Parse(s));
  }
}
