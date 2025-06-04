using com.democratia.ViewModels;

namespace com.democratia.test.ViewModels
{
    
    public class MainPageViewModelTest
    {
        private readonly IServiceProvider _serviceProvider;

        public MainPageViewModelTest()
        {
            _serviceProvider = TestServiceCollection.CreateTestServiceProvider();
        }
        [Fact]
        public async Task ConnecterInternauteTest()
        {
            var mainPageViewModel = _serviceProvider.GetRequiredService<MainPageViewModel>();
            mainPageViewModel.AdresseMail = "modadary56@gmail.com";
            mainPageViewModel.MotDePasse = "Djonodo20050207/";
            mainPageViewModel.ErrorMessage = null;

            var internaute = await mainPageViewModel.ConnecterInternaute();

            Assert.NotNull(internaute);
            Assert.NotNull(internaute.id_internaute);
            Assert.Null(mainPageViewModel.ErrorMessage);
            Assert.Equal(158, internaute.id_internaute);
            Assert.Equal("Darouèche", internaute.nom_internaute);
            Assert.Equal("19 Rue Saint-Merry", internaute.adresse_postal);
            Assert.Equal("Naherry", internaute.prenom_internaute);
            Assert.Equal(mainPageViewModel.AdresseMail, internaute.courriel);

        }
        // TODO : traiter les différentes cas limites dans d'autres tests
    }
}
