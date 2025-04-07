using System;

namespace CRM.Interfaces;

public interface ITokenService
{
    public string CreateToken(string userid , string email, string username);
    public string VerifyTokenGetId(string token);

}
