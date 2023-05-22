using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Data;
using Primitives;
using Core.Base_Repositories;
using Core.Base;

namespace Core
{
    public class PersonaggioRepository : BaseCompleteRepository<PersonaggioInPartita>, IPersonaggioRepository
    {
        public PersonaggioRepository(UnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected override UnitOfWork UnitOfWork { get; }

        protected override string GetElements => _get;

        protected override string GetElementById => _getById;

        protected override string GetElementByCode => _getByCode;

        protected override string AddElement => _add;

        protected override string UpdateElementById => _updateById;
        
        protected override string UpdateElementByCode => _updateByCode;

        protected override string DeleteElementById => _deleteById;

        protected override string DeleteElementByCode => _deleteByCode;


        private const string _table = "[Articoli]";

        private const string _barcode = "@Barcode";

        private const string _byId = " WHERE Id_Articolo = " + _id;
        private const string _byCode = " WHERE Codice = " + _code;
        private const string _byBarcode = " WHERE Barcode = " + _barcode;

        private const string _get = "SELECT Id_Articolo,Codice,Descrizione,Id_Unita_Misura,Barcode,Qta_Massima_Udc,Peso,Indice_Rotazione,Data_Importazione,Data_Creazione,Data_Aggiornamento,Id_Categoria FROM " + _table;
        private const string _getById = _get + _byId;
        private const string _getByCode = _get + _byCode;
        private const string _getByBarcode = _get + _byBarcode;

        private const string _add = "INSERT INTO " + _table
            + " (Codice,Descrizione,Id_Unita_Misura,Barcode,Qta_Massima_Udc,Peso,Indice_Rotazione,Data_Importazione,Data_Creazione,Data_Aggiornamento,Id_Categoria)"
            + " VALUES "
            + " (@Code,@Descrizione,@Id_Unita_Misura,@Barcode,@Qta_Massima_Udc,@Peso,@Indice_Rotazione,@Data_Importazione,@Data_Creazione,@Data_Aggiornamento,@Id_Categoria)"
            + " SET @Id = SCOPE_IDENTITY() ";

        private const string _updateById = "UPDATE " + _table
            + " SET"
            + "  Descrizione = @Descrizione,"
            + "  Id_Unita_Misura = @Id_Unita_Misura,"
            + "  Barcode = @Barcode,"
            + "  Qta_Massima_Udc = @Qta_Massima_Udc,"
            + "  Peso = @Peso,"
            + "  Indice_Rotazione = @Indice_Rotazione,"
            + "  Data_Importazione = @Data_Importazione,"
            + "  Data_Creazione = @Data_Creazione,"
            + "  Data_Aggiornamento = @Data_Aggiornamento,"
            + "  Id_Categoria = @Id_Categoria"
            + _byId;

        private const string _updateByCode = "UPDATE " + _table
            + " SET"
            + "  Descrizione = @Descrizione,"
            + "  Id_Unita_Misura = @Id_Unita_Misura,"
            + "  Barcode = @Barcode,"
            + "  Qta_Massima_Udc = @Qta_Massima_Udc,"
            + "  Peso = @Peso,"
            + "  Indice_Rotazione = @Indice_Rotazione"
            + "  Data_Importazione = @Data_Importazione,"
            + "  Data_Creazione = @Data_Creazione,"
            + "  Data_Aggiornamento = @Data_Aggiornamento,"
            + "  Id_Categoria = @Id_Categoria"
            + _byCode;

        private const string _delete = "DELETE " + _table;
        private const string _deleteById = _delete + _byId;
        private const string _deleteByCode = _delete + _byCode;


        protected override SqlParameter[] GetParameters(PersonaggioInPartita element)
         => new SqlParameter[]
         {
            new SqlParameter
            {
                ParameterName = "@_IdPersonaggioPartita",
                Value = element.IdPersonaggioPartita,
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
               (string)collection[2].Value,
               (string?)collection[3].ParseFromDbNullable(),
               (string?)collection[4].ParseFromDbNullable(),
               (decimal?)collection[5].ParseFromDbNullable(),
               (decimal?)collection[6].ParseFromDbNullable(),
               ((string?)collection[7].ParseFromDbNullable())?.FirstOrDefault(),
               (DateTime?)collection[8].ParseFromDbNullable(),
               (DateTime?)collection[9].ParseFromDbNullable(),
               (DateTime?)collection[10].ParseFromDbNullable(),
               (int?)collection[11].ParseFromDbNullable()
            );

        protected override PersonaggioInPartita ParseReader(SqlDataReader r)
         => new 
            (
               r.GetInt32(0),
               r.GetString(1),
               r.GetString(2),
               r.IsDBNull(3) ? null : r.GetString(3),
               r.IsDBNull(4) ? null : r.GetString(4),
               r.IsDBNull(5) ? null : r.GetDecimal(5),
               r.IsDBNull(6) ? null : r.GetDecimal(6),
               r.IsDBNull(7) ? null : r.GetString(7).FirstOrDefault(),
               r.IsDBNull(8) ? null : r.GetDateTime(8),
               r.IsDBNull(9) ? null : r.GetDateTime(9),
               r.IsDBNull(10) ? null : r.GetDateTime(10),
               r.IsDBNull(11) ? null : r.GetInt32(11)
            );

        public PersonaggioInPartita? GetById(int id)
        {
            using var cmd = UnitOfWork.GetCommand();
            cmd.CommandText = _byId;
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
            cmd.CommandText = _byId;
            cmd.Parameters.AddWithValue(_id, id);

            using var r = await cmd.ExecuteReaderAsync(token);

            if (r.HasRows && await r.ReadAsync(token))
                return ParseReader(r);
            else
                return null;
        }      
    }

}
