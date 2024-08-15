using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AccountDTOs
{
    public class CreateAccountDTO
    {
        public string? Username { get; set; }

        public string? Email { get; set; }
        public string? Password { get; set; }

        public string? Address { get; set; }

        public string? Phonenumber { get; set; }
        public int? RoleId { get; set; }
    }
}
