//using Microsoft.AspNetCore.Http.Features;
//using Microsoft.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlTypes;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Primitives
//{
//    public class DataAccess
//    {
//        private readonly string _connString = string.Empty;
//        public DataAccess(IConfiguration configuration)
//        {
//            _connString = configuration.GetConnectionString("sqlStringConnection");
//        }

//        public DataAccess(string connectionString)
//        {
//            _connString = connectionString;
//        }

//        public async Task AddPersonaggioInPartiotaToDb(Personaggio pgBase, int idPartita)
//        {
//            string query = @"INSERT INTO [dbo].[Personaggi_In_Partita]
//                                  ([id_personaggio_base]
//                                  ,[id_partita]
//                                  ,[taglia]
//                                  ,[livello]
//                                  ,[tipo_personaggio]
//                                  ,[posizione_x_personaggio]
//                                  ,[posizione_y_personaggio]
//                                  ,[punti_vita]
//                                  ,[attacco]
//                                  ,[difesa]
//                                  ,[stato])
//                            VALUES
//                                  (@idPersonaggioBase
//                                  ,@idPartita
//                                  ,0
//                                  ,0
//                                  ,1
//                                  ,0
//                                  ,0
//                                  ,@puntiVita
//                                  ,@attacco
//                                  ,@difesa
//                                  ,@stato)";

//            await using var conn = new SqlConnection(_connString);
//            await using var cmd = new SqlCommand(query, conn);

//            cmd.Parameters.AddWithValue("@idPersonaggioBase", pgBase.Id);
//            cmd.Parameters.AddWithValue("@idPartita", idPartita);
//            cmd.Parameters.AddWithValue("@puntiVita", pgBase.PuntiVitaMassimi);
//            cmd.Parameters.AddWithValue("@attacco", pgBase.Attacco);
//            cmd.Parameters.AddWithValue("@difesa", pgBase.Difesa);
//            cmd.Parameters.AddWithValue("@stato", "");

//            await conn.OpenAsync();
//            await cmd.ExecuteNonQueryAsync();
//        }
//        public async Task<IEnumerable<Personaggio>> GetAllPersonaggiBaseFromDb()
//        {
//            string getFromDB = @"SELECT [id_personaggio]
//                                        ,[nome]
//                                        ,[punti_vita_massimi]
//                                        ,[attacco]
//                                        ,[difesa]
//                                        ,[Descrizione]
//                                     FROM [OPG_DB].[dbo].[Personaggi_Base]";




//            await using var conn = new SqlConnection(_connString);
//            await using var cmd = new SqlCommand(getFromDB, conn);


//            await conn.OpenAsync();

//            using var r = await cmd.ExecuteReaderAsync();

//            var allCharacter = new List<Personaggio>();

//            while (await r.ReadAsync())
//            {
//                allCharacter.Add(new Personaggio(r.GetInt32(0),
//                    r.GetString(1),
//                    r.GetInt32(2),
//                    r.GetInt32(3),
//                    r.GetInt32(4),
//                   r.IsDBNull(5) ? string.Empty : r.GetString(5))
//                ); 
//            }

//            return allCharacter;

//        }

//        public async Task<PersonaggioInPartita> GetPersonaggioInPartitaByIdFromDb(int idPersonaggio)
//        {
//            var query = @" SELECT 
//                                    	   PB.[id_personaggio]
//                                    	  ,PB.[nome]
//                                    	  ,Pb.[punti_vita_massimi]
//                                    	  ,PB.[attacco]
//                                    	  ,PB.[difesa]
//                                          ,PB.[Descrizione]
//                                          ,PP.[id_personaggio_partita]
//                                          ,PP.[id_partita]
//                                          ,PP.[livello]
//                                          ,PP.[tipo_personaggio]
//                                          ,PP.[posizione_x_personaggio]
//                                          ,PP.[posizione_y_personaggio]
//                                          ,PP.[punti_vita]
//                                          ,PP.[attacco]
//                                          ,PP.[difesa]
//                                          ,PP.[stato]
//                                          ,PP.[taglia]
                                    
//                                      FROM [dbo].[Personaggi_In_Partita] PP
//                                      LEFT JOIN 
//                                      [dbo].[Personaggi_Base] PB
//                                    	ON PP.id_personaggio_base = PB.id_personaggio
//										where id_personaggio = @idPersonaggio";

