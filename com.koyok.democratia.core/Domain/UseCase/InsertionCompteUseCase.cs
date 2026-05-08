using com.koyok.democratia.Domain.Enumerations;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Utils;

namespace com.koyok.democratia.core.Domain.UseCase
{
    public class InsertionCompteUseCase(IInternauteRepository repository)
    {
        private readonly IInternauteRepository repository = repository;

        public async Task InsertAsync(TypeGestion type, Internaute internaute)
        {
            bool mailValide = await VerifierMailDoublon(internaute);
            if (mailValide)
            {
                await Verification.HasherMotDePasse(internaute!);
                string reponse = type == TypeGestion.AJOUTER ? 
                    await repository?.CreateModelAsync(internaute)! : await repository?.UpdateModelAsync(internaute!)!;
                List<object> values = repository!.RecuprerInformationConnexion<object>(reponse);
                if(values.Count > 0) throw new ConnexionErrorException();
            }
            else throw new CompteExistantException();
        }

        private async Task<bool> VerifierMailDoublon(Internaute internaute)
        {
            string retourJson = await repository?.DoublonEmailAsync(internaute!.courriel!)!;
            List<Dictionary<string, int>>? listeInformation = repository!.RecuprerInformationConnexion<Dictionary<string, int>>(retourJson);
            int? nombreMail = listeInformation![0].TryGetValue("COUNT(courriel)", out var value) ? value : null;
            return nombreMail == 0 ? true : throw new CompteExistantException();
        }

    }
}
