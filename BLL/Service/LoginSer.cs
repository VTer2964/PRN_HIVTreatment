using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Repository;

namespace BLL.Service
{
    public class LoginSer
    {
        private readonly AccountRepo _Repo;
        public LoginSer()
        {
            _Repo = new AccountRepo();
        }
        public async Task<Account?> UserLogin(string name_or_email, string password)
        {
            return await _Repo.UserLogin(name_or_email, password);
        }
    }
}
