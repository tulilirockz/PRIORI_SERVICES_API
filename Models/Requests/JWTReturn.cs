namespace PRIORI_SERVICES_API.Model.Request;

public sealed class JWTReturn
{
    public int id { get; set; } = 0;
    public string jwt_key { get; set; } = String.Empty;
}