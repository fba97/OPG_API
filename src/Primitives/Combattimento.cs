using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primitives
{
    public class Combattimento
    {
        public int Id { get; set; } = -1;
        public string Nome { get; set; } = string.Empty;
        public List<int> ListaEroi { get; set; } = new List<int>();
        public List<int> ListaNPCs { get; set; } = new List<int>();
    }
}
