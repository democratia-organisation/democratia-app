using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.groupe;
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

            if (Application.Current!.Resources.TryGetValue("Light-background", out light) && Application.Current!.Resources.TryGetValue("Dark-background", out dark))
            {
                lightCouleur = (Color)light;
                darkCouleur = (Color)dark;
                this.SetAppThemeColor(BackgroundColorProperty, lightCouleur, darkCouleur);
            }
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

            this._stackLayout = new VerticalStackLayout
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

            this._ownGroupeLabel = new Label
            {
                Text = $"{AppResources.groupe}",
                Style = (Style?)Application.Current?.Resources["HeadlineStyle"],
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20),
                AutomationId = "MyGroupsLabel",
            };
            AutomationProperties.SetName(this._ownGroupeLabel, "MyGroupsLabel");
            
            this._collectionView = new CollectionView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                ItemsSource = viewModel.Groupes,
                ItemTemplate = new(() => new ButtonGroupeCell()),
                ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical)
                {
                    HorizontalItemSpacing = 10,
                    VerticalItemSpacing = 10
                }
            };
            this._collectionView.HeightRequest = 400;

            this._createGroupButton = new Button
            {
                Text = $"{AppResources.NewGroupe}",
                Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                Command = viewModel.NavigateTappedCommand,
                CommandParameter = $"{nameof(PremiereCreationPage)}",
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(20),
                AutomationId = "CreateGroupButton",
            };
            AutomationProperties.SetName(this._createGroupButton, "CreateGroupButton");

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
                    this._stackLayout,
                    this._ownGroupeLabel,
                    this._collectionView,
                    this._createGroupButton
                }
            };

            Grid.SetRow(this._stackLayout, 0);
            Grid.SetRow(this._ownGroupeLabel, 1);
            Grid.SetRow(this._collectionView, 2);
            Grid.SetRow(this._createGroupButton, 3);

            Content = grillePrincipale;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is HomeViewModel vm)
            {
                vm.InitializeAsync();
            }
        }
    }
}