//            await using var conn = new SqlConnection(_connString);
//            await using var cmd = new SqlCommand(query, conn);

//            cmd.Parameters.AddWithValue("@idPersonaggio", idPersonaggio);

//            await conn.OpenAsync();

//            using var r = await cmd.ExecuteReaderAsync();

//            PersonaggioInPartita pgInPartita = new();

//            while (await r.ReadAsync())
//            {
//                var statoPg = new Stato();
//                var statoString = r.IsDBNull(15) ? string.Empty : r.GetString(15);

//                pgInPartita.Id = r.GetInt32(0);
//                pgInPartita.Nome = r.GetString(1);
//                pgInPartita.PuntiVitaMassimi = r.GetInt32(2);
//                pgInPartita.Attacco = r.GetInt32(3);
//                pgInPartita.Difesa = r.GetInt32(4);
//                pgInPartita.Descrizione = r.IsDBNull(5) ? string.Empty : r.GetString(5);
//                pgInPartita.IdPersonaggioPartita = r.GetInt32(6);
//                pgInPartita.IdPartita = r.GetInt32(7);
//                pgInPartita.Livello = r.GetInt32(8);
//                pgInPartita.TipoPersonaggio = r.GetInt32(9);
//                pgInPartita.PosizioneXPersonaggio = r.GetInt32(10);
//                pgInPartita.PosizioneYPersonaggio = r.GetInt32(11);
//                pgInPartita.PuntiVitaPersonaggio = r.GetInt32(12);
//                pgInPartita.Attacco_InPartita = r.GetInt32(13);
//                pgInPartita.Difesa_InPartita = r.GetInt32(14);
//                pgInPartita.Stato = statoPg;
//                pgInPartita.Taglia = r.GetInt32(16);
//            }

//            return pgInPartita;

//        }

//        public async Task<IEnumerable<PersonaggioInPartita>> GetAllPersonaggiInPartitaFromDb()
//        {
//            string getFromDB = @"      SELECT 
//                                    	   PB.[id_personaggio]
//                                    	  ,PB.[nome]
//                                    	  ,Pb.[punti_vita_massimi]
//                                    	  ,PB.[attacco]
//                                    	  ,PB.[difesa]
//                                          ,PB.[Descrizione]
//                                          ,PP.[id_personaggio_partita]
//                                          ,PP.[id_partita]
//                                          ,PP.[livello]
//                                          ,PP.[tipo_personaggio]
//                                          ,PP.[posizione_x_personaggio]
//                                          ,PP.[posizione_y_personaggio]
//                                          ,PP.[punti_vita]
//                                          ,PP.[attacco]
//                                          ,PP.[difesa]
//                                          ,PP.[stato]
//                                          ,PP.[taglia]
                                    
//                                      FROM [dbo].[Personaggi_In_Partita] PP
//                                      LEFT JOIN 
//                                      [dbo].[Personaggi_Base] PB
//                                    	ON PP.id_personaggio_base = PB.id_personaggio
//                                        WHERE tipo_personaggio=1";




//            await using var conn = new SqlConnection(_connString);
//            await using var cmd = new SqlCommand(getFromDB, conn);


//            await conn.OpenAsync();

//            using var r = await cmd.ExecuteReaderAsync();

//            var allCharacter = new List<PersonaggioInPartita>();

//            while (await r.ReadAsync())
//            {
//                var statoPg = new Stato();
//                var statoString = r.IsDBNull(15) ? string.Empty : r.GetString(15);


//                allCharacter.Add(new PersonaggioInPartita()
//                );
//                allCharacter[0].Id = r.GetInt32(0);
//                allCharacter[0].Nome = r.GetString(1);
//                allCharacter[0].PuntiVitaMassimi = r.GetInt32(2);
//                allCharacter[0].Attacco = r.GetInt32(3);
//                allCharacter[0].Difesa = r.GetInt32(4);
//                allCharacter[0].Descrizione = r.IsDBNull(5) ? string.Empty : r.GetString(5);
//                allCharacter[0].IdPersonaggioPartita = r.GetInt32(6);
//                allCharacter[0].IdPartita = r.GetInt32(7);
//                allCharacter[0].Livello = r.GetInt32(8);
//                allCharacter[0].TipoPersonaggio = r.GetInt32(9);
//                allCharacter[0].PosizioneXPersonaggio = r.GetInt32(10);
//                allCharacter[0].PosizioneYPersonaggio = r.GetInt32(11);
//                allCharacter[0].PuntiVitaPersonaggio = r.GetInt32(12);
//                allCharacter[0].Attacco_InPartita = r.GetInt32(13);
//                allCharacter[0].Difesa_InPartita = r.GetInt32(14);
//                allCharacter[0].Stato = statoPg;
//                allCharacter[0].Taglia = r.GetInt32(16);
//            }

