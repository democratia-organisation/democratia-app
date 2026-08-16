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

    private void EnregistrerButton_Clicked(object sender, EventArgs e)
    {
        var bits = new BitArray(checkBoxes.Length);
        foreach (CheckBox checkBox in checkBoxes)
            bits[Array.IndexOf(checkBoxes, checkBox)] = checkBox.IsChecked;

        if (BindingContext is NotificationViewModel viewModel)
            viewModel.SaveNotificationCommand.Execute(bits);
    }
}