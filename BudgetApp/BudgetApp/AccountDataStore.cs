using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    class AccountDataStore
    {
        public static AccountDataStore DataStore = new AccountDataStore();

        public AccountDataStore()
        {
            databaseAccessor = new DatabaseAccessor();
            accounts = databaseAccessor.SelectAllAccounts();
        }

        public List<Account> GetAccounts(Boolean refresh)
        {
            if (refresh) accounts = databaseAccessor.SelectAllAccounts();
            return accounts;
        }

        public Account GetAccountById(int id, Boolean refresh)
        {
            if (refresh) accounts = databaseAccessor.SelectAllAccounts();
            return accounts.First(a => a.AccountId == id);
        }

        private DatabaseAccessor databaseAccessor;
        private List<Account> accounts;
    }
}
