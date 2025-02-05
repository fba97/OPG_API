using Core.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Primitives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Core;
using Core.Game_dir;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Core.Map_Handling.Managers
{
    public class AttaccoHandler : IActionHandler
    {
        private Game _game;


        public AttaccoHandler(Game game)
        {
            _game = game;
        }

        public ActionResult Execute(Azione azione)
        {
            {
                try
                {

                    // tra i controlli possibili controlla anche che l'attacco sia possibile in base alla distanza (a quante caselle puo attaccare l'attaccante?)
                    var attacco = (Attacco)azione;

                    var attaccante = attacco.Personaggio;
                    var attaccato = attacco.Difensore;

                    var combattimentiInteressati = _game.PartitaAttuale?.Combattimenti.Where(c => c.ListaEroi.Contains(attaccante.Id)
                                                                                            || c.ListaEroi.Contains(attaccato.Id)
                                                                                            || c.ListaNPCs.Contains(attaccante.Id)
                                                                                            || c.ListaNPCs.Contains(attaccato.Id));


                    // qua vabe sbatti perchè non ho amici/nemici ma ho 4 opzioni. da aggiungere valutazione su amici npc e nemici personaggi
                    var nemico = attaccante.TipoPersonaggio == TipoPersonaggio.NemicoNPC ? attaccante :
                                                                                           attaccato.TipoPersonaggio == TipoPersonaggio.NemicoNPC ? attaccato
                                                                                                                                                   : null;

                    var eroe = attaccante.TipoPersonaggio == TipoPersonaggio.AmicoPersonaggio ? attaccante : attaccato.TipoPersonaggio == TipoPersonaggio.AmicoPersonaggio ? attaccato : null;

                    if (nemico is null || eroe is null)
                    {
                        return new BadRequestObjectResult(new
                        {
                            message = "I personaggi coinvolti non sono reperibili"
                        });
                    }

                    Combattimento combattimento;

                    if (combattimentiInteressati is null || !combattimentiInteressati.Any())
                    {
                        //Creo Il nuovo Combattimento
                        var nomeCombattimento = string.Concat("Scontro con ", nemico.Nome);

                        combattimento = new Combattimento()
                        {
                            Id = GetCorrectId(),
                            Nome = nomeCombattimento,
                            ListaEroi = new List<int>() { eroe.Id },
                            ListaNPCs = new List<int>() { nemico.Id }
                        };
                    }
                    else
                    {
                        combattimento = combattimentiInteressati.First();
                        if (!combattimento.ListaEroi.Contains(eroe.Id))
                            combattimento.ListaEroi.Add(eroe.Id);
                        if (!combattimento.ListaNPCs.Contains(nemico.Id))
                            combattimento.ListaNPCs.Add(nemico.Id);
                    }


                    // Esegui l'attacco
                    var attaccoEseguito = Attacca(attacco);

                    return new OkObjectResult(attaccoEseguito);
                }
                catch (Exception ex)
                {
                    return new ObjectResult(new
                    {
                        message = "Si è verificato un errore durante l'esecuzione della missione.",
                        error = ex.Message
                    })
                    {
                        StatusCode = 500
                    };
                }
            }


        }

        private Attacco Attacca(Attacco attacco)
        {
            //questa è un'operazione da fare con un semaforo ma per il momento va bene cosi easy
            //game.PartitaAttuale.Combattimenti.FirstOrDefault(c => c.Id == );

            var attaccante = attacco.Personaggio;
            var attaccato = attacco.Difensore;

            // ricordati come funziona la difesa

            attaccato.Punti_Vita -= attaccante.Attacco;

            if (attaccato.Punti_Vita < 0)
            {
                attaccato.Punti_Vita = 0;
            }
            return attacco;
        }

        private int GetCorrectId()
        {
            throw new NotImplementedException();
        }
    }
}