using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace com.democratia.ViewModels.groupe
{
    public partial class GroupeViewModel : ConnectableViewModel, INavigeablleViewModel, IQueryAttributable
    {
        [ObservableProperty] private ImageSource? image;
        [ObservableProperty] private Groupe? groupe;
        [ObservableProperty] private ObservableCollection<Proposition> propositions = [];
        [ObservableProperty] private ObservableCollection<Thematique> thematiques = [];
        // TODO : savoir si c'est un décideur afin d'afficher certaines options en fonction
        [ObservableProperty]
        private ObservableCollection<Critere> criteres = [
                    Critere.PRIX,
                    Critere.POPULARITE,
                    Critere.REACTIONS
        ];
        [ObservableProperty] private Critere critere;
        private readonly INavigationService navigationService;
        

        public GroupeViewModel(IEnumerable<IClient> clients, INavigationService navigationService, ILocalizationService localizationService) : base(clients.OfType<IGroupeClient>().FirstOrDefault(), localizationService)
        {
            this.navigationService = navigationService;
            WeakReferenceMessenger.Default.Register<GroupeViewModel,EventEndScroll,string>(this, TypeEventScroll.EndScroll.ToString() ,UpdateList);
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            ShellNavigationQueryParameters? parameters = commande == "DecideurPage" ?
                new ShellNavigationQueryParameters { { "thematiques", Thematiques }, { "groupe" , Groupe! } } : null;
            await navigationService?.GoToAsync(commande, parameters)!;
        }

        [RelayCommand]
        private void ClasserPropositions() 
        {
            throw new NotImplementedException();
        }

        public async Task ChargerElements()
        {
            var propositionClient = ServiceHelper.GetService<IPropositionClient>();
            var thematiqueClient = ServiceHelper.GetService<IThematiqueClient>();
            Groupe = groupe ?? await RetrouverModele<Groupe>()!;
            string response = await ((PropositionClient)propositionClient!).GetAllPropositionsAsync(Groupe!.IdGroupe);
            List<Proposition> propositions = RecuprerInformationConnexion<Proposition>(response)!;
            response = await ((GroupClient)client!).GetJointureThemeEtGroupeAsync(Groupe!.IdGroupe)!;
            List<Thematique> thematiques = RecuprerInformationConnexion<Thematique>(response)!;
            propositions.ForEach(p => {
                p.JourDiscussion = (int)Groupe.NombreDeJourDiscuss!;
                Propositions.Add(p);
            });
            thematiques.ForEach(t => Thematiques.Add(t) );

        }

        public async void GetImageAsync(string? url) => Image = await client!.GetImageAsync(url);

        [RelayCommand]
        private async Task OpenGroup(string nomGroupe)
        {
            var parameters = new ShellNavigationQueryParameters { { "nomGroupe", nomGroupe }, { "modele", Groupe! }, { "Image", Image! } };
            await navigationService?.GoToAsync("GroupePage", parameters)!;
        }

        private void UpdateList(object _, EventEndScroll __)
        {
            throw new NotImplementedException();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Groupe = query.TryGetValue("modele", out var groupe) ? (Groupe)groupe : new();
            Image = query.TryGetValue("Image", out var image) ? (ImageSource)image : null;
        }

        public record class EventEndScroll {}
        public enum TypeEventScroll
        {
            EndScroll,
            RechargeScoll
        }
    }
}
