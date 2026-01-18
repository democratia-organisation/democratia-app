using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using System.Text.RegularExpressions;
using Crypt = BCrypt.Net.BCrypt;

namespace com.democratia.ViewModels
{
    public partial class CreationViewModel : ConnectableViewModel, INavigeablleViewModel
    {

        [ObservableProperty] private string? nomDeFamille;

        [ObservableProperty] private string? prenom;

        [ObservableProperty] private string? adressePostal;

        [ObservableProperty] private string? adresseMail;

        [ObservableProperty] private string? motDePasse;

        [ObservableProperty] private string? retourMessage;


        private readonly INavigationService? navigationService;
        private readonly IEnumerable<IClient?>? clients;

        public CreationViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients)
            : base(clients?.OfType<InternauteClient>().FirstOrDefault())
        {
            this.navigationService = navigationService;
            this.clients = clients;
            if (client is null)
                this.client = clients?.OfType<FakeClient>().FirstOrDefault();
        }

        public CreationViewModel() : base(null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande) => await navigationService?.GoToAsync("MainPage")!;

        [RelayCommand]
        public async Task CreerInternauteTapped()
        {
            try
            {
                await CreerInternaute();
                RetourMessage = "Création réussie";

            }
            catch (Exception ex)
            {
                RetourMessage = ex.Message; // TODO : à changer en phase de prod
            }
        }


        internal async Task CreerInternaute()
        {
            try
            {
                if (await VerifierToutesLesConditions())
                {

                    string reponse = await client?.CreateModelAsync(NomDeFamille, Prenom, AdresseMail, AdressePostal, Crypt.HashPassword(MotDePasse))!;
                    List<Dictionary<string,object>> values = RecuprerInformationConnexion(reponse);
                    int? valeurRetourne = ((JsonElement?)values?[0]["courriel"])?.GetInt32();
                    if (valeurRetourne != 0) throw new Exception("Erreur lors de la création du compte");
                }

            }
            catch (Exception)
            {
                throw;
            }

        }

        private bool VerifierChampComplet()
        {
            if (!(!string.IsNullOrEmpty(AdressePostal) &&
              !string.IsNullOrEmpty(NomDeFamille) &&
              !string.IsNullOrEmpty(Prenom) &&
              !string.IsNullOrEmpty(MotDePasse) &&
              !string.IsNullOrEmpty(AdresseMail)))
                throw new Exception("Vérifier que tous les champs soient complets");
            else return true;
        }
        private bool VerifierFormatageMail()
        {
            FormatRule emailRule = new(@"^([w.-]+)@([w-]+)((.(w){2,3})+)$");
            return emailRule.Check(AdresseMail!) ? true : throw new Exception("Le format de l'adresse mail est incorrecte");
        }
        private async Task<bool> VerifierMailDoublon()
        {
            string retourJson = await ((InternauteClient?)client)?.DoublonEmailAsync(AdresseMail!)!;
            List<Dictionary<string, object>>? listeInformation = RecuprerInformationConnexion(retourJson);
            int? nombreMail = ((JsonElement?)listeInformation?[0]["courriel"])?.GetInt32();
            return nombreMail == 0
                ? true
                : throw new Exception("L'adresse mail est déjà utilisée");
        }
        private bool VerifierFormatageMotDePasse()
        {
            FormatRule passwordRule = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$");
            return passwordRule.Check(MotDePasse!) ? true : throw new Exception("Le format du mot de passe est incorrecte");
        }

        private async Task<bool> VerifierToutesLesConditions() => VerifierChampComplet() && VerifierFormatageMail() && await VerifierMailDoublon() && VerifierFormatageMotDePasse();


        private record FormatRule(string pattern)
        {
            private readonly GeneratedRegexAttribute _regex = new(pattern);

            public bool Check(string value) =>
                value is string str && _regex.Match(str);
        }



    }
}
