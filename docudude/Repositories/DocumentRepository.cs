﻿using docudude.Models;
using docudude.Models.Steps;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.IO;

namespace docudude.Repositories
{
    public class DocumentRepository
    {
        public byte[] Perform(Input input, byte[] file)
        {
            using (var outputStream = new MemoryStream())
            using (var inputStream = new MemoryStream(file))
            {
                var writer = new PdfWriter(outputStream);
                var reader = new PdfReader(inputStream);
                var pdfDoc = new PdfDocument(reader, writer);
                var doc = new Document(pdfDoc);

                foreach(var step in input.Steps)
                {
                    var data = step.GetData<TextInputData>();
                    var page = pdfDoc.GetPage(data.Page);
                    float bottom = GetBottom(data, page);

                    var text = new Text(data.Content)
                        .SetFontSize(data.FontSize);

                    Paragraph p = new Paragraph(text);

                    p.SetPageNumber(data.Page)
                        .SetFixedPosition(data.Left, bottom, 500);
                    doc.Add(p);
                }

                doc.Close();

                return outputStream.ToArray();
            }
        }

        private static float GetBottom(TextInputData data, PdfPage page)
        {
            var height = page.GetCropBox().GetHeight();
            var bottom = height - data.Top;
            return bottom;
        }

        public void GenerateTestDoc()
        {
            using (var outputStream = new MemoryStream())
            {
                var writer = new PdfWriter(outputStream);
                var doc = new PdfDocument(writer);

                var page = doc.AddNewPage();

                var document = new Document(doc);

                document.Close();

                File.WriteAllBytes("c:\\dev\\blank.pdf", outputStream.ToArray());
            }
        }
    }
}