using com.democratia.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace com.democratia.ViewModels
{
    /// <summary>
    /// Classe abstraite qui représente tout viewModel qui peut se connecter à l'API
    /// </summary>
    public abstract class ConnectableViewModel : ObservableObject
    {
        protected Client? client;

    }

}
