using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Oggetto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descrizione { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int BonusAttacco { get; set; }
        public int BonusDifesa { get; set; }
    }
}