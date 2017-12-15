using System;
using System.Collections.Generic;
using Herotale.Models;

namespace Herotale.IRepositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        List<Account> GetAll();
        bool RegisterChecker(string mail);
        bool LoginChecker(Account acc);
        bool CheckAdmin(Account acc);
        string IdGetter(Account acc);
		Account Get(int id);

    }
}