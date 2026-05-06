using com.koyok.democratia.core.Domain.Service;

namespace com.koyok.democratia.Domain.Exception
{
    internal class MailException : System.Exception { }

    internal class PassWordException : System.Exception { }

    internal class EmptyEmailFieldException : System.Exception { }

    internal class EmptyPassWordFieldException : System.Exception { }

    internal class EmptyRequiredFieldException(string message) : System.Exception(message) 
    {
        public EmptyRequiredFieldException() : this("") { }

    }
    internal class ConnexionErrorException : System.Exception { }
    internal class BadPasswordException : System.Exception { }

    internal class NoUserException : System.Exception { }
    internal class FetchDataException : System.Exception { }

    internal class CompteExistantException : System.Exception { }
    internal class NoImageGiven : System.Exception { }


    internal static class MapExceptionMessage
    {
        public static string? MappingException(System.Exception e, ILocalizationService localizationService, params object[] args)
        {
            return e switch
            {
                EmptyEmailFieldException => localizationService?.GetString("errorMailMessage"),
                EmptyPassWordFieldException => localizationService?.GetString("errorPasswordMessage"),
                EmptyRequiredFieldException when args.Length > 0 => localizationService?.GetString("errorEmptyFieldMessage", args[0]),
                EmptyRequiredFieldException => localizationService?.GetString("errorUnknowEmptyFieldMessage"),
                MailException => localizationService?.GetString("errorMailMessage"),
                PassWordException => localizationService?.GetString("errorPasswordMessage"),
                ConnexionErrorException => localizationService?.GetString("connexionErreur"),
                NoUserException => localizationService?.GetString("noUser"),
                BadPasswordException => localizationService?.GetString("mauvaisMdp"),
                FetchDataException => localizationService?.GetString("erreurDonne"),
                CompteExistantException => localizationService?.GetString("compteExistantErreur"),
                NoImageGiven => localizationService?.GetString("erreurPhoto"),
                _ => localizationService?.GetString("erreurInattendu")
            };
        }
    }
}
