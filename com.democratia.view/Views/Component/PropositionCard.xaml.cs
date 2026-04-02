using com.democratia.Models;
using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.groupe;

namespace com.democratia.Views.Component
{
    public partial class PropositionCard : ContentView
    {
        public PropositionCard(GroupeViewModel viewModel, Proposition proposition)
        {
            BindingContext = viewModel;
            InitializeComponent();
            labelTitre.BindingContext = proposition;
            labelDate.BindingContext = proposition;
            labelDate.Text = $"{AppResources.finDiscussion} {proposition.FormatDateFinDiscussion}";
        }
    }

    public partial class PropositionCell(GroupeViewModel viewModel) : ContentView
    {
        private readonly GroupeViewModel viewModel = viewModel;
        
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is Proposition proposition)
                Content = new PropositionCard(viewModel, proposition);
            
        }        
    }
};