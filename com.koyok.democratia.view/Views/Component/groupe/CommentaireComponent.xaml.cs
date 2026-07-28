using com.koyok.democratia.Domain.Models;
using com.koyok.democratia.view.Resources.Localization;

namespace com.koyok.democratia.UI.Component.groupe;

public partial class CommentaireComponent : ContentView
{
    public static BindableProperty CouleurProperty = BindableProperty.Create(
        nameof(Couleur),typeof(string), typeof(CommentaireComponent),defaultValue:"#000000");
    public string Couleur
    {
        get => ( string )GetValue( CouleurProperty );
        set => SetValue( CouleurProperty, value );
    }
	public CommentaireComponent()
	{
		InitializeComponent();
	}

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();
        if (BindingContext is Commentaire commentaire)
        {
            string name = (bool)commentaire.himself! ? AppResources.vous : $"{commentaire.prenomAuteur} {commentaire.nomAuteur}";
            nameAuteur.Text = name + $" ({commentaire.role})";
            sigleLabel.Text = (bool)commentaire.himself! ? AppResources.vous : $"{commentaire.prenomAuteur.ToUpper().First()} {commentaire.nomAuteur.ToUpper().First()}";
        }
    }
}