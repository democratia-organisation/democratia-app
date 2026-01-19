using com.democratia.view.Views.Component;
using com.democratia.ViewModels;

namespace com.democratia.view.Views
{
    public partial class Home : ContentPage
    {
        public Home(HomeViewModel viewModel)
        {
            
            BindingContext = viewModel;
            Content = new CollectionView
            {
                ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical)
                {
                    HorizontalItemSpacing = 10,
                    VerticalItemSpacing = 10
                },
                ItemTemplate = new DataTemplate(() =>
                {
                    var grille = new Grid() { Padding = 10 };
                    grille.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    grille.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    grille.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                    for (int i = 0; i < 10; i++) grille.Add(new ButtonGroupe("icon.png", "Button " + (i + 1)));
                    return new Border()
                    {
                        Style = (Style?)Application.Current?.Resources["BorderStyle"],
                        Content = grille
                    };

                }),
            };

        }
    }
}
