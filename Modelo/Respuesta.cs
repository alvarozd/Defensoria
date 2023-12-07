using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasEnel.Modelo
{
    public class Respuesta
    {
        public string Id { get; set; }
        public string NombreUsuario { get; set; }
        public string NumeroFactura { get; set; }
        public string Mensaje { get; set; }
        public string Fecha { get; set; }
        public decimal ValorConsulta { get; set; }
        public string CodigoNura { get; set; }
        public string CodigoBarras { get; set; }
        public string Dirección { get; set; }
        public string Teléfono { get; set; }
        public string Celular { get; set; }
        public bool RegistraUsuario { get; set; }
        public string Departamento { get; set; }
        public string Ciudad { get; set; }
        public string Email { get; set; }
    }
}
