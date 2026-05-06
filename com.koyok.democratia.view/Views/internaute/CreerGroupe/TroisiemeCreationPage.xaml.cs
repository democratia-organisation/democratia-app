using com.koyok.democratia.ViewModels.internaute.CreerGroupe;

namespace com.koyok.democratia.Views.internaute.CreerGroupe
{
    public partial class TroisiemeCreationPage : ContentPage
    {
        public TroisiemeCreationPage(TroisiemeCreationViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

        }
    }
}