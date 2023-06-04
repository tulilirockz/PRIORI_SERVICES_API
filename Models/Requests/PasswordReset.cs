namespace PRIORI_SERVICES_API.Model.Request;

public sealed class PasswordReset
{
    public string email {get;set;} = String.Empty;
    public string senha { get; set; } = String.Empty;
}