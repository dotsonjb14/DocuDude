using System.Threading.Tasks;

namespace docudude.Repositories
{
    public interface IS3Repository
    {
        Task<byte[]> GetDocument(string bucket, string key);
    }
}