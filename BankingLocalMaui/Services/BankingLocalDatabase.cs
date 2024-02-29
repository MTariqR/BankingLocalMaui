using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingLocalMaui.Models;
using SQLite;
using SQLitePCL;


namespace BankingLocalMaui.Services
{
    public class BankingLocalDatabase
    {
        private SQLiteConnection _dbConnection;
        public string GetDatabasePath()
        {
            string fileName = "bankingdata.db";
            string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(pathToDb,fileName);
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bankingdata.db");
        }

        public BankingLocalDatabase() 
        {
            _dbConnection = new SQLiteConnection(GetDatabasePath());

            _dbConnection.CreateTable<ClientType>();
            _dbConnection.CreateTable<BankAccountType>();
            _dbConnection.CreateTable<TransactionType>();
            _dbConnection.CreateTable<Bank>();

            SeedDatabase();
        }

        public void SeedDatabase()
        {
            if (_dbConnection.Table<ClientType>().Count() == 0) //If the rows in the table is 0, it will run the code//we want SeedDatabase() to do nothing if there are ANY rows
            {
                ClientType clientType = new ClientType()
                {
                    ClientTypeDescription = "Private"
                };
                _dbConnection.Insert(clientType);

                clientType = new ClientType()
                {
                    ClientTypeDescription = "MVP"
                };
                _dbConnection.Insert(clientType);

                clientType = new ClientType()
                {
                    ClientTypeDescription = "Standard"
                };
                _dbConnection.Insert(clientType);
            }

            if (_dbConnection.Table<BankAccountType>().Count() == 0)
            {
                BankAccountType bankAccountType = new()
                {
                    BankAccountTypeDescription = "Credit"
                };
                _dbConnection.Insert(bankAccountType);
                bankAccountType = new()
                {
                    BankAccountTypeDescription = "Savings"
                };
                _dbConnection.Insert(bankAccountType);
                bankAccountType = new()
                {
                    BankAccountTypeDescription = "Cheque"
                };
                _dbConnection.Insert(bankAccountType);
            }

            if (_dbConnection.Table<TransactionType>().Count() == 0)
            {
                TransactionType transactionType = new()
                {
                    TransactionTypeDescription = "Deposit"
                };
                _dbConnection.Insert(transactionType);

                transactionType = new()
                {
                    TransactionTypeDescription = "Withdraw"
                };
                _dbConnection.Insert(transactionType);
            }
        }

        public List<ClientType> GetAllClientTypes()
        {
            var clientTypes = _dbConnection.Table<ClientType>().ToList();

            return clientTypes;
        }
    }
}
