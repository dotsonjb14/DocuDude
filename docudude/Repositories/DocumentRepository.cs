using docudude.Models;
using docudude.Models.Steps;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Signatures;
using Org.BouncyCastle.Security;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace docudude.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        public readonly IS3Repository s3Repository;
        public async Task<byte[]> Sign(byte[] source, SigningProperties signingProperties)
        {
            using (var inputStream = new MemoryStream(source))
            using (var reader = new PdfReader(inputStream))
            using (var outputStream = new MemoryStream())
            {
                var stampProps = new StampingProperties();
                var signer = new PdfSigner(reader, outputStream, stampProps);

                signer.SetCertificationLevel(PdfSigner.CERTIFIED_NO_CHANGES_ALLOWED);

                var sap = signer.GetSignatureAppearance();

                sap.SetLocation(signingProperties.Location);
                sap.SetReason("Certification");
                sap.SetReuseAppearance(false);
                
                var certData = await s3Repository.GetDocument(signingProperties.Bucket, signingProperties.Key);
                var certificate = new X509Certificate2(certData, signingProperties.Password, X509KeyStorageFlags.Exportable);

                var bouncyCert = DotNetUtilities.FromX509Certificate(certificate);

                var pk = DotNetUtilities.GetKeyPair(certificate.PrivateKey).Private;

                var chain = new Org.BouncyCastle.X509.X509Certificate[] { bouncyCert };

                IExternalSignature externalSignature = new PrivateKeySignature(pk, DigestAlgorithms.SHA256);

                signer.SignDetached(externalSignature, chain, null, null, null, 0, PdfSigner.CryptoStandard.CADES);

                return outputStream.ToArray();
            }
        }

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
