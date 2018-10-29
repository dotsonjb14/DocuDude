using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using docudude.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace docudude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        public ActionResult<string> Get()
        {
            return "ok";
        }
    }
}