using com.koyok.democratia.Extension;
using com.koyok.democratia.UI.Component.groupe;
using System.Collections;

namespace com.koyok.democratia.UI.groupe;

public partial class NotificationPage : ContentPage
{
	private readonly CheckBox[] checkBoxes;
    public NotificationPage(NotificationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        checkBoxes = [.. NotificationStackLayout.Children.Where(c => c is ValidationComponent).Cast<ValidationComponent>().Select(vc => vc.CheckBox)];
    }

    private async void EnregistrerButton_Clicked(object sender, EventArgs e)
    {
        BitArray bits = new(checkBoxes.Length);
        foreach (CheckBox checkBox in checkBoxes)
        {
#if !WINDOWS
            if(checkBoxes.Last() == checkBox && checkBox.IsChecked)
            {
                PermissionStatus status = await CheckAndRequestLocationPermission<Permissions.PostNotifications>();
                if (status != PermissionStatus.Granted)
                {
                    checkBox.IsChecked = false;
                    checkBox.IsEnabled = false;
                }
            }
#endif
            bits[Array.IndexOf(checkBoxes, checkBox)] = checkBox.IsChecked;
        }
        bits = bits.BitsReverse();

        if (BindingContext is NotificationViewModel viewModel)
            viewModel.SaveNotificationCommand.Execute(bits);
    }

    private async Task<PermissionStatus> CheckAndRequestLocationPermission<T>() where T : Permissions.BasePermission, new()
    {
        PermissionStatus status = await Permissions.CheckStatusAsync<T>();

        if (status == PermissionStatus.Granted)
            return status;
#if IOS
        if (status == PermissionStatus.Denied)
        {
            await DisplayAlertAsync("Notification", "Pour activer les notifications, veuillez les activer dans les paramètre", "OK");
            return status;
        }
#endif
#if ANDROID
        if (Permissions.ShouldShowRationale<T>())
        {
            await DisplayAlertAsync("Notification", "Veuillez activez les notifications afin de vous prévenir des dernières actualités", "OK");
        }
#endif
        status = await Permissions.RequestAsync<T>();
        return status;
    }
}