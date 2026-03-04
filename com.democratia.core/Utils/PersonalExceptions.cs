namespace com.democratia.Utils
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
            switch (e)
            {
                case EmptyEmailFieldException _:
                    return localizationService.GetString("errorPasswordMessage");
                case EmptyPassWordFieldException _:
                    return localizationService?.GetString("errorMailMessage");
                case EmptyRequiredFieldException _:
                    if (args.Length>0)
                        return localizationService.GetString("errorEmptyFieldMessage", args[0]);
                    else 
                        return localizationService.GetString("errorUnknowEmptyFieldMessage");
                case MailException _:
                    return localizationService.GetString("errorMailMessage");
                case PassWordException _:
                    return localizationService.GetString("errorPasswordMessage");
                case ConnexionErrorException _:
                    return localizationService?.GetString("connexionErreur");
                case NoUserException _:
                    return localizationService.GetString("noUser");
                case BadPasswordException _:
                    return localizationService.GetString("mauvaisMdp");
                case FetchDataException _:
                    return localizationService.GetString("erreurDonne");
                case CompteExistantException _:
                    return localizationService.GetString("compteExistantErreur");
                case NoImageGiven _:
                    return localizationService.GetString("erreurPhoto");
                default:
                    return localizationService?.GetString("erreurInattendu");
            }
        }
    }
}
