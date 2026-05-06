using com.koyok.democratia.Models;
using com.koyok.democratia.test.Localization;
using com.koyok.democratia.Utils;
using com.koyok.democratia.ViewModels.internaute;
using System.Globalization;



namespace com.koyok.democratia.test.ViewModels.internaute
{

    public class MainPageViewModelTest
    {
        private IServiceProvider _serviceProvider;
        
        public MainPageViewModelTest()
        {
        }

        [Fact(DisplayName = "Cas de base")]
        public async Task ConnecterInternauteTest()
        {
            

        }

        [Theory(DisplayName="En cas de renvoie inattendu du serveur")]
        [InlineData("<html>", "Erreur lors de la récupération des données")]
        [InlineData("{\"data\" : 1 }", "Erreur lors de la connexion du compte")]
        public async Task NotTextExceptedError(string? fakeResponse, string? messageAttendu)
        {
            
        }


        [Fact(DisplayName = "Simulation d'erreur de connexion")]
        public async Task ConnecterInternauteErrorInternetTest()
        {
            

        }

        [Theory(DisplayName = "Différentes fautes de frappes possible")]
        [InlineData("fezfzfzefz", "Djonodo20050207/", "Aucun internaute trouvé avec cette adresse mail")]
        [InlineData("modadary56@gmail.com", "Djonodo20050207/erreur", "Mot de passe incorrecte")]
        [InlineData("", "", "Veuillez saisir votre adresse mail")]
        [InlineData("dadadzadzada", "", "Veuillez saisir votre mot de passe")]
        public async Task ConnecterInternauteErrorIdentificationTest(string? identifiant, string? motDePasse, string? messageDerreur)
        {
        }
    }
}
