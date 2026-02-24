using com.democratia.ViewModels.internaute.CreerGroupe;

namespace com.democratia.Views.internaute.CreerGroupe
{
    public partial class DeuxiemeCreationPage : ContentPage
    {
        public DeuxiemeCreationPage(DeuxiemePageViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}