using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace com.democratia.ViewModels
{
    /// <summary>
    /// Classe abstraite qui représente tout viewModel qui peut se connecter à l'API
    /// </summary>
    public abstract class ConnectableViewModel : ObservableObject
    {
        protected IClient? client;

        public ConnectableViewModel(IClient? client) { this.client = client; }

    }

}
