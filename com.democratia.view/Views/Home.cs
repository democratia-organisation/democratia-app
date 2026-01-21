using com.democratia.view.Views.Component;
using com.democratia.ViewModels;
using com.democratia.Views.Component;

namespace com.democratia.view.Views
{
    public partial class Home : ContentPage
    {
        private readonly VerticalStackLayout _stackLayout;
        private readonly CollectionView _collectionView;
        private readonly Button _createGroupButton;
        private readonly Label _ownGroupeLabel;

        public Home(HomeViewModel viewModel)
        {
            BindingContext = viewModel;

            if (Application.Current!.Resources.TryGetValue("Light-background", out var light) && Application.Current!.Resources.TryGetValue("Dark-background", out var dark))
            {
                Color lightCouleur = (Color)light;
                Color darkCouleur = (Color)dark;
                this.SetAppThemeColor(BackgroundColorProperty, lightCouleur, darkCouleur);
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
                            new ImageButton
                            {
                                Source = "profile_icon.jpeg",
                                CornerRadius = 50,
                                HeightRequest = 50,
                                WidthRequest = 50,
                            }
                        }
                    }
                }
            };

            this._ownGroupeLabel = new Label
            {
                Text = "Mes Groupes",
                Style = (Style?)Application.Current?.Resources["HeadlineStyle"],
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20)
            };

            this._collectionView = new CollectionView
            {
                BackgroundColor = Colors.Blue,
                ItemsSource = viewModel.listeRecu,
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                
                ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical)
                {
                    HorizontalItemSpacing = 10,
                    VerticalItemSpacing = 10
                },
                ItemTemplate = new DataTemplate(() =>
                {
                    var container = new Border
                    {
                        Style = (Style?)Application.Current?.Resources["BorderStyle"],
                    };

                    container.BindingContextChanged += (s, e) =>
                    {
                        var border = (Border?)s;
                        if (border?.BindingContext is Models.Groupe groupe && this.BindingContext is HomeViewModel vm)
                        {
                            border.Content = new ButtonGroupe(
                                groupe.Image,
                                groupe.NomGroupe,
                                vm.OpenGroupCommand,
                                groupe.IdGroupe,
                                vm
                            );
                        }
                    };
                    return container;
                }),
            };
            this._collectionView.HeightRequest = 400;

            this._createGroupButton = new Button
            {
                Text = "Créer un nouveau groupe",
                Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                Command = viewModel.NavigateTappedCommand,
                CommandParameter = "CreerGroupe",
                VerticalOptions = LayoutOptions.End,
                Margin = new Thickness(20)
            };

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