using com.democratia.Models;
using com.democratia.ViewModels;

namespace com.democratia.test.ViewModels
{
    
    public class MainPageViewModelTest
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MainPageViewModel? mainPageViewModel;

        // TODO : tester :
        //      - NavigateTapped via Appium
        //      - si le serveur renvoie une page html au lieu d'un json
        //      - si les données json ne sont ni un tableau ni un nombre

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
            Internaute? internaute = await mainPageViewModel!.ConnecterInternaute();

            Assert.NotNull(internaute);
            Assert.NotNull(internaute.id_internaute);
            Assert.Null(mainPageViewModel.ErrorMessage);
            Assert.Equal(158, internaute.id_internaute);
            Assert.Equal("Darouèche", internaute.nom_internaute);
            Assert.Equal("19 Rue Saint-Merry", internaute.adresse_postal);
            Assert.Equal("Naherry", internaute.prenom_internaute);
            Assert.Equal(mainPageViewModel.AdresseMail, internaute.courriel);

        }

        [Fact]
        public async Task ConnecterInternauteErrorInternetTest()
        {
            mainPageViewModel!.Client!.SetPort(1234); // Port incorrect pour provoquer une erreur de connexion
            
            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await mainPageViewModel!.ConnecterInternaute());
            
            Assert.Equal("Erreur de connexion inattendu", exception.Message);

        }

        [Theory]
        [InlineData("fezfzfzefz", "Djonodo20050207/", "Aucun internaute trouvé avec cette adresse mail")]
        [InlineData("modadary56@gmail.com", "Djonodo20050207/erreur", "Mot de passe incorrecte")]
        [InlineData("", "", "Veuillez saisir votre adresse mail")]
        [InlineData("dadadzadzada", "", "Veuillez saisir votre mot de passe")]
        public async Task ConnecterInternauteErrorIdentificationTest(string? identifiant, string? motDePasse, string? messageDerreur)
        {

            mainPageViewModel!.AdresseMail = identifiant;
            mainPageViewModel.MotDePasse = motDePasse;
            Exception exception;

            if (string.IsNullOrEmpty(mainPageViewModel.MotDePasse) || string.IsNullOrEmpty(mainPageViewModel.AdresseMail))
                exception = await Assert.ThrowsAsync<ArgumentException>(async () => await mainPageViewModel!.ConnecterInternaute());
            
            else
                exception = await Assert.ThrowsAsync<Exception>(async () => await mainPageViewModel.ConnecterInternaute());

            Assert.Equal(messageDerreur, exception.Message);

        }
    }
}
