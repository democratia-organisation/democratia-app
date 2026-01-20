using com.democratia.view.Views.Component;
using com.democratia.ViewModels;
using com.democratia.Views.Component;

namespace com.democratia.view.Views
{
    public partial class Home : ContentPage
    {
        public Home(HomeViewModel viewModel)
        {
            
            BindingContext = viewModel;
            if (Application.Current!.Resources.TryGetValue("Light-background", out var light) && Application.Current!.Resources.TryGetValue("Dark-background", out var dark))
            {
                Color lightCouleur = (Color)light, darkCouleur = (Color)dark;
                this.SetAppThemeColor(BackgroundColorProperty, lightCouleur, darkCouleur);
            }

            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Header(),
                    new Grid
                    {
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Star }, // Prend tout l'espace
                            new RowDefinition { Height = GridLength.Auto }  // Prend juste la place du bouton
                        },
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
                        },
                        HeightRequest = 80,
                        WidthRequest = 200,
                    },
                    new BoxView { HeightRequest = 50 },
                    new Label
                    {
                        Text = "Mes Groupes",
                        Style = (Style?)Application.Current?.Resources["HeadlineStyle"],
                        HorizontalOptions = LayoutOptions.Center
                    },
                    new BoxView { HeightRequest = 50 },
                    new CollectionView
                    {
                        ItemsSource = ((HomeViewModel)BindingContext).groupes,
                        ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical)
                        {
                            HorizontalItemSpacing = 10,
                            VerticalItemSpacing = 10
                        },
                        ItemTemplate = new DataTemplate(() =>
                        {
                            Grid grille = NewGrid()!;

                            return new Border()
                            {
                                Style = (Style?)Application.Current?.Resources["BorderStyle"],
                                Content = grille
                            };

                        }),
                    },
                    new BoxView { HeightRequest = 80 },
                    new Button
                    {
                        Text = "Créer un nouveau groupe",
                        Style = (Style?)Application.Current?.Resources["ButtonStyle"],
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter = "CreerGroupe",
                        VerticalOptions = LayoutOptions.End
                    }
                }
            };

        }

        private Grid? NewGrid()
        {
            if (BindingContext is HomeViewModel viewModel)
            {
                var grille = new Grid() { Padding = 10 };
                grille.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grille.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                grille.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                for (int i = 0; i < viewModel.groupes.Count; i++)
                    grille.Add(new ButtonGroupe(viewModel.groupes[i].Image, viewModel.groupes[i].NomGroupe, viewModel.OpenGroupCommand, viewModel.groupes[i].IdGroupe, (HomeViewModel)BindingContext));
                return grille;
            }
            else return null;
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            // 1. Récupérer le ViewModel depuis le BindingContext
            if (BindingContext is HomeViewModel vm)
            {
                vm.InitializeAsync();
            }
        }
    }
}
