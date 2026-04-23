using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace com.democratia.ViewModels.groupe
{
    public partial class GroupeViewModel(
        IEnumerable<IClient> clients, 
        INavigationService navigationService, 
        ILocalizationService localizationService,
        Services.AppContext context
    ) : ConnectableViewModel(clients.OfType<IGroupeClient>().FirstOrDefault(), localizationService), INavigeablleViewModel, IQueryAttributable
    {
        [ObservableProperty] private ImageSource? image;
        [ObservableProperty] private Groupe? groupe;
        [ObservableProperty] private ObservableCollection<Proposition> propositions = [];
        [ObservableProperty] private ObservableCollection<Thematique> thematiques = [];
        private int cursor = 0;

        private Internaute? internaute;
        private Services.AppContext context = context;
        // TODO : savoir si c'est un décideur afin d'afficher certaines options en fonction
        [ObservableProperty]
        private ObservableCollection<Critere> criteres = [
                    Critere.PRIX,
                    Critere.POPULARITE,
                    Critere.REACTIONS
        ];
        [ObservableProperty] private Critere critere;
        private readonly INavigationService navigationService = navigationService;


        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            ShellNavigationQueryParameters parameters = new() { { "thematiques", Thematiques }, { "groupe" , Groupe! } };
            await navigationService?.GoToAsync(commande, parameters)!;
        }

        [RelayCommand]
        private void ClasserPropositions() 
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        private async Task ChargerElements()
        {
            // TODO : paginer la récupération de propositions
            var propositionClient = ServiceHelper.GetService<IPropositionClient>();
            var thematiqueClient = ServiceHelper.GetService<IThematiqueClient>();
            string response = await ((PropositionClient)propositionClient!).GetAllPropositionsAsync(Groupe!.IdGroupe);
            List<Proposition> propositions = RecuprerInformationConnexion<Proposition>(response)!;
            response = await ((GroupClient)client!).GetJointureThemeEtGroupeAsync(Groupe!.IdGroupe)!;          
            List<Thematique> thematiques = RecuprerInformationConnexion<Thematique>(response)!;
            Propositions.Clear();
            Thematiques.Clear();
            propositions.ForEach(p => {
                p.JourDiscussion = (int)Groupe.NombreDeJourDiscuss!;
                Propositions.Add(p);
            });
            thematiques.ForEach(t => Thematiques.Add(t) );

        }

        
        [RelayCommand]
        private async Task UpdateList()
        {
            cursor += 1;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Groupe = query.TryGetValue("groupe", out var groupe) ? (Groupe)groupe : context.Groupe;
            Image = query.TryGetValue("Image", out var image) ? (ImageSource)image : null;
            internaute = query.TryGetValue("modele", out var user) ? (Internaute)user : context.Internaute;
        }

    }
}
