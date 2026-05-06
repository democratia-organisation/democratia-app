namespace com.koyok.democratia.UI.internaute.CreerGroupe
{
    public partial class PremiereCreationPage : ContentPage
    {
        public PremiereCreationPage(PremiereCreationViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
            behavior.BindingContext = BindingContext;
        }

        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            PremiereCreationViewModel viewModel = (PremiereCreationViewModel)BindingContext;
            await viewModel.RemplirThematique();
        }
    }
}