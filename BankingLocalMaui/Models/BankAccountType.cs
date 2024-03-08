using SQLite;

namespace BankingLocalMaui.Models
{
    public class BankAccountType
    {
        [PrimaryKey, AutoIncrement]
        public int BankAccountTypeId { get; set; }
        public string BankAccountTypeDescription { get; set; }
    }
}
