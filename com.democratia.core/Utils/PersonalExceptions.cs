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
        public static string? MappingException(Exception e, ILocalizationService localizationService, string? personaliseMessage = null)
        {
            switch (e)
            {
                case EmptyEmailFieldException _:
                    return personaliseMessage ?? localizationService.GetString("errorPasswordMessage");
                case EmptyPassWordFieldException _:
                    return personaliseMessage ?? localizationService?.GetString("errorMailMessage");
                case EmptyRequiredFieldException _:
                    return personaliseMessage ?? localizationService.GetString("");
                case MailException _:
                    return personaliseMessage ?? localizationService.GetString("");
                case PassWordException _:
                    return personaliseMessage ?? localizationService.GetString("");
                case ConnexionErrorException _:
                    return personaliseMessage ?? localizationService?.GetString("connexionErreur");
                case NoUserException _:
                    return personaliseMessage ?? localizationService.GetString("noUser");
                case BadPasswordException _:
                    return personaliseMessage ?? localizationService.GetString("mauvaisMdp");
                case FetchDataException _:
                    return personaliseMessage ?? localizationService.GetString("erreurDonne");
                case CompteExistantException _:
                    return personaliseMessage ?? localizationService.GetString("");
                default:
                    return personaliseMessage ?? localizationService?.GetString("erreurInattendu");
            }
        }
    }
}
