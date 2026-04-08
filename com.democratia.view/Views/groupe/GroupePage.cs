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
        private Grid? grille;
        private Microsoft.Maui.Controls.Application application;
        private bool _isInitialized = false;
        private int cursor = 0;
        private RefreshView? refreshView;
        private Grid? mailGrille;
        private double smallSize;

        public GroupePage(GroupeViewModel viewModel)
        {
            BindingContext = viewModel;
            application = Microsoft.Maui.Controls.Application.Current!;
            Style = (Style)application.Resources["fondEcran"];
            smallSize = (double)application!.Resources["SpacingSmall"];
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
            grille = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition{ Width = GridLength.Auto},
                    new ColumnDefinition{ Width = GridLength.Star},
                    new ColumnDefinition{ Width = GridLength.Auto},
                },
            };
            mailGrille = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Auto},
                    new RowDefinition{Height = GridLength.Star},
                    new RowDefinition{Height = GridLength.Auto}
                },
                RowSpacing = smallSize
            };
            var carSize = (double)application.Resources["SpacingUltraLarge"];
            var extraLarge = (double)application.Resources["SpacingLarge"];
            var image = new Image
            {
                Source = viewModel.Image,
                HeightRequest = carSize,
                WidthRequest = carSize,
                AutomationId = $"{viewModel.Groupe!.NomGroupe}groupeImageButton"
            };
            var newbutton = new Button
            {
                Command = viewModel.NavigateTappedCommand,
                CommandParameter = $"{nameof(NouvelleProposition)}",
                Text = AppResources.nouvellProp,
                Style = (Style)application!.Resources["ButtonStyle"],
                VerticalOptions = LayoutOptions.End,
                AutomationId = "newButton"

            };
            var picker = new Microsoft.Maui.Controls.Picker
            {
                ItemsSource = viewModel.Criteres,
                Title = AppResources.critere,
                WidthRequest = extraLarge,
                SelectedItem = viewModel.Critere,
                HorizontalOptions = LayoutOptions.Start,
                AutomationId = "critere"
            };
            picker.On<iOS>().SetUpdateMode(UpdateMode.WhenFinished);
            picker.Behaviors.Add(new EventToCommandBehavior {
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
                WidthRequest = extraLarge + extraLarge,
                AutomationId = "groupeImageButton"
            };
            var label = new Label
            {
                Style = (Style)application!.Resources["SubHeadlineStyle"],
                Text = viewModel.Groupe!.NomGroupe,
                HorizontalOptions = LayoutOptions.Center,

            };
            refreshView = new RefreshView
            {
                Content = new CollectionView
                {
                    ItemsSource = viewModel.Propositions,
                    ItemTemplate = new(() => new PropositionCell(viewModel)),
                    ItemsLayout = new GridItemsLayout(1, ItemsLayoutOrientation.Vertical)
                    {
                        HorizontalItemSpacing = smallSize,
                        VerticalItemSpacing = smallSize
                    },
                    HorizontalOptions = LayoutOptions.Center,
                    RemainingItemsThreshold = 1,
                    RemainingItemsThresholdReachedCommand = viewModel.UpdateListCommand,
                    RemainingItemsThresholdReachedCommandParameter = cursor += 1,
                    AutomationId = "propositionsCollectionView"

                },
                Command = viewModel.UpdateListCommand,
                CommandParameter = 0,
                AutomationId = "propositionsRefresh"
            };
            var horizontalLayout = new HorizontalStackLayout
            {
                Spacing = smallSize,
                Children =
                {

                    new ImageButton
                    {
                        Source = "rouage.png",
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter = $"{nameof(Parametre)}",
                        HeightRequest = extraLarge,
                        WidthRequest = extraLarge,
                        AutomationId = "rouageImageButton"

                    },
                    new ImageButton
                    {
                        Source = "loupe.png",
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter = $"{nameof(DecideurPage)}",
                        HeightRequest = extraLarge,
                        WidthRequest = extraLarge,
                        AutomationId = "loupeImageButton"

                    }
                }
            };

            if (application.Resources.TryGetValue("Light-surfaceContainer", out var light) && application.Resources.TryGetValue("Dark-surfaceContainer", out var dark))
                grille.SetAppThemeColor(BackgroundColorProperty, (Color)light, (Color)dark);
            
            View[] optionsListe = [button, label, horizontalLayout];
            View[] mainElements = [new Header(),image, grille, picker, refreshView, newbutton];
            foreach (var (index,item) in optionsListe.Index())
                grille.Add(item, column: index);
            foreach (var (index,item) in mainElements.Index())
                mailGrille.Add(item, row: index);

            Content = mailGrille;
        }
    }

}