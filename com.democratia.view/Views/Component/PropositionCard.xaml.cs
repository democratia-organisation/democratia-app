using com.democratia.Models;
using com.democratia.view.Resources.Localization;

namespace com.democratia.Views.Component
{
    public partial class PropositionCard : ContentView
    {
        public PropositionCard()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            dateLabel.Text = $"{AppResources.finDiscussion} {((Proposition)BindingContext).FormatDateFinDiscussion}";

        }
    }
};