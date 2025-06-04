using com.democratia.ViewModels;

namespace com.democratia.test.ViewModels
{
    
    public class MainPageViewModelTest
    {
        [Fact]
        public async Task ConnecterInternauteTest()
        {
            MainPageViewModel mainPageViewModel = new()
            {
                AdresseMail = "modadary56@gmail.com",
                MotDePasse = "Djonodo20050207/",
                ErrorMessage = null,
            };
            var internaute = await mainPageViewModel.ConnecterInternaute();
            
            
            Assert.NotNull(internaute);
            Assert.Null(mainPageViewModel.ErrorMessage);
            Assert.Equal(internaute.courriel, mainPageViewModel.AdresseMail);
            Assert.NotNull(internaute.id_internaute);
            Assert.NotNull(internaute.nom_internaute);
            

        }
        // TODO : traiter les différentes cas limites dans d'autres tests
    }
}
