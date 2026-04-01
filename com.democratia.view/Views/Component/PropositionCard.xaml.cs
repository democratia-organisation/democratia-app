using com.democratia.Models;

namespace com.democratia.Views.Component
{
    public partial class PropositionCard : ContentView
    {
        public PropositionCard(Proposition proposition)
        {
            BindingContext = proposition;
            InitializeComponent();
        }
    }

    public partial class PropositionCell : ContentView
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (BindingContext is Proposition proposition)
                Content = new PropositionCard(proposition);
        }        
    }
};