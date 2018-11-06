using docudude.Models;

namespace docudude.Repositories
{
    public interface IDocumentRepository
    {
        byte[] Transform(Input input, byte[] file);
        byte[] Sign(byte[] source, SigningProperties signingProperties);
    }
}
