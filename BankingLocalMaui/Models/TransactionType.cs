using SQLite;

namespace BankingLocalMaui.Models
{
    public class TransactionType
    {
        [PrimaryKey, AutoIncrement]
        public int TransactionTypeId { get; set; }
        public string TransactionTypeDescription { get; set; }
    }
}
