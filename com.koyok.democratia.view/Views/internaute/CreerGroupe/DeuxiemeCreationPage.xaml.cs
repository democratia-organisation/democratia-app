namespace com.koyok.democratia.UI.internaute.CreerGroupe
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