using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace com.democratia.ViewModels
{
    /// <summary>
    /// Classe abstraite qui représente tout viewModel qui peut se connecter à l'API
    /// </summary>
    public abstract class ConnectableViewModel(IClient? client) : ObservableObject
    {
        protected readonly IClient? client = client;
        public IClient? Client => client;
    }

}
