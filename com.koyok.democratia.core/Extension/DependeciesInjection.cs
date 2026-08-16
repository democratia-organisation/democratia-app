using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Mapper.LocalToDomain;
using com.koyok.democratia.Data.Mapper.RemoteToDomain;
using com.koyok.democratia.Data.Repository.LocalRepository;
using com.koyok.democratia.Data.Repository.RepositoryImpl;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.UseCase;
using com.koyok.democratia.UI;
using com.koyok.democratia.UI.groupe;
using com.koyok.democratia.UI.internaute;
using com.koyok.democratia.UI.internaute.CreerGroupe;
using com.koyok.democratia.UI.internaute.gestionCompte;
using Microsoft.Extensions.DependencyInjection;


namespace com.koyok.democratia.Extension
{
    public static class DependeciesInjection
    {
        extension(IServiceCollection services)
        {
            /// <summary>
            /// Méthode pour ajouter les services nécessaires à l'application.
            /// </summary>
            /// <returns>Retourne la collection de services après l'ajout des services.</returns>
            public IServiceCollection AddServices()
            {
                services.AddDataLocalSources();
                services.AddDataRemoteSources();
                services.AddLocalToDomain();
                services.AddRemoteToDomain();
                services.AddClient();
                services.AddLocalRepository();
                services.AddRepositoryImpl();
                services.AddUsesCases();
                services.AddTransientViewModel();
                return services;
            }

            public IServiceCollection AddRemoteToDomain()
            {
                services.AddSingleton<IRemoteToDomain, InternauteRemoteToDomain>();
                services.AddSingleton<IRemoteToDomain, GroupeRemoteToDomain>();
                services.AddSingleton<IRemoteToDomain, ThematiqueRemoteToDomain>();
                services.AddSingleton<IRemoteToDomain, PropositionRemoteToDomain>();
                services.AddSingleton<IRemoteToDomain, CommentaireRemoteToDomain>();
                services.AddSingleton<IRemoteToDomain, DeviceInstallationRemoteToDomain>();
                return services;
            }

            public IServiceCollection AddLocalToDomain()
            {
                services.AddSingleton<ILocalToDomain, InternauteLocalToDomain>();
                services.AddSingleton<ILocalToDomain, GroupeLocalToDomain>();
                services.AddSingleton<ILocalToDomain, ThematiqueLocalToDomain>();
                services.AddSingleton<ILocalToDomain, PropositionLocalToDomain>();
                services.AddSingleton<ILocalToDomain, CommentaireLocalToDomain>();
                return services;
            }

            public IServiceCollection AddUsesCases()
            {
                services.AddSingleton<AuthenticateUseCase>();
                services.AddSingleton<CreerGroupeUseCase>();
                services.AddSingleton<DeterminateRoleUseCase>();
                services.AddSingleton<InsertionCompteUseCase>();
                services.AddSingleton<ListeDonneeUseCase>();
                services.AddSingleton<IManipulateImage, ManipulateGroupeImageUseCase>();

                return services;
            }

            public IServiceCollection AddDataLocalSources()
            {
                services.AddSingleton<ILocalSource, InternauteLocalSource>();
                services.AddSingleton<ILocalSource, GroupeLocalSource>();
                services.AddSingleton<ILocalSource, ThematiqueLocalSource>();
                services.AddSingleton<ILocalSource, PropositionLocalSource>();
                services.AddSingleton<ILocalSource, CommentaireLocalSource>();
                return services;
            }

            public IServiceCollection AddDataRemoteSources()
            {
                services.AddSingleton<IRemoteSource, InternauteRemoteSource>();
                services.AddSingleton<IRemoteSource, GroupeRemoteSource>();
                services.AddSingleton<IRemoteSource, ThematiqueRemoteSource>();
                services.AddSingleton<IRemoteSource, PropositionRemoteSource>();
                services.AddSingleton<IRemoteSource, CommentaireRemoteSource>();
                services.AddSingleton<IRemoteSource, DeviceInstallationRemoteSource>();

                return services;
            }
            public IServiceCollection AddClient()
            {

                services.AddSingleton<IRepository>(s => s.GetRequiredService<IInternauteRepository>());
                services.AddSingleton<IRepository>(s => s.GetRequiredService<IGroupeRepository>());
                services.AddSingleton<IRepository>(s => s.GetRequiredService<IThematiqueRepository>());
                services.AddSingleton<IRepository>(s => s.GetRequiredService<IPropositionRepository>());
                services.AddSingleton<IRepository>(s => s.GetRequiredService<ICommentaireRepository>());

                return services;
            }

            public IServiceCollection AddLocalRepository()
            {
                services.AddSingleton<DataBaseConnexion>();
                services.AddSingleton<DataBaseCreation<InternauteLocalSource>>();
                services.AddSingleton<DataBaseCreation<GroupeLocalSource>>();
                services.AddSingleton<DataBaseCreation<ThematiqueLocalSource>>();
                services.AddSingleton<DataBaseCreation<PropositionLocalSource>>();
                services.AddSingleton<DataBaseCreation<CommentaireLocalSource>>();
                services.AddSingleton<InternauteLocalRepository>(s => new(s.GetRequiredService<DataBaseCreation<InternauteLocalSource>>(), s.GetServices<ILocalToDomain>().OfType<InternauteLocalToDomain>().FirstOrDefault()!));
                services.AddSingleton<GroupeLocalRepository>(s => new(s.GetRequiredService<DataBaseCreation<GroupeLocalSource>>(),s.GetServices<ILocalToDomain>().OfType<GroupeLocalToDomain>().FirstOrDefault()!));
                services.AddSingleton<ThematiqueLocalRepository>(s => new(s.GetRequiredService<DataBaseCreation<ThematiqueLocalSource>>(), s.GetServices<ILocalToDomain>().OfType<ThematiqueLocalToDomain>().FirstOrDefault()!));
                services.AddSingleton<PropositionLocalRepository>(s => new(s.GetRequiredService<DataBaseCreation<PropositionLocalSource>>(), s.GetServices<ILocalToDomain>().OfType<PropositionLocalToDomain>().FirstOrDefault()!));
                services.AddSingleton<CommentaireLocalRepository>(s => new(s.GetRequiredService<DataBaseCreation<CommentaireLocalSource>>(), s.GetServices<ILocalToDomain>().OfType<CommentaireLocalToDomain>().FirstOrDefault()!));

                return services;
            }

            public IServiceCollection AddRepositoryImpl()
            {

                services.AddSingleton<IInternauteRepository, InternauteRepository>();
                services.AddSingleton<IGroupeRepository, GroupeRepository>();
                services.AddSingleton<IThematiqueRepository, ThematiqueRepository>();
                services.AddSingleton<IPropositionRepository, PropositionRepository>();
                services.AddSingleton<ICommentaireRepository, CommentaireRepository>();

                return services;
            }

            private IServiceCollection AddTransientViewModel()
            {
                services.AddTransient<LoginViewModel>();
                services.AddTransient<MainViewModel>();
                services.AddTransient<CreationViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<HomeGestionViewModel>();
                services.AddTransient<PremiereCreationViewModel>();
                services.AddTransient<TroisiemeCreationViewModel>();
                services.AddTransient<GroupeViewModel>();
                services.AddTransient<ModifierGestionViewModel>();
                services.AddTransient<PreferenceViewModel>();
                services.AddTransient<PropositionViewModel>();
                services.AddTransient<NotificationViewModel>();
                services.AddTransient<ParametreViewModel>();

                return services;
            }
        }
    }
}
