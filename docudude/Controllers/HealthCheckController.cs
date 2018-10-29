using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using docudude.Models;
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
        private readonly IConfiguration configuration;
        private readonly Whitelists whitelists;

        public HealthCheckController(IConfiguration configuration, Whitelists whitelists) 
        {
            this.configuration = configuration;
            this.whitelists = whitelists;
        }
        public ActionResult<HealthCheckData> Get()
        {
            return new HealthCheckData() 
            {
                PDFBuckets = whitelists.PDFBuckets,
                ImageBuckets = whitelists.ImageBuckets
            };
        }
    }

    public class HealthCheckData 
    {
        public List<string> PDFBuckets { get; internal set; }
        public List<string> ImageBuckets { get; internal set; }
    }
}
