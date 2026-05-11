using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Domain.Models;
using System.Text.Json;

namespace com.koyok.democratia.Data.Mapper.RemoteToDomain
{
    public interface IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel;
    }

    public class InternauteRemoteToDomain : IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            var remoteSource =  JsonSerializer.Deserialize<InternauteRemoteSource>(source)!;
            var internaute = new Internaute(
                remoteSource.id_internaute,
                remoteSource.nom_internaute,
                remoteSource.prenom_internaute,
                remoteSource.adresse_postale,
                remoteSource.courriel,
                remoteSource.hashageMDP
            );
            return internaute as T;
        }
    }

    public class PropositionRemoteToDomain : IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            var remoteSource = JsonSerializer.Deserialize<PropositionRemoteSource>(source)!;
            var proposition = new Proposition(
                remoteSource.id_proposition,
                remoteSource.titre_proposition,
                remoteSource.description_proposition,
                remoteSource.date_publication,
                remoteSource.budget,
                remoteSource.nb_signalement,
                remoteSource.id_thematique,
                remoteSource.id_groupe
            );
            return proposition as T;
        }
    }

    public class ThematiqueRemoteToDomain : IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            var remoteSource = JsonSerializer.Deserialize<ThematiqueRemoteSource>(source)!;
            var thematique = new Thematique(
                remoteSource.id_thematique,
                remoteSource.nom_thematique,
                remoteSource.budget_thematique,
                remoteSource.budget
            );
            return thematique as T;
        }
    }

    public class GroupeRemoteToDomain : IRemoteToDomain
    {
        public T? Mapping<T>(string source) where T : class, IModel
        {
            var remoteSource = JsonSerializer.Deserialize<GroupeRemoteSource>(source)!;
            var groupe = new Groupe(
                remoteSource.id_groupe,
                remoteSource.nom_groupe,
                remoteSource.couleur_groupe,
                remoteSource.image,
                remoteSource.budget,
                remoteSource.nbj_dft_vote,
                remoteSource.nbj_dft_discuss,
                remoteSource.nb_signalement
            );
            return groupe as T;
        }
    }
}
