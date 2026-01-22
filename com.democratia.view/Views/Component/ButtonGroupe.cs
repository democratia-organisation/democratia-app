using com.democratia.ViewModels;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;

namespace com.democratia.view.Views.Component;

public class ButtonGroupe : ContentView
{
    private string? imageUrl;
    private string? title;
    private HomeViewModel? viewModel;
    private ImageSource? imageSource;
    private ImageButton? button;

    public ButtonGroupe(string? imageUrl, string? title, IAsyncRelayCommand command, int? groupeParameter, HomeViewModel viewModel)
    {
        this.imageUrl = imageUrl;
        this.title = title;
        this.viewModel = viewModel;
        button = new ImageButton
        {
            HeightRequest = 50,
            WidthRequest = 50,
            HorizontalOptions = LayoutOptions.Center,
            Command = command,
            CommandParameter = groupeParameter.ToString(),
        };
        button.AutomationId = "ButtonGroupe_" + title;
        AutomationProperties.SetName(button, "ButtonGroupe_" + title);
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
                button,
                label
            },
            
        };
        CreateImageSourceAsync();
    }
    public async void CreateImageSourceAsync() {

        try {
            this.imageSource = await viewModel!.GetImageAsync(this.imageUrl!);
            MainThread.BeginInvokeOnMainThread(() => {
                button!.Source = this.imageSource;
            });
        } catch (Exception e) { Debug.WriteLine("bug"); }
    }
    
}