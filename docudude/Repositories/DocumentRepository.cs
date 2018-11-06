using docudude.Models;
using docudude.Models.Steps;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Signatures;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using System.IO;
using System.Linq;
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
                
                // code from https://stackoverflow.com/questions/12470498/how-to-read-the-pfx-file
                using(var keyStream = new MemoryStream(certData))
                {
                    var store = new Pkcs12Store(keyStream, signingProperties.Password.ToCharArray());

                    string alias = store.Aliases.OfType<string>().First(x => store.IsKeyEntry(x));

                    var privateKey = store.GetKey(alias).Key;

                    var keyChain = store.GetCertificateChain(alias)
                        .Select(x => x.Certificate).ToArray();

                    IExternalSignature externalSignature = new PrivateKeySignature(privateKey, DigestAlgorithms.SHA256);

                    signer.SignDetached(externalSignature, keyChain, null, null, null, 0, PdfSigner.CryptoStandard.CADES);

                    return outputStream.ToArray();
                }
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
