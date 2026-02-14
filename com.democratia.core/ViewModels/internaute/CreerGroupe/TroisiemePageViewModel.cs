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
        private ILocalizationService? localizationService;
        [ObservableProperty] private ImageSource? _image;
        [ObservableProperty] private bool? _isObservable = false;
        [ObservableProperty] private string? _errorMessage;
        [ObservableProperty] private bool _isFinish = false;
        private List<string>? thematiques { get; set; }
        public TroisiemePageViewModel(IEnumerable<IClient?>? clients, ILocalizationService? localizationService, INavigationService service) 
            : base(clients?.FirstOrDefault(), localizationService)
        {
            this.service = service;
            this.localizationService = localizationService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"];
            thematiques = (List<string>)query["thematique"];
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            await client!.CreateModelAsync(groupe!, thematiques!, Image);
            await ((GroupClient)client).UploadImage(groupe!.IdGroupe, Image!);
        }

        [RelayCommand]
        public void ChoisirImage() => IsObservable = true;

        [RelayCommand]
        private async Task PrendrePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();
                AfiichageImage(photo!);
            }
            IsObservable = false;
            IsFinish = true;
        }

        [RelayCommand]
        private async Task ChoisirGalerie()
        {
            var result = await MediaPicker.Default.PickPhotosAsync();
            AfiichageImage(result.FirstOrDefault()!);
            IsObservable = false;
            IsFinish = true;
        }

        private async void AfiichageImage(FileResult photo)
        {
            try // bloque try catch au cas où l'utilisateur annule la sélection de la photo
            {
                using Stream sourceStream = await photo.OpenReadAsync();
                var memoryStream = new MemoryStream();
                await sourceStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                Image = ImageSource.FromStream(() => memoryStream);
            }
            catch (Exception ex)
            {
#if DEBUG
                ErrorMessage = ex.Message;
#elif !DEBUG
                ErrorMessage = localizationService?.GetString("erreurPhoto");
#endif
            }
        }
    }
}
