using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.Domain.Service;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace com.koyok.democratia.Domain.Utils
{

    public partial record class AppContext : INotifyPropertyChanged
    {
        private MapExceptionMessage? mapper;
        public MapExceptionMessage? Mapper
        {
            get => mapper;
            set
            {
                mapper = value;
                OnPropertyChanged(nameof(mapper));
            }
        }
        private Internaute? internaute;
        public Internaute? Internaute
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
