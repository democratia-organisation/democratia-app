namespace com.democratia.view.Views.Component;

public class ButtonGroupe : ContentView
{
	private string? imageUrl;
	private string? title;

    public ButtonGroupe(string? imageUrl, string? title)
	{
		this.imageUrl = imageUrl;
		this.title = title;
		Content = new VerticalStackLayout
		{
			Children = {
				new ImageButton { Source = this.imageUrl, HeightRequest = 50, WidthRequest = 50, HorizontalOptions = LayoutOptions.Center },
				new Label { HorizontalOptions = LayoutOptions.Center, Text = this.title, Style = (Style?)Application.Current?.Resources["SubHeadlineStyle"] },
                
			}
		};
	}
}