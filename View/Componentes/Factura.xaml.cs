using FacturasEnel.Logica;
using FacturasEnel.Util;
using System;
using System.Data;
using System.Threading;
using System.Windows;
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;
using System.Globalization;

namespace FacturasEnel.View.Componentes
{
    /// <summary>
    /// Lógica de interacción para Factura.xaml
    /// </summary>
    public partial class Factura : Window
    {
        private readonly Contexto Contexto;
        public string NombreArchivo { get; set; }

        //variables para parametros impresión

        string VLcomprobante = "";  // fijoVps
        string VLCambio = ""; // fijoVps
        string VLTotalPago = "";// fijoVps
        string VLrecaudo = ""; // fijoVps
        string VLfactura = "";  // fijoVps
        string VLpago = ""; // fijoVps
        string VLEstadoTransaccion = ""; // fijoVps
        string VLEstadoTransaccionD = ""; // fijoVps
        string VLTextF5 = ""; // fijoVps

        string VLcodigoBancoValor = ""; // fijoVps
        string VLRecibo = ""; // se compone de la palabra "Recibo " + parametro de impresio
        string VLcodigounico = "";  // fijoVps
        string VLAprobado = ""; // se compone de la palabra "Aprob " + parametro de impresio
        string VLterminal = ""; // se compone de la palabra "Terminal " + parametro de impresio
        string VLCorresponsal = ""; // fijoVps
        string VLnombreAlmacen = ""; // fijoVps
        string VLcodservicio = ""; // fijoVps
        string VLDireccion = ""; // fijoVps
        string VLTextF1 = string.Empty; // fijoVps
        string VLTextF2 = string.Empty; // fijoVps
        string VLTextF3 = string.Empty; // fijoVps
        string VLTextF4 = string.Empty; // fijoVps
        //String VLFecha = ""; // se recibe como parametro para impresion
        //String VLNumeConvenio = ""; // se recibe como parametro para impresion
        //String VLNombreConvenio = ""; // se recibe como parametro para impresion
        //String VLValorPagar = "";// se recibe como parametro para impresion
        //String VLValorRecibido = "";// se recibe como parametro para impresion
        //String VLcambioValor = ""; // se recibe como parametro para impresion
        //String VLcodServicioValor = ""; // se recibe como parametro para impresion
        //String VLfacturaValor = ""; // se recibe como parametro para impresion
        //String VLcodigoBanco = ""; // se recibe como parametro para impresion
        //String VLcodigounicoValor = ""; // se recibe como parametro para impresion
        //String VLrrn = ""; // fijoVps
        //String VLRRNValor = ""; // se recibe como parametro para impresion


        public Factura(Contexto contexto)
        {
            CultureInfo.CurrentCulture = Consumo.InfoPais;
            CultureInfo.DefaultThreadCurrentCulture = Consumo.InfoPais;
            CultureInfo.DefaultThreadCurrentUICulture = Consumo.InfoPais;
            Thread.CurrentThread.CurrentCulture = Consumo.InfoPais;
            Thread.CurrentThread.CurrentUICulture = Consumo.InfoPais;

            Contexto = contexto;

            InitializeComponent();
            logo.Source = Utilitario.EstablecerImagen($@"Util\logo.jpg");
        }

