using System;

namespace Biblioteca.Models
{
    public class Livro
    {
        #region Propriedades

        public int Id { get; set; }
        public string Descricao { get; set; }
        public Int64 ISBN { get; set; }
        public int AnoLancamento { get; set; }
        public DateTime CriadoEm { get; set; }
        public Autor Autor { get; set; }
        public int AutorId { get; set; }

        #endregion      
    }
}
