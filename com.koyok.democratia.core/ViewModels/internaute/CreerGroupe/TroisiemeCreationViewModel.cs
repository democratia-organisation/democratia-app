using com.koyok.democratia.Models;
using com.koyok.democratia.Services;
using com.koyok.democratia.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;

namespace com.koyok.democratia.UI.internaute.CreerGroupe
{
    public partial class TroisiemeCreationViewModel(IEnumerable<IClient?>? clients, ILocalizationService? LocalizationService, 
        INavigationService service, Services.AppContext context) : ConnectableViewModel(clients?.OfType<GroupClient>().FirstOrDefault(), 
            LocalizationService), INavigeablleViewModel , IQueryAttributable
    {

        private readonly INavigationService service = service;
        private Groupe? groupe;
        private string imagePath = string.Empty;
        private Internaute? internaute;
        private Services.AppContext context = context;
        [ObservableProperty] public partial ImageSource? image { get; set; }
        [ObservableProperty] public partial bool? isObservable { get; set; } = false;
        [ObservableProperty] public partial string? errorMessage { get; set; }
        [ObservableProperty] public partial bool isFinish { get; set; } = false;
        private List<Thematique>? thematiques { get; set; }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = (Groupe)query["groupe"];
            thematiques = (List<Thematique>)query["thematique"];
            internaute = (Internaute)query["internaute"] ?? context.Internaute;
        }

        [RelayCommand]
        public async Task NavigateTapped(string commande)
        {
            try
            {
                groupe!.IdGroupe = Guid.CreateVersion7();
                await client!.CreateModelAsync(groupe!);
                foreach (Thematique item in thematiques!)
                    await ((GroupClient)client).CreateJointureThemeEtGroupeAsync(groupe!.IdGroupe, item.id_thematique, item.budget);
                if (string.IsNullOrWhiteSpace(imagePath)) throw new NoImageGiven();
                await client.UploadImage(groupe!.IdGroupe, imagePath);
                await ((GroupClient)client).AjouterCreateur(internaute!.id_internaute, groupe.IdGroupe);                
                await service.GoToAsync(commande, new ShellNavigationQueryParameters { {"modele" , internaute } });
            } catch (Exception ex)
            {
                MapExceptionMessage.MappingException(ex, LocalizationService!);
            }
        }

        [RelayCommand]
        public void ChoisirImage() => isObservable = true;

        [RelayCommand]
        private async Task PrendrePhoto()
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult? photo = await MediaPicker.Default.CapturePhotoAsync();
                await AfiichageImage(photo!);
            }
            isObservable = false;
            isFinish = true;
        }

        [RelayCommand]
        private async Task ChoisirGalerie()
        {
            List<FileResult> result = await MediaPicker.Default.PickPhotosAsync();
            await AfiichageImage(result.FirstOrDefault()!);
            isObservable = false;
            isFinish = true;
        }

        private async Task AfiichageImage(FileResult photo)
        {
            try // bloque try catch au cas où l'utilisateur annule la sélection de la photo
            {
                using Stream sourceStream = await photo.OpenReadAsync();
                imagePath = photo.FullPath;
                var memoryStream = new MemoryStream();
                await sourceStream.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                image = ImageSource.FromStream(() => memoryStream);
            }
            catch (Exception ex)
            {
#if DEBUG
                errorMessage = MapExceptionMessage.MappingException(ex,LocalizationService!);
#elif !DEBUG
                errorMessage = LocalizationService?.GetString("erreurInattendu");
#endif
            }
        }
    }
}
