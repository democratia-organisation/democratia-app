using com.democratia.ViewModels.groupe;

namespace com.democratia.Views.groupe
{
    public partial class GroupePage : ContentPage
    {
        public GroupePage(GroupeViewModel viewModel)
        {
            BindingContext = viewModel;
            Content = new VerticalStackLayout
            {
                Children = {
                new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
                }
            }
            };
        }
    }
}