//            return allCharacter;

//        }




//        public async Task<IEnumerable<PersonaggioInPartita>> GetAllNpcsFromDb()
//        {
//            string getFromDB = @"      SELECT 
//                                    	   PB.[id_personaggio]
//                                    	  ,PB.[nome]
//                                    	  ,Pb.[punti_vita_massimi]
//                                    	  ,PB.[attacco]
//                                    	  ,PB.[difesa]
//                                          ,PB.[Descrizione]
//                                          ,PP.[id_personaggio_partita]
//                                          ,PP.[id_partita]
//                                          ,PP.[livello]
//                                          ,PP.[tipo_personaggio]
//                                          ,PP.[posizione_x_personaggio]
//                                          ,PP.[posizione_y_personaggio]
//                                          ,PP.[punti_vita]
//                                          ,PP.[attacco]
//                                          ,PP.[difesa]
//                                          ,PP.[stato]
//                                          ,PP.[taglia]
                                    
//                                      FROM [dbo].[Personaggi_In_Partita] PP
//                                      LEFT JOIN 
//                                      [dbo].[Personaggi_Base] PB
//                                    	ON PP.id_personaggio_base = PB.id_personaggio
//                                        WHERE tipo_personaggio=2";




//            await using var conn = new SqlConnection(_connString);
//            await using var cmd = new SqlCommand(getFromDB, conn);


//            await conn.OpenAsync();

//            using var r = await cmd.ExecuteReaderAsync();

//            var allCharacter = new List<PersonaggioInPartita>();

//            while (await r.ReadAsync())
//            {
//                var statoPg = new Stato();
//                var statoString = r.IsDBNull(15) ? string.Empty : r.GetString(15);


//                allCharacter.Add(new PersonaggioInPartita()
//                );
//                allCharacter[0].Id = r.GetInt32(0);
//                allCharacter[0].Nome = r.GetString(1);
//                allCharacter[0].PuntiVitaMassimi = r.GetInt32(2);
//                allCharacter[0].Attacco = r.GetInt32(3);
//                allCharacter[0].Difesa = r.GetInt32(4);
//                allCharacter[0].Descrizione = r.IsDBNull(5) ? string.Empty : r.GetString(5);
//                allCharacter[0].IdPersonaggioPartita = r.GetInt32(6);
//                allCharacter[0].IdPartita = r.GetInt32(7);
//                allCharacter[0].Livello = r.GetInt32(8);
//                allCharacter[0].TipoPersonaggio = r.GetInt32(9);
//                allCharacter[0].PosizioneXPersonaggio = r.GetInt32(10);
//                allCharacter[0].PosizioneYPersonaggio = r.GetInt32(11);
//                allCharacter[0].PuntiVitaPersonaggio = r.GetInt32(12);
//                allCharacter[0].Attacco_InPartita = r.GetInt32(13);
//                allCharacter[0].Difesa_InPartita = r.GetInt32(14);
//                allCharacter[0].Stato = statoPg;
//                allCharacter[0].Taglia = r.GetInt32(16);
//            }

//            return allCharacter;

//        }

//        public async Task<int> UpdatePersonaggioInPartitaByIdToDb(PersonaggioInPartita updatePersonaggio)
//        {
//            var query = @"UPDATE		[dbo].[Personaggi_In_Partita]
                                     
//							SET			id_partita = @id_partita,			
//										taglia = @taglia,
//										livello = @livello,
//										tipo_personaggio = @tipo_personaggio,
//										posizione_x_personaggio = @posizione_x_personaggio,
//										posizione_y_personaggio = @posizione_y_personaggio,
//										punti_vita = @punti_vita,
//										attacco = @attacco,
//										difesa = @difesa,
//										stato = @stato
										 
