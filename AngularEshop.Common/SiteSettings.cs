using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Common
{
    public class SiteSettings
    {
        public JwtSettings JwtSettings { get; set; }
        public IdentitySettings IdentitySettings { get; set; }

        public PathTools PathTools { get; set; }
    }

    public class IdentitySettings
    {
        public bool PasswordRequiredDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool RequireUniqueEmail { get; set; }
    }

    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Encryptkey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
    }

    public class PathTools
    {
        public string Domain { get; set; }

        public string ProductImagePath { get; set; }
    }
}