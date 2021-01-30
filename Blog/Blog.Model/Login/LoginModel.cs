using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Blog.Model.Login
{
    public partial class LoginModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserPassword { get; set; }
    }
}
