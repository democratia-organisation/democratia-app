using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using System.ComponentModel;

namespace com.koyok.democratia.UI.internaute.CreerGroupe
{
    public partial class TroisiemeCreationViewModel(Domain.Utils.AppContext context) 
        : ObservableObject, INotifyPropertyChanged , IQueryAttributable
    {

        private Groupe? groupe;
        private string imagePath = string.Empty;
        private Internaute? internaute;
        private readonly Domain.Utils.AppContext context = context;
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
                groupe!.idGroupe = Guid.CreateVersion7();
                await client!.CreateModelAsync(groupe!);
                foreach (Thematique item in thematiques!)
                    await ((GroupClient)client).CreateJointureThemeEtGroupeAsync(groupe!.idGroupe, item.idThematique, item.budget);
                if (string.IsNullOrWhiteSpace(imagePath)) throw new NoImageGiven();
                await client.UploadImage(groupe!.idGroupe, imagePath);
                await ((GroupClient)client).AjouterCreateur(internaute!.idInternaute, groupe.idGroupe);                
                await Shell.Current.GoToAsync(commande, new ShellNavigationQueryParameters { {"modele" , internaute } });
            } catch (Exception ex)
            {
               context!.Mapper!.MappingException(ex);
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
                errorMessage = context!.Mapper!.MappingException(ex);
            }
        }
    }
}
