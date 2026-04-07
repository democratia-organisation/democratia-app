using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.groupe;
using com.democratia.Views.Component;
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
        private RefreshView? collectionView;
        private Grid mailGrille;
        private double smallSize;

        public GroupePage(GroupeViewModel viewModel)
        {
            BindingContext = viewModel;
            application = Microsoft.Maui.Controls.Application.Current!;
            Style = (Style)application.Resources["fondEcran"];
            smallSize = (double)application!.Resources["SpacingSmall"];
            grille = new Grid
            {
                ColumnDefinitions = 
                { 
                    new ColumnDefinition{ Width = GridLength.Auto},
                    new ColumnDefinition{ Width = GridLength.Star},
                    new ColumnDefinition{ Width= GridLength.Auto},
                },
            };
            mailGrille = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Star},
                    new RowDefinition{Height = GridLength.Auto}
                },
                RowSpacing = smallSize
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ((GroupeViewModel)BindingContext).ChargerElements();
            if (_isInitialized) return;
            Building();

            _isInitialized = true;
        }

        private void Building()
        {
            var viewModel = (GroupeViewModel)BindingContext;
            
            var carSize = (double)application.Resources["CardHeight"];
            var extraLarge = (double)application.Resources["SpacingLarge"];
            var image = new Image
            {
                Source = viewModel.Image,
                HeightRequest = carSize,
                WidthRequest = carSize
            };
            var newbutton = new Button
            {
                Command = viewModel.NavigateTappedCommand,
                CommandParameter = $"{nameof(NouvelleProposition)}",
                Text = AppResources.nouvellProp,
                Style = (Style)application!.Resources["ButtonStyle"],
                VerticalOptions = LayoutOptions.End

            };
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
            collectionView = new RefreshView
            {
                Content = new CollectionView
                {
                    ItemsSource = viewModel.Propositions,
                    ItemTemplate = new DataTemplate(() => new PropositionCell(viewModel)),
                    ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Horizontal)
                    {
                        HorizontalItemSpacing = smallSize,
                        VerticalItemSpacing = smallSize
                    },
                    HorizontalOptions = LayoutOptions.Center,
                    RemainingItemsThreshold = 1,
                    RemainingItemsThresholdReachedCommand = viewModel.UpdateListCommand,
                    RemainingItemsThresholdReachedCommandParameter = cursor += 1,

                },
                Command = viewModel.UpdateListCommand
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
            
            View[] optionsListe = [button, label, horizontalLayout];
            View[] mainElements = [image, grille, picker, collectionView, newbutton];
            foreach (var (index,item) in optionsListe.Index())
                grille.Add(item, column: index);
            foreach (var (index,item) in mainElements.Index())
                mailGrille.Add(item, row: index);

            Content = new VerticalStackLayout
            {
                Spacing = smallSize,
                Children =
                {
                    new Header(),
                    mailGrille
                    
                }
            };
        }
    }

}