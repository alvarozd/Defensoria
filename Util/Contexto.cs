using FacturasEnel.Logica;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using VirtualCash.Business.Services;
using FacturasEnel.View.Componentes;
using System.Windows.Controls;
using Renci.SshNet;
using BusinessEnel;
using VirtualCash.Business.Services.Devices;
using System.Configuration;
using VirtualCash.Business.Services.Interfaces;
using System.Runtime.CompilerServices;
using ServiceReference;
using System.Threading;
using Renci.SshNet.Sftp;
using Renci.SshNet.Common;
using System.ServiceModel;
using NLog;
using System.Net.Sockets;
using WinSCP;
using FacturasEnel.Util;
using CTM;
using System.Media;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Enel.Modelo;

namespace FacturasEnel.Util
{
    [Serializable()]
    public class Contexto
    {

        public MediaPlayer Audios = new MediaPlayer();

        public enum EstadoTransaccion
        {
            SinIniciar,         // NO HACE NADA, ES EL ESTADO INICIAL
            RecaudoCompleto,    // NO HACE NADA, SE USA PARA SABER QUE SE RECAUDO Y PUEDE IR A PROCESAR EL PAGO
            Aprobada,           // SOLO DEBE IMPRIMIR
            Cancelada,          // SOLO DEBE IMPRIMIR
            CanceladaNoCambio,  // SOLO DEBE IMPRIMIR RECIBO ADMINISTRATIVO
            Rechazada           // DEBE IMPRIMIR Y REVERSAR
        }

        public int Alto { get; set; }
        public int Ancho { get; set; }

        [NonSerialized]
        private Parametros @params;

        public Parametros GetParams()
        {
            return @params;
        }

        public void SetParams(Parametros value)
        {
            @params = value;
        }

        public string Convenio { get; set; }
        public string MenuAnt { get; set; }
        public string CualMenu { get; set; }
        public bool Cargado { get; set; }
        public string DatoConsulta { get; set; }
        public bool MuestraFinaliza { get; set; } = false;


        /// para la impresión del recibo
        public string IMPTipoFlujo { get; set; } = "";
        public string IMPCantidadIngresada { get; set; } = "";
        public string IMPCambioEntregado { get; set; } = "";
        public string IMPCambioFaltante { get; set; } = "";
        public bool ErrorEntrega { get; set; } = false;
        ///


        public dynamic ConsultaAbono = null;
        public dynamic respuestaImpresion = null;
        public dynamic retornoabono = null;
        public dynamic retornoabono1 = null;
        public dynamic RespuestaGeneraArchivo = null;


        public String ValorTicket { get; set; } = "";
        public String numeroTicket { get; set; } = "";
        public String numeroDocumento { get; set; } = "";
        public String numeroPin { get; set; } = "";

        public bool Altocontraste = true;


        public String NombreCliente { get; set; }
        public int PagoSemanal { get; set; } = 0;
        public int PagoSugerido { get; set; } = 0;
        public int PagoLiquidar { get; set; } = 0;
        public int NumerodeCreditos { get; set; } = 0;
        public String NumeroProducto { get; set; } = "";
        public String NumeroCredito { get; set; } = "";

        public bool HayCargaIncial = false;


        //DATOS DE APLICACION DE DEFENSORIA DEL PUEBLO

        public string NombrePersona = "";

        public string ApellidoPersona = "";

        public string IdDocumentoPersona = "";
        public string DocumentoPersona = "";

        public string IdSexo = "";
        public string IdDepartamento = "";

        public string IdMunicipio = "";


        public string IdDepartamentoHechos = "";

        public string IdMunicipioHechos = "";

        public string Token = "";

        public string CorreoPersona = "";

        public string TelefonoPersona = "";

        public string Resumen = "";

        public string BreveDescripcion = "";

       


        public List<Sexo> Combosexo = new List<Sexo>();
        public List<Departamento> ComboDepartamento = new List<Departamento>();
        public List<Municipio> ComboMunicipio = new List<Municipio>();
        public List<Documento> ComboDocumento = new List<Documento>();


        public string NroCliente { get; set; }
        public bool pagoTotal = true;
        public string RespuestaPago1 { get; set; } //toc a dejrlos otra vez cuando se decuelva el servicio web
        public string InfoFactura1 { get; set; } //toc a dejrlos otra vez cuando se decuelva el servicio web


