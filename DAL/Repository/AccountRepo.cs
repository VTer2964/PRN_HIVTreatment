using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class AccountRepo
    {
        private readonly AppDbContext _context;
        public AccountRepo()
        {
            _context = new AppDbContext();
        }
        // Login
        public async Task<Account?> UserLogin(string name_or_email, string password)
        {
            return await _context.Accounts.Include(a => a.User).FirstOrDefaultAsync
                (u => (u.Email == name_or_email || u.Username == name_or_email) && u.PasswordHash == password);
        }
        // lấy role account
        public string? getRoleAccount(Account a) => a.User?.Role;
    }
}
