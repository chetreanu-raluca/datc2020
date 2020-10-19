using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Models;

namespace Tema4
{
    class Program
    {
        private static CloudTableClient tableClient;

        private static CloudTable studentsTable;

        static void Main(string[] args)
        {
            Task.Run(async () => {await Initialize(); })
                .GetAwaiter()
                .GetResult();
        }

        static async Task Initialize()
        {
            string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=raludatc2020;AccountKey=dhl7NCUKYbDPL3bKrV4/H2DSY7cas5NO0+8ODVqOixZhba5IgXsy7ni58Y2z/o6w2WuI1zeM/5LgnQGBom5Ayg==;EndpointSuffix=core.windows.net";
            var account = CloudStorageAccount.Parse(storageConnectionString);
            tableClient = account.CreateCloudTableClient();

            studentsTable= tableClient.GetTableReference("studenti");

            await studentsTable.CreateIfNotExistsAsync();
            await AddNewStudent();
            await EditStudent();
            await GetAllStudents();
        }

        private static async Task GetAllStudents()
        {
            Console.WriteLine("UNIVERSITATE\tCNP\tNume\tEmail\tAn\tFacultate");
            TableQuery<StudentEntity> query = new TableQuery<StudentEntity>();

            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<StudentEntity> resultSegment = await studentsTable.ExecuteQuerySegmentedAsync(query, token);
                token= resultSegment.ContinuationToken;

                foreach (StudentEntity entity in resultSegment.Results)
                {
                    Console.WriteLine("{0}\t{1}\t{2} {3}\t{4}\t{5}\t{6}",entity.PartitionKey, entity.RowKey, entity.Prenume, entity.Nume,
                    entity.Email, entity.An_studiu, entity.Facultate);
                }

            } while (token!= null);
        }

        private static async Task AddNewStudent()
        {
            var student = new StudentEntity("UPT", "2980314286547");
            student.Prenume ="Melissa";
            student.Nume ="Popovici";
            student.Email ="melissa.popovici@yahoo.com";
            student.An_studiu = 3;
            student.Facultate ="ETC";

            var insertOperation = TableOperation.Insert(student);
            await studentsTable.ExecuteAsync(insertOperation);
        }

        private static async Task EditStudent()
        {
            var student = new StudentEntity("UVT", "2950626456334");
            student.Prenume= "Alessia";
            student.An_studiu = 4;
            student.ETag ="*";
            var editOperation = TableOperation.Merge(student);

            await studentsTable.ExecuteAsync(editOperation);
        }

    }
}
