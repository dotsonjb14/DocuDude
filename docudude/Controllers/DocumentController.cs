using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using docudude.Models;
using docudude.Models.Steps;
using docudude.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace docudude.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository documentRepository;
        private readonly IS3Repository s3Repository;

        public DocumentController(IDocumentRepository documentRepository, IS3Repository s3Repository)
        {
            this.documentRepository = documentRepository;
            this.s3Repository = s3Repository;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Input input)
        {
            var source = await s3Repository.GetDocument(input.SourceBucket, input.sourceFile);

            var output = documentRepository.Perform(input, source);

            System.IO.File.WriteAllBytes("c:\\dev\\out.pdf", output);
            
            return File(output, "application/pdf", Guid.NewGuid().ToString() + ".txt");
        }


    }
}