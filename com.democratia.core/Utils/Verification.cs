using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace com.democratia.Utils
{
    public static class Verification
    {
        public  static bool VerifierFormatage(string valeur, FormatRule champ) => champ.Check(valeur);
        
        public record FormatRule(string pattern)
        {
            private readonly Regex _regex = new(pattern, RegexOptions.Compiled | RegexOptions.CultureInvariant);

            public bool Check(string value) => value is string str && _regex.IsMatch(str);
        }
    }
}
