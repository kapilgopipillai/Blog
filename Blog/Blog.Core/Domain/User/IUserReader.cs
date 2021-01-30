using Blog.Model;
using Blog.Model.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Domain.User
{
    public interface IUserReader
    {
        Task<RegistrationModel> Login(string UserName, string Password);
        //Task UpdateRefreshToken(RefreshToken token);
    }
}