        public void ArmaImpresion(bool exitosa, String NombreCliente, String NumeroCliente, string referencia, string valor, string insertado, string cambio, string autorizacion)
        {
            LeeParametrosImpre();
            LTitulo.Content = "Financiera Comultransan";
            LTitulo1.Content = "Es posible";
            LTitulo2.Content = "";
            LCorresponsal.Content = "Nombre Kiosco: " + "Kiosco-001";

            Thread.Sleep(200);
            LFecha.Content = "Fecha y Hora:     " + DateTime.Now.ToString(Consumo.InfoPais);



            LNroCliente.Content = "Nombre cliente: ";
            if (Contexto.VieneDesembolso)
            {
                LValorPagar.Content = "Valor desembolsado: ";
                LfacturaValor.Content = Contexto.numeroDocumento;

                if (Contexto.CualMenu == "Servicios")
                {
                    LValorPagarValor.Content = 97000.ToString("C", Consumo.InfoPais);
                    Lfactura.Content = "Número Factura: ";
                    LNroClienteValor.Content = "Acuasan. E.S.P";
                    LcomprobanteValor.Content = "92561414848";
                    LValorPagarFactura.Visibility = Visibility.Hidden;
                    LNroCliente.Content = "Empresa / Servicio: ";
                }
                else
                {
                    Lfactura.Content = "No. de cliente : ";
                    LValorPagarValor.Content = 1000000.ToString("C", Consumo.InfoPais);
                    LNroClienteValor.Content = "María Salomé Jiménez Bustos";
                    LcomprobanteValor.Content = "398172631318";
                    LValorPagarFactura.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                LValorPagar.Content = "Cantidad Ingresada: ";
                LValorPagarFactura.Content = "VALOR PAGO:";

            }
            if (exitosa)
            {
                if (!Contexto.VieneDesembolso)
                {
                    LfacturaValor.Content = Contexto.numeroDocumento;
                    if (Contexto.CualMenu == "Servicios")
                    {
                        Lfactura.Content = "Número Factura: ";
                        LNroClienteValor.Content = "Acuasan. E.S.P";
                        LcomprobanteValor.Content = "92561414848";
                        LNroCliente.Content = "Empresa / Servicio: ";

                    }
                    else
                    {
                        Lfactura.Content = "No. de cliente : ";
                        LNroClienteValor.Content = "María Salomé Jiménez Bustos";
                        LcomprobanteValor.Content = "398172631318";

                    }
                    LValorPagarValorFactura.Content = Contexto.AmountToPay.ToString("C", Consumo.InfoPais);
                    LValorPagarValor.Content = Contexto.Entered.ToString("C", Consumo.InfoPais);
                    LValorRecibido.Content = Contexto.Cambio.ToString("C", Consumo.InfoPais);
                }
                else
                {
                    Lfactura.Content = "No. de cliente : ";
                    LValorPagarValorFactura.Visibility = Visibility.Hidden;
                    LValorRecibido.Visibility = Visibility.Hidden;
                    LTotalPago.Visibility = Visibility.Hidden;
                    LcomprobanteValor.Content = "1657468481";
                }
            }
            else
            {
                LfacturaValor.Content = referencia;
                LNroClienteValor.Content = Contexto.NroCliente;
                LValorPagarValor.Content = valor;
                LValorRecibido.Content = insertado;
            }

            LTotalPago.Content = "Cambio entregado: ";
            //Lpago.Content = VLpago;
            //Contador();

            //LNumeConvenio.Content = PLUConvenio;
            //Lcodigounico.Content = VLcodigounico;  // se debe cambiar por el enviado por sandra RBM
            //LcodigounicoValor.Content = CodigoUnico;   // ojo se cambio en vps
            //Lterminal.Content = VLterminal; // se debe cambiar por el enviado por sandra RBM;
            //LcodigoBanco.Content = VLcodigoBancoValor;
            //LRecibo.Content = VLRecibo + "   " + autorizacion;
            //Lrrn.Content = VLrrn + "         " + RRNValor;
            //LRRNValor.Content = ;
            //LAprobado.Content = VLAprobado + "   " + CodigoAprobacion;
            //LcodServicioValor.Content = CodigoServicio;
            //LTextF1.Content = VLTextF1;
            //LTextF2.Content = VLTextF2;
            //LTextF3.Content = VLTextF3;
            //LTextF4.Content = VLTextF4;


            //GuardarReciboPdf(CanvasImpresion);
            //GuardarReciboPdf();
        }

        public void Contador()
        {
            Consumo.LoggerInfo("Entra a crear y guardar el consecutivo de la impresion");


            String line = "";
            int contador = 0;
            String FechaEvaluar = "";
            String[] lines;

            try
            {
                StreamReader sr = new StreamReader(@"Contador.txt");
                line = sr.ReadLine();
                lines = File.ReadAllLines(@"Contador.txt");

                contador = Convert.ToInt32(line);
                FechaEvaluar = lines[1].ToString();
                sr.Close();
                if (FechaEvaluar == DateTime.Now.ToString("yyyy/MM/dd"))
                {
                    contador = contador + 1;
                    StreamWriter sw = new StreamWriter(@"Contador.txt");
                    sw.WriteLine(contador.ToString());
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd"));
                    sw.Close();
                    LNoCIn.Content = contador.ToString();
                }
                else
                {
                    contador = 1;
                    StreamWriter sw = new StreamWriter(@"Contador.txt");
                    sw.WriteLine(contador.ToString());
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd"));
                    sw.Close();
                    LNoCIn.Content = contador.ToString();
                }
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Creando y guardando el consecutivo");

            }



        }


        public void GuardarReciboPdf()
        {
            PdfSharp.Xps.XpsModel.XpsDocument pdfXpsDoc = null;
            try
            {
                MemoryStream lMemoryStream = new MemoryStream();
                Package package = Package.Open(lMemoryStream, FileMode.Create);
                XpsDocument doc = new XpsDocument(package);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                writer.Write(CanvasImpresion);
                doc.Close();
                package.Close();

                pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
                PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, NombreArchivo + ".pdf", 0);
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Guardando Recibo PDF");
            }
            finally
            {
                pdfXpsDoc.Close();
            }
        }

