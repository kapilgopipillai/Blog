using Blog.Core.Mapping;
using Blog.Data.Reader.User;
using Blog.Entity;
using Blog.Model;
using Blog.Model.Login;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Domain.User
{
    public class UserReader: IUserReader
    {
        private readonly IUserDataReader _userDataReader;
        private readonly IObjectMapper _objectMapper;
        public UserReader(IUserDataReader userDataReader,
            IObjectMapper objectMapper)
        {
            _userDataReader = userDataReader;
            _objectMapper = objectMapper;
        }

        public async Task<RegistrationModel> Login(string UserName, string Password)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                return null;
            var model = await _userDataReader.Login(UserName, Password);
            var user = _objectMapper.Map<RegistrationEnity, RegistrationModel>(model);
            // check if password is correct
            if (!VerifyPasswordHash(Password, user.UserPassword))
                return null;

            return user;
        }
        private static bool VerifyPasswordHash(string password, string storedHash)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            //if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (password != storedHash) { return false; }

            return true;
        }

        //public async Task UpdateRefreshToken(RefreshToken token)
        //{
        //    var entity = _objectMapper.Map<RefreshToken, RefreshTokenEntity>(token);
        //    await _userDataWriter.UpdateRefreshToken(entity);
        //}
    }
}
