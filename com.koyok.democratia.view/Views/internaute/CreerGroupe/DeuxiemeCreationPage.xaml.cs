using com.koyok.democratia.ViewModels.internaute.CreerGroupe;

namespace com.koyok.democratia.Views.internaute.CreerGroupe
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