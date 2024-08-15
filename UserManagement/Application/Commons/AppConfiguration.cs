using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commons
{
    public class JWTSection
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
    public class AppConfiguration
    {
        public string DatabaseConnection { get; set; }
        public JWTSection JWTSection { get; set; }
    }
}
