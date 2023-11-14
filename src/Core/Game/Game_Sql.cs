using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Game
{
    public partial class Game
    {

        private Mappa GetMappa()
        {

            string stockQuery = @"SELECT [Id]
                                     ,[Descrizione]
                              FROM [dbo].[Mappe]";

            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(stockQuery, conn);
            conn.Open();
            using var r = cmd.ExecuteReader();
            var mappa = new Mappa();
            while (r.Read())
            {
            }
            return mappa;
        }


        //private IEnumerable<Item> GetAllItems()
        //{
        //    var itemFactory = _services.GetRequiredService<IItemFactory>();
        //    var itemQuery = "SELECT ID_COMPONENTE, ID_SOTTOAREA, CODICE_ABBREVIATO, DESCRIZIONE,  ID_TIPO_COMPONENTE, COMM_CHANNEL,"
        //                + " Tipo, Configurazione"
        //                + " FROM Componenti";

        //    using var conn = new SqlConnection(_connString);
        //    using var cmd = new SqlCommand(itemQuery, conn);

        //    conn.Open();
        //    using var r = cmd.ExecuteReader();
        //    while (r.Read())
        //    {
        //        int id = r.GetInt32(0);
        //        int subAreaId = r.GetInt32(1);
        //        string code = r.GetString(2);
        //        string description = r.GetString(3);

        //        var subItems = _subItems.Where(s => s.ItemId == id);

        //        var type = ItemTypeUtils.GetItemTypeFromDbString(r.GetString(4));
        //        int communicationChannelId = r.IsDBNull(5) ? 1 : r.GetInt32(5);

        //        string? implementationType = r.IsDBNull(6) ? null : r.GetString(6);
        //        IConfiguration? cfg = r.GetConfigurationFromJsonField(7);

        //        yield return itemFactory
        //            .CreateItem
        //            (
        //                id,
        //                subAreaId,
        //                code,
        //                description,
        //                subItems,
        //                communicationChannelId,
        //                type,
        //                implementationType,
        //                cfg
        //             ).AsItem();
        //    }
        //}

        //private IEnumerable<SubArea> GetAllSubAreas()
        //{
        //    using var conn = new SqlConnection(_connString);
        //    using var cmd = new SqlCommand("SELECT ID_SOTTOAREA, ID_AREA, CODICE_ABBREVIATO, DESCRIZIONE, ISNULL([COMPORTAMENTO],0) AS COMPORTAMENTO" +
        //                                    " FROM SottoAree", conn);

        //    conn.Open();

        //    using var r = cmd.ExecuteReader();
        //    while (r.Read())
        //    {
        //        int id = r.GetInt32(0);
        //        int areaId = r.GetInt32(1);
        //        string code = r.GetString(2);
        //        string description = r.GetString(3);

        //        var items = _items.Where(i => i.SubAreaId == id)
        //                            .ToArray();

        //        var behaviour = (SubAreaBehaviour)r.GetInt32(4);

        //        yield return new SubArea(id, areaId, code, description, items, behaviour);
        //    }
        //}

        //private IEnumerable<Area> GetAllAreas()
        //{
        //    using var conn = new SqlConnection(_connString);
        //    using var cmd = new SqlCommand("SELECT ID_AREA, ID_MAGAZZINO, CODICE_ABBREVIATO, DESCRIZIONE" +
        //                                    " FROM Aree", conn);
        //    conn.Open();

        //    using var r = cmd.ExecuteReader();

        //    while (r.Read())
        //    {
        //        int id = r.GetInt32(0);
        //        int warehouseId = r.GetInt32(1);
        //        string code = r.GetString(2);
        //        string description = r.GetString(3);

        //        var subAreas = _subAreas.Where(s => s.AreaId == id)
        //                                .ToArray();

        //        yield return new Area(id, warehouseId, code, description, subAreas);
        //    }
        //}


        private IEnumerable<Punto> GetAllPunti()
        {
            //string partitionSelect = "SELECT  ID_PARTIZIONE, ID_SOTTOCOMPONENTE, CODICE_ABBREVIATO, DESCRIZIONE," +
            //                                "LOCKED_INFEED, LOCKED_OUTFEED, Motivo_Blocco, PESO, ALTEZZA, LARGHEZZA, PROFONDITA, CAPIENZA " +
            //                                "FROM  Partizioni WHERE Id_Tipo_Partizione <> 'OO' OR Codice_Abbreviato = '0000' OR Descrizione like '____.0000.____'";

            //using var conn = new SqlConnection(_connString);
            //using var cmd = new SqlCommand(partitionSelect, conn);
            //conn.Open();
            //using var r = cmd.ExecuteReader();

            //while (r.Read())
            //{
            //    int partitionId = r.GetInt32(0);
            //    int subItemId = r.GetInt32(1);
            //    string code = r.GetString(2);
            //    string description = r.GetString(3);
            //    bool infeedLock = !r.IsDBNull(4) && r.GetBoolean(4);
            //    bool outfeedLock = !r.IsDBNull(5) && r.GetBoolean(5);
            //    string? lockReason = r.IsDBNull(6) ? null : r.GetString(6);
            //    int maxContentWeight = r.GetInt32(7);
            //    var maxContentSize = new Size(r.GetInt32(8), r.GetInt32(9), r.GetInt32(10));
            //    int capacity = r.GetInt32(11);

            //    yield return new Partition(partitionId, subItemId, code, description, infeedLock, outfeedLock, maxContentSize, capacity, lockReason, maxContentWeight);

            //}
            return new List<Punto>();
        }
    }
}