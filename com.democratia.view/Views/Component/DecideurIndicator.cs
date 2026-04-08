using Microsoft.Maui.Controls.Shapes;

namespace com.democratia.Views.Component;

public partial class DecideurIndicator : ContentView
{
    public static readonly BindableProperty CouleurCercleProperty = BindableProperty.Create(
        nameof(CouleurCercle),
        typeof(Color),
        typeof(DecideurIndicator),
        default(Color),
        BindingMode.TwoWay
    );

    public static readonly BindableProperty TexteProperty = BindableProperty.Create(
        nameof(Texte),
        typeof(string),
        typeof(DecideurIndicator),
        default(string),
        BindingMode.TwoWay
    );

    public string? Texte 
	{ 
		get => (string)GetValue(TexteProperty); 
		set => SetValue(TexteProperty,value);
	}


    public Color? CouleurCercle 
	{ 
		get => (Color)GetValue(CouleurCercleProperty); 
		set => SetValue(CouleurCercleProperty,value) ; 
	}
    
	public DecideurIndicator()
	{
		var app = Application.Current!;
		var elipse = new Ellipse
		{
			WidthRequest = (double)app.Resources["SpacingLarge"],
			HeightRequest = (double)app.Resources["SpacingLarge"],
			
		};
		var label = new Label
		{
			Style = (Style)app.Resources["LabelStyle"],
			VerticalOptions = LayoutOptions.Center
		};
		label.SetBinding(Label.TextProperty, new Binding(nameof(Texte), source: this));
        elipse.SetBinding(Ellipse.FillProperty, new Binding(nameof(CouleurCercle), source: this));
        Content = new HorizontalStackLayout
		{
			Spacing = (double)app.Resources["SpacingVerySmall"],
			Children = { elipse, label }
		};
	}
}