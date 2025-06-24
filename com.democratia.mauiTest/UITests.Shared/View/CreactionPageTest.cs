using OpenQA.Selenium.Appium;
using System.ComponentModel;
using Xunit;

namespace UITests.View
{
    [Collection("UITest")]
    [DisplayName("Création de compte")]
    public class CreationPageTest : BaseTest
    {

        public CreationPageTest() : base()
        {
            new AppiumSetup();
            if (SystemInfo.SSHHost()) return;
            AppiumElement creationPage = FindUIElement("Créez en un !"); ;
            creationPage.Click();
        }
        
        [Fact(DisplayName = "Création de compte")]
        public void CreationCompteTest()
        {
            if (SystemInfo.SSHHost()) return;
            
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }      
    }   
}
