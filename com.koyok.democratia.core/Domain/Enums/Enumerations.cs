namespace com.koyok.democratia.Domain.Enumerations
{
    public enum Critere
    {
        POPULARITE,
        PRIX,
        REACTIONS
    }

    public enum TypeGestion
    {
        MODIFIER,
        AJOUTER
    }

    public enum Role
    {
        ADMINISTRATEUR,
        DECIDEUR,
        MEMBRE
    }

    public enum Settings
    {
        Theme,
        Language
    }
    public enum SecureStorageKeys
    {
        API_KEY,
        REFRESH,
        is_refresh_key_fresh,
        IdInternaute,
        MotDePasseInternaute,
        LastLogin,
        isConnected
    }

}
