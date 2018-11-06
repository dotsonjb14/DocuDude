using System.Threading.Tasks;
using docudude.Models;

namespace docudude.Repositories
{
    public interface IDocumentRepository
    {
        byte[] Transform(Input input, byte[] file);
        Task<byte[]> Sign(byte[] source, SigningProperties signingProperties);
    }
}
