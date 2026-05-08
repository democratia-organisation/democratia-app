using com.koyok.democratia.Data.Repository;
using com.koyok.democratia.Domain.Enumerations;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace com.koyok.democratia.UI.groupe
{
    public partial class GroupeViewModel(
        Domain.Utils.AppContext context
    ) : ObservableObject, IQueryAttributable
    {
        [ObservableProperty] public partial ImageSource? image { get; set;}
        [ObservableProperty] public partial Groupe? groupe { get; set; }
        [ObservableProperty] public partial ObservableCollection<Proposition> propositions { get; set; } = [];
        [ObservableProperty] public partial ObservableCollection<Thematique> thematiques { get; set; } = [];
        private int cursor = 0;

        private Internaute? internaute;
        private readonly Domain.Utils.AppContext context = context;
        // TODO : savoir si c'est un décideur afin d'afficher certaines options en fonction
        [ObservableProperty]
        public partial ObservableCollection<Critere> criteres { get; set; } = [Critere.PRIX,Critere.POPULARITE,Critere.REACTIONS];
        [ObservableProperty] public partial Critere critere { get; set; }
        
        [ObservableProperty] public partial bool isRefreshing { get; set; } = false;

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            ShellNavigationQueryParameters parameters = new() { { "thematiques", thematiques }, { "groupe" , groupe! } };
            await Shell.Current?.GoToAsync(commande, parameters)!;
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
            var propositionClient = ServiceHelper.GetService<IPropositionRepository>();
            var thematiqueClient = ServiceHelper.GetService<IThematiqueRepository>();
            string response = await ((PropositionRepository)propositionClient!).GetAllPropositionsAsync(groupe!.idGroupe);
            List<Proposition> propositionsListe = RecuprerInformationConnexion<Proposition>(response)!;
            response = await ((GroupRepository)client!).GetJointureThemeEtGroupeAsync(groupe!.idGroupe)!;          
            List<Thematique> thematiquesListe = RecuprerInformationConnexion<Thematique>(response)!;
            propositions.Clear();
            thematiques.Clear();
            propositionsListe.ForEach(p => {
                p.jourDiscussion = (int)groupe.nombreDeJourDiscuss!;
                propositions.Add(p);
            });
            thematiquesListe.ForEach(t => thematiques.Add(t) );

        }

        [RelayCommand]
        private async Task OuvrirPropositionAsync(Proposition proposition)
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
            internaute = (Internaute)query["modele"] ?? context.Internaute;
        }

    }
}
