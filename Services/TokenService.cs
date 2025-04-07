using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using CRM.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace CRM.Services;

public class TokenService : ITokenService
{
    private readonly string _secretKey;

    public TokenService(IConfiguration configuration) {
        _secretKey = configuration["Jwt:SecretKey"] ?? throw new ArgumentException("There is Some Problem with the SecretKey");
    }

    public string CreateToken(string userid, string email, string username)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var tokenDescriptor = new SecurityTokenDescriptor {
            
            Subject = new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, userid.ToString()),
                new Claim(ClaimTypes.Email , email),
                new Claim(ClaimTypes.Name, username)
            ]),
            Expires = DateTime.UtcNow.AddMonths(6),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string VerifyTokenGetId(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var validationParameters = new TokenValidationParameters {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            var principal = tokenHandler.ValidateToken(token , validationParameters,  out var validatedToken);

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if(userIdClaim != null) {
                return userIdClaim.Value;
            }else {
                throw new Exception("UserId not Found in Token!!");
            }
        }
        catch (Exception ex
        )
        {
            
             throw new Exception("Token validation failed: " + ex.Message);
        }
    }
}
