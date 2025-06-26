using com.democratia.ViewModels;

namespace com.democratia.test.ViewModels
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
        
        
        [Fact(DisplayName = "Cas de base")]  
        public async Task TestCreationViewModel()
        {
            creationViewModel?.AdresseMail = "email@example.com";
            creationViewModel?.NomDeFamille = "Doe";
            creationViewModel?.Prenom = "John";
            creationViewModel?.AdressePostal = "123 Rue de Stain";
            creationViewModel?.MotDePasse = "ElouiComme/20";

            await creationViewModel?.CreerInternaute() !;

            Assert.NotNull(creationViewModel);
            Assert.Equal("Votre compte a été créé", creationViewModel?.RetourMessage);

        }

        [Theory(DisplayName = "Différents cas d'erreurs lors de la création d'un compte")]
        [InlineData("", "", "", "", "","Un ou plusieurs champs sont vides")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "dzadazda", "Djonodo8/","L'adresse mail est mal formaté")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "email@example.com", "Djonodo/","Le mot de passe n'est pas assez fort, il faut 8 caractères dont au moins une majuscule, une minuscule, un caractère spécial")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "modadary56@gmail.com", "Djonodo8/", "L'adresse mail est déjà utilisée par un autre compte")]
        public async Task TestCreationViewModelError(string? prenom, string? nomDeFamille, string? adresseMail, string? adressePostale, string? motDePasse, string? messageDeRetour)
        {
            creationViewModel?.AdresseMail = adresseMail;
            creationViewModel?.NomDeFamille = nomDeFamille;
            creationViewModel?.Prenom = prenom;
            creationViewModel?.AdressePostal = adressePostale;
            creationViewModel?.MotDePasse = motDePasse;

            await Assert.ThrowsAsync<Exception>(async () => await creationViewModel?.CreerInternaute() !);

            Assert.NotNull(creationViewModel);
            Assert.Equal(messageDeRetour, creationViewModel?.RetourMessage);

        }
    }
}
