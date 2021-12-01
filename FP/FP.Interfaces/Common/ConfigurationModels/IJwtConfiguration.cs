using Microsoft.IdentityModel.Tokens;

namespace FP.Interfaces.Common.ConfigurationModels
{
    public interface IJwtConfiguration
    {
        string Issuer { get; }
        string Audience { get; }
        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}
