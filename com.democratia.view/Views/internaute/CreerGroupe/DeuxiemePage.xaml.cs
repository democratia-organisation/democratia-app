using com.democratia.ViewModels.internaute.CreerGroupe;

namespace com.democratia.Views.internaute.CreerGroupe
{
    public partial class DeuxiemePage : ContentPage
    {
        public DeuxiemePage(DeuxiemPageViewModel viewModel)
        {
            BindingContext = viewModel;
            InitializeComponent();
        }
    }
}