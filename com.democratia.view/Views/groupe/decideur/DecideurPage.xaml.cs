using com.democratia.ViewModels.groupe.decideur;
using static com.democratia.Views.Component.DecideurThematique;

namespace com.democratia.Views.groupe.decideur
{
    public partial class DecideurPage : ContentPage
    {
        public DecideurPage(DecideurViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}

