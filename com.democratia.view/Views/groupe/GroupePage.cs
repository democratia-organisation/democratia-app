using com.democratia.Models;
using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.groupe;
using com.democratia.Views.Component;
using com.democratia.Views.groupe.decideur;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Extensions;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace com.democratia.Views.groupe
{
    public partial class GroupePage : ContentPage
    {
        private Grid grille;
        private Microsoft.Maui.Controls.Application application;
        private bool _isInitialized = false;
        private int cursor = 0;
        private CollectionView collectionView;
        public GroupePage(GroupeViewModel viewModel)
        {
            BindingContext = viewModel;
            application = Microsoft.Maui.Controls.Application.Current!;
            Style = (Style)application.Resources["fondEcran"];
            grille = new Grid
            {
                ColumnDefinitions = 
                { 
                    new ColumnDefinition{ Width = GridLength.Auto},
                    new ColumnDefinition{ Width = GridLength.Star},
                    new ColumnDefinition{ Width= GridLength.Auto},
                },
            };
            collectionView = new CollectionView
            {
                ItemsSource = viewModel.Propositions,
                ItemTemplate = new DataTemplate(() => new PropositionCell(viewModel))

            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (_isInitialized) return;
            Building();
            _isInitialized = true;
        }

        private void Building()
        {
            var viewModel = (GroupeViewModel)BindingContext;
            var smallSize = (double)application!.Resources["SpacingSmall"];
            var carSize = (double)application.Resources["CardHeight"];
            var extraLarge = (double)application.Resources["SpacingLarge"];
            var picker = new Microsoft.Maui.Controls.Picker
            {
                Style = (Style)application.Resources["PickerStyle"],
                ItemsSource = viewModel.Criteres,
                Title = AppResources.critere,
                WidthRequest = extraLarge,
                SelectedItem = viewModel.Critere,
                HorizontalOptions = LayoutOptions.Start,
            };
            picker.SetAppTheme(Microsoft.Maui.Controls.Picker.TextColorProperty,
                (Color)application.Resources["Light-onBackground"], (Color)application.Resources["Dark-onBackground"]);
            picker.On<iOS>().SetUpdateMode(UpdateMode.WhenFinished);
            picker.Behaviors.Add(new EventToCommandBehavior
            {
                EventName = "SelectedIndexChanged",
                Command = viewModel.ClasserPropositionsCommand,

            });
            var button = new ImageButton
            {
                Source = "groupe.png",
                Command = viewModel.NavigateTappedCommand,
                CommandParameter = $"{nameof(Membre)}",
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = extraLarge + extraLarge,
                WidthRequest = extraLarge + extraLarge
            };
            var label = new Label
            {
                Style = (Style)application!.Resources["SubHeadlineStyle"],
                Text = viewModel.Groupe!.NomGroupe,
                HorizontalOptions = LayoutOptions.Center,
            };
            var horizontalLayout = new HorizontalStackLayout
            {
                Children =
                {

                    new ImageButton
                    {
                        Source = "rouage.png",
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter = $"{nameof(Parametre)}",
                        HeightRequest = extraLarge,
                        WidthRequest = extraLarge

                    },
                    new BoxView { WidthRequest = smallSize },
                    new ImageButton
                    {
                        Source = "loupe.png",
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter = $"{nameof(DecideurPage)}",
                        HeightRequest = extraLarge,
                        WidthRequest = extraLarge

                    }
                }
            };
            if (application.Resources.TryGetValue("Light-surfaceContainer", out var light) && application.Resources.TryGetValue("Dark-surfaceContainer", out var dark))
                grille.SetAppThemeColor(BackgroundColorProperty, (Color)light, (Color)dark);

            grille.Children.Add(button);
            grille.Children.Add(label);
            grille.Children.Add(horizontalLayout);
            grille.SetColumn(button, 0);
            grille.SetColumn(label, 1);
            grille.SetColumn(horizontalLayout, 2);

            Content = new VerticalStackLayout
            {
                Spacing = smallSize,
                Children =
                {
                    new Header(),
                    new Image
                    {
                        Source = viewModel.Image,
                        HeightRequest = carSize,
                        WidthRequest = carSize
                    },

                    grille,
                    picker,
                    new RefreshView
                    {
                        Content =  collectionView,
                        Command = viewModel.UpdateListCommand,
                        CommandParameter = cursor+=1,
                    },
                    new Button
                    {
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter =$"{nameof(NouvelleProposition)}",
                        Text = AppResources.nouvellProp,
                        Style = (Style)application!.Resources["ButtonStyle"],
                        VerticalOptions = LayoutOptions.End

                    }
                }
            };
        }
    }

}