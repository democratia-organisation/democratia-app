using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Repository;
using com.koyok.democratia.Extension;
using com.koyok.democratia.Lib;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Collections;
using System.ComponentModel;

namespace com.koyok.democratia.UI.groupe
{
    public partial class NotificationViewModel(IInternauteRepository repository, 
        ILocalizationService localizationService) : ObservableObject, INotifyPropertyChanged, IQueryAttributable
    {
        [ObservableProperty]
        public partial Groupe? groupe { get; set; }
        [ObservableProperty]
        public partial string retourErreur { get; set; }
        private Internaute? internaute;

        private IInternauteRepository internauteRepository = repository;
        private ILocalizationService localizationService = localizationService;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            groupe = query.TryGetValue("groupe", out var group) ? (Groupe)group : Shell.Current.AppContext.Groupe;
            internaute = query.TryGetValue("internaute", out var user) ? (Internaute)user : Shell.Current.AppContext.Internaute;
        }

        [RelayCommand]
        private async Task SaveNotificationAsync(BitArray bits)
        {
            bool success = await internauteRepository.SaveNotification(groupe!, bits, internaute!);
            if (success)
            {
                ShellNavigationQueryParameters parameters = new()
                {
                    { "retourMessage", localizationService.GetString("notifReussi") }
                };
                await Shell.Current.GoToAsync("..", parameters);
            }
            else
            {
                retourErreur = localizationService.GetString("notifEchec");
            }
        }
    }
}
