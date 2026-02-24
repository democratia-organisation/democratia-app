using com.democratia.CustomException;
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
    public partial class TroisiemeCreationViewModel : ConnectableViewModel, INavigeablleViewModel , IQueryAttributable, INotifyPropertyChanged
    {

        private INavigationService service;
        private Groupe? groupe;
        private ILocalizationService? localizationService;
        private string imagePath;
        private Internaute? internaute;
        [ObservableProperty] private ImageSource? _image;
        [ObservableProperty] private bool? _isObservable = false;
        [ObservableProperty] private string? _errorMessage;
        [ObservableProperty] private bool _isFinish = false;
        private List<Thematique>? thematiques { get; set; }
        public TroisiemeCreationViewModel(IEnumerable<IClient?>? clients, ILocalizationService? localizationService, INavigationService service) 
            : base(clients?.OfType<GroupClient>().FirstOrDefault(), localizationService)
        {
            this.service = service;
            this.localizationService = localizationService;
            imagePath = string.Empty;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"];
            thematiques = (List<Thematique>)query["thematique"];
            internaute = (Internaute)query["internaute"];
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            groupe!.IdGroupe = Guid.CreateVersion7();
            await client!.CreateModelAsync(groupe!);
            foreach (Thematique item in thematiques!)
                await ((GroupClient)client).CreateJointureThemeEtGroupeAsync(groupe!.IdGroupe, item.id_thematique, item.budget);
            await client.UploadImage(groupe!.IdGroupe, imagePath);
            await ((GroupClient)client).AjouterCreateur(internaute!.id_internaute, groupe.IdGroupe);
            await service.GoToAsync(commande);
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
            List<FileResult> result = await MediaPicker.Default.PickPhotosAsync();
            AfiichageImage(result.FirstOrDefault()!);
            IsObservable = false;
            IsFinish = true;
        }

        private async void AfiichageImage(FileResult photo)
        {
            try // bloque try catch au cas où l'utilisateur annule la sélection de la photo
            {
                using Stream sourceStream = await photo.OpenReadAsync();
                imagePath = photo.FullPath;
                var memoryStream = new MemoryStream();
                await sourceStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                Image = ImageSource.FromStream(() => memoryStream);
            }
            catch (Exception ex)
            {
#if DEBUG
                ErrorMessage = MapExceptionMessage.MappingException(ex,localizationService!);
#elif !DEBUG
                ErrorMessage = localizationService?.GetString("erreurPhoto");
#endif
            }
        }
    }
}
