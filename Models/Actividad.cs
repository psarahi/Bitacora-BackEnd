using System;

namespace Bitacora.API.Models
{
    public class Actividad
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public string Descripcion { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public DateTime HoraInicial { get; set; }

        public DateTime HoraFinal { get; set; }

    }
}