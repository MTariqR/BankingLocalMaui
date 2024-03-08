using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BankingLocalMaui.Models
{
    public class Client
    {
        [PrimaryKey,AutoIncrement]
        public int ClientId { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientSurname { get; set; }
        public string ClientEmail { get; set; }
        public string ClientSaIdNumber { get; set; }
        public DateTime ClientDateOfBirth { get; set; }
        public string ClientContactNumber { get; set; }
        public string ClientPhysicalAddress { get; set;}

        [ForeignKey(typeof(ClientType))]
        public int ClientTypeId { get; set; }

        [OneToOne]
        public ClientType ClientType { get; set; } //if you query this table it will populate the client type description/ instead of just having the id you'll have what the ID references

        [ForeignKey(typeof(Bank))]
        public int BankId { get; set; }

        [ManyToOne] // **these statements are not needed 
        public Bank Bank { get; set; } // **these statements are not needed

        [OneToMany(CascadeOperations =CascadeOperation.All)]
        public List<BankAccount> BankAccounts { get; set; }

        public Client() //constructor to create an instance of bank account
        {
            BankAccounts = new();
        }

    }
}
