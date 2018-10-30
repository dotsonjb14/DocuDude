using docudude.Models;
using docudude.Models.Steps;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;

namespace docudude.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public byte[] Transform(Input input, byte[] file)
        {
            using (var outputStream = new MemoryStream())
            {
                using (var inputStream = new MemoryStream(file))
                using (var writer = new PdfWriter(outputStream))
                using (var reader = new PdfReader(inputStream))
                using (var pdfDoc = new PdfDocument(reader, writer))
                using (var doc = new Document(pdfDoc))
                {
                    foreach(var step in input.Steps)
                    {
                        var data = step.GetData<TextInputData>();
                        float bottom = GetBottom(data, pdfDoc.GetPage(data.Page));

                        var text = new Text(data.Content)
                            .SetFontSize(data.FontSize);

                        doc.Add(new Paragraph(text)
                            .SetPageNumber(data.Page)
                            .SetFixedPosition(data.Left, bottom, 500));
                    }
                }

                return outputStream.ToArray();
            }
        }

        private float GetBottom(TextInputData data, PdfPage page)
        {
            var height = page.GetCropBox().GetHeight();
            var bottom = height - data.Top;
            return bottom;
        }
    }
}
