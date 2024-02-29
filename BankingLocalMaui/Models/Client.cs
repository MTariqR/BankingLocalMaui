using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ClientType ClientType { get; set; }

        [ForeignKey(typeof(Bank))]
        public int BankId { get; set; }

        [ManyToOne] // **these statements are not needed 
        public Bank Bank { get; set; } // **these statements are not needed

    }
}
