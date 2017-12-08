using System.Collections.Generic;
using System.Data.SqlClient;
using Herotale.IRepositories;
using Herotale.Models;

namespace Herotale.Contexts
{
    public class AccountContext
    {
        readonly IAccountRepository Rep;

        public AccountContext(IAccountRepository rep)
        {
            Rep = rep;    
        }

        public bool InsertAccount(Account acc)
        {
           return Rep.Insert(acc);
        }

        public bool LoginAccount(Account acc)
        {
            return Rep.LoginChecker(acc);
        }

        public string LoginId(Account acc)
        {
            return Rep.IdGetter(acc);
        }
    }
}