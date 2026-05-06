using com.koyok.democratia.Models;
using com.koyok.democratia.view.Resources.Localization;

namespace com.koyok.democratia.UI.Component;

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