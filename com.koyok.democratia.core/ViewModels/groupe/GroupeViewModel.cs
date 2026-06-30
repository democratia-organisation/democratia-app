using com.koyok.democratia.Domain.Enumerations;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using com.koyok.democratia.Domain.Extension;
using System.ComponentModel;
using com.koyok.democratia.Domain.UseCase;

namespace com.koyok.democratia.UI.groupe
{
    [QueryProperty(nameof(image), "Image")]
    public partial class GroupeViewModel(
        IPropositionRepository propositionRepository,
        IGroupeRepository groupRepository,
        ClassementPropositionUseCase useCase
    ) : ObservableObject, INotifyPropertyChanged ,IQueryAttributable
    {
        [ObservableProperty] public partial string? image { get; set;}
        [ObservableProperty] public partial Groupe? groupe { get; set; }
        [ObservableProperty] public partial ObservableCollection<Proposition> propositions { get; set; } = [];
        [ObservableProperty] public partial ObservableCollection<Thematique> thematiques { get; set; } = [];
        private int cursor = 0;

        private readonly IPropositionRepository propositionRepository = propositionRepository;
        private readonly IGroupeRepository groupRepository = groupRepository;
        private readonly ClassementPropositionUseCase useCase = useCase;

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
            useCase.Classer(critere);
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
            string response = await propositionRepository.GetAllPropositionsAsync(groupe!.idGroupe);
            List<Proposition> propositionsListe = propositionRepository.RecuprerInformationConnexion<Proposition>(response)!;
            response = await groupRepository.GetJointureThemeEtGroupeAsync(groupe!.idGroupe)!;          
            List<Thematique> thematiquesListe = groupRepository.RecuprerInformationConnexion<Thematique>(response)!;
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
            groupe = (Groupe)query["groupe"] ?? Shell.Current!.AppContext.Groupe;
            image = (string)query["Image"] ?? Shell.Current!.AppContext.ImageSourceGroupe;
        }

    }
}
