using Amazon;
using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using docudude.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace docudude.Repositories
{
    public class KSMRepository : IKSMRepository
    {
        public async Task<byte[]> GetKey(KMSData data)
        {
            using (var kmsClient = new AmazonKeyManagementServiceClient(RegionEndpoint.GetBySystemName(data.Region)))
            using (var inputStream = new MemoryStream(Convert.FromBase64String(data.CypherText)))
            {
                var key = await kmsClient.DecryptAsync(new DecryptRequest()
                {
                    CiphertextBlob = inputStream
                });

                return key.Plaintext.ToArray();
            }
        }

        public string DecryptData(string data, byte[] key)
        {
            throw new NotImplementedException("next bro");
        }
    }
}
