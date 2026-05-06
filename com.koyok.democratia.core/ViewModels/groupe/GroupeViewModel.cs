using com.koyok.democratia.core.Data.Repository;
using com.koyok.democratia.core.Domain.Models;
using com.koyok.democratia.core.Domain.Repository;
using com.koyok.democratia.core.Domain.Utils;
using com.koyok.democratia.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace com.koyok.democratia.UI.groupe
{
    public partial class GroupeViewModel(
        IEnumerable<IRepository> clients,
        INavigationService navigationService,
        ILocalizationService localizationService,
        core.Domain.Service.AppContext context
    ) : ConnectableViewModel(clients.OfType<IGroupeClient>().FirstOrDefault(), localizationService), INavigeablleViewModel, IQueryAttributable
    {
        [ObservableProperty] public partial ImageSource? image { get; set;}
        [ObservableProperty] public partial Groupe? groupe { get; set; }
        [ObservableProperty] public partial ObservableCollection<PropositionRemoteSource> propositions { get; set; } = [];
        [ObservableProperty] public partial ObservableCollection<ThematiqueRemoteSource> thematiques { get; set; } = [];
        private int cursor = 0;

        private InternauteRemoteSource? internaute;
        private core.Domain.Service.AppContext context = context;
        // TODO : savoir si c'est un décideur afin d'afficher certaines options en fonction
        [ObservableProperty]
        public partial ObservableCollection<Critere> criteres { get; set; } = [Critere.PRIX,Critere.POPULARITE,Critere.REACTIONS];
        [ObservableProperty] public partial Critere critere { get; set; }
        private readonly INavigationService navigationService = navigationService;

        [ObservableProperty]
        public partial bool isRefreshing { get; set; } = false;

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            ShellNavigationQueryParameters parameters = new() { { "thematiques", thematiques }, { "groupe" , groupe! } };
            await navigationService?.GoToAsync(commande, parameters)!;
        }

        [RelayCommand]
        private void ClasserPropositions() 
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        private async Task InitialisationElementsAsync()
        {
            if (isRefreshing)
            {
                await ChargerElementsAsync();
                isRefreshing = false;
            }
            else return;
        }

        [RelayCommand]
        private async Task ChargerElementsAsync()
        {
            // TODO : paginer la récupération de propositions
            var propositionClient = ServiceHelper.GetService<IPropositionClient>();
            var thematiqueClient = ServiceHelper.GetService<IThematiqueClient>();
            string response = await ((PropositionRepository)propositionClient!).GetAllPropositionsAsync(groupe!.IdGroupe);
            List<PropositionRemoteSource> propositionsListe = RecuprerInformationConnexion<PropositionRemoteSource>(response)!;
            response = await ((GroupRepository)client!).GetJointureThemeEtGroupeAsync(groupe!.IdGroupe)!;          
            List<ThematiqueRemoteSource> thematiquesListe = RecuprerInformationConnexion<ThematiqueRemoteSource>(response)!;
            propositions.Clear();
            thematiques.Clear();
            propositionsListe.ForEach(p => {
                p.JourDiscussion = (int)groupe.NombreDeJourDiscuss!;
                propositions.Add(p);
            });
            thematiquesListe.ForEach(t => thematiques.Add(t) );

        }

        [RelayCommand]
        private async Task OuvrirPropositionAsync(PropositionRemoteSource proposition)
        {
            throw new NotImplementedException();
        }

        
        [RelayCommand]
        private async Task UpdateList()
        {
            cursor += 1;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"] ?? context.Groupe;
            image = (ImageSource)query["Image"] ?? context.ImageSourceGroupe;
            internaute = (InternauteRemoteSource)query["modele"] ?? context.Internaute;
        }

    }
}
