using AutoMapper;
using Chief.Application.DTOs;
using Chief.Application.Interfaces;
using Chief.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chief.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnboardingController(IMapper mapper, IOnboardingService onboardingService) : ControllerBase
    {
        [HttpPost]
        public IActionResult StartOnboarding([FromBody] OnboardingDto model)
        {
            var entity = mapper.Map<OnboardingEntity>(model);
            var result = onboardingService.StartOnboarding(entity);
            return Ok(result);
        }
    }
}