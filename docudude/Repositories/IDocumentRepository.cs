using docudude.Models;

namespace docudude.Repositories
{
    public interface IDocumentRepository
    {
        byte[] Perform(Input input, byte[] file);
    }
}