using com.koyok.democratia.ViewModels.internaute.gestionCompte;

namespace com.koyok.democratia.test.ViewModels.internaute.gestionCompte
{
    public class CreationViewModelTest
    {
        private readonly IServiceProvider _serviceProvider;
        
        public CreationViewModelTest()
        {
            
        }

        [Fact(DisplayName = "Simulation d'erreur de connexion")]
        public async Task ConnecterInternauteErrorInternetTest()
        {
            

        }


        [Fact(DisplayName = "Cas de base")]
        public async Task TestCreationViewModel()
        {
            

        }

        [Theory(DisplayName = "Différents cas d'erreurs lors de la création d'un compte")]
        [InlineData("", "", "", "", "", "Un ou plusieurs champs sont vides")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "dzadazda", "Djonodo8/", "L'adresse mail est mal formaté")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "email@example.com", "Djonodo/", "Le mot de passe n'est pas assez fort, il faut 8 caractères dont au moins une majuscule, une minuscule, un caractère spécial")]
        [InlineData("hello", "bonjour", "132 rue de Lyon", "modadary56@gmail.com", "Djonodo8/", "L'adresse mail est déjà utilisée par un autre compte")]
        public async Task TestCreationViewModelError(string? prenom, string? nomDeFamille, string? adresseMail, string? adressePostale, string? motDePasse, string? messageDeRetour)
        {
        }
    }
}
