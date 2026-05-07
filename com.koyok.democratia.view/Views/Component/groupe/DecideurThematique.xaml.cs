using com.koyok.democratia.Domain.Models;

namespace com.koyok.democratia.UI.Component.groupe;

public partial class DecideurThematique : ContentView
{
	public DecideurThematique()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if(BindingContext is Thematique thematique)
            ratioLabel.Text = $"{thematique.sommeUtilise}€ / {thematique.budget}€";
    }
}