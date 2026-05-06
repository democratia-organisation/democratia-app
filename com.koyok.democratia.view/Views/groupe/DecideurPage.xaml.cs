using com.koyok.democratia.ViewModels.groupe;

namespace com.koyok.democratia.Views.groupe
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

