using CraftMan_WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace CraftMan_WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocalizationController : ControllerBase
    {
        private readonly IStringLocalizer _localizer;

        public LocalizationController(IStringLocalizerFactory factory)
        {
            var type = typeof(LocalizationController); // Can be any class from your assembly
            _localizer = factory.Create("Labels", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);
        }

        [HttpGet("{culture}")]
        public IActionResult GetLocalizedStrings(string culture)
        {
            try
            {
                // Set culture dynamically for the current request
                var ci = new System.Globalization.CultureInfo(culture);
                System.Globalization.CultureInfo.CurrentCulture = ci;
                System.Globalization.CultureInfo.CurrentUICulture = ci;

                var localizedStrings = _localizer.GetAllStrings()
                    .Select(s => new LocalizedStringDto { Key = s.Name, Value = s.Value })
                    .ToList();

                return Ok(localizedStrings);
            }
            catch (CultureNotFoundException)
            {
                return BadRequest("Unsupported culture");
            }
        }
    }
}
