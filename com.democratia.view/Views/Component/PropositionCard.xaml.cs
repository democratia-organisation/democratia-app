using com.democratia.Models;
using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.groupe;

namespace com.democratia.Views.Component
{
    public partial class PropositionCard : ContentView
    {
        public PropositionCard()
        {
            InitializeComponent();
            dateLabel.Text = $"{AppResources.finDiscussion} {((Proposition)BindingContext).FormatDateFinDiscussion}";
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

        }
    }
};