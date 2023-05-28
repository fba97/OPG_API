using Core.Base;
using Core.Base_Repositories;
using Microsoft.Data.SqlClient;
using Primitives;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class PartitaRepository : BaseCompleteRepository<Partita>, IPersonaggioRepository
    {
        public PartitaRepository(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected override UnitOfWork UnitOfWork { get; }

        protected override string GetElementByCode => "";

        protected override string DeleteElementByCode => "";

        protected override string UpdateElementByCode => "";

        protected override string AddElement => "";

        protected override string UpdateElementById => "";

        protected override string DeleteElementById => "";

        protected override string GetElementById => "";

        protected override string GetElements => "";

        protected string getPersonaggioById = @" SELECT 
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
									        	where id_personaggio = " + _id;


        protected override SqlParameter[] GetParameters(Partita element)
         => new SqlParameter[]
         {
            new SqlParameter
            {
                ParameterName = _id,
                Value = element.Id,
                Direction = ParameterDirection.InputOutput
            },
            new SqlParameter
            {
                ParameterName = "@nome",
                Value = element.Nome,
                Size = 50,
                Direction = ParameterDirection.InputOutput
            },
            new SqlParameter
            {
                ParameterName = "@IdObiettivo",
                Value = element.IdObiettivo,
                Direction = ParameterDirection.InputOutput
            },
            new SqlParameter
            {
                ParameterName = "@Difficolta",
                Value = element.Difficolta,
                Direction = ParameterDirection.InputOutput
            },
            new SqlParameter
            {
                ParameterName = "@StatoPartita",
                Value = element.StatoPartita,
                Direction = ParameterDirection.InputOutput
            },
            new SqlParameter
            {
                ParameterName = "@DataInizioPartita",
                Value = element.DataInizioPartita.CoalesceNullToDBNull(),
                Direction = ParameterDirection.InputOutput
            },
            new SqlParameter
            {
                ParameterName = "@DataFinePartita",
                Value = element.DataFinePartita.CoalesceNullToDBNull(),
                Direction = ParameterDirection.InputOutput
            },
            new SqlParameter
            {
                ParameterName = "@PersonaggiInGioco",
                Value = element.PersonaggiInGioco,
                Direction = ParameterDirection.InputOutput
            },
                   new SqlParameter
            {
                ParameterName = "@OggettiInGioco",
                Value = element.OggettiInGioco,
                Direction = ParameterDirection.InputOutput
            }
         };

        protected override Partita GetElement(SqlParameterCollection collection)
            => new
            (
                (int)collection[0].Value,
                (string)collection[1].Value,
                (int)collection[2].Value,
                (int)collection[3].Value,
                (int)collection[4].Value,
                (DateTime?)collection[5].ParseFromDbNullable(),
                (DateTime?)collection[6].ParseFromDbNullable(),
                (List<int>)collection[7].Value,
                (List<int>)collection[8].Value
            );

        protected override Partita ParseReader(SqlDataReader r)
         => new
            (
               r.GetInt32(0),
               r.GetString(1),
               r.GetInt32(2),
               r.GetInt32(3),
               r.GetInt32(4),
               r.IsDBNull(5) ? null : r.GetDateTime(5),
               r.IsDBNull(6) ? null : r.GetDateTime(6),
               r.GetInt32(7),
               r.Get(8),
               r.Get(9)            
            );

        public Partita? GetById(int id)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = getPersonaggioById;
            cmd.Parameters.AddWithValue(_id, id);

            using var r = cmd.ExecuteReader();

            if (r.HasRows && r.Read())
                return ParseReader(r);
            else
                return null;
        }

        public async Task<Partita?> GetByIdAsync(int id, CancellationToken token = default)
        {
            await using var cmd = await UnitOfWork.GetCommandAsync(token);
            cmd.CommandText = getPersonaggioById;
            cmd.Parameters.AddWithValue(_id, id);

            using var r = await cmd.ExecuteReaderAsync(token);

            if (r.HasRows && await r.ReadAsync(token))
                return ParseReader(r);

            else
                return null;
        }
    }
}
