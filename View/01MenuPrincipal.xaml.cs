using FacturasEnel.Logica;
using FacturasEnel.Util;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Printing;
using System.Management;
using System.Media;
using Enel.Logica;
using System.Collections.Generic;
using Enel.Modelo;
using Newtonsoft.Json;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public sealed partial class MenuPrincipal : Page
    {
        private int Menu = 1;
        private Contexto Contexto;
        private int Clics = 1;
        string token = null;
        public List<Sexo> Combosexo = new List<Sexo>();
        public List<Departamento> ComboDepartamento = new List<Departamento>();
        public List<Documento> ComboDocumento = new List<Documento>();


        //        private DispatcherTimer TimerClics = new DispatcherTimer();
        //private DispatcherTimer TimerConciliacion = new DispatcherTimer();

        public MenuPrincipal(Contexto contexto)
        {
            this.Contexto = contexto;
            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            
            EstablecerImagenes();
            CargarElementos();



            //          TimerClics.Tick += new EventHandler(TimerClics_Tick);
            //          TimerClics.Interval = TimeSpan.FromSeconds(Contexto.GetParams().Params.TimeOutInactividadBotonesOcultos);
        }

        
        public async void CargarElementos()
        {

            Api api = new Api();


            var tokenTomado = await api.RecibirToken();
            
            string resultado = tokenTomado.Substring(11);

            token = resultado.Substring(0,resultado.Length - 3);

            Contexto.Token = token;

            var SexoResultado = await api.ObtenerSexo(token);

            Dictionary<string, string> datosSexo = JsonConvert.DeserializeObject<Dictionary<string, string>>(SexoResultado);

            foreach (var kvp in datosSexo)
            {
                Combosexo.Add(new Sexo { IdSexo = kvp.Key, NombreSexo = kvp.Value });
            }

            Contexto.Combosexo = Combosexo;

            var DocumentoResultado = await api.ObtenerDocumentos();

            Dictionary<string, string> datosDocumento = JsonConvert.DeserializeObject<Dictionary<string, string>>(DocumentoResultado);

            foreach (var kvp in datosDocumento)
            {
                ComboDocumento.Add(new Documento { IdDocumento = kvp.Key, NombreDocumento = kvp.Value });
            }

            Contexto.ComboDocumento = ComboDocumento;




            var DepartamentoResultado = await api.ObtenerDepartamento(token);

            Dictionary<string, string> datosDepartamento = JsonConvert.DeserializeObject<Dictionary<string, string>>(DepartamentoResultado);

            foreach (var kvp in datosDepartamento)
            {
                ComboDepartamento.Add(new Departamento { IdDepartamento = kvp.Key, NombreDepartamento = kvp.Value });
            }

            Contexto.ComboDepartamento = ComboDepartamento;

        }



        private async void EstablecerImagenes()
        {
            try
            {

                /*  if (Contexto.MuestraFinaliza == true)
                  {
                      Utilitario.PintarImagen(Finaliza, 0, 0, 1024, 768, "Finaliza", false, true);
                      await Task.Delay(10000);
                      Contexto.MuestraFinaliza = false;
                      Finaliza.Visibility = Visibility.Hidden;

                  }*/

                if (Contexto.Altocontraste)
                {
                    Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                }
                else
                {
                    Background = Utilitario.ObtenerFondo($@"Util\al{Menu:00}.jpg");
                }

               

                Utilitario.PintarBoton(Menu, 1, Defensoria, 624, 171, 570, 142, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 2, Registro, 624, 341, 570, 142, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 3, Videollamada, 622, 508, 570, 142, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 4, AltoContraste, 500, 917, 177, 56, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 5, Zoom, 690, 916, 177, 56, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 6, Silencio, 877, 917, 177, 56, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 7, Ayuda, 1072, 917, 177, 56, Contexto.Altocontraste);
                //Utilitario.PintarBoton(Menu, 7, Solicita, 240, 653, 262, 71);
                //Utilitario.PintarBoton(Menu, 8, Simulacion, 505, 650, 220, 79);
                //Utilitario.PintarBoton(Menu, 9, Asistencia, 728, 654, 246, 70);

                Video.Source = new Uri(@"util\Banner.mp4", UriKind.RelativeOrAbsolute);
                Video.Width = 1280;
                Video.Height = 600;
                Video.Play();
                this.Video.Visibility = Visibility.Visible;
                Videoprincipal.Source = new Uri(@"util\Videoprincipal.mp4", UriKind.RelativeOrAbsolute);

                Video.Play();
                Videoprincipal.Play();
                this.Video.Visibility = Visibility.Visible;
                this.Videoprincipal.Visibility = Visibility.Visible;


                //                Canvas.SetTop(MiniCargando, 0);
                //                Canvas.SetLeft(MiniCargando, Width - 60);


            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (Contexto.ConsultaReal)
            {
                Contexto.InfoFactura = null;
                Contexto.RespuestaPago = null;
                Contexto.DatoConsulta = "";
                Contexto.NroCliente = "";
            }
            else
            {
            }
            Contexto.Cargado = true;
            Contexto.ErrorEntrega = false;
            Contexto.NumeroProducto = "";
            Contexto.NumeroCredito = "";
            Contexto.NumEstadoImpresora = 0;
            Contexto.NumerodeCreditos = 0;
            Contexto.Entered = 0;
            Contexto.AmountToPay = 0;
            Contexto.Cambio = 0;
            Contexto.Convenio = "";
            Contexto.CambioBill = 0;
            Contexto.CambioBillEntregado = 0;
            Contexto.CambioCoin = 0;
            Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.SinIniciar;
            Contexto.IdUltimaTransaccion = 0;
            Consumo.IdTransa = "0";
            Consumo.IdVCash = "0";
            Consumo.ValorPagar = "0";
            Consumo.Redondeo = "0";
            Contexto.VieneDesembolso = false;
            Contexto.DatoConsulta = "";
            Contexto.InfoFactura1 = "";
            Contexto.NombreCliente = "";
            Contexto.PagoSemanal = 0;
            Contexto.PagoSugerido = 0;
            Contexto.PagoLiquidar = 0;
            Contexto.ConsultaAbono = null;
            Contexto.respuestaImpresion = null;
            Contexto.retornoabono = null;
            Contexto.retornoabono1 = null;
            Contexto.RespuestaGeneraArchivo = null;
            Contexto.ValorTicket = "";
            Contexto.numeroTicket = "";
            Contexto.tieneCambio = false;
            Contexto.IMPCantidadIngresada = "";
            Contexto.IMPCambioEntregado = "";
            Contexto.IMPCambioFaltante = "";
            Contexto.numeroDocumento = "";
            Contexto.numeroPin = "";


            //            Contexto.VCash.CheckDispensablesLevels(Contexto.GetParams().logicaCantidades.ListaCashLogica, Contexto.GetParams().logicaCantidades.TopeMinDinero);
            //Contexto.VCash.CheckDispensablesLevels("1:5;2:3",200);


            if (Contexto.CargarDispositivos)
            {
//                Utilitario.PintarControl(LblCargaActual, 180, 400, 200, 330, 24, "R", Contexto.ConsultarCarga(), "B", false);
                Consumo.LoggerInfo("Carga Consultada en el LOAD");
            }

            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);
            Consumo.LoggerInfo("MENÚ PRINCIPAL CARGADO");

            try

            {
                Consumo.LoggerInfo("Valida si se tiene la base de para poder dar cambio o no");
            //    Contexto.tieneCambio = Contexto.VCash.ValidarBaseOperativa(Contexto.GetParams().logicaCantidades.TopeMinDinero); ////
            }

            catch (Exception ex)
            {
                Consumo.Logger.Error("Error validando la base");
            }


            //// Contexto.tieneCambio = true; //// ojo ojo 

        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
//            btnIniciar.IsEnabled = false;
            Image image = sender as Image;
            string MenuSig = image.Name.ToString();

            if (image.Tag.ToString() != "2")
            {
                MiniCargando.Visibility = Visibility.Visible;
                Storyboard story = (Storyboard)FindResource("EncogerImg");
                image.BeginStoryboard(story);
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));

            try
            {
                SoundPlayer simpleSound = new SoundPlayer(@"util\Click.wav");
                simpleSound.Play();
            }
            catch (Exception ex)
            {
                Consumo.LoggerInfo("No se puedo ejecutar el sonido");
            }
            switch (MenuSig)
            {


                case "Defensoria":
                    {

                        break;
                    }
                case "Registro":
                    {
                        NavigationService.Navigate(new Escaneo(Contexto));
                     //   NavigationService.Navigate(new RegistroRup(Contexto));
                        break;
                    }
                case "Videollamada":
                    {
                        //NavigationService.Navigate(new Consultas(Contexto));
                        break;
                    }
                case "AltoContraste":
                    {
                        if (Contexto.Altocontraste)
                        {
                            Contexto.Altocontraste = false;
                            NavigationService.Navigate(new MenuPrincipal(Contexto));
                        }
                        else
                        {
                            Contexto.Altocontraste = true;
                            NavigationService.Navigate(new MenuPrincipal(Contexto));
                        }
                      
                        break;
                    }
                case "Zoom":
                    {
                      

                        break;
                    }
                case "Silencio":
                    {
                       // NavigationService.Navigate(new Consultas(Contexto));
                        break;
                    }
                case "Ayuda":
                    {
                        //NavigationService.Navigate(new Consultas(Contexto));
                        break;
                    }

                default:
                    {
                        MessageBox.Show(image.Name, "NOMBRE BOTÓN");
                        break;
                    }
            }
        }


        private void MostrarErrores(string titulo, string mensaje)
        {
            Btn_AceptoErrores.Content = "Aceptar";
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            ModalErro.IsOpen = true;
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
            ModalErro.IsOpen = false;
        }

        private void Video_MediaEnded(object sender, RoutedEventArgs e)
        {
            Video.Position = new TimeSpan(0, 0, 1);
            Video.Play();
        }
        private void Videoprincipal_MediaEnded(object sender, RoutedEventArgs e)
        {
            Videoprincipal.Position = new TimeSpan(0, 0, 1);
            Videoprincipal.Play();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Video.Stop();

        }
    }
}
