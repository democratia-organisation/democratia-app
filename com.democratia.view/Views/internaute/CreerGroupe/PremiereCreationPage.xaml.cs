using com.democratia.ViewModels.internaute.CreerGroupe;

namespace com.democratia.Views.internaute.CreerGroupe
{
    public partial class PremiereCreationPage : ContentPage
    {
        public PremiereCreationPage(PremiereCreationViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
            behavior.BindingContext = BindingContext;
        }
    }
}