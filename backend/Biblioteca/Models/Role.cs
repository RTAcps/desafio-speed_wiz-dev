using System.Collections.Generic;

namespace Biblioteca.Models
{
    public class Role
    {
        #region Propriedades
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public ICollection<Usuario> Usuarios { get; set; }
        #endregion

        #region Construtor
        protected Role() { }

        public Role(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        #endregion
    }
}
