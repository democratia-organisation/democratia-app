using com.democratia.ViewModels.internaute.gestionCompte;

namespace com.democratia.test.ViewModels.internaute.gestionCompte
{
    public class CreationViewModelTest
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CreationViewModel? creationViewModel;

        public CreationViewModelTest()
        {
            _serviceProvider = TestServiceCollection.CreateTestServiceProviderForCreationViewModel();
            creationViewModel = _serviceProvider.GetRequiredService<CreationViewModel>();
        }

        [Fact(DisplayName = "Simulation d'erreur de connexion")]
        public async Task ConnecterInternauteErrorInternetTest()
        {
            creationViewModel!.Client!.SetPort(1234); // Port incorrect pour provoquer une erreur de connexion

            Exception exception = await Assert.ThrowsAsync<Exception>(async () => await creationViewModel!.CreerInternaute());

            Assert.Equal("Erreur de connexion inattendu", exception.Message);

        }


        [Fact(DisplayName = "Cas de base")]
        public async Task TestCreationViewModel()
        {
            creationViewModel?.Internaute!.courriel = "email@example.com";
            creationViewModel?.Internaute!.nom_internaute = "Doe";
            creationViewModel?.Internaute!.prenom_internaute = "John";
            creationViewModel?.Internaute!.adresse_postale = "123 Rue de Stain";
            creationViewModel?.Internaute!.tempMDP = "ElouiComme/20";

            await creationViewModel?.CreerInternaute()!;

            Assert.NotNull(creationViewModel);
            Assert.Equal("Votre compte a été créé", creationViewModel?.RetourMessage);

        }

        [Theory(DisplayName = "Différents cas d'erreurs lors de la création d'un compte")]
        [InlineData("", "", "", "", "", "Un ou plusieurs champs sont vides")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "dzadazda", "Djonodo8/", "L'adresse mail est mal formaté")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "email@example.com", "Djonodo/", "Le mot de passe n'est pas assez fort, il faut 8 caractères dont au moins une majuscule, une minuscule, un caractère spécial")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "modadary56@gmail.com", "Djonodo8/", "L'adresse mail est déjà utilisée par un autre compte")]
        public async Task TestCreationViewModelError(string? prenom, string? nomDeFamille, string? adresseMail, string? adressePostale, string? motDePasse, string? messageDeRetour)
        {
            creationViewModel?.Internaute!.courriel = adresseMail;
            creationViewModel?.Internaute!.nom_internaute = nomDeFamille;
            creationViewModel?.Internaute!.prenom_internaute = prenom;
            creationViewModel?.Internaute!.adresse_postale = adressePostale;
            creationViewModel?.Internaute!.tempMDP = motDePasse;

            await Assert.ThrowsAsync<Exception>(async () => await creationViewModel?.CreerInternaute()!);

            Assert.NotNull(creationViewModel);
            Assert.Equal(messageDeRetour, creationViewModel?.RetourMessage);

        }
    }
}
