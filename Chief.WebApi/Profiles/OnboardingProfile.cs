using AutoMapper;
using Chief.Application.DTOs;
using Chief.Domain.Entities;

namespace Chief.Api.Profiles;

public class OnboardingProfile : Profile
{
    public OnboardingProfile()
    {
        CreateMap<OnboardingDto, OnboardingEntity>();
    }
}