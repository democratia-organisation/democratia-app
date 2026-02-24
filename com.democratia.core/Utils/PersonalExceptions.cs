namespace com.democratia.Utils
{
    internal class MailException : Exception { }

    internal class PassWordException : Exception { }

    internal class EmptyEmailFieldException : Exception { }

    internal class EmptyPassWordFieldException : Exception { }

    internal class EmptyRequiredFieldException : Exception { }
    internal class ConnexionErrorException : Exception { }
    internal class BadPasswordException : Exception { }

    internal class NoUserException : Exception { }
    internal class FetchDataException : Exception { }

    internal class CompteExistantException : Exception { }


    internal static class MapExceptionMessage
    {
        public static string? MappingException(Exception e, ILocalizationService localizationService)
        {
            switch (e)
            {
                case EmptyEmailFieldException _:
                    return localizationService.GetString("errorPasswordMessage");
                case EmptyPassWordFieldException _:
                    return localizationService?.GetString("errorMailMessage");
                case EmptyRequiredFieldException _:
                    return localizationService.GetString("");
                case MailException _:
                    return localizationService.GetString("");
                case PassWordException _:
                    return localizationService.GetString("");
                case ConnexionErrorException _:
                    return localizationService?.GetString("connexionErreur");
                case NoUserException _:
                    return localizationService.GetString("noUser");
                case BadPasswordException _:
                    return localizationService.GetString("mauvaisMdp");
                case FetchDataException _:
                    return localizationService.GetString("erreurDonne");
                case CompteExistantException _:
                    return localizationService.GetString("");

                default:
                    return localizationService?.GetString("erreurInattendu");
            }
        }
    }
}
