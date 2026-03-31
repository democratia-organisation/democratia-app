using com.democratia.ViewModels.groupe;
using com.democratia.Views.Component;
using com.democratia.Views.groupe.decideur;
using CommunityToolkit.Mvvm.Messaging;

namespace com.democratia.Views.groupe
{
    public partial class GroupePage : ContentPage
    {
        private HorizontalStackLayout? horizontalLayout;
        Color darkColor = new(), lightColor = new();

        public GroupePage(GroupeViewModel viewModel)
        {
            BindingContext = viewModel;
            object? valueDarkColor, valueLightColor;
            WeakReferenceMessenger.Default.Register<GroupePage, GroupeViewModel.EventEndScroll,string>(this, GroupeViewModel.TypeEventScroll.RechargeScoll.ToString(), RecharPage);
            var scroll = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Children =
                    {
                        new CollectionView
                        {
                            ItemsSource = viewModel.Propositions,
                            ItemTemplate = new DataTemplate(()=> new PropositionCard())
                                 
                        }
                    }
                },
            };
            
            if (Application.Current!.Resources.TryGetValue("", out valueDarkColor) && Application.Current!.Resources.TryGetValue("", out valueLightColor))
            {
                darkColor = (Color)valueDarkColor;
                lightColor = (Color)valueLightColor;
            }
            BindingContext = viewModel;
            Content = new VerticalStackLayout
            {
                Children = {
                 new Header(),
                 new Image {
                     Source = viewModel.Image
                 },
                 horizontalLayout,
                 scroll,
                 new Button
                 {
                     Command = viewModel.NavigateTappedCommand,
                     CommandParameter = "nouvelle proposition",
                     Style = (Style)Application.Current!.Resources["ButtonStyle"]
                 }

                }
            };
            scroll.Scrolled += Scroll_Scrolled;
        }

        private void RecharPage(GroupePage _, GroupeViewModel.EventEndScroll __)
        {
            throw new NotImplementedException();
            // TODO : faire une animation de chargement de page
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            GroupeViewModel viewModel = ((GroupeViewModel)BindingContext);
            this.horizontalLayout = new HorizontalStackLayout
            {
                Children =
                     {
                         new ImageButton { }, //icone groupe
                         new Label
                         {
                             Style = (Style)Application.Current!.Resources["LabelStyle"],
                             Text = viewModel.Groupe!.NomGroupe
                         },
                         new ImageButton{ }, // icone rouage
                         new ImageButton {
                            Command = viewModel.NavigateTappedCommand,
                            CommandParameter = $"{typeof(DecideurPage)}"

                         }, // icone loupe
                     }
            };
            horizontalLayout.SetAppThemeColor(BackgroundColorProperty, darkColor, lightColor);
            await viewModel.ChargerProposition();
        }

        private void Scroll_Scrolled(object? sender, ScrolledEventArgs e)
        {
            if (sender is not ScrollView scrollView) return;

            var scrollSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollSpace > e.ScrollY) return;
            // TODO : détecter si on a regardé toutes les propositions
            WeakReferenceMessenger.Default.Send<GroupeViewModel.EventEndScroll,string>(GroupeViewModel.TypeEventScroll.EndScroll.ToString());
        }
    }

}