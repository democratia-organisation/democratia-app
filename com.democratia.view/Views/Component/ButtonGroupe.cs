using com.democratia.ViewModels.groupe;
using CommunityToolkit.Mvvm.Input;

namespace com.democratia.Views.Component
{
    public class ButtonGroupe : ContentView
    {
        private string? imageUrl;
        private string? title;
        private GroupeViewModel? viewModel;
        private ImageButton? button;

        public ButtonGroupe(string? imageUrl, string? title, IAsyncRelayCommand command, Guid? groupeParameter, GroupeViewModel? viewModel)
        {
            this.imageUrl = imageUrl;
            this.title = title;
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
            button = new ImageButton
            {
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                Command = command,
                CommandParameter = groupeParameter.ToString(),
                AutomationId = "ButtonGroupe_" + title
            };
            AutomationProperties.SetName(button, "ButtonGroupe_" + title);
            button.SetBinding(ImageButton.SourceProperty, "Image");
            var label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = this.title,
                Style = (Style?)Application.Current?.Resources["SubHeadlineStyle"],
                LineBreakMode = LineBreakMode.WordWrap

            };
            if (Application.Current!.Resources.TryGetValue("Light-onPrimaryContainer", out var lightP) && Application.Current!.Resources.TryGetValue("Dark-onPrimaryContainer", out var darkP))
            {
                Color lightCouleur = (Color)lightP, darkCouleur = (Color)darkP;
                label.SetAppThemeColor(Label.TextColorProperty, lightCouleur, darkCouleur);
            }
            if (Application.Current!.Resources.TryGetValue("Light-primaryContainer", out var light) && Application.Current!.Resources.TryGetValue("Dark-primaryContainer", out var dark))
            {
                Color lightCouleur = (Color)light, darkCouleur = (Color)dark;
                this.SetAppThemeColor(BackgroundColorProperty, lightCouleur, darkCouleur);
            }

            Content = new VerticalStackLayout
            {
                Children = {
                button,
                label
            },
                MaximumHeightRequest = 300,
                MaximumWidthRequest = 300

            };
            this.viewModel!.GetImageAsync(imageUrl!);
        }
    }
}

