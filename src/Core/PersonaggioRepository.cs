using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Data;
using Core.Base_Repositories;
using Core.Base;
using Microsoft.Data.SqlClient;
using Core.Primitives;

namespace Core
{
    public class PersonaggioRepository : BaseCompleteRepository<PersonaggioInPartita>, IPersonaggioRepository
    {
        public PersonaggioRepository(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected override UnitOfWork UnitOfWork { get; }

        protected override string GetElementByCode => throw new NotImplementedException();

        protected override string DeleteElementByCode => throw new NotImplementedException();

        protected override string UpdateElementByCode => throw new NotImplementedException();

        protected override string AddElement => throw new NotImplementedException();

        protected override string UpdateElementById => throw new NotImplementedException();

        protected override string DeleteElementById => throw new NotImplementedException();

        protected override string GetElementById => throw new NotImplementedException();

        protected override string GetElements => throw new NotImplementedException();

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
									        	where id_personaggio" + _id;


        protected override SqlParameter[] GetParameters(PersonaggioInPartita element)
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
                ParameterName = "@_IdPartita",
                Value = element.IdPartita,
                Size = 50,
                Direction = ParameterDirection.InputOutput
            },
            new SqlParameter
            {
                ParameterName = "@Livello",
                Value = element.Livello,
                Direction = ParameterDirection.Input
            },
            new SqlParameter
            {
                ParameterName = "@TipoPersonaggio",
                Value = element.TipoPersonaggio,
                Direction = ParameterDirection.Input
            },
            new SqlParameter
            {
                ParameterName = "@PosizioneXPersonaggio",
                Value = element.PosizioneXPersonaggio.CoalesceNullToDBNull(),
                Direction = ParameterDirection.Input
            },
            new SqlParameter
            {
                ParameterName = "@PosizioneYPersonaggio",
                Value = element.PosizioneYPersonaggio.CoalesceNullToDBNull(),
                Direction = ParameterDirection.Input
            },
            new SqlParameter
            {
                ParameterName = "@PuntiVitaPersonaggio",
                Value = element.PuntiVitaPersonaggio,
                Direction = ParameterDirection.Input
            },
            new SqlParameter
            {
                ParameterName = "@Attacco_InPartita",
                Value = element.Attacco_InPartita,
                Direction = ParameterDirection.Input
            },
            new SqlParameter
            {
                ParameterName = "@Difesa_InPartita",
                Value = element.Difesa_InPartita,
                Direction = ParameterDirection.Input
            },
            new SqlParameter
            {
                ParameterName = "@Stato",
                Value = element.Stato.CoalesceNullToDBNull(),
                Direction = ParameterDirection.Input
            },
            new SqlParameter
            {
                ParameterName = "@Taglia",
                Value = element.Taglia.CoalesceNullToDBNull(),
                Direction = ParameterDirection.Input
            }         
         };

        protected override PersonaggioInPartita GetElement(SqlParameterCollection collection)
            => new
            (
                (int)collection[0].Value,
                (string)collection[1].Value,
                (int)collection[2].Value,
                (int)collection[3].Value,
                (int)collection[4].Value,
                (string?)collection[5].ParseFromDbNullable(),
                (int)collection[6].Value,
                (int)collection[7].Value,
                (int)collection[8].Value,
                (int)collection[9].Value,
                (int)collection[10].Value,
                (int)collection[11].Value,
                (int)collection[12].Value,
                (int)collection[13].Value,
                (int)collection[14].Value,
                (Stato)collection[15].Value,
                (int)collection[16].Value
            );

        protected override PersonaggioInPartita ParseReader(SqlDataReader r)
         => new 
            (
               r.GetInt32(0),
               r.GetString(1),
               r.GetInt32(2),
               r.GetInt32(3),
               r.GetInt32(4),
               r.IsDBNull(5) ? null : r.GetString(5),
               r.GetInt32(6),
               r.GetInt32(7),
               r.GetInt32(8),
               r.GetInt32(9),
               r.GetInt32(10),
               r.GetInt32(11),
               r.GetInt32(12),
               r.GetInt32(13),
               r.GetInt32(14),
               new Stato(),
               r.GetInt32(14)
            );

        public PersonaggioInPartita? GetById(int id)
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

        public async Task<PersonaggioInPartita?> GetByIdAsync(int id, CancellationToken token = default)
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
