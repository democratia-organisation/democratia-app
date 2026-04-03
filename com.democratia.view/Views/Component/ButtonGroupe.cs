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
            
            Style = (Style)Application.Current!.Resources["ContenViewStyle"];
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

