using System.Threading.Tasks;
using THT.AWS.Abstractions.S3;

namespace docudude.Repositories
{
    public class S3Repository
    {
        private readonly IFileWrapper fileWrapper;

        public S3Repository(IFileWrapper fileWrapper)
        {
            this.fileWrapper = fileWrapper;
        }

        public async Task<byte[]> GetDocument(string bucket, string key)
        {
            return await fileWrapper.ReadAsync(bucket, key);
        }
    }
}
