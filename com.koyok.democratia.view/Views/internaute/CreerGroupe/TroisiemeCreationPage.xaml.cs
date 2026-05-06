namespace com.koyok.democratia.UI.internaute.CreerGroupe
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