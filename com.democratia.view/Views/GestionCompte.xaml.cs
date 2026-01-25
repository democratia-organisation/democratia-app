using com.democratia.core.Utils;
using com.democratia.ViewModels;
using com.democratia.Views.Component;

namespace com.democratia.Views;

public partial class GestionCompte : ContentPage
{
	public GestionCompte(GestionCompteViewModel viewModel, ILocalizationService service)
	{
		InitializeComponent();
        BindingContext = viewModel;     
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.RetourMessage) && !string.IsNullOrEmpty(viewModel.RetourMessage))
            {
                stackLayout.Children.Clear();
                if (viewModel.RetourMessage == "Modif")
                {
                    string[] nombreElements = [$"{service.GetString("Nom")}",$"{service.GetString("prenom")}",$"{service.GetString("adress")}", $"{service.GetString("mail")}", $"{service.GetString("Mdp")}"];
                    foreach (var item in nombreElements)
                    {
                        var entry = new EntryComponent { Title = item };
                        if (item == $"{service.GetString("Mdp")}") entry.PassWord = true;
                        stackLayout.Children.Add(entry);
                        stackLayout.Children.Add(new BoxView { HeightRequest = 50 });
                    }
                    var button = new Button
                    {
                        Text = $"{service.GetString("enregistrer")}",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                        AutomationId = "enregistrerBoutton",
                        Command = viewModel.ModifierInternauteCommand,
                    };
                    AutomationProperties.SetName(button, "enregistrerBoutton");
                    stackLayout.Children.Add(button);
                }
                if (viewModel.RetourMessage == "Supp")
                {
                    var label = new Label
                    {
                        Text = $"{service.GetString("mauvaiseNouvelle")}",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Style = (Style?)Application.Current?.Resources["SubHeadlineStyle"],
                        AutomationId = "supprimerLabel"
                    };
                    AutomationProperties.SetName(label, "supprimerLabel");
                    var buttonYes = new Button
                    {
                        Text = $"{service.GetString("confirmeSupp")}",
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                        AutomationId = "suppressionBoutton",
                        Command = viewModel.SupprimerCompteCommand

                    };
                    AutomationProperties.SetName(buttonYes, "suppressionBoutton");

                    stackLayout.Children.Add(new BoxView { HeightRequest = 150 });
                    stackLayout.Children.Add(label);
                    stackLayout.Children.Add(new BoxView { HeightRequest = 50 });
                    stackLayout.Children.Add(buttonYes);
                }
                // TODO : ajuouter les condition après modification(revenir en arrière) et après suppresion (afficher l'écran de déception, proposer d'aller se créer un compte)
            }

        };
    }
}