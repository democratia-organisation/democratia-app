using com.koyok.democratia.Lib;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Utils;
using Microsoft.Maui.Storage;
using System.Diagnostics;

namespace com.koyok.democratia.Domain.UseCase
{
    public class AuthenticateUseCase(IInternauteRepository repository)
    {
        private readonly IInternauteRepository internauteRepository = repository;

        public async Task<Internaute?> Authenticate(string adresseMail, string motDePasse)
        {
            string? stringTime = await SecureStorage.Default.GetAsync(SecureStorageKeys.LastLogin.ToString());
            if (stringTime is not null)
            {
                TimeSpan span = DateTime.UtcNow - DateTime.Parse(stringTime);
                if (span.Days > 7)
                {
                    SecureStorage.Default.Remove(SecureStorageKeys.IdInternaute.ToString());
                    SecureStorage.Default.Remove(SecureStorageKeys.MotDePasseInternaute.ToString());
                    await SecureStorage.Default.SetAsync(SecureStorageKeys.LastLogin.ToString(), DateTime.Now.ToString("U"));
                    return null;
                }
            }
            Task adresseMailTask = SecureStorage.Default.SetAsync(SecureStorageKeys.IdInternaute.ToString(), adresseMail);
            Task motDePasseTask = SecureStorage.Default.SetAsync(SecureStorageKeys.MotDePasseInternaute.ToString(), motDePasse);
            List<Internaute> listeInformation = [];
            Task listeRun = Task.Run(async () => listeInformation = [.. (await internauteRepository?.GetModelAsync(adresseMail, motDePasse)!).Cast<Internaute>()]);
            await Task.WhenAll(adresseMailTask, motDePasseTask, listeRun);
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
            await SecureStorage.Default.SetAsync(SecureStorageKeys.LastLogin.ToString(), DateTime.Now.ToString("U"));
            return internaute;
        }
    }
}