        public void GuardarCopiaReciboXps()
        {
            try
            {
                XpsDocument doc = new XpsDocument(NombreArchivo + ".xps", FileAccess.Write);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

                writer.Write(CanvasImpresion);
                doc.Close();

            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Guardando Copia Recibo");
            }
        }

        public void LeeParametrosImpre()
        {
            Consumo.SetCurrentCultureInfo();

            Contexto.GetParams().DSparams = new DataSet();
            try
            {
                Utilitario.CargarXmltoDataSet(@"Xml\\ParametrosImpre.Xml", ref Contexto.GetParams().DSparams);
                if (Contexto.GetParams().DSparams.Tables.Count > 0)
                {
                    for (int j = 0; j < Contexto.GetParams().DSparams.Tables.Count; j++)
                    {
                        if (Contexto.GetParams().DSparams.Tables[j].TableName.ToUpper() == "PARAMETRO")
                        {
                            if (Contexto.GetParams().DSparams.Tables[j].Rows.Count > 0)
                            {
                                for (int i = 0; i < Contexto.GetParams().DSparams.Tables[j].Rows.Count; i++)
                                {
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCOMPROBANTE")
                                        VLcomprobante = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOTOTALPAGO")
                                        VLTotalPago = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCAMBIO")
                                        VLCambio = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULORECUADO")
                                        VLrecaudo = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOFACTURA")
                                        VLfactura = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOPAGOFACTURA")
                                        VLpago = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOTRANSACCIONEXITOSA")
                                        VLEstadoTransaccion = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOTRANSACCIONDECLINADA")
                                        VLEstadoTransaccionD = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT5")
                                        VLTextF5 = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();

                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCODIGODELBANCO")
                                        VLcodigoBancoValor = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULORECIBO")
                                        VLRecibo = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCODIGOUNICO")
                                        VLcodigounico = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOAPROBADO")
                                        VLAprobado = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOTERMINAL")
                                        VLterminal = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "CORRESPONSAL")
                                        VLCorresponsal = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "NOMBREALMACEN")
                                        VLnombreAlmacen = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULOCODIGOSERVICIO")
                                        VLcodservicio = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "DIRECCIONALMACEN")
                                        VLDireccion = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    //if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TITULORRN")
                                    //    this.VLrrn = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT1")
                                        VLTextF1 = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT2")
                                        VLTextF2 = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT3")
                                        VLTextF3 = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                    if (Contexto.GetParams().DSparams.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TFOOT4")
                                        VLTextF4 = Contexto.GetParams().DSparams.Tables[j].Rows[i]["valor"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Leyendo parámetros de impresión Factura");
            }

        }

    }
}
