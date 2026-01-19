using com.democratia.Resources.Styles;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls; 

namespace com.democratia.view.Views.Component;

public class ButtonGroupe : ContentView
{
    private string? imageUrl;
    private string? title;

    public ButtonGroupe(string? imageUrl, string? title, IAsyncRelayCommand command, int? groupeParameter)
    {
        this.imageUrl = imageUrl;
        this.title = title;
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
                    Source = this.imageUrl,
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
}