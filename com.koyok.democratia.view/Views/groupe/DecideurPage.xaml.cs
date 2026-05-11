namespace com.koyok.democratia.UI.groupe
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

