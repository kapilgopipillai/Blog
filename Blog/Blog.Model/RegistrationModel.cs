using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model
{
    public class RegistrationModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string PhoneNumber { get; set; }

        public string EmailAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public bool Disabled { get; set; }

        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTime Modified { get; set; }

        public string ModifiedBy { get; set; }
    }
}
