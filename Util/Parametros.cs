using VPSservices;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Data;
using FacturasEnel.Logica;
using System.Configuration;

namespace FacturasEnel.Util
{
    public sealed class Parametros : IDisposable
    {
        public class InfoFileSFTP
        {
            public string VCODUNICOINT { get; set; }
            public string VMONEDA { get; set; }
            public string VIDENTIFICACIONNEGOCIO { get; set; }
            public string VTIPOTRANSACCIONSERVICIOPUBLICO { get; set; }
            public string VTIPOTRANSACCIONPILA { get; set; }
            public string VTIPOMEDIOPAGO { get; set; }
            public string VIDBANCO { get; set; }
            public string VIDCONVENIO { get; set; }
        }
        public InfoFileSFTP infoFileSFTP;

        public class LogicaCantidades
        {
            /// <summary>
            /// MÁXIMO TOPE DE DINERO EN EL MONEDERO
            /// </summary>
            public int TopeMaxMonedero { get; set ;}
            /// <summary>
            /// MÍNIMO TOPE DE DINERO EN EL MONEDERO
            /// </summary>
            public int TopeMinMonedero { get; set; }
            /// <summary>
            /// MÁXIMO TOPE DE DINERO EN EL CASHBOX - STACKER
            /// </summary>
            public int TopeMaxCashBox { get; set; }
            /// <summary>
            /// -1 MÍNIMO TOPE DE DINERO EN EL CASHBOX
            /// </summary>
            public int TopeMinCashBox { get; set; }
            /// <summary>
            /// SUMA DE DINERO EN LOS DISPOSITIVOS
            /// </summary>
            public int TopeMaxDinero { get; set; }
            /// <summary>
            /// -1 MÍNIMO TOPE DE DINERO
            /// </summary>
            public int TopeMinDinero { get; set; }
            public string ListaCashLogica { get; set; }
            public bool ValidarListaCashLogica { get; set; }
        }
        public LogicaCantidades logicaCantidades;

        public class InfoSFTP
        {
            public string Server { get; set; }
            public string User { get; set; }
            public string Pass { get; set; }
            public int Port { get; set; }
            public string Folder { get; set; }
        }
        public InfoSFTP infoSFTP;

        //variables para parametros impresión
        public static string EndPointVps = ConfigurationManager.AppSettings.Get("WS_VPS");
        public static string IdKiosco = ConfigurationManager.AppSettings.Get("IdKiosco");
        public string NombreKiosco { get; set; }
        public string AlmacenKiosco { get; set; }
        public string DireccionAlmacen { get; set; }
        //public string[][] AudioMenus;


        private VPSservices.ws_vps.ServiceSoapClient WsImp;
        private readonly XmlUtil Xml = new XmlUtil();
        public DataSet DSparams;

        public VPSservices.ws_vps.Parametros Params { get; set; }

        public int TimeOutInactividadPantallas { get; set; }
        private RegistroVirtual ParametrosVps;

        public async Task ConsultaParametrosVps()
        {
        }

        public void LeerParametros()
        {
            XmlSerializer xs = new XmlSerializer(typeof(VPSservices.ws_vps.Parametros));
            using (var sr = new StreamReader(@"XML\parametros.Xml"))
            {
                Params = (VPSservices.ws_vps.Parametros)xs.Deserialize(sr);
            }
            this.TimeOutInactividadPantallas = Params.TimeOutInactividadPantallas;
        }

        public async Task<bool> DescargarParametros()
        {
            try
            {
                ParametrosVps = new RegistroVirtual(Consumo.IdKiosco, Consumo.IdAplicacion, Consumo.EndPointVps);
                
                var res = await ParametrosVps.ObtenerParametrosOBJAsync();

                Consumo.LoggerInfo("Parámetros descargados correctamente");
                return res;
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "DescargarParametros");
                return false;
            }
        }

