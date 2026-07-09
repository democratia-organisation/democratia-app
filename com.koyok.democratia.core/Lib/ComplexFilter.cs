using com.koyok.democratia.Lib;
using Microsoft.Maui.Controls.Xaml;

namespace com.koyok.democratia.Lib
{
    public class ComplexFilter(string texte) : IMarkupExtension<ComplexFilterEnum>
    {

        public string Texte { get; set; } = texte;
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return ((IMarkupExtension<ComplexFilterEnum>)this).ProvideValue(serviceProvider);
        }

        public ComplexFilter() : this(string.Empty) { }

        ComplexFilterEnum IMarkupExtension<ComplexFilterEnum>.ProvideValue(IServiceProvider serviceProvider)
        {
            if (string.IsNullOrWhiteSpace(Texte))
                throw new ArgumentNullException(nameof(serviceProvider), "valeur non valable");
            if (Enum.TryParse<ComplexFilterEnum>(Texte, false, out var result))
                return result;
            throw new ArgumentNullException(nameof(serviceProvider), "valeur non valable");
        }
    }
}
