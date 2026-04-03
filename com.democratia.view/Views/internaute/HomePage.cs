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
        private readonly CollectionView _collectionView;
        private readonly Button _createGroupButton;
        private readonly Label _ownGroupeLabel;

        public HomePage(HomeViewModel viewModel)
        {
            BindingContext = viewModel;
            object  light, dark;
            Color lightCouleur = new(), darkCouleur = new();

            Style = (Style)Application.Current!.Resources["fondEcran"];
            var profileIcone = new Border  // bordure car sur iOS, les images sont davantages cropés, donc la bordure empeche le cropage de l'image
            { 
                MaximumWidthRequest = 60,
                MaximumHeightRequest = 60,
                HorizontalOptions = LayoutOptions.Center,
                StrokeThickness = 1,
                Padding = 0,
                StrokeShape = new RoundRectangle { CornerRadius = 50 },
                Content = new ImageButton
                {
                    Source = "profile_icon.jpeg",
                    Aspect = Aspect.AspectFill,
                    CornerRadius = 50,
                    MaximumHeightRequest = 60,
                    MaximumWidthRequest = 60,
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
                        HeightRequest = 80,
                        WidthRequest = 200,
                        Children =
                        {
                            new BoxView { WidthRequest = 700 },
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
                Margin = new Thickness(0, 20),
                AutomationId = "MyGroupsLabel",
            };
            AutomationProperties.SetName(_ownGroupeLabel, "MyGroupsLabel");
            
            _collectionView = new CollectionView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                ItemsSource = viewModel.Groupes,
                ItemTemplate = new(() => new ButtonGroupeCell()),
                ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical)
                {
                    HorizontalItemSpacing = 10,
                    VerticalItemSpacing = 10
                },
                HeightRequest = 400
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

            var grillePrincipale = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Star },
                    new RowDefinition { Height = GridLength.Auto }                  
                 },
                Children =
                {
                    _stackLayout,
                    _ownGroupeLabel,
                    _collectionView,
                    _createGroupButton
                }
            };

            Grid.SetRow(_stackLayout, 0);
            Grid.SetRow(_ownGroupeLabel, 1);
            Grid.SetRow(_collectionView, 2);
            Grid.SetRow(_createGroupButton, 3);

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