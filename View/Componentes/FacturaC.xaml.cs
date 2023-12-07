using ENEL.Logica;
using ENEL.Util;
using System;
using System.Data;
using System.Threading;
using System.Windows;

namespace ENEL.View.Componentes
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class FacturaC : Window
    {
        private readonly Contexto Contexto;

        //variables para parametros impresión

        String VLcomprobante = "";  // fijoVps
        String VLFecha = ""; // se recibe como parametro para impresion
        String VLCorresponsal = ""; // fijoVps
        String VLnombreAlmacen = ""; // fijoVps
        String VLDireccion = ""; // fijoVps
        String VLNumeConvenio = ""; // se recibe como parametro para impresion
        String VLNombreConvenio = ""; // se recibe como parametro para impresion
        String VLValorPagar = "";// se recibe como parametro para impresion
        String VLTotalPago = "";// fijoVps
        String VLValorRecibido = "";// se recibe como parametro para impresion
        String VLCambio = ""; // fijoVps
        String VLcambioValor = ""; // se recibe como parametro para impresion
        String VLcodigounico = "";  // fijoVps
        String VLcodigounicoValor = ""; // se recibe como parametro para impresion
        String VLterminal = ""; // se compone de la palabra "Terminal " + parametro de impresio
        String VLcodigoBanco = ""; // se recibe como parametro para impresion
        String VLcodigoBancoValor = ""; // fijoVps
        String VLRecibo = ""; // se compone de la palabra "Recibo " + parametro de impresio
        String VLrrn = ""; // fijoVps
        String VLRRNValor = ""; // se recibe como parametro para impresion
        String VLAprobado = ""; // se compone de la palabra "Aprob " + parametro de impresio
        String VLrecaudo = ""; // fijoVps
        String VLcodservicio = ""; // fijoVps
        String VLcodServicioValor = ""; // se recibe como parametro para impresion
        String VLfactura = "";  // fijoVps
        String VLfacturaValor = ""; // se recibe como parametro para impresion
        String VLpago = ""; // fijoVps
        String VLEstadoTransaccion = ""; // fijoVps
        String VLEstadoTransaccionD = ""; // fijoVps
        String VLTextF1 = ""; // fijoVps
        String VLTextF2 = ""; // fijoVps
        String VLTextF3 = ""; // fijoVps
        String VLTextF4 = ""; // fijoVps
        String VLTextF5 = ""; // fijoVps
        String VLRechazo = ""; // fijoVps

        public FacturaC(Contexto contexto)
        {
            Contexto = contexto;
            InitializeComponent();
        }

        public void ArmaImpresion(String Fecha,  String numeroFactura)
        {
            leeParametrosImpre();
            Thread.Sleep(200);
            Lcomprobante.Content = VLcomprobante;
            LFecha.Content = Fecha + " 8.5" ;
            LCorresponsal.Content = VLCorresponsal;
            LnombreAlmacen.Content = VLnombreAlmacen;
            LDireccion.Content = VLDireccion;
            Lcodigounico.Content = VLcodigounico;  // se debe cambiar por el enviado por sandra RBM
                                                   //            LcodigounicoValor.Content = CodigoUnico;   // ojo se cambio en vps
            Lterminal.Content = VLterminal; // se debe cambiar por el enviado por sandra RBM;
//            LRRNValor.Content = ;
            Lrecaudo.Content = VLrecaudo;
            Lfactura.Content = VLfactura;
            LfacturaValor.Content = numeroFactura;
            Lpago.Content = VLRechazo;
            LEstadoTransaccion.Content = VLEstadoTransaccionD;
            LTextF1.Content = VLTextF1;
            LTextF2.Content = VLTextF2;
            LTextF3.Content = VLTextF3;
            LTextF4.Content = VLTextF4;
            LTextF5.Content = VLTextF5;

        }

        public void leeParametrosImpre()
        {
            Contexto.Params.DSparams = new DataSet();
            try
            {
                Utilitario.CargaXML_DataSet(ref Contexto.Params.DSparams, (@"xml\\ParametrosImpre.xml"));
                if (Contexto.Params.DSparams.Tables.Count > 0)
                {
                    for (int j = 0; j < Contexto.Params.DSparams.Tables.Count; j++)
                    {
                        if (Contexto.Params.DSparams.Tables[j].TableName.ToUpper() == "PARAMETRO")
                        {
                            if (Contexto.Params.DSparams.Tables[j].Rows.Count > 0)
                            {
                                for (int i = 0; i < Contexto.Params.DSparams.Tables[j].Rows.Count; i++)
                                {
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCOMPROBANTE")
                                        this.VLcomprobante = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "CORRESPONSAL")
                                        this.VLCorresponsal = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "NOMBREALMACEN")
                                        this.VLnombreAlmacen = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "DIRECCIONALMACEN")
                                        this.VLDireccion = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOTOTALPAGO")
                                        this.VLTotalPago = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCAMBIO")
                                        this.VLCambio = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCODIGOUNICO")
                                        this.VLcodigounico = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOTERMINAL")
                                        this.VLterminal = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCODIGODELBANCO")
                                        this.VLcodigoBancoValor = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULORECIBO")
                                        this.VLRecibo = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULORRN")
                                        this.VLrrn = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOAPROBADO")
                                        this.VLAprobado = this.Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULORECUADO")
                                        this.VLrecaudo = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCODIGOSERVICIO")
                                        this.VLcodservicio = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOFACTURA")
                                        this.VLfactura = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOPAGOFACTURA")
                                        this.VLpago = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOTRANSACCIONEXITOSA")
                                        this.VLEstadoTransaccion = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOTRANSACCIONDECLINADA")
                                        this.VLEstadoTransaccionD = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT1")
                                        this.VLTextF1 = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT2")
                                        this.VLTextF2 = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT3")
                                        this.VLTextF3 = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT4")
                                        this.VLTextF4 = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT5")
                                        this.VLTextF5 = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.Params.DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULORECHAZO")
                                        this.VLRechazo = Contexto.Params.DSparams.Tables[j].Rows[i]["valor"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error("Exception leyendo parámetros de impresión Factura Cancelada: " + ex.ToString());
            }

        }

    }
}
