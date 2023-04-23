using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

        public async Task<IEnumerable<PersonaggioBase>> GetAllPersonaggiBaseFromDb()
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
                }); ;
            }

            return allCharacter;

        }


        public async Task<IEnumerable<PersonaggioInPartita>> GetAllPersonaggiInPartitaFromDb()
        {
            string getFromDB = @"      SELECT 
                                    	   PB.[id_personaggio]
                                    	  ,PB.[nome]
                                    	  ,Pb.[punti_vita_massimi]
                                    	  ,PB.[attacco]
                                    	  ,PB.[difesa]
                                          ,PB.[Descrizione]
                                          ,PP.[id_personaggio_partita]
                                          ,PP.[id_partita]
                                          ,PP.[livello]
                                          ,PP.[tipo_personaggio]
                                          ,PP.[posizione_x_personaggio]
                                          ,PP.[posizione_y_personaggio]
                                          ,PP.[punti_vita]
                                          ,PP.[attacco]
                                          ,PP.[difesa]
                                          ,PP.[stato]
                                          ,PP.[taglia]
                                    
                                      FROM [dbo].[Personaggi_In_Partita] PP
                                      LEFT JOIN 
                                      [dbo].[Personaggi_Base] PB
                                    	ON PP.id_personaggio_base = PB.id_personaggio
                                        WHERE tipo_personaggio=1";




            await using var conn = new SqlConnection(_connString);
            await using var cmd = new SqlCommand(getFromDB, conn);


            await conn.OpenAsync();

            using var r = await cmd.ExecuteReaderAsync();

            var allCharacter = new List<PersonaggioInPartita>();

            while (await r.ReadAsync())
            {
                var statoPg = new Stato();
                var statoString = r.IsDBNull(15) ? string.Empty : r.GetString(15);


                allCharacter.Add(new PersonaggioInPartita()
                );
                allCharacter[0].Id = r.GetInt32(0);
                allCharacter[0].Nome = r.GetString(1);
                allCharacter[0].PuntiVitaMassimi = r.GetInt32(2);
                allCharacter[0].Attacco = r.GetInt32(3);
                allCharacter[0].Difesa = r.GetInt32(4);
                allCharacter[0].Descrizione = r.IsDBNull(5) ? string.Empty : r.GetString(5);
                allCharacter[0].IdPersonaggioPartita = r.GetInt32(6);
                allCharacter[0].IdPartita = r.GetInt32(7);
                allCharacter[0].Livello = r.GetInt32(8);
                allCharacter[0].TipoPersonaggio = r.GetInt32(9);
                allCharacter[0].PosizioneXPersonaggio = r.GetInt32(10);
                allCharacter[0].PosizioneYPersonaggio = r.GetInt32(11);
                allCharacter[0].PuntiVitaPersonaggio = r.GetInt32(12);
                allCharacter[0].Attacco_InPartita = r.GetInt32(13);
                allCharacter[0].Difesa_InPartita = r.GetInt32(14);
                allCharacter[0].Stato = statoPg;
                allCharacter[0].Taglia = r.GetInt32(16);
            }

            return allCharacter;

        }




        public async Task<IEnumerable<PersonaggioInPartita>> GetAllNpcsFromDb()
        {
            string getFromDB = @"      SELECT 
                                    	   PB.[id_personaggio]
                                    	  ,PB.[nome]
                                    	  ,Pb.[punti_vita_massimi]
                                    	  ,PB.[attacco]
                                    	  ,PB.[difesa]
                                          ,PB.[Descrizione]
                                          ,PP.[id_personaggio_partita]
                                          ,PP.[id_partita]
                                          ,PP.[livello]
                                          ,PP.[tipo_personaggio]
                                          ,PP.[posizione_x_personaggio]
                                          ,PP.[posizione_y_personaggio]
                                          ,PP.[punti_vita]
                                          ,PP.[attacco]
                                          ,PP.[difesa]
                                          ,PP.[stato]
                                          ,PP.[taglia]
                                    
                                      FROM [dbo].[Personaggi_In_Partita] PP
                                      LEFT JOIN 
                                      [dbo].[Personaggi_Base] PB
                                    	ON PP.id_personaggio_base = PB.id_personaggio
                                        WHERE tipo_personaggio=2";




            await using var conn = new SqlConnection(_connString);
            await using var cmd = new SqlCommand(getFromDB, conn);


            await conn.OpenAsync();

            using var r = await cmd.ExecuteReaderAsync();

            var allCharacter = new List<PersonaggioInPartita>();

            while (await r.ReadAsync())
            {
                var statoPg = new Stato();
                var statoString = r.IsDBNull(15) ? string.Empty : r.GetString(15);


                allCharacter.Add(new PersonaggioInPartita()
                );
                allCharacter[0].Id = r.GetInt32(0);
                allCharacter[0].Nome = r.GetString(1);
                allCharacter[0].PuntiVitaMassimi = r.GetInt32(2);
                allCharacter[0].Attacco = r.GetInt32(3);
                allCharacter[0].Difesa = r.GetInt32(4);
                allCharacter[0].Descrizione = r.IsDBNull(5) ? string.Empty : r.GetString(5);
                allCharacter[0].IdPersonaggioPartita = r.GetInt32(6);
                allCharacter[0].IdPartita = r.GetInt32(7);
                allCharacter[0].Livello = r.GetInt32(8);
                allCharacter[0].TipoPersonaggio = r.GetInt32(9);
                allCharacter[0].PosizioneXPersonaggio = r.GetInt32(10);
                allCharacter[0].PosizioneYPersonaggio = r.GetInt32(11);
                allCharacter[0].PuntiVitaPersonaggio = r.GetInt32(12);
                allCharacter[0].Attacco_InPartita = r.GetInt32(13);
                allCharacter[0].Difesa_InPartita = r.GetInt32(14);
                allCharacter[0].Stato = statoPg;
                allCharacter[0].Taglia = r.GetInt32(16);
            }

            return allCharacter;

        }

        public async Task<Combattimento?> GetAllCombattimentoByIdFromDb(int idCombattimento)
        {

            string query = $@"select	C.Id_ListaEroi, 
                           		    C.Nome, 
                           		    LC.IdCombattente, 
                           		    LC.TipoPersonaggio
                           FROM 
                           dbo.combattimenti C
                           LEFT 
                           JOIN
                           dbo.ListeCombattenti LC
                           ON C.Id_ListaEroi = LC.Id_Combattimento
                           WHERE C.Id_ListaEroi = @idCombattimento";

            await using var conn = new SqlConnection(_connString);
            await using var cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@idCombattimento",idCombattimento);

            await conn.OpenAsync();

            using var r = await cmd.ExecuteReaderAsync();

            var combattimento  = new Combattimento();

            while (await r.ReadAsync())
            {
                combattimento.Id = r.GetInt32(0);
                combattimento.Nome = r.GetString(1);

                if (r.GetInt32(3) == 1)
                    combattimento.ListaEroi.Add(r.GetInt32(2));
                if (r.GetInt32(3) == 2)
                    combattimento.ListaNPCs.Add(r.GetInt32(2));
            }

            if (combattimento.Id == -1)
                return null;

            return combattimento;

        }

        public async Task<IEnumerable<Combattimento>> GetAllCombattimentiFromDb()
        {
            string query = $@"select	C.Id_ListaEroi, 
                           		        C.Nome
                           FROM 
                           dbo.combattimenti C";

            await using var conn = new SqlConnection(_connString);
            await using var cmd = new SqlCommand(query, conn);

            await conn.OpenAsync();

            using var r = await cmd.ExecuteReaderAsync();

            var combattimenti = new List<Combattimento>();

            while (await r.ReadAsync())
            {
                var comb = new Combattimento();
                comb.Id = r.GetInt32(0);
                comb.Nome = r.GetString(1);
                combattimenti.Add(comb);
            }
            return combattimenti;
        }
        
    }
}
