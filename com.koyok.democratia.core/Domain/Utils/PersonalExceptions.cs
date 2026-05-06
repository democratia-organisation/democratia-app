namespace com.koyok.democratia.core.Domain.Utils
{
    internal class MailException : Exception { }

    internal class PassWordException : Exception { }

    internal class EmptyEmailFieldException : Exception { }

    internal class EmptyPassWordFieldException : Exception { }

    internal class EmptyRequiredFieldException(string message) : Exception(message) 
    {
        public EmptyRequiredFieldException() : this("") { }

    }
    internal class ConnexionErrorException : Exception { }
    internal class BadPasswordException : Exception { }

    internal class NoUserException : Exception { }
    internal class FetchDataException : Exception { }

    internal class CompteExistantException : Exception { }
    internal class NoImageGiven : Exception { }


    internal static class MapExceptionMessage
    {
        public static string? MappingException(Exception e, ILocalizationService localizationService, params object[] args)
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
