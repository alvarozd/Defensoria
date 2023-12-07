using FacturasEnel.Modelo;
using FacturasEnel.Util;
using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VPSservices;
using Renci.SshNet;
using System.IO;
using System.Xml.Linq;
using System.Configuration;
using BusinessEnel;

namespace FacturasEnel.Logica
{
    public static class Consumo
    {
        //public class InfoConvenio
        //{
        //    public string PLU { get; set; }
        //    public string conv { get; set; }
        //    public string EMPRESA { get; set; }
        //}

        public static string IdKiosco = ConfigurationManager.AppSettings.Get("IdKiosco");
        public static string CodTerminal = ConfigurationManager.AppSettings.Get("CodTerminal");
        public const string IdAplicacion = "2";
        public static string EndPointVps = ConfigurationManager.AppSettings.Get("WS_VPS");

        public static string IdTransa { get; set; }
        public static string CodigoIacFactuador { get; set; }
        public static string IdVCash { get; set; }
        public static string ValorPagar { get; set; }
        public static decimal MontoOperacionParcial { get; set; }
        public static string Redondeo { get; set; } = "0";

        public static CultureInfo InfoPais = CultureInfo.CreateSpecificCulture("en-US");
        private static RegistroVirtual vps = new RegistroVirtual(IdKiosco, IdAplicacion, EndPointVps);
        public static bool ArchivosActualizados { get; set; }  = false;
        public static Logger Logger { get; set; } = LogManager.GetCurrentClassLogger();

        public static void LoggerInfo(string infoLog)
        {
            Logger.Info(InfoPais, infoLog);
        }

        public async static Task<CamposPago> ConsultarPorCodigoBarras(string codigoBarras)
        {
            CamposPago Campos = null; 
            Logger.Info($"Código leído: {codigoBarras}");

            await Task.Run(() =>
            {
                EnelBusinessClass PayClass = new EnelBusinessClass();

                try
                {
                    Campos = PayClass.DecodificarCodigo(codigoBarras);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex, "ConsultarPorCodigoBarras");
                }

                if (Campos != null)
                {
                    Logger.Info($"Información Factura. Referencia: {Campos.ReferenciaPago}, Valor a Pagar: {Campos.ValorPagar.ToString("C", InfoPais)}");
                }
                else
                {
                    Logger.Info($"No se puedo obtener la información de la factura para el código: {codigoBarras}.");
                }
            });

            return Campos;
        }

        public static async Task<CamposPago> ConsultarPorNumeroCliente(string numeroCliente)
        {
            CamposPago Campos = null;
            Logger.Info($"Número de Cliente: {numeroCliente}");

            await Task.Run(() =>
            {
                EnelBusinessClass PayClass = new EnelBusinessClass();

                Campos = PayClass.BuscarReferenciaArchivo(numeroCliente);

                if (Campos != null)
                {
                    Logger.Info($"Información Factura. Referencia: {Campos.ReferenciaPago}, Valor a Pagar: {Campos.ValorPagar.ToString("C", InfoPais)}");
                }
                else
                {
                    Logger.Info($"No se pudo obtener la información de la factura para número de cliente: {numeroCliente}.");
                }

            });

            return Campos;
        }

        public async static Task<PayResultClass> PagarFactura(TipoConsulta tipo, string datoConsulta)
        {
            Logger.Info($"Pagar factura. Tipo Pago: {tipo}, Dato consulta: {datoConsulta}");

            EnelBusinessClass PayClass = new EnelBusinessClass();
            PayResultClass PayResult = null;

            await Task.Run(() =>
            {
                switch (tipo)
                {
                    case TipoConsulta.CodigodeBarras:
                        {
                            PayResult = PayClass.GenerarPago(datoConsulta, IdTransa, CodTerminal);
                        }
                        break;
                    case TipoConsulta.NumerodeCliente:
                        {
                            PayResult = PayClass.GenerarPagoManual(datoConsulta, IdTransa, CodigoIacFactuador, CodTerminal);
                        }
                        break;
                }
            }
            ).ContinueWith(t => {
                Logger.Info("Estado tarea: {@estatustask}", t.Status);
                if (t.IsCompleted)
                {
                    if (t.IsFaulted)
                    {
                        Logger.Error(t.Exception.InnerException, $"Task IsFaulted PagarFactura");
                    }
                    else
                    {
                        Logger.Info($"PagarFactura Pago enviado al servidor");
                    }
                }
                else
                {
                    Logger.Error(t.Exception.InnerException, $"exception PagarFactura");
                }
            });

            Logger.Info("PayResult: {@PayResult}", PayResult);

            return PayResult;
        }

