using com.democratia.view.Resources.Localization;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System.Globalization;
using com.democratia.ViewModels;

namespace com.democratia.Views.Component;

public partial class Header : ContentView
{
    public Header()
    {
        InitializeComponent();
        SetTheme()  ;
    }


    private async void OnClicked(object sender, EventArgs e)
    {
        if (sender is ImageButton button)
        {
            if (button == switchImageButton)
            {
                if (Application.Current?.RequestedTheme == AppTheme.Dark)
                {
                    Application.Current.UserAppTheme = AppTheme.Light;
                    button.Source = "light.png";
                }
                else if (Application.Current?.RequestedTheme == AppTheme.Light)
                {
                    Application.Current.UserAppTheme = AppTheme.Dark;
                    button.Source = "dark.png";
                }
            }
            else if (button == backButton)
            {
                await AppShell.Current.GoToAsync("..");
            }
            else
            {
                await AppShell.Current.GoToAsync("//Home"); // TODO l'autoriser à y aller que s'il est connecté
            }

        }
        else if (sender is Picker picker)
        {
            // changer la langue

        }
    }

    private void SetTheme() =>
        switchImageButton.Source = Application.Current?.RequestedTheme == AppTheme.Dark ? "dark.png" : "light.png";
}