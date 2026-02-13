using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using System.ComponentModel;

namespace com.democratia.ViewModels.internaute.CreerGroupe
{
    public partial class TroisiemePageViewModel : ConnectableViewModel, INavigeablleViewModel , IQueryAttributable, INotifyPropertyChanged
    {

        private INavigationService service;
        private Groupe? groupe;
        [ObservableProperty] public Stream image;
        private List<string>? thematiques { get; set; }
        public TroisiemePageViewModel(IEnumerable<IClient?>? clients, ILocalizationService? localizationService, INavigationService service) 
            : base(clients?.FirstOrDefault(), localizationService)
        {
            this.service = service;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"];
            thematiques = (List<string>)query["thematique"];
        }

        [RelayCommand]
        public Task NavigateTapped(string commande)
        {
            throw new NotImplementedException();
        }

        [RelayCommand]
        public async Task PrendreImage()
        {
             var result = await MediaPicker.Default.PickPhotosAsync(new MediaPickerOptions
             {
                 MaximumHeight = 500,
                 MaximumWidth = 500,
                 CompressionQuality = 85
             });
            foreach (var item in result)
            {
                var stream = await item.OpenReadAsync();

                // TODO : faire la requete Create avec comme paramètre le groupe ainsi que l'image récupéré via stream
            }
        }


    }
}
