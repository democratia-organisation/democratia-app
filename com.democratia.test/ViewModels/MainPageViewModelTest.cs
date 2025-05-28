using com.democratia.ViewModels;

namespace com.democratia.test.ViewModels
{
    
    public class MainPageViewModelTest
    {
        [Fact]
        public void ConnecterInternauteTest()
        {
            MainPageViewModel mainPageViewModel = new()
            {
                AdresseMail = "modaherry@gmail.com",
                MotDePasse = "Djonodo20050207/",
                ErrorMessage = null
            };
            var internaute = mainPageViewModel.ConnecterInternaute();
            
            
            Assert.NotNull(internaute);
            Assert.Null(mainPageViewModel.ErrorMessage);
            Assert.Equal(internaute.courriel, mainPageViewModel.AdresseMail);
            Assert.NotNull(internaute.role);
            Assert.NotNull(internaute.id_internaute);
            Assert.NotNull(internaute.nom_internaute);
            

        }
    }
}
