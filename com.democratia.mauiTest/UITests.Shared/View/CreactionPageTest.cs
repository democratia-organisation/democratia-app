using Xunit;

namespace UITests.View
{
    public class CreationPageTest : BaseTest, IClassFixture<CreationPageTestFixture>
    {
        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class CreationPageTestFixture : IDisposable
    {
        public CreationPageTestFixture()
        {
            // Initialization code for the fixture
        }
        public void Dispose()
        {
            // Cleanup code for the fixture
        }
    }   
}
