using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AccountDTOs
{
    public class RegisterDTO
    {
        public string? Username { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Address { get; set; }

        public string? Phonenumber { get; set; }
    }
}
