using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Service;
using com.koyok.democratia.Domain.UseCase;
using com.koyok.democratia.Domain.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using AppContext = com.koyok.democratia.Domain.Utils.AppContext;

namespace com.koyok.democratia.UI
{
    public partial class MainViewModel(AuthenticateUseCase useCase, AppContext context) : ObservableObject
    {
        [ObservableProperty]
        public partial string? adresseMail { get; set; }

        public Internaute? modele { get; private set; }
        private AppContext contexte = context;

        [ObservableProperty]
        public partial string? motDePasse { get; set; }

        [ObservableProperty]
        public partial string? errorMessage { get; set; }
        
        private readonly AuthenticateUseCase useCase = useCase;

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            if (commande == "CreationPage")
            {
                await Shell.Current!.GoToAsync(commande);
            }
            else
            {
                try
                {
                    modele = await ConnecterInternaute();
                }
                catch (Exception ex)
                {
                    errorMessage = contexte.Mapper!.MappingException(ex);
                }

                contexte!.Internaute = modele;
                var parameters = new ShellNavigationQueryParameters { { "modele", modele! } };
                await Shell.Current!.GoToAsync(commande, parameters);
            }

        }

        internal async Task<Internaute?> ConnecterInternaute()
        {

            if (string.IsNullOrWhiteSpace(adresseMail)) throw new EmptyEmailFieldException();
            else if (string.IsNullOrWhiteSpace(motDePasse)) throw new EmptyPassWordFieldException();
            try
            {
                await SecureStorage.Default.SetAsync("id_internaute", adresseMail);
                string jsonString = await client?.GetModelAsync(adresseMail)!;
                List<Internaute> listeInformation = RecuprerInformationConnexion<Internaute>(jsonString);
                if (listeInformation.Count == 0) throw new NoUserException();
                var internaute = listeInformation![0];
                string motDePasseHash = internaute?.hashageMDP!;
                bool estAuthetifie;
#if DEBUG
                if (motDePasseHash != "root")
                // les mots de passe avec le mot root ne vont pas dans tempMDP pour éviter une erreur
                {
                    internaute!.tempMDP = motDePasse; // utilisation de internaute.tempMDP car son set vérifie le format du mot de passe
                    bool hashedPasswordIsNotEqual = !await Verification.VerifierMotDePasseUtilisateur(internaute!.tempMDP!, motDePasseHash);
                    estAuthetifie = hashedPasswordIsNotEqual;
                }
                else
                    estAuthetifie = motDePasseHash == motDePasse;
#elif !DEBUG
                internaute!.tempMDP = motDePasse;
                estAuthetifie = !await Verification.VerifiermotDePasseUtilisateur(internaute!.tempMDP!, motDePasseHash);;
#endif
                if (!estAuthetifie) throw new BadPasswordException();
                else
                {
                    EnregistrerModele(internaute!);
                    return internaute;
                }
            }
            catch (Exception)
            { throw; }
        }
    }
}