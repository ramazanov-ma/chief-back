using AutoMapper;
using Chief.Application.DTOs;
using Chief.Domain.Entities;

namespace Chief.Api.Profiles;

public class OnboardingMappingProfile : Profile
{
    public OnboardingMappingProfile()
    {
        CreateMap<OnboardingProfile, OnboardingProfileDto>()
            .ForMember(dest => dest.FoodPreferences, opt => opt.MapFrom(src => src.FoodPreferences))
            .ForMember(dest => dest.ExcludedProducts, opt => opt.MapFrom(src => src.ExcludedProducts))
            .ForMember(dest => dest.HouseholdMembers, opt => opt.MapFrom(src => src.HouseholdMembers))
            .ForMember(dest => dest.TimePreferences, opt => opt.MapFrom(src => src.TimePreferences));

        CreateMap<FoodPreference, FoodPreferenceDto>();
        CreateMap<FoodPreferenceDto, FoodPreference>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OnboardingProfile, opt => opt.Ignore())
            .ForMember(dest => dest.OnboardingProfileId, opt => opt.Ignore());

        CreateMap<ExcludedProduct, ExcludedProductDto>();
        CreateMap<ExcludedProductDto, ExcludedProduct>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OnboardingProfile, opt => opt.Ignore())
            .ForMember(dest => dest.OnboardingProfileId, opt => opt.Ignore());

        CreateMap<HouseholdMember, HouseholdMemberDto>();
        CreateMap<HouseholdMemberDto, HouseholdMember>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OnboardingProfile, opt => opt.Ignore())
            .ForMember(dest => dest.OnboardingProfileId, opt => opt.Ignore());

        CreateMap<TimePreference, TimePreferenceDto>();
        CreateMap<TimePreferenceDto, TimePreference>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.OnboardingProfile, opt => opt.Ignore())
            .ForMember(dest => dest.OnboardingProfileId, opt => opt.Ignore());

        CreateMap<UpdateOnboardingProfileDto, OnboardingProfile>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.FoodPreferences, opt => opt.Ignore())
            .ForMember(dest => dest.ExcludedProducts, opt => opt.Ignore())
            .ForMember(dest => dest.HouseholdMembers, opt => opt.Ignore())
            .ForMember(dest => dest.TimePreferences, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}