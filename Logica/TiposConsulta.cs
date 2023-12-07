using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturasEnel.Logica
{
    public enum TipoConsulta
    {
        None = 0,
        CodigodeBarras = 1,
        NumeroCuenta = 2,
        PagoPila = 3,
        NumerodeCliente = 4,
        NumeroTarjeta = 5
    }
}