        public string ArchivoConfig { get; set; } = "";
        public string IpAbono { get; set; } = "";
        public string WorkStation { get; set; } = "";

        // variable para validar si se tiene efectivo o no

        public bool tieneCambio { get; set; } = false;
        public int NumEstadoImpresora { get; set; } = 0;
        public bool VieneDesembolso { get; set; } = false;

        //


        [NonSerialized]
        private CamposPago infoFactura = null;

        [NonSerialized]
        public PayResultClass RespuestaPago;
        public TipoConsulta TipoConsulta { get; set; }
        public EstadoTransaccion EstadoTransaccionActual { get; set; }

        #region IMPORTANTE!!!! EN PRODUCCIÓN SIEMPRE DEBEN ESTAR EN TRUE LOS TRES BOOLEANOS
        public bool ConsultaReal { get; set; } = true;
        public bool PagoReal { get; set; } = true; // siempre true
        public bool RegistroReal { get; set; } = true;
        public bool CargarDispositivos { get; set; } = false;
        private static string _ultimoArchivoDescargado;

        #endregion

        #region DISPOSITIVOS DE PAGO
 //       [NonSerialized]
//        private GenericVCashService vCash;
        [NonSerialized]
        private IproDevice billetero;
        [NonSerialized]
        private SmartHopperDevice monedero;
        public static long IdUltimaTransaccion { get; set; }
        #endregion

        #region DISPOSITIVOS DE PAGO CTM
        [NonSerialized]
        private CtmClient vCash;

        #endregion

        #region TransaccionesBancoAzteca


        #endregion

        #region VARIABLES MANEJO ENTRADA Y SALIDA DE DINERO
        public String CualFlujo { get; set; } = "";
        public decimal Entered { get; set; } = 0;
        public decimal CambioFaltante { get; set; } = 0;
        public decimal AmountToPay { get; set; } = 0;
        public int Cambio { get; set; } = 0;
        public decimal CambioBill { get; set; } = 0;
        public int CambioBillEntregado { get; set; } = 0;
        public decimal CambioCoin { get; set; } = 0;
        public IproDevice Billetero { get => billetero; set => billetero = value; }
        public SmartHopperDevice Monedero { get => monedero; set => monedero = value; }
        public CamposPago InfoFactura { get => infoFactura; set => infoFactura = value; }
        public CtmClient VCash { get => vCash; set => vCash = value; }

        #endregion

        #region VARIABLES CONSULTA TOPES DISPOSITIVOS

        [NonSerialized] Parametros.LogicaCantidades topes;
        [NonSerialized] VCashServiceClient cashClient;
//        [NonSerialized] Devices[] lstNvlsDvcs;
//        [NonSerialized] Devices[] lstTotsDvcs;
        string[] msgsEstadoCantidades;

        #endregion

        public Contexto()
        {
            Consumo.InfoPais.NumberFormat.CurrencyDecimalDigits = 0;
            LogManager.Configuration.DefaultCultureInfo = Consumo.InfoPais;

            Alto = Convert.ToInt32(Application.Current.MainWindow.Height);
            Ancho = Convert.ToInt32(Application.Current.MainWindow.Width);
            Cargado = false;
            ArchivoConfig = @"C:\ADN\NET64_EKT\PM_ESTCONFIG.txt";
            ConsultarArchivoConfig();


        }

        public async Task ConsultarArchivoConfig()
        {
            try
            {
                string[] lines = File.ReadAllLines(ArchivoConfig);

                foreach (string line in lines )
                {
                    //Console.WriteLine(line);
                    IpAbono = lines[0].ToString();
                    WorkStation = lines[1].ToString();
                }

                IpAbono = IpAbono.Trim('/', '>', '[',']');
                WorkStation = WorkStation.Trim('/', '>', '[', ']');

            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, $"Error en la consulta del archivo de configuración Kiosco BAZ");
            }
        }

