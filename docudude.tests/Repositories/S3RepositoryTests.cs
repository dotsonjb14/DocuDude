using docudude.Repositories;
using Moq;
using THT.AWS.Abstractions.S3;
using Xunit;

namespace docudude.tests.Repositories
{
    public class S3RepositoryTests
    {
        [Fact]
        public void CanInitialize() 
        {
            var fileWrapper = new Mock<IFileWrapper>();

            var x = new S3Repository(fileWrapper.Object);
        }
    }
}