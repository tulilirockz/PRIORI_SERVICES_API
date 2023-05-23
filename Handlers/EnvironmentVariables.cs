namespace PRIORI_SERVICES_API.Shared;

public static class EnvironmentVariables
{
    public static readonly string PRIORI_SECRET_JWT_KEY = Environment.GetEnvironmentVariable("PRIORI_SECRET_JWT_KEY") ?? "s5nh2s1c2t41234l33t";
    public static readonly string PRIORI_DATABASE_PORT = Environment.GetEnvironmentVariable("PRIORI_DATABASE_PORT") ?? "1433";
    public static readonly string PRIORI_DATABASE_IP = Environment.GetEnvironmentVariable("PRIORI_DATABASE_IP") ?? "localhost";
    public static readonly string PRIORI_DATABASE_NAME = Environment.GetEnvironmentVariable("PRIORI_DATABASE_NAME") ?? "Priori";
    public static readonly string PRIORI_DATABASE_USER = Environment.GetEnvironmentVariable("PRIORI_DATABASE_USER") ?? "sa";
    public static readonly string? PRIORI_DATABASE_PASSWORD = Environment.GetEnvironmentVariable("PRIORI_DATABASE_PASSWORD");
    public static readonly string DATABASE_LOCALE = "E. South America Standard Time";
}