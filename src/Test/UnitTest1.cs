using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Primitives;

namespace Test
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var connectionString = "Server=localhost;Database=OPG_DB;Trusted_Connection=True;TrustServerCertificate=true;";
            //var testCs = new DataAccess(connectionString);
            //var data = await testCs.GetAllPersonaggiBase();
            //var personaggiInPartita = await testCs.GetAllPersonaggiInPartitaFromDb();

        }
    }
}