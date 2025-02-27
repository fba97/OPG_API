﻿using Core.Base;
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
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Core.Map_Handling.Managers
{
    public class SpostaOggettoHandler : IActionHandler
    {
        private Game _game;
        private readonly OggettoManager _oggettoManager;

        public SpostaOggettoHandler(Game game, IServiceProvider services)
        {
            _game = game;
            _oggettoManager = services.GetRequiredService<OggettoManager>();
        }

        public ActionResult Execute(Azione azione)
        {
            // Lo spostamento di oggetti tra i vari inventari
            // da terra ad un inventario 
            // da giocatore al porto(vendita) in futuro

            //Creazione di un movimento base in base al tipo di azione 
            var spostamento = (SpostaOggetto)azione;
            Result<bool> res = Result<bool>.Failure("HANDLER DI SPOSTAMENTO OGGETTO NON ANCORA IMPLEMENTATO");
            switch(spostamento.TipoAzione)
            {
                case TipoAzione.Raccogli:
                    res = _oggettoManager.RaccogliOggetto(spostamento);
                    break;
                default: 
                    break;
            }

            if(res.IsSuccess)
                return new OkObjectResult(res);
            

            return new ObjectResult(new{message = "HANDLER DI SPOSTAMENTO OGGETTO NON ANCORA IMPLEMENTATO"})
            {
                StatusCode = 500
            };
        }
    }
}