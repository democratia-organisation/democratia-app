using com.koyok.democratia.Domain.Exception;
using com.koyok.democratia.Domain.Models;
using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace com.koyok.democratia.Domain.Utils
{

    public partial record class AppContext(MapExceptionMessage mapper) : INotifyPropertyChanged
    {
        private MapExceptionMessage? mapper = mapper;
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

        private string? imageSourceGroupe;
        public string? ImageSourceGroupe
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
