using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using docudude.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace docudude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        IConfiguration configuration;

        public HealthCheckController(IConfiguration configuration) 
        {
            this.configuration = configuration;
        }
        public ActionResult<HealthCheckData> Get()
        {
            return new HealthCheckData() 
            {
                Profile = configuration.GetValue<string>("s3:AwsProfile")
            };
        }
    }

    public class HealthCheckData 
    {
        public string Profile { get; set; }
    }
}
