using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(256)]
        public string UserName { get; set; }
        
        [MinLength(3)]
        public string Password { get; set; }

        [MinLength(3)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
