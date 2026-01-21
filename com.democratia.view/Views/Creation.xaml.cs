using com.democratia.ViewModels;

namespace com.democratia.Views;


public partial class Creation : ContentPage
{
    public Creation(IEnumerable<INavigeablleViewModel?>? navigeablleViewModels)
    {
        InitializeComponent();
        var viewModel = navigeablleViewModels!.OfType<CreationViewModel>().FirstOrDefault();
        BindingContext = viewModel ?? throw new ArgumentNullException("ViewModel cannot be null.", nameof(viewModel));
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.RetourMessage) && !string.IsNullOrEmpty(viewModel.RetourMessage))
            {
                if (viewModel.RetourMessage == "Création réussie; Connectez-vous maintenant")
                {
                    IView[] views = { nomDeFamilleComponent, prenomComponent, passwordComponent, mailComponent, addresseComponent, retourMessageLabel, inscriptionButton };
                    for (int i = 0; i < views.Length; i++)
                    {
                        if (stackLayout.Children.Contains(views[i])) 
                            stackLayout.Children.Remove(views[i]);
                        else throw new Exception("Élément inexistant");
                    }

                    var seConnecterButton = new Button
                    {
                        Text = "Me connecter",
                        Style = (Style?)Application.Current?.Resources["ButtonStyle"],

                    };
                    seConnecterButton.Command = viewModel.NavigateTappedCommand;
                    seConnecterButton.CommandParameter = "..";

                    stackLayout.Children.Add(new BoxView { HeightRequest = 120});

                    stackLayout.Children.Add(new Label
                    {
                        Text = "Quelle bonne idée de vous joindre à nous",
                        Style = (Style?)Application.Current?.Resources["HeadlineStyle"]
                    });

                    stackLayout.Children.Add(new BoxView { HeightRequest = 40 });

                    stackLayout.Children.Add(seConnecterButton);
                }
                else retourMessageLabel.Text = viewModel.RetourMessage;
            }

        };
    }
}