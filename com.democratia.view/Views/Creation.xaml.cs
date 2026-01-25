using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels;

namespace com.democratia.Views;


public partial class Creation : ContentPage
{
    public Creation(IEnumerable<INavigeablleViewModel?>? navigeablleViewModels)
    {
        InitializeComponent();
        var viewModel = navigeablleViewModels!.OfType<CreationViewModel>().FirstOrDefault();
        BindingContext = viewModel!;
        viewModel?.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.RetourMessage) && !string.IsNullOrEmpty(viewModel.RetourMessage))
            {
                if (viewModel.RetourMessage == $"{AppResources.compteCree}")
                {
                    IView[] views = { nomDeFamilleComponent, prenomComponent, passwordComponent, mailComponent, addresseComponent, retourMessageLabel, inscriptionButton };
                    for (int i = 0; i < views.Length; i++)
                    {
                        if (stackLayout.Children.Contains(views[i])) 
                            stackLayout.Children.Remove(views[i]);
                    }

                    var seConnecterButton = new Button
                    {
                        Text = $"{AppResources.connecter}",
                        Style = (Style?)Application.Current?.Resources["ButtonStyle"],

                    };
                    seConnecterButton.Command = viewModel.NavigateTappedCommand;
                    seConnecterButton.CommandParameter = "..";

                    stackLayout.Children.Add(new BoxView { HeightRequest = 120});

                    var label = new Label
                    {
                        Text = $"{AppResources.BonneNouvelle}",
                        Style = (Style?)Application.Current?.Resources["HeadlineStyle"],
                        AutomationId = "BienvenueLabel"
                    };
                    AutomationProperties.SetName(label, "BienvenueLabel");
                    stackLayout.Children.Add(label);

                    stackLayout.Children.Add(new BoxView { HeightRequest = 40 });

                    stackLayout.Children.Add(seConnecterButton);
                }
                else retourMessageLabel.Text = viewModel.RetourMessage;
            }

        };
    }
}