using com.democratia.core.Utils;
using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.Json;
using System.Text.RegularExpressions;
using Xunit.Abstractions;
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
        private readonly ILocalizationService? localizationService;

        public CreationViewModel(INavigationService? navigationService, IEnumerable<IClient?>? clients, ILocalizationService service, ILocalizationService localizationService)
            : base(clients?.OfType<InternauteClient>().FirstOrDefault(), localizationService)
        {
            this.navigationService = navigationService;
            client ??= clients?.OfType<FakeClient>().FirstOrDefault();
            this.localizationService = service;
        }

        public CreationViewModel() : base(null, null) { }

        [RelayCommand]
        public async Task NavigateTapped(string commande) => await navigationService?.GoToAsync(commande)!;

        [RelayCommand]
        public async Task CreerInternauteTapped()
        {
            try
            {
                await CreerInternaute();
                RetourMessage = $"{localizationService?.GetString("BonneNouvelle")}";

            }
            catch (Exception ex)
            {
#if DEBUG
                RetourMessage = ex.Message;
#elif !DEBUG
                RetourMessage = localizationService?.GetString("erreurInattendu");    
#endif

            }
        }


        internal async Task CreerInternaute()
        {
            try
            {
                if (await VerifierToutesLesConditions())
                {

                    string reponse = await client?.CreateModelAsync(NomDeFamille, Prenom, AdresseMail, AdressePostal, Crypt.HashPassword(MotDePasse))!;
                    List<object> values = RecuprerInformationConnexion(reponse);
                    if (values.Count != 0) throw new Exception($"{localizationService?.GetString("erreurCreation")}");
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
                throw new Exception($"{localizationService?.GetString("champComplet")}");
            else return true;
        }
        private bool VerifierFormatageMail()
        {
            FormatRule emailRule = new(@"^[\w.\+\-]+@[\w\-]+\.[A-Za-z]{2,}$");
            return emailRule.Check(AdresseMail!) ? true : throw new Exception($"{localizationService?.GetString("formatEmail")}");
        }
        private async Task<bool> VerifierMailDoublon()
        {
            string retourJson = await ((InternauteClient?)client)?.DoublonEmailAsync(AdresseMail!)!;
            List<object>? listeInformation = RecuprerInformationConnexion(retourJson);
            int? nombreMail = JsonSerializer.Deserialize<Dictionary<string, int>>(listeInformation[0].ToString()!)!.TryGetValue("COUNT(courriel)", out var value) ? value : null;
            return nombreMail == 0 ? true : throw new Exception($"{localizationService?.GetString("compteExistantErreur")}");
        }
        private bool VerifierFormatageMotDePasse()
        {
            FormatRule passwordRule = new(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$");
            return passwordRule.Check(MotDePasse!) ? true : throw new Exception($"{localizationService?.GetString("formatMotDePasse")}");
        }

        private async Task<bool> VerifierToutesLesConditions() => VerifierChampComplet() && VerifierFormatageMail() && await VerifierMailDoublon() && VerifierFormatageMotDePasse();


        private record FormatRule(string pattern)
        {
            private readonly Regex _regex = new(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);

            public bool Check(string value) => value is string str && _regex.IsMatch(str);
        }
    }
}
