using com.democratia.ViewModels.internaute.CreerGroupe;

namespace com.democratia.Views.internaute.CreerGroupe
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