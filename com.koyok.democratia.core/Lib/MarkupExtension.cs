using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace com.koyok.democratia.Lib
{

    [ContentProperty(nameof(EnumType))]
    public class CritereMarkup : IMarkupExtension
    {
        public Type? EnumType { get; set; }

        public CritereMarkup() { }


        public CritereMarkup(Type enumType)
        {
            EnumType = enumType;
        }
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (EnumType is null || !EnumType.IsEnum)
                throw new ArgumentException("Vous devez spécifier un type Enum valide.");

            return Enum.GetValues(EnumType);
        }
    }
}
