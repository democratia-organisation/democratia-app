using com.democratia.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace com.democratia.view.Views.Component;

public class ButtonGroupe : ContentView
{
    private string? imageUrl;
    private string? title;
    private HomeViewModel? viewModel;
    private ImageSource? imageSource;

    public ButtonGroupe(string? imageUrl, string? title, IAsyncRelayCommand command, int? groupeParameter, HomeViewModel viewModel)
    {
        this.imageUrl = imageUrl;
        this.title = title;
        this.viewModel = viewModel;
        CreateImageSourceAsync();
        var label = new Label
        {
            HorizontalOptions = LayoutOptions.Center,
            Text = this.title,
            Style = (Style?)Application.Current?.Resources["SubHeadlineStyle"],

        };
        if (Application.Current!.Resources.TryGetValue("Light-primary", out var light) && Application.Current!.Resources.TryGetValue("Dark-primary", out var dark))
        {
            Color lightCouleur = (Color)light, darkCouleur = (Color)dark;
            label.SetAppThemeColor(BackgroundColorProperty, lightCouleur, darkCouleur);
        }
        
        Content = new VerticalStackLayout
        {
            Children = {
                new ImageButton {
                    Source = this.imageSource ,
                    HeightRequest = 50,
                    WidthRequest = 50,
                    HorizontalOptions = LayoutOptions.Center,
                    Command = command,
                    CommandParameter = groupeParameter.ToString()
                },
                label
            }
        };
    }
    public async void CreateImageSourceAsync() => this.imageSource = await viewModel!.GetImageAsync(this.imageUrl!);
    
}