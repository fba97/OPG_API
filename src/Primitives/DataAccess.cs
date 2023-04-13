using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class DataAccess
    {
       private readonly string _connString = string.Empty;
        public DataAccess(IConfiguration configuration) 
        {
                _connString = configuration.GetConnectionString("sqlStringConnection");
        }

        public DataAccess(string connectionString)
        {
            _connString = connectionString;
        }

        public async Task<IEnumerable<PersonaggioBase>> GetAllCharacters()
        {
            string getFromDB = @"SELECT [id_personaggio]
                                        ,[nome]
                                        ,[punti_vita_massimi]
                                        ,[attacco]
                                        ,[difesa]
                                        ,[Descrizione]
                                     FROM [OPG_DB].[dbo].[Personaggi_Base]";
                  
                                


            await using var conn = new SqlConnection(_connString);
            await using var cmd = new SqlCommand(getFromDB, conn);


            await conn.OpenAsync();

            using var r = await cmd.ExecuteReaderAsync();

            var allCharacter = new List<PersonaggioBase>(); 

            while (await r.ReadAsync())
            {
                allCharacter.Add(new PersonaggioBase
                {
                    Id = r.GetInt32(0),
                    Nome = r.GetString(1),
                PuntiVitaMassimi = r.GetInt32(2),
                    Attacco = r.GetInt32(3),
                    Difesa = r.GetInt32(4),
                    Descrizione = r.IsDBNull(5) ? string.Empty : r.GetString(5)
                });;
            }

            return allCharacter;    

        }

    }
}
