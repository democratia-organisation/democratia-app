namespace com.democratia.Views.Component;

public partial class Header : ContentView
{
    public Header()
    {
        InitializeComponent();
        SetTheme();
    }


    private async void OnClicked(object sender, EventArgs e)
    {
        ImageButton button = (ImageButton)sender;
        if (button == switchImageButton)
        {
            if (Application.Current?.RequestedTheme == AppTheme.Dark)
            {
                Application.Current.UserAppTheme = AppTheme.Light;
                button.Source = "light.png";
            }
            else if (Application.Current?.RequestedTheme == AppTheme.Light)
            {
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                button.Source = "dark.png";
            }
        }
        else if (button == backButton)
        {
            await AppShell.Current.GoToAsync("..");
        }
        else
        {
            await AppShell.Current.GoToAsync("//MainPage");
        }
    }

    private void SetTheme() =>
        switchImageButton.Source = Application.Current?.RequestedTheme == AppTheme.Dark ? "dark.png" : "light.png";



}
