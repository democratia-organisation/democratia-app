using com.democratia.core.Utils;
using com.democratia.ViewModels;
using com.democratia.Views.Component;
using System.ComponentModel;

namespace com.democratia.Views;

public partial class GestionCompte : ContentPage
{
    ILocalizationService service;
	public GestionCompte(GestionCompteViewModel viewModel, ILocalizationService service)
	{
		InitializeComponent();
        BindingContext = viewModel;  
        this.service = service;
        viewModel.PropertyChanged += (sender, args) => MiseAJourPage(args);
    }

    private async void MiseAJourPage(PropertyChangedEventArgs args)
    {
        var viewModel = BindingContext as GestionCompteViewModel;
        if (args.PropertyName == nameof(viewModel.RetourMessage) && !string.IsNullOrEmpty(viewModel?.RetourMessage))
        {
            stackLayout.Children.Clear();
            if (viewModel.RetourMessage == "Modif")
            {
                string[] nombreElements = [$"{service.GetString("Nom")}", $"{service.GetString("prenom")}", $"{service.GetString("adress")}", $"{service.GetString("mail")}", $"{service.GetString("Mdp")}"];
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
            else if (viewModel.RetourMessage == "Supp")
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
            else if (viewModel.RetourMessage.Contains("OK"))
            {
                var uneActionDeModif = viewModel.RetourMessage.Contains("Modification");
                var textLabel = uneActionDeModif ? service.GetString("bienModifier") : service.GetString("bienSupp");
                var textButton = uneActionDeModif ? service.GetString("retourConnexion") : service.GetString("retourHome");
                foreach (var child in stackLayout.Children.ToList()) stackLayout.Children.Remove(child);
                var action = uneActionDeModif
                    ? new Func<Task>(() => Shell.Current.GoToAsync("//Home"))
                    : new Func<Task>(() => Shell.Current.GoToAsync("//MainPage"));

                var button = new Button
                {
                    Text = $"{textButton}",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                    AutomationId = "actionBouttonOui",
                };
                button.Clicked += async (_, _) => await action();
                var label = new Label
                {
                    Text = $"{textLabel}",
                    Style = (Style?)Application.Current?.Resources["SubHeadlineStyle"],
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    AutomationId = "actionLabelOui",
                };
                AutomationProperties.SetName(button, "actionRetourOui");
                AutomationProperties.SetName(label, "actionLabelOui");
                stackLayout.Children.Add(label);
                stackLayout.Children.Add(new BoxView { HeightRequest = 50 });
                stackLayout.Children.Add(button);
            }
            else if (viewModel.RetourMessage.Contains("Erreur"))
            {
                var uneActionDeModif = viewModel.RetourMessage.Contains("Modification");
                var textLabel = uneActionDeModif ? service.GetString("malModifier") : service.GetString("malSupp");
                var label = new Label
                {
                    Text = $"{textLabel}",
                    Style = (Style?)Application.Current?.Resources["SubHeadlineStyle"],
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    AutomationId = "actionLabelNon",
                };

                AutomationProperties.SetName(label, "actionLabelNon");
                stackLayout.Children.Add(new BoxView { HeightRequest = 150 });
                stackLayout.Children.Add(label);
            }
        }
    }
}