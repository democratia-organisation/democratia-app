using com.koyok.democratia.Domain.Enumerations;
using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Utils;
using System.Text.Json;

namespace com.koyok.democratia.Domain.UseCase
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
            }
            else throw new CompteExistantException();
        }

        private async Task<bool> VerifierMailDoublon(Internaute internaute)
        {
            string retourJson = await repository?.DoublonEmailAsync(internaute!.courriel!)!;
            var tableau = JsonSerializer.Deserialize<Dictionary<string, object>>(retourJson);
            var reponse = tableau!["data"] as List<Dictionary<string,object>>;
            return int.Parse(reponse![0]["COUNT(courriel)"].ToString()!) == 0;
        }

    }
}
