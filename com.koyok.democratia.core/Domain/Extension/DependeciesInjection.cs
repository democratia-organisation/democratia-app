using com.koyok.democratia.Data.DataSource.Local;
using com.koyok.democratia.Data.DataSource.Remote;
using com.koyok.democratia.Data.Repository;
using com.koyok.democratia.Domain.Extension.DelegatesHandler;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.UseCase;
using com.koyok.democratia.UI;
using com.koyok.democratia.UI.groupe;
using com.koyok.democratia.UI.internaute;
using com.koyok.democratia.UI.internaute.CreerGroupe;
using com.koyok.democratia.UI.internaute.gestionCompte;
using Microsoft.Extensions.DependencyInjection;

namespace com.koyok.democratia.Domain.Extension
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
                services.AddSingleton<Utils.AppContext>();
                services.AddDataLocalSources();
                services.AddDataRemoteSources();
                services.AddClient();
                services.AddUsesCases();
                services.AddTransientViewModel();

                return services;
            }

            public IServiceCollection AddUsesCases()
            {
                services.AddSingleton<AuthenticateUseCase>();
                services.AddSingleton<ClassementPropositionUseCase>();
                services.AddSingleton<CreerGroupeUseCase>();
                services.AddSingleton<DeterminateRoleUseCase>();
                services.AddSingleton<InsertionCompteUseCase>();
                services.AddSingleton<ListeDonneeUseCase>();
                services.AddSingleton<ManipulateImageUseCase>();

                return services;
            }

            public IServiceCollection AddDataLocalSources()
            {
                services.AddTransient<ILocalSource, InternauteLocalSource>();
                services.AddTransient<ILocalSource, GroupeLocalSource>();
                services.AddTransient<ILocalSource, ThematiqueLocalSource>();
                services.AddTransient<ILocalSource, PropositionLocalSource>();
                return services;
            }

            public IServiceCollection AddDataRemoteSources()
            {
                services.AddTransient<IRemoteSource, InternauteRemoteSource>();
                services.AddTransient<IRemoteSource, GroupeRemoteSource>();
                services.AddTransient<IRemoteSource, ThematiqueRemoteSource>();
                services.AddTransient<IRemoteSource, PropositionRemoteSource>();

                return services;
            }
            public IServiceCollection AddClient()
            {

                services.AddTransient<IRepository>(s => s.GetRequiredService<IInternauteRepository>());
                services.AddTransient<IRepository>(s => s.GetRequiredService<IGroupeRepository>());
                services.AddTransient<IRepository>(s => s.GetRequiredService<IThematiqueRepository>());
                services.AddTransient<IRepository>(s => s.GetRequiredService<IPropositionRepository>());

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
                services.AddTransient<DeuxiemePageViewModel>();
                services.AddTransient<TroisiemeCreationViewModel>();
                services.AddTransient<GroupeViewModel>();
                services.AddTransient<ModifierGestionViewModel>();
                services.AddTransient<PreferenceViewModel>();
                services.AddTransient<DecideurViewModel>();

                return services;
            }
        }
    }
}
