using System;

namespace Biblioteca.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Int64 ISBN { get; set; }
        public int AnoLancamento { get; set; }
        public DateTime CriadoEm { get; set; }
        public Autor Autor { get; internal set; }
        public int AutorId { get; set; }
    }
}
