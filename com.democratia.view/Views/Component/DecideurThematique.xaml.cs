using com.democratia.Models;
using com.democratia.view.Resources.Localization;

namespace com.democratia.Views.Component;

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
            ratioLabel.Text = $"{thematique.Somme_utilise}€ / {thematique.budget}€";
    }
}