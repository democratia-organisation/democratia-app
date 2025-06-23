using Xunit;

namespace UITests.View
{
    public class CreationPageTest : BaseTest, IClassFixture<CreationPageTestFixture>
    {
        private readonly MainPageTestFixture? _mainPageTestFixture;

        public CreationPageTest(MainPageTestFixture? mainPageTestFixture)
        {
            if (SystemInfo.SSHHost()) return;
            if (AppiumSetup.device == "windows") AppiumSetup.RunBeforeAnyTests();
            _mainPageTestFixture = mainPageTestFixture;

        }

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
