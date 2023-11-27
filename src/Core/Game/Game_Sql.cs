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

        private Mappa? GetMappa(int id_mappa)
        {

            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@"SELECT [Id]
                                                ,[Descrizione]
                                            FROM [dbo].[Mappe]
                                        WHERE Id = @idmappa", conn);

            cmd.Parameters.AddWithValue("@idmappa", id_mappa);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                int id = r.GetInt32(0);
                string descrizione = r.GetString(1);

                var aree = _aree.Where(i => i.Id_Mappa == id)
                                    .ToArray();


                return new Mappa(id, descrizione, aree);
            }
            return null;
        }

        private IEnumerable<Tessera> GetAllTessere()
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@"SELECT [Id]
                                                    ,[Id_Area]
                                                    ,[Descrizione]
                                                    ,[Tipo]
                                                    ,[Configurazione]
                                             FROM [dbo].[Tessere]", conn);

            conn.Open();

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                int id = r.GetInt32(0);
                int id_area = r.GetInt32(1);
                string descrizione = r.GetString(2);
                string comportamento = r.GetString(3);
                string configurazione = r.GetString(4);

                var punti = _punti.Where(i => i.Id_Tessera == id)
                                    .ToArray();


                yield return new Tessera(id, id_area, descrizione, punti);
            }
        }

        private IEnumerable<Area> GetAllAree()
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@"SELECT [Id]
                                                    ,[Id_Mappa]
                                                    ,[Descrizione]
                                                    ,[Comportamento]
                                                    ,[Configurazione]
                                              FROM [dbo].[Aree]", conn);

            conn.Open();

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                int id = r.GetInt32(0);
                int id_mappa = r.GetInt32(1);
                string descrizione = r.GetString(2);
                string comportamento = r.GetString(3);
                string configurazione = r.GetString(4);

                var tessere = _tessere.Where(i => i.Id_Area == id)
                                    .ToArray();


                yield return new Area(id, id_mappa, descrizione, _tessere);
            }
        }

        private IEnumerable<Punto> GetAllPunti()
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@"SELECT [Id]
                                                    ,[Id_Tessera]
                                                    ,[Descrizione]
                                                    ,[Tipo]
                                                    ,[Capienza]
                                                    ,[Blocco]
                                             FROM [dbo].[Punti]", conn);
            conn.Open();

            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                int id = r.GetInt32(0);
                int id_tessera = r.GetInt32(1);
                string descrizione = r.GetString(2);
                int tipo = r.GetInt32(3);
                int capienza = r.GetInt32(4);
                bool blocco = r.GetBoolean(5);

                yield return new Punto(id, id_tessera, descrizione, capienza, blocco);
            }
        }

        private IEnumerable<Personaggio> GetAllPersonaggi()
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@"SELECT [id_personaggio]
                                                 ,[nome]
                                                 ,[punti_vita]
                                                 ,[attacco]
                                                 ,[difesa]
                                                 ,[Descrizione]
                                                 ,[Tipo_Personaggio]
                                                 ,[Id_Posizione]
                                             FROM [dbo].[Personaggi]", conn);
            conn.Open();

            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                int id = r.GetInt32("id_personaggio");
                string nome = r.GetString("nome");
                int punti_vita = r.GetInt32("punti_vita");
                int attacco = r.GetInt32("attacco");
                int difesa = r.GetInt32("difesa");
                string descrizione = r.IsDBNull("Descrizione") ? string.Empty : r.GetString("Descrizione");
                int tipo_personaggio = r.GetInt32("Tipo_Personaggio");
                int id_posizione = r.GetInt32("Id_Posizione");


                yield return new Personaggio(id,nome,punti_vita,attacco,difesa,descrizione,tipo_personaggio,id_posizione, taglia:0, livello:0);
            }
        }

        private IEnumerable<Oggetto> GetAllOggetti()
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@"SELECT [id_oggetto]
                                                 ,[nome]
                                                 ,[descrizione]
                                                 ,[tipo]
                                                 ,[bonus_attacco]
                                                 ,[bonus_difesa]
                                                 ,[Id_Posizione]
                                                 ,[id_inventario]
                                             FROM [dbo].[Oggetti]", conn);
            conn.Open();

            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                int id_oggetto = r.GetInt32("id_oggetto");
                string nome = r.GetString("nome");
                string descrizione = r.IsDBNull("descrizione") ? string.Empty : r.GetString("Descrizione");
                int tipo_oggetto = r.GetInt32("tipo");
                int id_posizione = r.GetInt32("Id_Posizione");
                int bonus_attacco = r.GetInt32("bonus_attacco");
                int bonus_difesa = r.GetInt32("bonus_difesa");
                int id_inventario = r.GetInt32("id_inventario");


                yield return new Oggetto(id_oggetto, nome, descrizione, tipo_oggetto, bonus_attacco, bonus_difesa, id_posizione, id_inventario);
            }
        }

    }
}