//							WHERE     id_personaggio_partita = @id_personaggio_partita";


//            await using var conn = new SqlConnection(_connString);
//            await using var cmd = new SqlCommand(query, conn);

//            cmd.Parameters.AddWithValue("@id_personaggio_partita", updatePersonaggio.IdPersonaggioPartita);
//            cmd.Parameters.AddWithValue("@id_partita", updatePersonaggio.IdPartita); 
//            cmd.Parameters.AddWithValue("@taglia", updatePersonaggio.Taglia); 
//            cmd.Parameters.AddWithValue("@livello", updatePersonaggio.Livello); 
//            cmd.Parameters.AddWithValue("@tipo_personaggio", updatePersonaggio.TipoPersonaggio); 
//            cmd.Parameters.AddWithValue("@posizione_x_personaggio", updatePersonaggio.PosizioneXPersonaggio); 
//            cmd.Parameters.AddWithValue("@posizione_y_personaggio", updatePersonaggio.PosizioneYPersonaggio);
//            cmd.Parameters.AddWithValue("@punti_vita", updatePersonaggio.PuntiVitaPersonaggio);
//            cmd.Parameters.AddWithValue("@attacco", updatePersonaggio.Attacco_InPartita);
//            cmd.Parameters.AddWithValue("@difesa", updatePersonaggio.Difesa_InPartita);
//            cmd.Parameters.AddWithValue("@stato", string.Empty);

//            await conn.OpenAsync();
//            return await cmd.ExecuteNonQueryAsync();
//        }



//        public async Task<Combattimento?> GetCombattimentoByIdFromDb(int idCombattimento)
//        {

//            string query = $@"select	C.Id_ListaEroi, 
//                           		    C.Nome, 
//                           		    LC.IdCombattente, 
//                           		    LC.TipoPersonaggio
//                           FROM 
//                           dbo.combattimenti C
//                           LEFT 
//                           JOIN
//                           dbo.ListeCombattenti LC
//                           ON C.Id_ListaEroi = LC.Id_Combattimento
//                           WHERE C.Id_ListaEroi = @idCombattimento";

//            await using var conn = new SqlConnection(_connString);
//            await using var cmd = new SqlCommand(query, conn);

//            cmd.Parameters.AddWithValue("@idCombattimento", idCombattimento);

//            await conn.OpenAsync();

//            using var r = await cmd.ExecuteReaderAsync();

//            var combattimento = new Combattimento();

//            while (await r.ReadAsync())
//            {
//                combattimento.Id = r.GetInt32(0);
//                combattimento.Nome = r.GetString(1);

//                if (r.GetInt32(3) == 1)
//                    combattimento.ListaEroi.Add(r.GetInt32(2));
//                if (r.GetInt32(3) == 2)
//                    combattimento.ListaNPCs.Add(r.GetInt32(2));
//            }

//            if (combattimento.Id == -1)
//                return null;

//            return combattimento;

//        }

//        public async Task<IEnumerable<Combattimento>> GetAllCombattimentiFromDb()
//        {
//            string query = $@"select	C.Id_ListaEroi, 
//                           		        C.Nome
//                           FROM 
//                           dbo.combattimenti C";

//            await using var conn = new SqlConnection(_connString);
//            await using var cmd = new SqlCommand(query, conn);

//            await conn.OpenAsync();

//            using var r = await cmd.ExecuteReaderAsync();

//            var combattimenti = new List<Combattimento>();

//            while (await r.ReadAsync())
//            {
//                var comb = new Combattimento();
//                comb.Id = r.GetInt32(0);
//                comb.Nome = r.GetString(1);
//                combattimenti.Add(comb);
//            }
//            var combattimentiTotale = new List<Combattimento>();
//            foreach(var c in combattimenti)
//            {
//                var comb = await GetCombattimentoByIdFromDb(c.Id);
//                if (comb is not null )
//                combattimentiTotale.Add(comb);   
//            }
//            return combattimentiTotale;
//        }

//        public async Task<string> PostAttaccoToDB(int attaccato, int attaccante)
//        {

//            return string.Empty;
//        }

//    }
//}
