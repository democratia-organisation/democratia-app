using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Extension;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace com.koyok.democratia.UI.groupe
{
    public partial class PropositionViewModel(ICommentaireRepository commentaireRepository) : ObservableObject, IQueryAttributable, INotifyPropertyChanged
    {
        
        [ObservableProperty] public partial Proposition? proposition { get; set; }

        private ICommentaireRepository commentaireRepository = commentaireRepository;

        [ObservableProperty]
        public partial Groupe? groupe { get; set; }
        [ObservableProperty]
        public partial ObservableCollection<Commentaire> commentaires { get; set; } = [];
        [ObservableProperty]
        public partial bool isRefreshing { get; set; } = false;

        [ObservableProperty]
        public partial string? commentaire { get; set; }


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            proposition = query.TryGetValue("proposition", out var data) ? (Proposition)data : throw new ArgumentException("Aucune proposition existante");
            groupe = query.TryGetValue("groupe", out var dataGroupe) ? (Groupe)dataGroupe : Shell.Current.AppContext.Groupe;
        }

        [RelayCommand]
        private async Task RechargerElements()
        {
            if (isRefreshing)
            {
                await LoadCommentairesAsync();
                isRefreshing = false;
            }
            else return;
        }

        [RelayCommand]
        private async Task LoadCommentairesAsync()
        {
            try
            {
                List<Commentaire> commentaires = [.. (await commentaireRepository.GetModelAsync(groupe, proposition, Shell.Current.AppContext.Internaute)).Cast<Commentaire>()];
                this.commentaires.RemplacerElements(commentaires);
            }
            catch { throw; } 
        }

        [RelayCommand]
        private async Task AjouterCommentaireAsync()
        {
            List<Commentaire> commentaires = [.. this.commentaires];
            var internaute = Shell.Current.AppContext.Internaute;
            Commentaire nouveauCommentaire = new(1, commentaire, DateTime.Now, 0, internaute!.nomInternaute!, internaute.prenomInternaute!, Role.Membre, (int)internaute.idInternaute!);
            nouveauCommentaire.himself = true;
            commentaires.Add(nouveauCommentaire);
            this.commentaires.RemplacerElements(commentaires);
            bool isSuccess = await commentaireRepository.CreateModelAsync(commentaire);
            if (!isSuccess)
            {
                commentaires.Remove(nouveauCommentaire);
                this.commentaires.RemplacerElements(commentaires);
                throw new Exception("Erreur lors de l'ajout du commentaire");
            }

        }

    }
}
