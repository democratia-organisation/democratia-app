namespace com.democratia.Views.Component;

public class PropositionPresentation : ContentView
{
	public PropositionPresentation()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Text = "Welcome to .NET MAUI!"
				}
			}
		};
	}
}