using System.Threading.Tasks;
using docudude.Models;

namespace docudude.Repositories
{
    public interface IKSMRepository
    {
        string DecryptData(string data, byte[] key);
        Task<byte[]> GetKey(KMSData data);
    }
}