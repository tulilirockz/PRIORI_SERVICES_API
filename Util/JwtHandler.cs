using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PRIORI_SERVICES_API.Util;
public static class JwtHandler
{
    public static JwtSecurityToken GenJWT(IConfiguration configuration_context, List<Claim> claims, string jwt_key_attribute = "JWTKey")
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration_context.GetSection("JWTKey").Value!));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        return new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: creds
            );
    }
}
