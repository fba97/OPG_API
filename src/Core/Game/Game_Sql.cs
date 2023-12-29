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
            conn.Open();
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
                int id_posizione = r.IsDBNull("Descrizione") ? 0 : r.GetInt32("Id_Posizione");


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
        private ActualPartita? GetActualPartita(int id)
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@" SELECT TOP 1 
                                                    [Id_Partita]
                                                    ,[Id_giocatore]
                                                    ,[Stato]
                                                    ,[Nome]
                                                    ,[Data_Creazione]
                                                    ,[Data_ultimo_Salvataggio]
                                                    ,[Difficolta]
                                                    ,[Id_Obiettivo]
                                                    ,[Data_Fine_Partita]
                                                    ,[Data_Inizio_Partita]
                                                    ,[SalvataggioJSON]
                                                    
                                                    FROM[dbo].[Partite_Salvate]
                                                    WHERE Id_Partita = @id
                                                    ORDER BY Data_ultimo_Salvataggio desc", conn);

            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            using var r = cmd.ExecuteReader();

            if (r.Read())
            {
                int id_partita = r.GetInt32("Id_Partita");
                int IdGiocatore = r.GetInt32("Id_giocatore");
                string nome = r.GetString("Nome");
                int idObiettivo = r.GetInt32("Id_Obiettivo");
                int difficolta = r.GetInt32("Difficolta");
                int statoPartita = r.GetInt32("Stato");
                var dataInizioPartita  = r.IsDBNull("Data_Inizio_Partita") ? null : (DateTime?)r.GetDateTime("Data_Inizio_Partita");
                var data_ultimo_Salvataggio = r.IsDBNull("Data_ultimo_Salvataggio") ? null : (DateTime?)r.GetDateTime("Data_ultimo_Salvataggio");
                var dataFinePartita = r.IsDBNull("Data_Fine_Partita") ? null : (DateTime?)r.GetDateTime("Data_Fine_Partita");
                var json = r.IsDBNull("SalvataggioJSON") ? string.Empty : (string)r.GetString("SalvataggioJSON");


                return new ActualPartita(id_partita, nome, IdGiocatore, idObiettivo, difficolta, statoPartita, dataInizioPartita, data_ultimo_Salvataggio, dataFinePartita, json);
            }
            return null;
        }


        private int SalvaPartitaToDB(ActualPartita actualPartita)
        {
            // NEL CASO IN CUI CI SIA UNA PARTITA CHE HA ID PARTITA 0 SIGNIFICA CHE è NUOVA E VA AGGIUNTO UN NUOVO ID PARTITA. L'ID PARTITA LO TROVO CON UNA GET CHE ORDINA PER ID DESCRESCENTE E PRENDE SOLO LA TOP 1 ID
            
            if (actualPartita.Id == 0)
            {
                actualPartita.Id = NewPartitaId();
                actualPartita.JSONSalvataggio = actualPartita.Serialize();
            }

            using var conn = new SqlConnection(_connString);

            using var cmd = new SqlCommand(@"INSERT INTO [dbo].[Partite_Salvate]
                                              ([Id_Partita]
                                              ,[Id_giocatore]
                                              ,[Stato]
                                              ,[Nome]
                                              ,[Data_Creazione]
                                              ,[Data_ultimo_Salvataggio]
                                              ,[Difficolta]
                                              ,[Id_Obiettivo]
                                              ,[Data_Fine_Partita]
                                              ,[Data_Inizio_Partita]
                                              ,[SalvataggioJSON])
                                        VALUES
                                              (@Id
                                              ,@IdGiocatore
                                              ,@StatoPartita
                                              ,@Nome
                                              ,@DataInizioPartita
                                              ,@DataUltimoSalvataggio
                                              ,@Difficolta
                                              ,@IdObiettivo
                                              ,@DataFinePartita
                                              ,@DataInizioPartita
                                              ,@JSONSalvataggio);", conn);

            cmd.Parameters.AddWithValue("@Id",actualPartita.Id);
            cmd.Parameters.AddWithValue("@IdGiocatore", actualPartita.IdGiocatore);
            cmd.Parameters.AddWithValue("@StatoPartita", actualPartita.StatoPartita);
            cmd.Parameters.AddWithValue("@Nome", actualPartita.Nome);
            cmd.Parameters.AddWithValue("@DataInizioPartita", ((object?)actualPartita.DataInizioPartita) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DataUltimoSalvataggio", DateTime.Now);
            cmd.Parameters.AddWithValue("@Difficolta", actualPartita.Difficolta);
            cmd.Parameters.AddWithValue("@IdObiettivo", actualPartita.IdObiettivo);
            cmd.Parameters.AddWithValue("@DataFinePartita", ((object?)actualPartita.DataFinePartita) ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@JSONSalvataggio", ((object?)actualPartita.JSONSalvataggio) ?? DBNull.Value);

            conn.Open();
            cmd.ExecuteNonQuery();

            return actualPartita.Id;
        }

        private int NewPartitaId()
        {
            string query = @"SELECT TOP 1 Id_Partita
                            FROM dbo.[Partite_Salvate]
                            ORDER BY Id_Partita Desc";

            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(query, conn);

            conn.Open();

            using var r = cmd.ExecuteReader();

            if (r.Read())
                return r.GetInt32("Id_Partita")+1;
            return 1;
        }

        private IEnumerable<Adiacenza> GetAllAdiacenze()
        {
            using var conn = new SqlConnection(_connString);
            using var cmd = new SqlCommand(@"SELECT [Id]
                                                    ,[IdPuntoUno]
                                                    ,[IdPuntoDue]
                                                    ,[Bidirezionale]
                                                    ,[Abilitata]
                                            FROM[OPG_DB].[dbo].[Adiacenze]", conn);
            conn.Open();

            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
                int Id = r.GetInt32("Id");
                int idPuntoUno = r.GetInt32("IdPuntoUno");
                int idPuntoDue = r.GetInt32("IdPuntoUno");
                bool Bidirezionale = r.GetBoolean("Bidirezionale");
                bool Abilitata = r.GetBoolean("Abilitata");

                yield return new Adiacenza(Id, idPuntoUno, idPuntoDue, Bidirezionale,Abilitata);
            }
        }
    }
}