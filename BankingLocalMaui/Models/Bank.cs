using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BankingLocalMaui.Models
{
    public class Bank
    {
        [PrimaryKey,AutoIncrement]
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BranchCode { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Client> Clients { get; set; }
        public Bank()
        {
            Clients = new List<Client>();
        }
    }
}
