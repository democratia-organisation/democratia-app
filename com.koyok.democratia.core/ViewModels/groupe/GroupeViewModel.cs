using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Domain.UseCase;
using com.koyok.democratia.Extension;
using com.koyok.democratia.Lib;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.koyok.democratia.UI.groupe
{
    [QueryProperty(nameof(image), "Image")]
    public partial class GroupeViewModel : ObservableObject, INotifyPropertyChanged, IQueryAttributable
    {
        [ObservableProperty] public partial string? image { get; set; }
        [ObservableProperty] public partial Groupe? groupe { get; set; }
        [ObservableProperty] public partial ObservableCollection<Proposition> propositions { get; set; } = [];
        [ObservableProperty] public partial ObservableCollection<Thematique> thematiques { get; set; } = [];
        [ObservableProperty] public partial Critere critere { get; set; }
        [ObservableProperty] public partial bool isRefreshing { get; set; } = false;
        [ObservableProperty] public partial ComplexFilterEnum complexFilter { get; set; }

        private readonly IPropositionRepository propositionRepository;
        private readonly IGroupeRepository groupRepository;
        private readonly ClassementPropositionUseCase useCase;
        private int cursor = 0;
        public GroupeViewModel(IPropositionRepository propositionRepository, IGroupeRepository groupRepository)
        {
            this.propositionRepository = propositionRepository;
            this.groupRepository = groupRepository;
            useCase = new([.. propositions], propositionRepository);
        }


        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            ShellNavigationQueryParameters parameters = new() { { "thematiques", thematiques }, { "groupe", groupe! } };
            await Shell.Current?.GoToAsync(commande, parameters)!;
        }

        [RelayCommand]
        private void ClasserPropositions()
        {
            var propositionsClasser = useCase.Classer(critere);
            propositions.RemplacerElements(propositionsClasser);
        }

        [RelayCommand]
        private void FiltrerPropositions()
        {
            // TODO : déléguer cette tâche au useCase
            switch (complexFilter)
            {
                case ComplexFilterEnum.MaxSatisfactionMinBudget:
                    break;
                case ComplexFilterEnum.PlusGrandeSatisfactionTheme:
                    break;
                case ComplexFilterEnum.PlusPropositionsTheme:
                    break;
            }

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
            var thematiquesTask = groupRepository.GetJointureThemeEtGroupeAsync(groupe!.idGroupe);
            var propositionsTask = propositionRepository.GetAllPropositionsAsync(groupe!.idGroupe);

            await Task.WhenAll(thematiquesTask, propositionsTask);

            var thematiquesListe = await thematiquesTask;
            var propositionsListe = await propositionsTask;
            // executer dans le MainThread afin que seulement un thread nettoie puis ajout les éléments dans la liste
            MainThread.BeginInvokeOnMainThread(() => 
            {
                thematiques.RemplacerElements(thematiquesListe);
                propositions.RemplacerElements(propositionsListe);
            });
        }

        [RelayCommand]
        private static async Task OuvrirPropositionAsync(Proposition proposition)
        {
            ShellNavigationQueryParameters parameters = new() { { "proposition", proposition } };
            await Shell.Current?.GoToAsync("PropositionPage", parameters)!;
        }

        [RelayCommand]
        private async Task OuvrirDetailPropositionAsync(Proposition proposition)
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
        }

    }
}
