using Chief.Models;
using Chief.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chief.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnboardingController : ControllerBase
    {
        private readonly IOnboardingService _onboardingService;

        public OnboardingController(IOnboardingService onboardingService)
        {
            _onboardingService = onboardingService;
        }

        [HttpPost]
        public IActionResult StartOnboarding([FromBody] OnboardingModel model)
        {
            // var result = _onboardingService.StartOnboarding(model);
            // return Ok(result);
            return Ok();
        }
    }
}