using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingLocalMaui.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLitePCL;


namespace BankingLocalMaui.Services
{
    public class BankingLocalDatabase
    {
        private SQLiteConnection _dbConnection;
        public string GetDatabasePath() =>
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bankingdata.db");
        /*{
            string fileName = "bankingdata.db";
            string pathToDb = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(pathToDb,fileName);
            //return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "bankingdata.db");
        }*/

        public BankingLocalDatabase() 
        {
            _dbConnection = new SQLiteConnection(GetDatabasePath());

            _dbConnection.CreateTable<ClientType>();
            _dbConnection.CreateTable<BankAccountType>();
            _dbConnection.CreateTable<TransactionType>();
            _dbConnection.CreateTable<Bank>();
            _dbConnection.CreateTable<Client>();
            _dbConnection.CreateTable<BankAccount>();
            _dbConnection.CreateTable<Transaction>();

            SeedDatabase();
        }

        public void SeedDatabase()
        {
            if (_dbConnection.Table<ClientType>().Count() == 0) //If the rows in the table is 0, it will run the code//we want SeedDatabase() to do nothing if there are ANY rows
            {
                ClientType clientType = new()
                {
                    ClientTypeDescription = "Private"
                };
                _dbConnection.Insert(clientType);

                clientType = new()
                {
                    ClientTypeDescription = "MVP"
                };
                _dbConnection.Insert(clientType);

                clientType = new()
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

            if (_dbConnection.Table<Client>().Count() == 0)
            {
                Client client = new()
                {
                    ClientFirstName = "Brandon",
                    ClientSurname = "Mack",
                    ClientSaIdNumber = "1843877687",
                    ClientContactNumber = "0825551234",
                    ClientEmail = "me@computer.com",
                    ClientPhysicalAddress = "Swellendam"
                };
                _dbConnection.Insert(client);

                Bank bank = new()
                {
                    BankName = "Nedbank",
                    BranchCode = "12345"
                };
                _dbConnection.Insert(bank);

                bank.Clients.Add(client);
                _dbConnection.UpdateWithChildren(bank);

                BankAccount bankAccount = new()
                {
                    BankAccountNumber = "0001",
                    BankAccountTypeId = 1,
                };
                _dbConnection.Insert(bankAccount);

                client.BankAccounts.Add(bankAccount);
                _dbConnection.UpdateWithChildren(client);

                Transaction transaction = new()
                {
                    TransactionAmount = 1000,
                    TransactionDescription = "Money for lunch",
                    TransactionDatetime = DateTime.Now,
                };
                bankAccount.DepositMoney(transaction);
                SaveTransaction(bankAccount, transaction);

                try
                {
                    transaction = new()
                    {
                        TransactionAmount = 10,
                        TransactionDescription = "Withdraw for lunch",
                        TransactionDatetime = DateTime.Now,
                    };
                    bankAccount.WithdrawMoney(transaction);
                    SaveTransaction(bankAccount, transaction);
                }
                catch (InvalidOperationException ex)
                {
                    //Display message in UI
                }
            }
        }

        public List<ClientType> GetAllClientTypes()=>
            _dbConnection.Table<ClientType>().ToList(); //simpler syntax
        /*{
            var clientTypes = _dbConnection.Table<ClientType>().ToList();

            return clientTypes;
        }*/

        public void SaveTransaction(BankAccount bankAccount, Transaction transaction)
        {
            _dbConnection.Insert(transaction);
            _dbConnection.UpdateWithChildren(bankAccount);
        }

        public List<Client> GetAllClients() =>
            _dbConnection.Table<Client>().ToList(); //returns a list

        public Client GetClientBySaId(string saId) =>
            _dbConnection.Table<Client>().Where(x => x.ClientSaIdNumber == saId).FirstOrDefault(); //returns a single entry where it compares 'x' to a parameter and returns FirstOrDefault

        public List<Transaction> GetAllTransactions()=> 
            _dbConnection.Table<Transaction>().ToList();

        public Client GetClientById(int id)
        {
            Client client = _dbConnection.Table<Client>().Where(x => x.ClientId == id).FirstOrDefault();

            if(client != null)
                _dbConnection.GetChildren(client, true);

            return client;
        }
    }
}