        public async Task ConsultarParametros()
        {
            try
            {
                SetParams(new Parametros());
                await GetParams().ConsultaParametrosVps();
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, $"Contexto.ConsultarParametros()");
            }
        }

        public bool InicializarDispositivosPago()
        {
            var taskVcash = Task.Run(() => InicializarVirtualCash());
            Task.WaitAll(taskVcash);

            return taskVcash.Result;
        }

        public bool InicializarVirtualCash()
        {
            bool paso = true;

            Consumo.LoggerInfo("INICIALIZANDO VIRTUAL CASH");

            try
            {
//                VCash = new CtmClient(Convert.ToInt32(Consumo.IdKiosco, Consumo.InfoPais), "http://212.83.170.202/SDK/VCashSoapServicesGenerico/VCashService.svc");
                VCash = new CtmClient(Convert.ToInt32(Consumo.IdKiosco, Consumo.InfoPais), "http://localHost/Azteca/VCashSoapServicesGenerico/VCashService.svc");
                string respu = VCash.InitializeCTM();
                if (string.IsNullOrEmpty(respu))
                {
                    Consumo.Logger.Info("VIRTUAL CASH INICIALIZADO");
                }
                else
                {
                    Consumo.Logger.Error("INICIALIZANDO VIRTUAL CASH", respu);
                }
                //VCash = new GenericVCashService(new IVCashDevice[] { Monedero }, 1000M, Params.Params.EndPointVCash, Convert.ToInt32(Consumo.IdKiosco));
                /*                VCash.CashAccept += Ctm_CashAccept;
                                VCash.CashAcceptComplete += Ctm_CashAcceptComplete;
                                VCash.DeviceError += Ctm_DeviceError;
                                VCash.TransactionComplete += Ctm_TransactionComplete;
                                VCash.DeviceStatus += Ctm_DeviceStatus;  estos se van a usar después*/


            }
            catch (Exception ex)
            {
                paso = false;
                Consumo.Logger.Error(ex, "INICIALIZANDO VIRTUAL CASH");
                //Consumo.EnviarEmailExcepciones(ex, "Inicializando VirtualCash");
            }
            return paso;
        }

        private void Billetero_StackerStateEvent(object sender, StackerState e)
        {
            string msg = "EL STACKER DEL BILLETERO FUE ";
            if (e == StackerState.Removed)
                msg += "REMOVIDO";
            if (e == StackerState.Inserted)
                msg += "INSERTADO";

            Consumo.Logger.Error("Alerta Billetero. " + msg);
            Consumo.EnviarEmail("Alerta Billetero", msg);
        }

        private void VCash_VcashError(object sender, Exception e)
        {
            if (Cargado)
            {
                Consumo.EnviarEmailExcepciones(e, "VCash_VcashError");
            }
            else
            {
                string cuerpo = $"Kiosco Fuera de línea por Error en Virtual Cash<br>";
                cuerpo += $"Error: {e}";
                Consumo.EnviarEmail("Fuera de línea", cuerpo, "VCash_VcashError");
            }
        }

        private void VCash_ServiceError(object sender, Exception e)
        {
            if (Cargado)
            {
                Consumo.EnviarEmailExcepciones(e, "VCash_ServiceError");
            }
            else
            {
                string cuerpo = $"Kiosco Fuera de línea por Error en el servicio web<br>";
                cuerpo += $"Error: {e}";
                Consumo.EnviarEmail("Fuera de línea", cuerpo, "VCash_ServiceError");
            }
        }


        public static int AproximarMontoOperacion(decimal Monto)
        {
            int Aprox = (int)Math.Floor(Monto / 100) * 100;
            Consumo.Redondeo = (Monto - Aprox).ToString("C", Consumo.InfoPais);
            return Aprox;
        }

        public void ImprimirRecibo(bool exitosa)
        {
            Consumo.LoggerInfo($"IMPRIMIENDO RECIBO");

            switch (EstadoTransaccionActual)
            {
                case EstadoTransaccion.Aprobada:
                    Consumo.InsertarDetalleTransaccionServer("Transacción exitosa", "Imprimiendo recibo");
                    break;
                case EstadoTransaccion.Rechazada:
                    Consumo.InsertarDetalleTransaccionServer("Transacción rechazada", "Imprimiendo recibo");
    //                RespuestaPago = new PayResultClass() { Autorizacion = "No Aplica", Mensaje = "", Resultado = "" };
                    break;
                case EstadoTransaccion.Cancelada:
                    Consumo.InsertarDetalleTransaccionServer("Transacción cancelada", "Imprimiendo recibo");
     //               RespuestaPago = new PayResultClass() { Autorizacion = "No Aplica", Mensaje = "", Resultado = "" };
                    break;
            }

            Factura recibo = new Factura(this);
            try
            {
//                Consumo.IdTransa = Contexto.IdUltimaTransaccion.ToString();

                recibo.NombreArchivo = Environment.CurrentDirectory + $@"\Recibos\VPS{Consumo.IdTransa.PadLeft(8, '0')}-VC{Consumo.IdVCash.PadLeft(8, '0')}";

                recibo.Left = 3000;
                recibo.ShowInTaskbar = false;

                decimal CambioReal = 0;


                recibo.ArmaImpresion(exitosa,
                            NombreCliente,
                            InfoFactura1,
                            InfoFactura1,
                            IMPCantidadIngresada, // VALOR RECIBIDO
                            IMPCambioEntregado, // VALOR PAGAR
                            IMPCambioFaltante,  // VALOR CAMBIO
                            RespuestaPago1 // NÚMERO RECIBO
                        ) ;
                recibo.Show();
       //         recibo.GuardarReciboPdf();
                PrintDialog pd = new PrintDialog();
                pd.PrintVisual(recibo.CanvasImpresion, $"Comprobante transacción {Consumo.IdTransa}");

            }
            catch (RuntimeWrappedException ex)
            {
                Consumo.Logger.Error(ex, "Imprimiendo recibo");
                Consumo.InsertarDetalleTransaccionServer($"Impresión recibo, error: {ex.Message}", "Imprimiendo recibo");
                Consumo.InsertarLogServer("Impresión", "Imprimiendo", $"Transacción {EstadoTransaccionActual} error: {ex}");
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Imprimiendo recibo");
                Consumo.InsertarDetalleTransaccionServer($"Impresión recibo, error: {ex.Message}", "Imprimiendo recibo");
                Consumo.InsertarLogServer("Impresión", "Imprimiendo", $"Transacción {EstadoTransaccionActual} error: {ex}");
            }
            finally
            {
                recibo.Close();
            }

            //if (REGISTROREAL)
            //    LlenarArchivoConciliacion();
        }

        public void LlenarArchivoConciliacion()
        {
            //20191114|095823|0017153131|25000|COP|000001|8875869907|20|03|06|0052|0|01
            //20140714|173521|0017153131|3888|COP|000002|8875560010|20|03|06|0052|0|01

            string nombreArchivo = $"Transacciones{DateTime.Now.ToString("yyyyMMdd", Consumo.InfoPais)}.txt";
            string f = DateTime.Now.ToString("yyMMdd", Consumo.InfoPais); //Fecha = "191114",
            string h = DateTime.Now.ToString("HHmm", Consumo.InfoPais);  //Hora = "1057",

            DateTime dt = new DateTime(Convert.ToInt32(f.Substring(0, 2), Consumo.InfoPais) + 2000, Convert.ToInt32(f.Substring(2, 2), Consumo.InfoPais), Convert.ToInt32(f.Substring(4, 2), Consumo.InfoPais), Convert.ToInt32(h.Substring(0, 2), Consumo.InfoPais), Convert.ToInt32(h.Substring(2, 2), Consumo.InfoPais), 0, 0);

            string Fecha = dt.ToString("yyyyMMdd", Consumo.InfoPais).ToUpper();
            string Hora = dt.ToString("HH:mm:ss", Consumo.InfoPais).ToUpper();

            string cualcolocar = "";

            // 0 es VTIPOTRANSACCIONSERVICIOPUBLICO
            // 1 es VTIPOTRANSACCIONPILA

            cualcolocar = GetParams().infoFileSFTP.VTIPOTRANSACCIONSERVICIOPUBLICO;

            string RutaArchivo = @"Conciliacion\" + nombreArchivo;
            string Informacion = Fecha + "|" + Hora + "|" + GetParams().infoFileSFTP.VCODUNICOINT + "|" + InfoFactura.ValorPagar +
                                 "|" + GetParams().infoFileSFTP.VMONEDA + "|" + "códigoAprobación" + "|" + InfoFactura.ReferenciaPago +
                                 "|" + GetParams().infoFileSFTP.VIDENTIFICACIONNEGOCIO + "|" + cualcolocar +
                                 "|" + GetParams().infoFileSFTP.VTIPOMEDIOPAGO + "|" + GetParams().infoFileSFTP.VIDBANCO + "|0|" + GetParams().infoFileSFTP.VIDCONVENIO;
            try
            {
                using (StreamWriter writer = new StreamWriter(RutaArchivo, true))
                {
                    writer.WriteLine(Informacion);
                }
            }
            catch (IOException ex)
            {
                Consumo.Logger.Error(ex, "Error creando archivo conciliación");
            }
        }

        public static void SubirArchivo()
        {
            const int port = 22;
            const string host = "transfer.grupo-exito.com";
            const string username = "jjimenez@virtual.com.co";
            const string password = "Ncr2019virtual123*";
            const string workingdirectory = "/EntradaKiosko";
            //string uploadfile = "";

            string archivoSubir = $"Transacciones{DateTime.Now.ToString("yyyyMMdd", Consumo.InfoPais)}.txt";

            string RutaArchivo = @"Conciliacion\" + archivoSubir;

            try
            {
                using (var client = new SftpClient(host, port, username, password))
                {
                    try
                    {
                        client.Connect();
                        client.ConnectionInfo.Timeout = TimeSpan.FromSeconds(300);
                    }
                    catch (ObjectDisposedException ex)
                    {
                        Consumo.Logger.Error(ex, "Conectando con el servidor FTP");
                    }
                    catch (Exception ex)
                    {
                        Consumo.Logger.Error(ex, "Conectando con el servidor FTP");
                    }

                    client.ChangeDirectory(workingdirectory);

                    using (var fileStream = new FileStream(RutaArchivo, FileMode.Open))
                    {
                        client.BufferSize = 4 * 1024; // bypass Payload error large files
                        client.UploadFile(fileStream, Path.GetFileName(RutaArchivo));
                    }
                }
                Consumo.InsertarEstadisticaServer("Menú Principal", $"Archivo {archivoSubir} de conciliación subido", "Subido", "0");
            }
            catch (IOException ex)
            {
                Consumo.Logger.Error(ex, $"Escribiendo el archivo {archivoSubir}");
            }
            catch (ObjectDisposedException ex)
            {
                Consumo.Logger.Error(ex, "Conectando con el servidor FTP.");
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, $"Error subiendo archivo {archivoSubir}");
            }

        }

        public static void InicializarDispositivosPago2()
        {
            Task taskVCash = Task.Run(() => Task.Delay(2000));
            Task.WaitAll(taskVCash);
        }

        public void CargaInicial()
        {
            StreamReader FileCarga = new StreamReader(@"Carga.txt");
            Dictionary<decimal, int> CargaBilletero = new Dictionary<decimal, int>();
            Dictionary<decimal, int> CargaMonedero = new Dictionary<decimal, int>();

            string strBillete = FileCarga.ReadLine();
            string[] ListaCarga = strBillete.Split(';');

            foreach (var item in ListaCarga)
            {
                string[] Carga = item.Split(':');
                CargaBilletero.Add(Convert.ToDecimal(Carga[0], Consumo.InfoPais), Convert.ToInt32(Carga[1], Consumo.InfoPais));
            }

            if (Billetero != null)
                Billetero.SetCurrentBalance(CargaBilletero);

            string strMoneda = FileCarga.ReadLine();
            ListaCarga = strMoneda.Split(';');
            foreach (var item in ListaCarga)
            {
                string[] Carga = item.Split(':');
                CargaMonedero.Add(Convert.ToDecimal(Carga[0], Consumo.InfoPais), Convert.ToInt32(Carga[1], Consumo.InfoPais));
            }
            if (Monedero != null)
                Monedero.SetCurrentBalance(CargaMonedero);
            FileCarga.Close();
        }

        public string ConsultarCarga()
        {
            string cadenaCarga = "BILLETERO\n";

            try
            {
                cadenaCarga += string.Join("\n", Billetero.GetCurrentRecyclerLevels(0).Select(x => "$" + x.Key + ": " + x.Value));
                cadenaCarga += "\n\nMONEDERO\n";
                cadenaCarga += string.Join("\n", Monedero.GetCurrentRecyclerLevels(0).Select(x => "$" + x.Key + ": " + x.Value));
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Exception consultando Carga");
            }

            return cadenaCarga;
        }
    }
}
