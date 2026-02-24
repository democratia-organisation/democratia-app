using com.democratia.Models;
using com.democratia.test.Localization;
using com.democratia.Utils;
using com.democratia.ViewModels.internaute;



namespace com.democratia.test.ViewModels.internaute
{

    public class MainPageViewModelTest
    {
        private IServiceProvider _serviceProvider;
        private MainViewModel? mainPageViewModel;

        public MainPageViewModelTest()
        {
            _serviceProvider = TestServiceCollection.CreateTestServiceProviderForMainViewModel();
            mainPageViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainPageViewModel.AdresseMail = "modadary56@gmail.com";
            mainPageViewModel.MotDePasse = "Djonodo20050207/";
            mainPageViewModel.ErrorMessage = null;
            AppResources.Culture = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        }

        [Fact(DisplayName = "Cas de base")]
        public async Task ConnecterInternauteTest()
        {
            Internaute? internaute = await mainPageViewModel!.ConnecterInternaute();

            Assert.NotNull(internaute);
            Assert.NotNull(internaute.id_internaute);
            Assert.Null(mainPageViewModel.ErrorMessage);
            Assert.Equal("Darouèche", internaute.nom_internaute);
            Assert.Equal("19 Rue Saint-Merry", internaute.adresse_postale);
            Assert.Equal("Naherry", internaute.prenom_internaute);
            Assert.Equal(mainPageViewModel.AdresseMail, internaute.courriel);

        }

        [Theory(DisplayName="En cas de renvoie inattendu du serveur")]
        [InlineData("<html>", "Erreur lors de la récupération des données")]
        [InlineData("{\"data\" : 1 }", "Erreur lors de la connexion du compte")]
        public async Task NotTextExceptedError(string? fakeResponse, string? messageAttendu)
        {
            _serviceProvider = TestServiceCollection.CreateFakeServiceProviderForMainViewModel(fakeResponse);
            mainPageViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainPageViewModel.AdresseMail = "modadary56@gmail.com";
            mainPageViewModel.MotDePasse = "Djonodo20050207/";

            var exception = await Assert.ThrowsAsync<ConnexionErrorException>(async () => await mainPageViewModel!.ConnecterInternaute());

            Assert.Equal(messageAttendu, exception.Message);
        }


        [Fact(DisplayName = "Simulation d'erreur de connexion")]
        public async Task ConnecterInternauteErrorInternetTest()
        {
            mainPageViewModel!.Client!.SetPort(1234); // Port incorrect pour provoquer une erreur de connexion

            Exception exception = await Assert.ThrowsAsync<ConnexionErrorException>(async () => await mainPageViewModel!.ConnecterInternaute());

            Assert.Equal("Erreur de connexion inattendu", exception.Message);

        }

        [Theory(DisplayName = "Différentes fautes de frappes possible")]
        [InlineData("fezfzfzefz", "Djonodo20050207/", "Aucun internaute trouvé avec cette adresse mail")]
        [InlineData("modadary56@gmail.com", "Djonodo20050207/erreur", "Mot de passe incorrecte")]
        [InlineData("", "", "Veuillez saisir votre adresse mail")]
        [InlineData("dadadzadzada", "", "Veuillez saisir votre mot de passe")]
        public async Task ConnecterInternauteErrorIdentificationTest(string? identifiant, string? motDePasse, string? messageDerreur)
        {

            mainPageViewModel!.AdresseMail = identifiant;
            mainPageViewModel.MotDePasse = motDePasse;
            Exception exception;

            if (string.IsNullOrEmpty(mainPageViewModel.AdresseMail))
                exception = await Assert.ThrowsAsync<MailException>(async () => await mainPageViewModel!.ConnecterInternaute());
            if (string.IsNullOrEmpty(mainPageViewModel.MotDePasse))
                exception = await Assert.ThrowsAsync<PassWordException>(async () => await mainPageViewModel!.ConnecterInternaute());

            else
                exception = await Assert.ThrowsAnyAsync<Exception>(async () => await mainPageViewModel.ConnecterInternaute());

            Assert.Equal(messageDerreur, exception.Message);

        }
    }
}
