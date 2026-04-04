using com.democratia.Models;
using com.democratia.view.Resources.Localization;
using com.democratia.ViewModels.groupe;
using com.democratia.Views.Component;
using com.democratia.Views.groupe.decideur;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

#if IOS
using IOS = Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using IOSPicker = Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific.Picker;
#endif

namespace com.democratia.Views.groupe
{
    public partial class GroupePage : ContentPage
    {
        private readonly Microsoft.Maui.Controls.ScrollView scroll;
        private Grid grille;
        private Microsoft.Maui.Controls.Application application;
        public GroupePage(GroupeViewModel viewModel)
        {
            BindingContext = viewModel;
            application = Microsoft.Maui.Controls.Application.Current!;
            WeakReferenceMessenger.Default.Register<GroupePage, GroupeViewModel.EventEndScroll,string>(this, GroupeViewModel.TypeEventScroll.RechargeScoll.ToString(), RecharPage);
            Style = (Style)application.Resources["fondEcran"];
            scroll = new Microsoft.Maui.Controls.ScrollView
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
            var smallSize = (double)application!.Resources["SpacingSmall"];
            var carSize = (double)application.Resources["CardHeight"];
            var extraLarge = (double)application.Resources["SpacingExtraLarge"];
            await viewModel.ChargerElements();
            var picker = new Microsoft.Maui.Controls.Picker
            {
                Style = (Style)application.Resources["PickerStyle"],
                ItemsSource = viewModel.Criteres,
                Title = AppResources.critere,
                WidthRequest = extraLarge,
                SelectedItem = viewModel.Critere
            };
            picker.On<iOS>().SetUpdateMode(UpdateMode.WhenFinished);
            picker.Behaviors.Add(new EventToCommandBehavior
            {
                EventName = "SelectedIndexChanged",
                Command = viewModel.ClasserPropositionsCommand,
                
            });
            var button = new ImageButton
            {
                Source = "groupe.png",
                Command = viewModel.NavigateTappedCommand,
                CommandParameter = $"{nameof(Membre)}",
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = 100,
                WidthRequest = 100
            };
            var label = new Label
            {
                Style = (Style)application!.Resources["SubHeadlineStyle"],
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
                        CommandParameter = $"{nameof(Parametre)}",
                        HeightRequest = extraLarge,
                        WidthRequest = extraLarge

                    },
                    new BoxView { WidthRequest = 10 },
                    new ImageButton
                    {
                        Source = "loupe.png",
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter = $"{nameof(DecideurPage)}",
                        HeightRequest = extraLarge,
                        WidthRequest = extraLarge

                    }
                }
            };
            if (application.Resources.TryGetValue("Light-surfaceContainer", out var light) && application.Resources.TryGetValue("Dark-surfaceContainer", out var dark))
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
                        HeightRequest = carSize,
                        WidthRequest = carSize
                    },
                    new BoxView { HeightRequest = smallSize },
                    grille,
                    new BoxView { HeightRequest = smallSize },
                    picker,
                    new BoxView { HeightRequest = smallSize },
                    scroll,
                    new BoxView { HeightRequest = smallSize },
                    new Button
                    {
                        Command = viewModel.NavigateTappedCommand,
                        CommandParameter =$"{nameof(NouvelleProposition)}",
                        Text = AppResources.nouvellProp,
                        Style = (Style)application!.Resources["ButtonStyle"],
                        VerticalOptions = LayoutOptions.End

                    }
                }
            };
        }
        private void Scroll_Scrolled(object? sender, ScrolledEventArgs e)
        {
            if (sender is not Microsoft.Maui.Controls.ScrollView scrollView) return;

            var scrollSpace = scrollView.ContentSize.Height - scrollView.Height;

            if (scrollSpace > e.ScrollY) return;
            // TODO : détecter si on a regardé toutes les propositions et charger la page ou non en fonction
            WeakReferenceMessenger.Default.Send<GroupeViewModel.EventEndScroll,string>(GroupeViewModel.TypeEventScroll.EndScroll.ToString());
        }
    }

}