        public async Task DescargarInfoKiosco()
        {
            try
            {
                NombreKiosco = "Comultrasan";

            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "DescargarInfoKiosco");
            }
        }

        //public async Task<bool> DescargarConvenios()
        //{
        //    VPSservices.ws_vps.ServiceSoapClient ws = new VPSservices.ws_vps.ServiceSoapClient();

        //    try
        //    {
        //        DataSet dtConvenios = await ws.getConveniosAsync();

        //        dtConvenios = ws.getConvenios();
        //        dtConvenios.DataSetName = "Convenios";
        //        dtConvenios.Tables[0].TableName = "Convenio";
        //        dtConvenios.WriteXml(@"Xml\Convenios.Xml");
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Consumo.Logger.Error($"DescargarConvenios() {ex.ToString()}");
        //        return false;
        //    }
        //}

        public void DescargarParametrosFacturas()
        {
            DataSet ParametrosImpre = new DataSet();
            try
            {
                ParametrosImpre = WsImp.getParametros("1");
                this.Xml.CreaXml("ParametrosImpre");
                if (ParametrosImpre.Tables[0].Rows.Count > 0)
                {
                    this.Xml.CreaHijo("Parametros");
                    for (int i = 0; i < ParametrosImpre.Tables[0].Rows.Count; i++)
                    {
                        this.Xml.CreaNieto("Parametro");
                        for (int j = 0; j < ParametrosImpre.Tables[0].Columns.Count; j++)
                        {
                            this.Xml.CreaAtributosNieto(ParametrosImpre.Tables[0].Columns[j].ToString(), ParametrosImpre.Tables[0].Rows[i][ParametrosImpre.Tables[0].Columns[j].ToString()].ToString());
                            // TODO LECTURA Y CARGA DE AUDIOS
                            if (ParametrosImpre.Tables[0].Columns[j].ToString() == "parametro")
                            {
                                if (ParametrosImpre.Tables[0].Rows[i][ParametrosImpre.Tables[0].Columns[j].ToString()].ToString().Contains("AudioMenu"))
                                {
                                    int Menu = Convert.ToInt32(ParametrosImpre.Tables[0].Rows[i][ParametrosImpre.Tables[0].Columns[j].ToString()].ToString().Replace("AudioMenu", ""), Consumo.InfoPais);
                                    string Texto = ParametrosImpre.Tables[0].Rows[i][ParametrosImpre.Tables[0].Columns[j+1].ToString()].ToString();
                                    Utilitario.GetAudioMenus().Add(Menu, Texto);
                                }
                            }
                            if (ParametrosImpre.Tables[0].Rows[i][ParametrosImpre.Tables[0].Columns[j].ToString()].ToString().Equals("CodigoIAC"))
                            {
                                Consumo.CodigoIacFactuador = ParametrosImpre.Tables[0].Rows[i][ParametrosImpre.Tables[0].Columns[j + 1].ToString()].ToString();
                            }
                        }
                    }
                    this.Xml.AñadeHijo();
                }
                this.Xml.GuardaXml(@"Xml\\ParametrosImpre.Xml");
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error($"DescargarParametrosFacturas() Error creando archivo ParametrosImpre.Xml {ex.ToString()}"); 
            }
            finally
            {
                ParametrosImpre.Dispose();
            }

        }

        public void LeerArchivosSftp()
        {
            infoSFTP = new InfoSFTP();
            DataSet inf = new DataSet();
            try
            {
                inf = WsImp.getParametros("1");

                if (inf.Tables.Count > 0)
                {
                    for (int j = 0; j < inf.Tables.Count; j++)
                    {
                        if (inf.Tables[j].Rows.Count > 0)
                        {
                            for (int i = 0; i < inf.Tables[j].Rows.Count; i++)
                            {
                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "FTPSERVER")
                                    infoSFTP.Server = inf.Tables[j].Rows[i]["valor"].ToString();

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "FTPUSER")
                                    infoSFTP.User = inf.Tables[j].Rows[i]["valor"].ToString();

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "FTPPASS")
                                    infoSFTP.Pass = inf.Tables[j].Rows[i]["valor"].ToString();

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "FTPFOLDER")
                                    infoSFTP.Folder = inf.Tables[j].Rows[i]["valor"].ToString();

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "FTPPORT")
                                    infoSFTP.Port = Convert.ToInt32(inf.Tables[j].Rows[i]["valor"].ToString(), Consumo.InfoPais);

                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "LeerParametrosLogicaVCash");
            }
            finally
            {
                inf.Dispose();
            }

        }

        public void LeerParametrosLogicaVCash()
        {
            logicaCantidades = new LogicaCantidades();
            DataSet inf = new DataSet();
            try
            {
                inf = WsImp.getParametros("1");

                if (inf.Tables.Count > 0)
                {
                    for (int j = 0; j < inf.Tables.Count; j++)
                    {
                        if (inf.Tables[j].Rows.Count > 0)
                        {
                            for (int i = 0; i < inf.Tables[j].Rows.Count; i++)
                            {
                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "TOPEMAXMONEDERO")
                                    logicaCantidades.TopeMaxMonedero = Convert.ToInt32(inf.Tables[j].Rows[i]["valor"].ToString(), Consumo.InfoPais);

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "TOPEMINMONEDERO")
                                    logicaCantidades.TopeMinMonedero = Convert.ToInt32(inf.Tables[j].Rows[i]["valor"].ToString(), Consumo.InfoPais);

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "TOPEMAXCASHBOX")
                                    logicaCantidades.TopeMaxCashBox = Convert.ToInt32(inf.Tables[j].Rows[i]["valor"].ToString(), Consumo.InfoPais);

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "TOPEMINCASHBOX")
                                    logicaCantidades.TopeMinCashBox = Convert.ToInt32(inf.Tables[j].Rows[i]["valor"].ToString(), Consumo.InfoPais);

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "TOPEMAXDINERO")
                                    logicaCantidades.TopeMaxDinero = Convert.ToInt32(inf.Tables[j].Rows[i]["valor"].ToString(), Consumo.InfoPais);

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "TOPEMINDINERO")
                                    logicaCantidades.TopeMinDinero = Convert.ToInt32(inf.Tables[j].Rows[i]["valor"].ToString(), Consumo.InfoPais);

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "LISTACASHLOGICA")
                                    logicaCantidades.ListaCashLogica = inf.Tables[j].Rows[i]["valor"].ToString();

                                if (inf.Tables[j].Rows[i]["parametro"].ToString().ToUpper(Consumo.InfoPais) == "VALIDARLISTACASHLOGICA")
                                    logicaCantidades.ValidarListaCashLogica = Convert.ToBoolean(inf.Tables[j].Rows[i]["valor"].ToString(), Consumo.InfoPais);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "LeerParametrosLogicaVCash");
            }
            finally
            {
                inf.Dispose();
            }
        }

        public void LeerParametrosSftp()
        {
            infoFileSFTP = new InfoFileSFTP();
            DataSet cargaparametros = new DataSet();
            try
            {
                Utilitario.CargarXmltoDataSet(@"Xml\\ParametrosImpre.Xml", ref cargaparametros);
                if (cargaparametros.Tables.Count > 0)
                {
                    for (int j = 0; j < cargaparametros.Tables.Count; j++)
                    {
                        if (cargaparametros.Tables[j].TableName.ToUpper() == "PARAMETRO")
                        {
                            if (cargaparametros.Tables[j].Rows.Count > 0)
                            {
                                for (int i = 0; i < cargaparametros.Tables[j].Rows.Count; i++)
                                {
                                    if (cargaparametros.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "CODUNICOINT")
                                        infoFileSFTP.VCODUNICOINT = cargaparametros.Tables[j].Rows[i]["valor"].ToString();
                                    if (cargaparametros.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "MONEDA")
                                        infoFileSFTP.VMONEDA = cargaparametros.Tables[j].Rows[i]["valor"].ToString();
                                    if (cargaparametros.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "IDENTIFICACIONNEGOCIO")
                                        infoFileSFTP.VIDENTIFICACIONNEGOCIO = cargaparametros.Tables[j].Rows[i]["valor"].ToString();
                                    if (cargaparametros.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TIPOTRANSACCIONSERVICIOPUBLICO")
                                        infoFileSFTP.VTIPOTRANSACCIONSERVICIOPUBLICO = cargaparametros.Tables[j].Rows[i]["valor"].ToString();
                                    if (cargaparametros.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TIPOTRANSACCIONPILA")
                                        infoFileSFTP.VTIPOTRANSACCIONPILA = cargaparametros.Tables[j].Rows[i]["valor"].ToString();
                                    if (cargaparametros.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "TIPOMEDIOPAGO")
                                        infoFileSFTP.VTIPOMEDIOPAGO = cargaparametros.Tables[j].Rows[i]["valor"].ToString();
                                    if (cargaparametros.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "IDBANCO")
                                        infoFileSFTP.VIDBANCO = cargaparametros.Tables[j].Rows[i]["valor"].ToString();
                                    if (cargaparametros.Tables[j].Rows[i]["parametro"].ToString().ToUpper() == "IDCONVENIO")
                                        infoFileSFTP.VIDCONVENIO = cargaparametros.Tables[j].Rows[i]["valor"].ToString();
                                }
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "LeerParametrosSftp");
            }
            finally
            {
                cargaparametros.Dispose();
            }

        }

        public static void CrearArchivoConciliacion()
        {
            string nombreArchivo = $"Transacciones{DateTime.Now.ToString("yyyyMMdd", Consumo.InfoPais)}.txt";

            string RutaArchivo = @"Conciliacion\" + nombreArchivo;
            string Informacion = null;

            if (File.Exists(RutaArchivo))
                return;

            try
            {
                using (StreamWriter writer = new StreamWriter(RutaArchivo, true))
                {
                    writer.Write(Informacion);
                }
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex , $"CrearArchivoConciliacion archivo: {nombreArchivo}");
            }
        }

        void IDisposable.Dispose()
        {
            DSparams.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
