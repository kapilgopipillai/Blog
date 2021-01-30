using Blog.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Reader.User
{
    public interface IUserDataReader
    {
        Task<RegistrationEnity> Login(string UserName, string Password);
    }
}
