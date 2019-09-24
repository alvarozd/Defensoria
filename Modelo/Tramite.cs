using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comfandi_.Modelo
{
    public class Tramite
    {
        public string IdUsuario { get; set; }
        public string ContraseñaUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public TipoServicio Servicio { get; set; }
        public Respuesta RespuestaConsumo { get; set; }
    }
}
