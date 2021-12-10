using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Biblioteca.InputModel
{
    public class AtualizarLivroInput
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int ISBN { get; set; }
        public int AnoLancamento { get; set; }

    }
}
