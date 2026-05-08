using com.koyok.democratia.Domain.Enumerations;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Utils;
using Microsoft.Maui.Storage;

namespace com.koyok.democratia.Domain.UseCase
{
    public class AuthenticateUseCase(IInternauteRepository repository)
    {
        private readonly IInternauteRepository internauteRepository = repository;

        public async Task<Internaute> Authenticate(string adresseMail, string motDePasse)
        {
            await SecureStorage.Default.SetAsync(SecureStorageKeys.IdInternaute.ToString(), adresseMail);
            await SecureStorage.Default.SetAsync(SecureStorageKeys.MotDePasseInternaute.ToString(), motDePasse);
            string jsonString = await internauteRepository?.GetModelAsync(adresseMail)!;
            List<Internaute> listeInformation = internauteRepository.RecuprerInformationConnexion<Internaute>(jsonString);
            if (listeInformation.Count == 0) throw new NoUserException();
            var internaute = listeInformation[0];
            string motDePasseHash = internaute!.hashageMDP!;
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
                estAuthetifie = !await Verification.VerifierMotDePasseUtilisateur(internaute!.tempMDP!, motDePasseHash);
#endif
            if (!estAuthetifie) throw new BadPasswordException();
            return internaute;
        }
    }
}
