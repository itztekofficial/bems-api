using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Login.Api.Controllers
{
    /// <summary>
    /// StatusController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;

        /// <summary>
        /// StatusController
        /// </summary>
        /// <param name="logger"></param>+
        public StatusController(ILogger<StatusController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "Gt")]
        public string Get()
        {
            return "OK";
        }
    }
}