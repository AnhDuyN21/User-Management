using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AccountDTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }

        public string Email { get; set; } = null!;

        public string? Address { get; set; }

        public string? Phonenumber { get; set; }

        public int? RoleId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
