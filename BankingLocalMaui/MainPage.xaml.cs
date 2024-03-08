using BankingLocalMaui.Models;
using BankingLocalMaui.Services;

namespace BankingLocalMaui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        private BankingLocalDatabase _database;

        public MainPage()
        {
            InitializeComponent();
            _database = new BankingLocalDatabase();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

            List<Client> clientsList = _database.GetAllClients();
            List<Transaction> transactionsList = _database.GetAllTransactions();
            Client client2 = _database.GetClientById(2); 

            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
