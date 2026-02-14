using com.democratia.Models;
using com.democratia.Services;
using com.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using System.ComponentModel;

namespace com.democratia.ViewModels.internaute.CreerGroupe
{
    public partial class TroisiemePageViewModel : ConnectableViewModel, INavigeablleViewModel , IQueryAttributable, INotifyPropertyChanged
    {

        private INavigationService service;
        private Groupe? groupe;
        [ObservableProperty] private ImageSource? _image;
        [ObservableProperty] private bool? _isObservable = false;
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
            // TODO : faire la requete Create avec comme paramètre le groupe ainsi que l'image récupéré via stream
        }

        [RelayCommand]
        public void ChoisirImage() => IsObservable = true;

        [RelayCommand]
        private async Task PrendrePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    using (Stream sourceStream = await photo.OpenReadAsync())
                    {
                        var memoryStream = new MemoryStream();
                        await sourceStream.CopyToAsync(memoryStream);
                        memoryStream.Position = 0;
                        Image = ImageSource.FromStream(() => memoryStream);
                    }
                }
            }
            IsObservable = false;
        }

        [RelayCommand]
        private async Task ChoisirGalerie()
        {
            var result = await MediaPicker.Default.PickPhotosAsync(new MediaPickerOptions
            {
                MaximumHeight = 500,
                MaximumWidth = 500,
                CompressionQuality = 85
            });
            using(Stream sourceStream = await result[0].OpenReadAsync())
            {
                var memoryStream = new MemoryStream();
                await sourceStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                Image = ImageSource.FromStream(() => memoryStream);

            }
            
            IsObservable = false;
        }
    }
}
