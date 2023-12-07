using FacturasEnel.Logica;
using System.Text;

namespace FacturasEnel.Modelo
{
    public class RespuestaDatafono
    {
        public TipoConsulta Tipo { get; set; }    // TIPO DE CONSULTA SELECCIONADA POR EL USUARIO
        public decimal MontoAproximado { get; set; } // VALOR A PAGAR APROXIMADO AL MENOR 100
        public string NombreConvenio { get; set; }   // NOMBRE DE IDENTIFICACIÓN DEL CONVENIO
        public string DatoConsulta { get; set; }     // CODIGO DE BARRRAS LEIDO Ó DIGITADO POR EL USUARIO
        public bool PagoDiferente { get; set; }      // DEFINE SI PERMITE CAMBIAR EL VALOR A PAGAR
        public bool PagarOtroValor { get; set; }     // DEFINE SI EL USUARIO DIGITO OTRO VALOR A PAGAR
        public string CodigoRespuesta { get; set; }  // CAMPO  0
        public string CodigoAprobacion { get; set; } // CAMPO  1
        public decimal MontoOperacion { get; set; }  // CAMPO 40
        public string NumeroRecibo { get; set; }     // CAMPO 65
        public string ModoEntrada { get; set; }      // CAMPO 69   =>   03: Cod Barras;   04: Manual
        public string IdBanco { get; set; }          // CAMPO 73  VER VALORES ABAJO 
        public string TipoPago { get; set; }         // CAMPO 76   =>   03: Total;        04: Parcial
        public string Terminal { get; set; }         // CAMPO 78
        public string Fecha { get; set; }            // CAMPO 80
        public string Hora { get; set; }             // CAMPO 81
        public string CodConvenio { get; set; }      // CAMPO 89
        public string Referencia { get; set; }       // CAMPO 93
        public string InfoAdicional { get; set; }    // CAMPO 94

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Tipo: " + Tipo);
            sb.AppendLine("MontoAproximado: " + MontoAproximado);
            sb.AppendLine("NombreConvenio: " + NombreConvenio);
            sb.AppendLine("DatoConsulta: " + DatoConsulta);
            sb.AppendLine("PagoDiferente: " + PagoDiferente);
            sb.AppendLine("CodigoRespuesta: " + CodigoRespuesta);
            sb.AppendLine("CodigoAprobacion: " + CodigoAprobacion);
            sb.AppendLine("MontoOperacion: " + MontoOperacion);
            sb.AppendLine("NumeroRecibo:" + NumeroRecibo);
            sb.AppendLine("ModoEntrada: " + ModoEntrada);
            sb.AppendLine("IDBanco: " + IdBanco);
            sb.AppendLine("TipoPago: " + TipoPago);
            sb.AppendLine("Terminal: " + Terminal);
            sb.AppendLine("Fecha: " + Fecha);
            sb.AppendLine("Hora: " + Hora);
            sb.AppendLine("CodConvenio: " + CodConvenio);
            sb.AppendLine("Referencia: " + Referencia);
            sb.AppendLine("InfoAdicional: " + InfoAdicional);
            return sb.ToString();
        }
        /*
        ID BANCO
            0007 Bancolombia 
            0001 Banco de Bogota
            0002 Banco Popular
            0023 Occidente
            0052 Av Villas 
            0903 ATH
        */
    }
}