        public static async Task<string> InsertarTransaccionServer(string descripcion, string estado)
        {
            string idCreado = "";
            IdVCash = "N/A";
            ValorPagar = 0.ToString("C", InfoPais);
            Redondeo = 0.ToString("C", InfoPais);
            var TaskRespu = Task.Run(async () => await vps.InsertarTransaccionAsync(IdVCash, descripcion, ValorPagar, estado));
            await Task.WhenAll(TaskRespu);

            idCreado = TaskRespu.Result;
            if (TaskRespu.Result.LastIndexOf("ERROR") >= 0)
            {
                Logger.Error($"Insertando transacción. Error: {TaskRespu.Result}");
            }
            else
            {
                if (string.IsNullOrEmpty(TaskRespu.Result))
                {
                    Logger.Error($"Insertando transacción. Error id transacción Vacío");
                }
                else
                {
                    //////// LA TRANSACCIÓN SE CREO CORRECTAMENTE
                    Logger.Info("\r\n+ + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + +" +
                                "\r\n+ + + + + + + + + + + + + + + + +INICIANDO TRANSACCIÓN+ + + + + + + + + + + + + + + +" +
                                "\r\n+ + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + +");
                    IdTransa = idCreado;
                }
            }
            return idCreado;
        }

        public static void FinTransaccion(string detalle, string estado, string NroCliente)
        {
            Logger.Info($"Número de Cliente {NroCliente}");

            Logger.Info("\r\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" +
            "\r\n- - - - - - - - - - - - - - - - - - - -FINALIZANDO TRANSACCIÓN- - - - - - - - - - - - - - - - - -" +
           $"\r\n- - - - - - - - - - - - - - - - - - - {estado.ToUpperInvariant()} - - - - - - - - - - - - - - - -" +
            "\r\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            vps.InsertarDetalleTransaccionAsyncSinEspera(IdTransa, IdVCash, detalle, ValorPagar, estado);
        }

        public static void FinTransaccionTimeOut()
        {
            Logger.Info("\r\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" +
                        "\r\n- - - - - - - - - - - - -TRANSACCIÓN FINALIZADA POR TIEMPO- - - - - - - - - - - - - -" +
                        "\r\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            vps.InsertarDetalleTransaccionAsyncSinEspera(IdTransa, IdVCash, "Tiempo de inactividad cumplido", ValorPagar, "Transacción cancelada");
        }

        public static void FinTransaccionMenuPrincipal()
        {
            Logger.Info("\r\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -" +
                        "\r\n- - - - - - - - - - - -TRANSACCIÓN FINALIZADA POR EL USUARIO- - - - - - - - - - - - -" +
                        "\r\n- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
            vps.InsertarDetalleTransaccionAsyncSinEspera(IdTransa, IdVCash, "El usuario regresó al Menú Principal", ValorPagar, "Transacción cancelada");
        }

        public static void EnviarEmailExcepciones(Exception excepcion, string modulo)
        {
            string Asunto = $"Excepción";
            string Cuerpo = excepcion.ToString().Replace("'", "''");

            if (string.IsNullOrEmpty(modulo))
                InsertarLogServer("Log envío email", Asunto, Cuerpo);
            else
                InsertarLogServer("Log envío email", $"{Asunto} {modulo}", Cuerpo);

            vps.EnviarEmailAsyncSinEspera(Asunto, Cuerpo);
        }

        public static void EnviarEmail(string asunto, string cuerpo)
        {
            InsertarLogServer("EnviarEmail", asunto, asunto);
            vps.EnviarEmailAsyncSinEspera(asunto, cuerpo);
        }

        public static void EnviarEmail(string asunto, string cuerpo, string modulo)
        {
            InsertarLogServer(modulo, asunto, cuerpo);
            vps.EnviarEmailAsyncSinEspera(asunto, cuerpo);
        }

        public static void InsertarDetalleTransaccionServer(string detalle, string estado)
        {
            vps.InsertarDetalleTransaccionAsyncSinEspera(IdTransa, IdVCash, detalle, ValorPagar, estado);
        }

        public static void InsertarEstadisticaServer(string modulo, string descripcion, string estado, string redondeo)
        {
            vps.InsertarEstadisticaAsyncSinEspera(modulo, descripcion, estado, redondeo);
        }

        public static void InsertarLogServer(string modulo, string proceso, string descripcion)
        {
            vps.InsertarLogAsyncSinEspera(modulo, proceso, descripcion);
        }

        public static string AutenticarUsuario(string usuario, string password)
        {
            return vps.ValidarUsuarioAsync(usuario, password);
        }

        public static void SetCurrentCultureInfo()
        {
            CultureInfo.CurrentCulture = Consumo.InfoPais;
            CultureInfo.DefaultThreadCurrentCulture = Consumo.InfoPais;
            CultureInfo.DefaultThreadCurrentUICulture = Consumo.InfoPais;
            Thread.CurrentThread.CurrentCulture = Consumo.InfoPais;
            Thread.CurrentThread.CurrentUICulture = Consumo.InfoPais;
        }

        //public static CodigosRespuesta EstadoConsulta(string code)
        //{
        //    switch (code)
        //    {
        //        // Cualquier codigo diferente a los relacionados estaran asociados a "Rechazo General"
        //        default: return CodigosRespuesta.Rechazo_General;
        //        case "SD":
        //        case "00": return CodigosRespuesta.Aprobado;
        //        case "01":
        //        case "02": return CodigosRespuesta.Llame_al_Emisor;
        //        case "03": return CodigosRespuesta.Marca_No_Habilitada;
        //        case "05":
        //        case "70":
        //        case "71":
        //        case "72":
        //        case "73": return CodigosRespuesta.Rechazo_General;
        //        case "12": return CodigosRespuesta.Transaccion_Invalida;
        //        case "13": return CodigosRespuesta.Monto_Invalido;
        //        case "14": return CodigosRespuesta.Tarjeta_Invalida;
        //        case "19": return CodigosRespuesta.Reinicie_Transaccion;
        //        case "25": return CodigosRespuesta.No_Existe_Comprobante;
        //        case "30": return CodigosRespuesta.Formato_Invalido;
        //        case "31": return CodigosRespuesta.Tarjeta_No_Soportada;
        //        case "41":
        //        case "43": return CodigosRespuesta.Retenga_Y_Llame;
        //        case "51": return CodigosRespuesta.Fondos_Insuficientes;
        //        case "54": return CodigosRespuesta.Tarjeta_Vencida;
        //        case "55": return CodigosRespuesta._Pin_invalido_si_es_chip;
        //        case "57": return CodigosRespuesta.Transac_no_Permitida;
        //        case "61": return CodigosRespuesta.Excede_Monto_Limite;
        //        case "62": return CodigosRespuesta.Tarj_Uso_Restringido;
        //        case "65": return CodigosRespuesta.Excede_Uso_Dia;
        //        case "66": return CodigosRespuesta.Bono_No_Habilitado;
        //        case "67": return CodigosRespuesta.Bono_Bloqueado;
        //        case "68": return CodigosRespuesta.Bono_Vencido;
        //        case "69": return CodigosRespuesta.Bono_Ya_Habilitado;
        //        case "74": return CodigosRespuesta.Cedula_Invalida;
        //        case "75": return CodigosRespuesta.Excede_Intentos_PIN;
        //        case "80": return CodigosRespuesta.Factura_No_Existe;
        //        case "81": return CodigosRespuesta.Factura_Ya_Pagada;
        //        case "82": return CodigosRespuesta.Factura_Vencida;
        //        case "83": return CodigosRespuesta.Servicio_No_Permitido;
        //        case "84": return CodigosRespuesta.Tarj_No_Autorizada;
        //        case "85": return CodigosRespuesta.Factura_No_Pagada;
        //        case "86": return CodigosRespuesta.Cuenta_Invalida;
        //        case "87": return CodigosRespuesta.Excede_Monto_Diario;
        //        case "92": return CodigosRespuesta.Cancelado_por_Usuario;

        //    }
        //}

        //public static InfoConvenio ObtenerInformacionConvenio(string BuscaConvenio)
        //{
        //    string value = File.ReadAllText(@"Xml\Convenios.Xml");
        //    XDocument doc = XDocument.Parse(value);
        //    XElement LosConvenios = doc.Element("Convenios");
        //    InfoConvenio[] Buscar = null;
        //    if (LosConvenios != null)
        //    {
        //        IEnumerable<XElement> words = LosConvenios.Elements("Convenio");
        //        Buscar = (from itm in words
        //                  where itm.Element("conv") != null
        //                      && itm.Element("conv").Value == BuscaConvenio
        //                      && itm.Element("EMPRESA") != null
        //                      && itm.Element("PLU") != null
        //                  select new InfoConvenio()
        //                  {
        //                      PLU = itm.Element("PLU").Value,
        //                      conv = itm.Element("conv").Value,
        //                      EMPRESA = itm.Element("EMPRESA").Value,
        //                  }).ToArray<InfoConvenio>();
        //    }

        //    if (Buscar != null)
        //    {
        //        foreach (InfoConvenio itm in Buscar)
        //        {
        //            Logger.Info($"InfoConvenio CodConvenio: {itm.conv}, EMPRESA: {itm.EMPRESA}, PLU: {itm.PLU}");
        //            return itm;
        //        }
        //    }
        //    return null;
        //}

    }
}
