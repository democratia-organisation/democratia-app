using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.view.Resources.Localization;
using System.Windows.Input;

namespace com.koyok.democratia.UI.Component.groupe
{
    public partial class PropositionCard : ContentView
    {
        public static readonly BindableProperty OpenCommandProperty = BindableProperty.Create(
            nameof(OpenCommand), typeof(ICommand), typeof(PropositionCard),defaultValue:default(ICommand),defaultBindingMode: BindingMode.TwoWay);
        public ICommand OpenCommand
        {
            get => (ICommand)GetValue(OpenCommandProperty);
            set => SetValue(OpenCommandProperty, value);
        }
        public PropositionCard()
        {
            InitializeComponent();
        }
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            dateLabel.Text = $"{AppResources.finDiscussion} {((Proposition)BindingContext).formatDateFinDiscussion}";

        }
    }
};