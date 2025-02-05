using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public abstract class Azione
    {
        public int Id { get; set; }
        public Personaggio Personaggio { get; set; }
        public TipoAzione TipoAzione { get; set; }
        public int PesoAzione { get; set; } = 1;
    }

    public interface IActionHandler
    {
        ActionResult Execute(Azione azione);
    }
}
