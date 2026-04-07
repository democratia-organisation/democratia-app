using com.democratia.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace com.democratia.Services
{
    public partial record class AppContext : INotifyPropertyChanged
    {
        private Internaute? internaute;
        public Internaute? Internaute
        {
            get => internaute;
            set
            {
                internaute = value;
                OnPropertyChanged();
            }
        }

        private Groupe? groupe;
        public Groupe? Groupe
        {
            get => groupe;
            set
            {
                groupe = value;
                OnPropertyChanged();
            }
        }

        private void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
