using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.groupe;
using com.democratia.Views.Component;
using com.democratia.Views.groupe.decideur;
using CommunityToolkit.Mvvm.Messaging;

namespace com.democratia.Views.groupe
{
    public partial class GroupePage : ContentPage
    {
        private readonly ScrollView scroll;
        private Grid grille;
        public GroupePage(GroupeViewModel viewModel)
        {
            BindingContext = viewModel;
            WeakReferenceMessenger.Default.Register<GroupePage, GroupeViewModel.EventEndScroll,string>(this, GroupeViewModel.TypeEventScroll.RechargeScoll.ToString(), RecharPage);
            Style = (Style)Application.Current!.Resources["fondEcran"];
            scroll = new ScrollView
            {
                Content = new VerticalStackLayout
                {
                    Children =
                    {
                        new CollectionView
                        {
                            ItemsSource = viewModel.Propositions,
                            ItemTemplate = new DataTemplate(()=> new PropositionCell(viewModel))

                        }
                    }
                },
            };
            scroll.Scrolled += Scroll_Scrolled;
            grille = new Grid
            {
                ColumnDefinitions = 
                { 
                    new ColumnDefinition{ Width = GridLength.Auto},
                    new ColumnDefinition{ Width = GridLength.Star},
                    new ColumnDefinition{ Width= GridLength.Auto},
                },
            };
        }

        private void RecharPage(GroupePage _, GroupeViewModel.EventEndScroll __)
        {
            throw new NotImplementedException();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = (GroupeViewModel)BindingContext;
            await viewModel.ChargerProposition();
            var button = new ImageButton
            {
                Source = "groupe.png",
                Command = viewModel.NavigateTappedCommand,
                CommandParameter = $"{typeof(Membre)}",
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 100,
                WidthRequest = 100
            };
            var label = new Label
            {
                Style = (Style)Application.Current!.Resources["SubHeadlineStyle"],
                Text = viewModel.Groupe!.NomGroupe,
                HorizontalOptions = LayoutOptions.Center,
            };
            var horizontalLayout = new HorizontalStackLayout
            {
                Children =
                {

                    new ImageButton
                    {
                        Source = "rouage.png",
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter = $"{typeof(Parametre)}",
                        HeightRequest = 50,
                        WidthRequest = 50

                    },
                    new BoxView { WidthRequest = 20 },
                    new ImageButton
                    {
                        Source = "loupe.png",
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter = $"{typeof(DecideurPage)}",
                        HeightRequest = 50,
                        WidthRequest = 50

                    }
                }
            };
            if (Application.Current.Resources.TryGetValue("Light-surfaceContainer", out var light) && Application.Current.Resources.TryGetValue("Dark-surfaceContainer", out var dark))
                grille.SetAppThemeColor(BackgroundColorProperty, (Color)light, (Color)dark);

            grille.Children.Add(button);
            grille.Children.Add(label);
            grille.Children.Add(horizontalLayout);
            grille.SetColumn(button, 0);
            grille.SetColumn(label, 1);
            grille.SetColumn(horizontalLayout, 2);
            
            Content = new VerticalStackLayout
            {
                Children =
                {
                    new Header(),
                    new Image
                    {
                        Source = viewModel.Image,
                        HeightRequest = 200,
                        WidthRequest = 200
                    },
                    new BoxView { HeightRequest = 20 },
                    grille,
                    scroll,
                    new Button
                    {
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter =$"{typeof(NouvelleProposition)}",
                        Text = AppResources.nouvellProp,
                        Style = (Style)Application.Current!.Resources["ButtonStyle"],
                        VerticalOptions = LayoutOptions.End

                    }

                }
            };
        }
        private void Scroll_Scrolled(object? sender, ScrolledEventArgs e)
        {
            if (sender is not ScrollView scrollView) return;

            var scrollSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollSpace > e.ScrollY) return;
            // TODO : détecter si on a regardé toutes les propositions et charger la page ou non en fonction
            WeakReferenceMessenger.Default.Send<GroupeViewModel.EventEndScroll,string>(GroupeViewModel.TypeEventScroll.EndScroll.ToString());
        }
    }

}