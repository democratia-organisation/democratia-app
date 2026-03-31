using com.democratia.ViewModels.groupe;

namespace com.democratia.Views.Component
{
    public partial class ButtonGroupe : ContentView
    {
        private Models.Groupe groupe;
        private GroupeViewModel? viewModel;
        private ImageButton? button;

        public ButtonGroupe(Models.Groupe groupe, GroupeViewModel? viewModel)
        {
            this.groupe = groupe;
            this.viewModel = viewModel;
            BindingContext = this.viewModel;
            
            button = new ImageButton
            {
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.Center,
                Command = this.viewModel!.OpenGroupCommand,
                CommandParameter = this.groupe.NomGroupe,
                AutomationId = "ButtonGroupe_" + this.groupe.NomGroupe
            };
            AutomationProperties.SetName(button, "ButtonGroupe_" + this.groupe.NomGroupe);
            button.SetBinding(ImageButton.SourceProperty, "Image");
            var label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                Text = this.groupe.NomGroupe,
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
            Content = new Border
            {
                Content = new VerticalStackLayout
                {
                    Children =
                    {
                        button,
                        label
                    }
                },
                Style = (Style?)Application.Current?.Resources["BorderStyleButton"],
                MaximumHeightRequest = 300,
                MaximumWidthRequest = 300
            };
            this.viewModel!.GetImageAsync(this.groupe.Image!);
        }
    }

    public partial class ButtonGroupeCell : ContentView
    {
        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is Models.Groupe groupe)
            {
                var groupeViewModel = ServiceHelper.GetService<GroupeViewModel>();
                groupeViewModel!.Groupe = groupe;
                this.Content = new ButtonGroupe(groupe, groupeViewModel);
            }
        }
    }
}

