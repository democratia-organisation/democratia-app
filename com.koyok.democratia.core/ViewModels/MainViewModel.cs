using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.UseCase;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using AppContext = com.koyok.democratia.Domain.Utils.AppContext;

namespace com.koyok.democratia.UI
{
    public partial class MainViewModel(AuthenticateUseCase useCase,
        AppContext context) : ObservableObject, INotifyPropertyChanged
    {
        private readonly AuthenticateUseCase useCase = useCase;
        private readonly AppContext context = context;
        public bool loading;

        [ObservableProperty]
        public partial bool isConnected { get; set; }
        
        [RelayCommand]
        private void Connect()
        {
            Internaute? internaute = useCase.Authenticate();
            if (internaute is not null)
            {
                context.Internaute = internaute;
                isConnected = true;
            }
            else isConnected = false;
        }

    }
}
