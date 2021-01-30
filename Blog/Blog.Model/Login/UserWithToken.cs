using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Model.Login
{
    public class UserWithToken: RegistrationModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public UserWithToken(RegistrationModel user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.UserName = user.UserName;
            this.UserPassword = user.UserPassword;
            this.PhoneNumber = user.PhoneNumber;
            this.EmailAddress = user.EmailAddress;
            this.City = user.City;
            this.State = user.State;
            this.Address = user.Address;
            this.PostalCode = user.PostalCode;
            this.Disabled = user.Disabled;
            this.Created = user.Created;
            this.CreatedBy = user.CreatedBy;
            this.Modified = user.Modified;
            this.ModifiedBy = user.ModifiedBy;
        }
    }
}
