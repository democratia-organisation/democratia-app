using com.democratia.ViewModels;

namespace com.democratia.test.ViewModels
{
    
    public class MainPageViewModelTest
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MainPageViewModel? mainPageViewModel;

        public MainPageViewModelTest()
        {
            _serviceProvider = TestServiceCollection.CreateTestServiceProviderForMainViewModel();
            mainPageViewModel = _serviceProvider.GetRequiredService<MainPageViewModel>();
            mainPageViewModel.AdresseMail = "modadary56@gmail.com";
            mainPageViewModel.MotDePasse = "Djonodo20050207/";
            mainPageViewModel.ErrorMessage = null;
        }

        [Fact]
        public async Task ConnecterInternauteTest()
        {
            var internaute = await mainPageViewModel!.ConnecterInternaute();
            Assert.NotNull(internaute);
            Assert.NotNull(internaute.id_internaute);
            Assert.Null(mainPageViewModel.ErrorMessage);
            Assert.Equal(158, internaute.id_internaute);
            Assert.Equal("Darouèche", internaute.nom_internaute);
            Assert.Equal("19 Rue Saint-Merry", internaute.adresse_postal);
            Assert.Equal("Naherry", internaute.prenom_internaute);
            Assert.Equal(mainPageViewModel.AdresseMail, internaute.courriel);

        }

        [Theory]
        [InlineData("", "Djonodo20050207/")]
        [InlineData("modadary56@gmail.com", "Djonodo20050207/erreur")]
        public async Task ConnecterInternauteErrorIdentificationTest(string? identifiant, string? motDePasse)
        {
            mainPageViewModel!.AdresseMail = identifiant;
            mainPageViewModel.MotDePasse = motDePasse;
            await Assert.ThrowsAsync<Exception>(async () => await mainPageViewModel.ConnecterInternaute());
        }
    }
}
