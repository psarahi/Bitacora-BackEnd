using System.Collections.Generic;

namespace Bitacora.API.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public ICollection<Actividad> Bitacoras { get; set; }
        
    }
}