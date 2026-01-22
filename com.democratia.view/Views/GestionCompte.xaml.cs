using com.democratia.ViewModels;
using com.democratia.Views.Component;

namespace com.democratia.Views;

public partial class GestionCompte : ContentPage
{
	public GestionCompte(GestionCompteViewModel viewModel)
	{
        // TODO : implémentez les actions en fonction de si on modifie ou supprime le compte
		InitializeComponent();
        BindingContext = viewModel;     
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.RetourMessage) && !string.IsNullOrEmpty(viewModel.RetourMessage))
            {
                stackLayout.Children.Clear();
                if (viewModel.RetourMessage == "Modif")
                {
                    string[] nombreElements = ["Nom","Prénom","Adresse Postale", "Adresse Mail", "Mot de Passe"];
                    foreach (var item in nombreElements)
                    {
                        var entry = new EntryComponent { Title = item };
                        if (item == "Mot de Passe") entry.PassWord = true;
                        stackLayout.Children.Add(entry);
                        stackLayout.Children.Add(new BoxView { HeightRequest = 50 });
                    }
                    var button = new Button
                    {
                        Text = "Enregitrer",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                        AutomationId = "btnEnregistrerModifications"
                    };
                    AutomationProperties.SetName(button, "btnEnregistrerModifications");
                    stackLayout.Children.Add(button);
                }
                if (viewModel.RetourMessage == "Supp")
                {
                    var label = new Label
                    {
                        Text = "Êtes-vous sûr de vouloir supprimer le compte ?",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Style = (Style?)Application.Current?.Resources["SubHeadlineStyle"],
                        AutomationId = "lblConfirmationSuppression"
                    };
                    AutomationProperties.SetName(label, "lblConfirmationSuppression");
                    var buttonYes = new Button
                    {
                        Text = "Oui, supprimer mon compte",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                        AutomationId = "btnConfirmerSuppression"
                    };
                    AutomationProperties.SetName(buttonYes, "btnConfirmerSuppression");

                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(new BoxView { HeightRequest = 50 });
                    stackLayout.Children.Add(buttonYes);
                }
            }

        };
    }
}