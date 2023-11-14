using Core.Base;
using Core.Base_Repositories;
using Core.Primitives;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core.Repository
{
    public class PartitaRepository : IPartitaRepository
    {
        protected UnitOfWork UnitOfWork { get; }

        public PartitaRepository(UnitOfWork uow)
        {
            UnitOfWork = uow;
        }



        private const string _getPartitaById = @"SELECT
                                                 [Id]
                                                ,[Id_giocatore]
                                                ,[Stato]
                                                ,[Nome]
                                                ,[Data_Creazione]
                                                ,[Data_ultimo_Salvataggio]
                                                ,[Difficolta]
                                                ,[Id_Obiettivo]
                                                ,[Data_Fine_Partita]
                                                
                                                FROM[OPG_DB].[dbo].[Partite] 
                                                WHERE Id = @id";
        

        private const string _GetAllPartite = @"SELECT
                                                 [Id]
                                                ,[Id_giocatore]
                                                ,[Stato]
                                                ,[Nome]
                                                ,[Data_Creazione]
                                                ,[Data_ultimo_Salvataggio]
                                                ,[Difficolta]
                                                ,[Id_Obiettivo]
                                                ,[Data_Fine_Partita]
                                                
                                                FROM[OPG_DB].[dbo].[Partite]";

        private const string _addPartita = @"INSERT INTO[dbo].[Partite]
                                                         ([Id_giocatore]
                                                            ,[Stato]
                                                            ,[Nome]
                                                            ,[Data_Creazione]
                                                            ,[Data_ultimo_Salvataggio]
                                                            ,[Difficolta]
                                                            ,[Id_Obiettivo]
                                                            ,[Data_Fine_Partita])
                                                      VALUES
                                                            (@Id_giocatore
                                                            ,@Stato
                                                            ,@Nome
                                                            ,@Data_Creazione
                                                            ,@Data_ultimo_Salvataggio
                                                            ,@Difficolta
                                                            ,@Id_Obiettivo
                                                            ,@Data_Fine_Partita)";


        private const string _updatePartita = @"UPDATE [dbo].[Partite]

                                                SET  [Id_giocatore] = @id_Giocatore
                                                    ,[Stato] =  @Stato
                                                    ,[Nome] = @Nome
                                                    ,[Data_Creazione] = <@Data_Creazione
                                                    ,[Data_ultimo_Salvataggio] = @Data_ultimo_Salvataggio
                                                    ,[Difficolta] = @Difficolta
                                                    ,[Id_Obiettivo] = @Id_Obiettivo
                                                    ,[Data_Fine_Partita] = @Data_Fine_Partita

                                                WHERE Id = @id";

        private const string _deletePartita = @"DELETE [dbo].[Partite] WHERE Id = @id";

        public async Task<Partita?> GetByIdAsync(int id, CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = _getPartitaById;
            cmd.Parameters.AddWithValue("@id", id);

            using var r = await cmd.ExecuteReaderAsync(token);

            if (r.HasRows && await r.ReadAsync(token))
            {
                var idd = r.GetInt32("Id");
                var Id_Giocatore = r.GetInt32("Id_giocatore");
                var nome = await r.IsDBNullAsync("Nome", token) ? string.Empty : (string)r.GetString("Nome");
                var idObiettivo = r.GetInt32("Id_Obiettivo");
                var difficolta = r.GetInt32("Difficolta");
                var statoPartita = r.GetInt32("Stato");
                var dataInizioPartita = await r.IsDBNullAsync("Data_Creazione", token) ? null : (DateTime?)r.GetDateTime("Data_Creazione");
                var dataFinePartita = await r.IsDBNullAsync("Data_Fine_Partita", token) ? null : (DateTime?)r.GetDateTime("Data_Fine_Partita");
                var dataUltimoSalvataggio = await r.IsDBNullAsync("Data_ultimo_Salvataggio", token) ? null : (DateTime?)r.GetDateTime("Data_ultimo_Salvataggio");

                return new Partita
                {
                    Id = idd,
                    Id_Giocatore = Id_Giocatore,
                    Nome = nome,
                    IdObiettivo = idObiettivo,
                    Difficolta = difficolta,
                    StatoPartita = statoPartita,
                    DataInizioPartita = dataInizioPartita,
                    DataFinePartita = dataFinePartita,
                    DataUltimoSalvataggio = dataUltimoSalvataggio
                };
            }
            else
                return null;
        }

        public async Task<Partita?> AddOrUpdatePartitaAsync(Partita partitaToAddOrUpdate, CancellationToken token = default)
        {
                var partita = await GetByIdAsync(partitaToAddOrUpdate.Id, token);

            if (partita == null)
            {
                return await AddAsync(partitaToAddOrUpdate, token);
            }
            else
            {
                partita.Id = partitaToAddOrUpdate.Id; 
                return await UpdateAsync(partita, token);
            }
        }

        public async Task<Partita?> UpdateAsync(Partita partita, CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = _updatePartita;

            cmd.Parameters.AddWithValue("@Id", partita.Id);

            cmd.Parameters.AddWithValue("@Id_giocatore", partita.Id_Giocatore);
            cmd.Parameters.AddWithValue("@Stato", partita.StatoPartita);
            cmd.Parameters.AddWithValue("@Nome", partita.Nome);
            cmd.Parameters.AddWithValue("@Data_Creazione", partita.DataInizioPartita is null ? DBNull.Value : partita.DataInizioPartita);
            cmd.Parameters.AddWithValue("@Data_ultimo_Salvataggio", partita.DataUltimoSalvataggio is null ? DBNull.Value : partita.DataUltimoSalvataggio);
            cmd.Parameters.AddWithValue("@Difficolta", partita.Difficolta);
            cmd.Parameters.AddWithValue("@Id_Obiettivo", partita.IdObiettivo);
            cmd.Parameters.AddWithValue("@Data_Fine_Partita", partita.DataFinePartita is null ? DBNull.Value : partita.DataFinePartita);


            await cmd.ExecuteNonQueryAsync(token);

            return await GetByIdAsync(partita.Id, token); //qui non penso di riuscire a prenderlo in quanto non ho fatto uow.saveChangesAsync() quindi ritornerà sempre null
        }

        public async Task<Partita?> AddAsync(Partita partita, CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = _addPartita;

            cmd.Parameters.AddWithValue("@Id_giocatore", partita.Id_Giocatore);
            cmd.Parameters.AddWithValue("@Stato", partita.StatoPartita);
            cmd.Parameters.AddWithValue("@Nome", partita.Nome);
            cmd.Parameters.AddWithValue("@Data_Creazione", partita.DataInizioPartita is null ? DBNull.Value : partita.DataInizioPartita);
            cmd.Parameters.AddWithValue("@Data_ultimo_Salvataggio", partita.DataUltimoSalvataggio is null ? DBNull.Value : partita.DataUltimoSalvataggio);
            cmd.Parameters.AddWithValue("@Difficolta", partita.Difficolta);
            cmd.Parameters.AddWithValue("@Id_Obiettivo", partita.IdObiettivo);
            cmd.Parameters.AddWithValue("@Data_Fine_Partita", partita.DataFinePartita is null ? DBNull.Value : partita.DataFinePartita);


            await cmd.ExecuteNonQueryAsync(token);

            return await GetByIdAsync(partita.Id,token); //qui non penso di riuscire a prenderlo in quanto non ho fatto uow.saveChangesAsync() quindi ritornerà sempre null
        }

        public async Task<int> DeleteAsync(int id, CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = _deletePartita;

            cmd.Parameters.AddWithValue("@id", id);

            return await cmd.ExecuteNonQueryAsync(token);
        }

        public Partita? GetById(int id)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = _getPartitaById;
            cmd.Parameters.AddWithValue("@id", id);

            using var r = cmd.ExecuteReader();

            if (r.HasRows && r.Read())
            {
                var idd = r.GetInt32("Id");
                var Id_Giocatore = r.GetInt32("Id_giocatore");
                var nome = r.IsDBNull("Nome") ? string.Empty : (string)r.GetString("Nome");
                var idObiettivo = r.GetInt32("Id_Obiettivo");
                var difficolta = r.GetInt32("Difficolta");
                var statoPartita = r.GetInt32("Stato");
                var dataInizioPartita = r.IsDBNull("Data_Creazione") ? null : (DateTime?)r.GetDateTime("Data_Creazione");
                var dataFinePartita = r.IsDBNull("Data_Fine_Partita") ? null : (DateTime?)r.GetDateTime("Data_Fine_Partita");
                var dataUltimoSalvataggio = r.IsDBNull("Data_ultimo_Salvataggio") ? null : (DateTime?)r.GetDateTime("Data_ultimo_Salvataggio");

                return new Partita
                {
                    Id = idd,
                    Id_Giocatore = Id_Giocatore,
                    Nome = nome,
                    IdObiettivo = idObiettivo,
                    Difficolta = difficolta,
                    StatoPartita = statoPartita,
                    DataInizioPartita = dataInizioPartita,
                    DataFinePartita = dataFinePartita,
                    DataUltimoSalvataggio = dataUltimoSalvataggio
                };
            }
            else
                return null;
        }

        public Partita? AddOrUpdatePartita(Partita partitaToAddOrUpdate)
        {
            var partita = GetById(partitaToAddOrUpdate.Id);

            if (partita == null)
            {
                return Add(partitaToAddOrUpdate);
            }
            else
            {
                partita.Id = partitaToAddOrUpdate.Id;
                return Update(partita);
            }
        }

        public Partita? Update(Partita partita)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = _updatePartita;

            cmd.Parameters.AddWithValue("@Id", partita.Id);

            cmd.Parameters.AddWithValue("@Id_giocatore", partita.Id_Giocatore);
            cmd.Parameters.AddWithValue("@Stato", partita.StatoPartita);
            cmd.Parameters.AddWithValue("@Nome", partita.Nome);
            cmd.Parameters.AddWithValue("@Data_Creazione", partita.DataInizioPartita is null ? DBNull.Value : partita.DataInizioPartita);
            cmd.Parameters.AddWithValue("@Data_ultimo_Salvataggio", partita.DataUltimoSalvataggio is null ? DBNull.Value : partita.DataUltimoSalvataggio);
            cmd.Parameters.AddWithValue("@Difficolta", partita.Difficolta);
            cmd.Parameters.AddWithValue("@Id_Obiettivo", partita.IdObiettivo);
            cmd.Parameters.AddWithValue("@Data_Fine_Partita", partita.DataFinePartita is null ? DBNull.Value : partita.DataFinePartita);

            cmd.ExecuteNonQuery();

            return GetById(partita.Id); // Devi considerare la gestione del salvataggio dei cambiamenti qui
        }

        public Partita? Add(Partita partita)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = _addPartita;

            cmd.Parameters.AddWithValue("@Id_giocatore", partita.Id_Giocatore);
            cmd.Parameters.AddWithValue("@Stato", partita.StatoPartita);
            cmd.Parameters.AddWithValue("@Nome", partita.Nome);
            cmd.Parameters.AddWithValue("@Data_Creazione", partita.DataInizioPartita is null ? DBNull.Value : partita.DataInizioPartita);
            cmd.Parameters.AddWithValue("@Data_ultimo_Salvataggio", partita.DataUltimoSalvataggio is null ? DBNull.Value : partita.DataUltimoSalvataggio);
            cmd.Parameters.AddWithValue("@Difficolta", partita.Difficolta);
            cmd.Parameters.AddWithValue("@Id_Obiettivo", partita.IdObiettivo);
            cmd.Parameters.AddWithValue("@Data_Fine_Partita", partita.DataFinePartita is null ? DBNull.Value : partita.DataFinePartita);

            cmd.ExecuteNonQuery();

            return GetById(partita.Id); // Devi considerare la gestione del salvataggio dei cambiamenti qui
        }

        public int Delete(int id)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = _deletePartita;

            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery();
        }

        public async Task<IEnumerable<Partita>> GetAllAsync(CancellationToken token = default)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = _GetAllPartite;

            var r = await cmd.ExecuteReaderAsync(token);
            var allPartite = new List<Partita>();

            while(!r.Read())
            {
                var idd = r.GetInt32("Id");
                var Id_Giocatore = r.GetInt32("Id_giocatore");
                var nome = r.IsDBNull("Nome") ? string.Empty : (string)r.GetString("Nome");
                var idObiettivo = r.GetInt32("Id_Obiettivo");
                var difficolta = r.GetInt32("Difficolta");
                var statoPartita = r.GetInt32("Stato");
                var dataInizioPartita = r.IsDBNull("Data_Creazione") ? null : (DateTime?)r.GetDateTime("Data_Creazione");
                var dataFinePartita = r.IsDBNull("Data_Fine_Partita") ? null : (DateTime?)r.GetDateTime("Data_Fine_Partita");
                var dataUltimoSalvataggio = r.IsDBNull("Data_ultimo_Salvataggio") ? null : (DateTime?)r.GetDateTime("Data_ultimo_Salvataggio");

                allPartite.Add( new Partita
                {
                    Id = idd,
                    Id_Giocatore = Id_Giocatore,
                    Nome = nome,
                    IdObiettivo = idObiettivo,
                    Difficolta = difficolta,
                    StatoPartita = statoPartita,
                    DataInizioPartita = dataInizioPartita,
                    DataFinePartita = dataFinePartita,
                    DataUltimoSalvataggio = dataUltimoSalvataggio
                });
            }

            return allPartite;  
        }
    }
}
