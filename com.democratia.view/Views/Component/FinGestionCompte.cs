using System.Windows.Input;
using StackLayout = Microsoft.Maui.Controls.StackLayout;

namespace com.democratia.Views.Component
{
    public partial class FinGestionCompte : ContentView
    {
        
        private readonly Label _label;
        private readonly Button _buttonYes;

        public string? LabelText
        {
            get => _label.Text;
            set => _label.Text = value;
        }

        public string? ButtonText
        {
            get => _buttonYes.Text;
            set => _buttonYes.Text = value;
        }

        public ICommand? Command
        {
            get => _buttonYes.Command;
            set => _buttonYes.Command = value;
        }

        public object? CommandParameter
        {
            get => _buttonYes.CommandParameter;
            set => _buttonYes.CommandParameter = value;
        }

        public FinGestionCompte()
        {
            _label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Style = (Style?)Application.Current?.Resources["SubHeadlineStyle"],
                AutomationId = "supprimerLabel"
            };
            AutomationProperties.SetName(_label, "supprimerLabel");

            _buttonYes = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                AutomationId = "suppressionBoutton"
            };
            AutomationProperties.SetName(_buttonYes, "suppressionBoutton");

            var stackLayout = new StackLayout();
            stackLayout.Children.Add(new BoxView { HeightRequest = 150 });
            stackLayout.Children.Add(_label);
            stackLayout.Children.Add(new BoxView { HeightRequest = 50 });
            stackLayout.Children.Add(_buttonYes);

            Content = stackLayout;
        }
    }
}