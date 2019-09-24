using Comfandi_.Logica;
using Comfandi_.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comfandi_.Util
{
    public class Contexto
    {
        public TipoServicio Servicio { get; set; }
        public Tramite Tramite { get; set; }
        public Consumo Consultar { get; set; }
    }
}
