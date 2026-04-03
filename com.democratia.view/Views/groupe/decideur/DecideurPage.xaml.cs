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
    }
}

