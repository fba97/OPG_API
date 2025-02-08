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
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Core.Map_Handling.Managers
{
    public class UsaOggettoHandler : IActionHandler
    {
        private Game _game;
        private readonly OggettoManager _oggettoManager;

        public UsaOggettoHandler(Game game, IServiceProvider services)
        {
            _game = game;
            _oggettoManager = services.GetRequiredService<OggettoManager>();
        }

        public ActionResult Execute(Azione azione)
        {
            // per utilizzare un oggetto supponiamo inizialmente che ce ne siano di un solo tipo.
            // come ad esempio potenziamenti poi aggiungerò gli altri in un futuro
            return new ObjectResult(new{message = "HANDLER DI UTILIZZO OGGETTO NON ANCORA IMPLEMENTATO"})
            {
                StatusCode = 500
            };
        }
    }
}