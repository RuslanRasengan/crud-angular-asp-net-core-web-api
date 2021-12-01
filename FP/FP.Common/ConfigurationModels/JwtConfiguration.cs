using FP.Interfaces.Common.ConfigurationModels;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FP.Common.ConfigurationModels
{
    public class JwtConfiguration : IJwtConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
    }
}
