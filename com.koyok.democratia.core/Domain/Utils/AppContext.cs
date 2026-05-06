using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Models;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace com.koyok.democratia.Domain.Utils
{
    public partial record class AppContext : INotifyPropertyChanged
    {
        private InternauteRemoteSource? internaute;
        public InternauteRemoteSource? Internaute
        {
            get => internaute;
            set
            {
                internaute = value;
                OnPropertyChanged(nameof(internaute));
            }
        }

        private Groupe? groupe;
        public Groupe? Groupe
        {
            get => groupe;
            set
            {
                groupe = value;
                OnPropertyChanged(nameof(groupe));
            }
        }

        private ImageSource? imageSourceGroupe;
        public ImageSource? ImageSourceGroupe
        {
            get => imageSourceGroupe;
            set
            {
                imageSourceGroupe = value;
                OnPropertyChanged(nameof(imageSourceGroupe));
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
