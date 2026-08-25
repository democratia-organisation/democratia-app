using com.koyok.democratia.Lib;

namespace com.koyok.democratia.Domain.Exception
{
    internal class CredentialException : System.Exception { }

    internal class EmptyEmailFieldException : System.Exception { }

    internal class EmptyPassWordFieldException : System.Exception { }

    public class TooManyRequestException(int delay) : System.Exception { 
        public int Delay { get => delay; } 
        private int delay = delay;
        public void CountdownAsync()
        {
            while (delay!=0)
            {
                Task.Delay(1000);
                delay -= 1;
            }
        }
    }

    internal class EmptyRequiredFieldException(string message) : System.Exception(message) 
    {
        public EmptyRequiredFieldException() : this("") { }

    }
    internal class ConnexionErrorException(string message) : System.Exception(message)
    {
        public ConnexionErrorException() : this("") { }
    }
    internal class FetchDataException : System.Exception { }

    internal class CompteExistantException : System.Exception { }
    internal class NoImageGiven : System.Exception { }


    public class MapExceptionMessage(ILocalizationService localizationService)
    {

        private readonly ILocalizationService localizationService = localizationService;
        public string? MappingException(System.Exception e, params object[] args)
        {
            return e switch
            {
                EmptyEmailFieldException => localizationService?.GetString("errorMailMessage"),
                EmptyPassWordFieldException => localizationService?.GetString("errorPasswordMessage"),
                EmptyRequiredFieldException when args.Length > 0 => localizationService?.GetString("errorEmptyFieldMessage", args[0]),
                EmptyRequiredFieldException => localizationService?.GetString("errorUnknowEmptyFieldMessage"),
                CredentialException => localizationService?.GetString("errorCredential"),
                ConnexionErrorException when args.Length > 0 => localizationService?.GetString("connexionErreur", args[0]),
                ConnexionErrorException => localizationService?.GetString("connexionErreur"),
                FetchDataException => localizationService?.GetString("erreurDonne"),
                CompteExistantException => localizationService?.GetString("compteExistantErreur"),
                NoImageGiven => localizationService?.GetString("erreurPhoto"),
                _ => localizationService?.GetString("erreurInattendu")
            };
        }
    }
}
