namespace PRIORI_SERVICES_API.Repository;

public static class APIConfiguration
{
    public static string PRIORI_SECRET_JWT_KEY { get; set; } = Environment.GetEnvironmentVariable("PRIORI_SECRET_JWT_KEY") ?? "s5nh2s1c2t41234l33t";
    public static string PRIORI_DATABASE_PORT { get; set; } = Environment.GetEnvironmentVariable("PRIORI_DATABASE_PORT") ?? "1433";
    public static string PRIORI_DATABASE_IP { get; set; } = Environment.GetEnvironmentVariable("PRIORI_DATABASE_IP") ?? "localhost";
    public static string PRIORI_DATABASE_NAME { get; set; } = Environment.GetEnvironmentVariable("PRIORI_DATABASE_NAME") ?? "Priori";
    public static string? PRIORI_DATABASE { get; set; } = Environment.GetEnvironmentVariable("PRIORI_DATABASE_USER");
    public static string? PRIORI_DATABASE_PASSWORD { get; set; } = Environment.GetEnvironmentVariable("PRIORI_DATABASE_PASSWORD");
}