using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Text;
using docudude.Models;
using docudude.Repositories;
using iText.Kernel.Pdf;
using Moq;
using THT.AWS.Abstractions.S3;
using Xunit;

namespace docudude.tests.Repositories
{
    public class DocumentRepositoryTests
    {
        [Fact]
        public void CanInitialize()
        {
            var testDoc = GenerateTestDoc();
            var input = new Input();
            input.Steps = new List<InputStep>();

            var documentRepo = new DocumentRepository();

            var outDoc = documentRepo.Perform(input, testDoc);
        }

        [Fact(Skip = "meh")]
        public void AddText()
        {
            var testDoc = GenerateTestDoc();
            var input = new Input();
            input.Steps = new List<InputStep>();
            input.Steps.Add(new InputStep()
            {
                
            });

            var documentRepo = new DocumentRepository();

            var outDoc = documentRepo.Perform(input, testDoc);
        }

        private byte[] GenerateTestDoc()
        {
            using(var outputStream = new MemoryStream()) 
            using(var writer = new PdfWriter(outputStream)) 
            using(var pdfDoc = new PdfDocument(writer)) 
            {
                pdfDoc.AddNewPage();
                pdfDoc.Close();

                return outputStream.ToArray();
            }
            
        }
    }
}
