using Microsoft.WindowsAzure.Storage.Table;

namespace Models
{
    public class StudentEntity : TableEntity
    {
        public StudentEntity(string univeristy, string cnp)
        {
            this.PartitionKey = univeristy;
            this.RowKey= cnp;
        }

        public StudentEntity() {}

        public string Prenume {get; set; }

        public string Nume {get; set; }

        public string Email {get; set; }

        public string Facultate {get; set; }

        public int An_studiu {get; set; }
    }
}

