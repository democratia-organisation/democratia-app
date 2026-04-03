using com.democratia.ViewModels.groupe.decideur;

namespace com.democratia.Views.groupe.decideur
{
    public partial class DecideurPage : ContentPage
    {
        public DecideurPage(DecideurViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        private void ScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {

        }

        protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
        {
            base.OnNavigatingFrom(args);
        }

        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            base.OnNavigatedFrom(args);
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
        }
    }
}

