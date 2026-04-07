using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.internaute;
using com.democratia.Views.Component;
using com.democratia.Views.internaute.CreerGroupe;
using com.democratia.Views.internaute.gestionCompte;
using Microsoft.Maui.Controls.Shapes;

namespace com.democratia.Views.internaute
{
    public partial class HomePage : ContentPage
    {
        private readonly VerticalStackLayout _stackLayout;
        private readonly RefreshView collectionView;
        private readonly Button _createGroupButton;
        private readonly Label _ownGroupeLabel;
        private int cursor = 0;

        public HomePage(HomeViewModel viewModel)
        {
            BindingContext = viewModel;
            object  light, dark;
            Color lightCouleur = new(), darkCouleur = new();

            Style = (Style)Application.Current!.Resources["fondEcran"];
            var large = (double)Application.Current.Resources["SpacingLarge"];
            var card = (double)Application.Current.Resources["CardHeight"];
            var small = (double)Application.Current.Resources["SpacingSmall"];
            var medium = (double)Application.Current.Resources["SpacingMedium"];
            // bordure car sur iOS, les images sont davantages cropés, donc la bordure empeche le cropage de l'image
            var profileIcone = new Border  
            { 
                MaximumWidthRequest = large,
                MaximumHeightRequest = large,
                HorizontalOptions = LayoutOptions.Center,
                StrokeThickness = 1,
                Padding = 0,
                StrokeShape = new RoundRectangle { CornerRadius = (int)large },
                Content = new ImageButton
                {
                    Source = "profile_icon.jpeg",
                    Aspect = Aspect.AspectFill,
                    CornerRadius = (int)large,
                    MaximumHeightRequest = large,
                    MaximumWidthRequest = large,
                    Command = viewModel.NavigateTappedCommand,
                    CommandParameter = $"{nameof(HomeGestionPage)}",
                    AutomationId = "ProfileButton",
                }
            };
            AutomationProperties.SetName(profileIcone, "ProfileButton");
            if (Application.Current!.Resources.TryGetValue("Light-onSurface", out light) && Application.Current!.Resources.TryGetValue("Dark-onSurface", out dark))
            {
                lightCouleur = (Color)light;
                darkCouleur = (Color)dark;
                profileIcone.SetAppThemeColor(Border.StrokeProperty, lightCouleur, darkCouleur);
            }

            _stackLayout = new VerticalStackLayout
            {
                Children =
                {
                    new Header(),
                    new Grid
                    {
                        HeightRequest = large,
                        WidthRequest = card,
                        Children =
                        {
                            new BoxView { WidthRequest = card },
                            profileIcone
                        }
                    }
                }
            };

            _ownGroupeLabel = new Label
            {
                Text = $"{AppResources.groupe}",
                Style = (Style?)Application.Current?.Resources["HeadlineStyle"],
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, medium),
                AutomationId = "MyGroupsLabel",
            };
            AutomationProperties.SetName(_ownGroupeLabel, "MyGroupsLabel");
            collectionView = new RefreshView
            {
                Content = new CollectionView
                {
                    VerticalOptions = LayoutOptions.Fill,
                    HorizontalOptions = LayoutOptions.Fill,
                    ItemsSource = viewModel.Groupes,
                    ItemTemplate = new(() => new ButtonGroupeCell()),
                    ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical)
                    {
                        HorizontalItemSpacing = small,
                        VerticalItemSpacing = small
                    },
                    HeightRequest = card,
                    RemainingItemsThreshold = 2,
                    RemainingItemsThresholdReachedCommand = viewModel.RefreshListGroupeCommand,
                    RemainingItemsThresholdReachedCommandParameter = cursor += 1

                },
                Command = viewModel.RefreshListGroupeCommand
            };



            _createGroupButton = new Button
            {
                Text = $"{AppResources.NewGroupe}",
                Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                Command = viewModel.NavigateTappedCommand,
                CommandParameter = $"{nameof(PremiereCreationPage)}",
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(20),
                AutomationId = "CreateGroupButton",
            };
            AutomationProperties.SetName(_createGroupButton, "CreateGroupButton");
            View[] views = [_stackLayout,_ownGroupeLabel,collectionView,_createGroupButton];
            var grillePrincipale = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Auto }                  
                 }
            };
            foreach (var (index, item) in views.Index())
                grillePrincipale.Add(item, row: index);
            Content = grillePrincipale;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is HomeViewModel vm)
            {
                vm.InitializeAsync();
            }
        }
    }
}