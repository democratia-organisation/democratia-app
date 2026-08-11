using com.koyok.democratia.Lib;
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
                bool reponse = type == TypeGestion.AJOUTER ? 
                    await repository?.CreateModelAsync(internaute)! : await repository?.UpdateModelAsync(internaute!)!;
                if (!reponse) throw new System.Exception(); 
            }
            else throw new CompteExistantException();
        }

        private async Task<bool> VerifierMailDoublon(Internaute internaute)
        {
            return await repository?.DoublonEmailAsync(internaute!.courriel!)!;
            
        }

    }
}
