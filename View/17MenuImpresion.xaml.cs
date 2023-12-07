using FacturasEnel.Util;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using VirtualCash.Business.Model;
using System.ComponentModel;
using FacturasEnel.Logica;
using System.Threading;
using System.IO;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using System.Net;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageMenuPago.xaml
    /// </summary>
    public sealed partial class PageMenuImpresion : Page
    {
        private int Menu = 17;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();
        //public byte[] StatusEvent = new byte[1];
        private int ContinuarEnPago = 0;
        private bool RecaudoFinalizado = false;
        private DispatcherTimer VaMenuPrincipal = new DispatcherTimer();
        private DispatcherTimer Timeout = new DispatcherTimer();

        public PageMenuImpresion(Contexto contexto)
        {
            Contexto = contexto;
            Height = Contexto.Alto;
            Width = Contexto.Ancho;

            InitializeComponent();
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;
            EstablecerImagenes();

            TimerHome.Tick += new EventHandler(TimerHome_TickAsync);
            TimerHome.Interval = TimeSpan.FromSeconds(3);

            VaMenuPrincipal.Tick += new EventHandler(VaMenuPrincipal_Tick);
            VaMenuPrincipal.Interval = TimeSpan.FromSeconds(2);
            //      VaMenuPrincipal.Start();

            Timeout.Tick += new EventHandler(TimerTimeout_Tick);
            Timeout.Interval = TimeSpan.FromSeconds(Contexto.GetParams().TimeOutInactividadPantallas);
            Timeout.Start();
        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                //Utilitario.PintarBoton(Menu, 1, btnImprimir, 201, 590, 297, 131);
                //Utilitario.PintarBoton(Menu, 2, btnFinalizar, 527, 589, 297, 131);
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
//            btnCancelar.IsEnabled = false;
            TimerHome.Stop();
            Timeout.Stop();

            Storyboard story = (Storyboard)FindResource("EncogerImg");
            Image image = (Image)sender;
            image.BeginStoryboard(story);

            await Task.Delay(TimeSpan.FromMilliseconds(300));

            string MenuSig = image.Tag.ToString();
            switch (MenuSig)
            {
                case "Imprimir":
                    {
                        Timeout.Stop();
                        TimerHome.Stop();
                        VaMenuPrincipal.Stop();

                        //////solo pruebas
                      //  Thread.Sleep(3000);
                      //  VaMenuPrincipal.Start();
                        ////

                       Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.Aprobada;  // ya

                             try
                             {
                                 btnImprimir.Visibility = Visibility.Hidden;
                                 btnFinalizar.Visibility = Visibility.Hidden;
                                 Utilitario.PintarImagen(Imprime, 0, 0, 1024, 768, "Imprime", false, true);
                                 await Task.Delay(1000);

                                 Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.Aprobada;  // ya

                                 var url = $"http://localhost:9001/WSDesTecAppsLocal/ImprimirTickets?idticket=" + Contexto.numeroTicket;
                                 var request = (HttpWebRequest)WebRequest.Create(url);
                                 request.Method = "POST";
                                 request.ContentType = "application/json; charset=utf-8";
                                 request.Accept = "application/json";
                                 request.ContentLength = 0;
                                 using (WebResponse response = request.GetResponse())
                                 {
                                     using (Stream strReader = response.GetResponseStream())
                                     {
                                         if (strReader == null) return;
                                         using (StreamReader objReader = new StreamReader(strReader))
                                         {
                                             string responseBody = objReader.ReadToEnd();
                                             // Do something with responseBody
                                             Contexto.RespuestaGeneraArchivo = JsonConvert.DeserializeObject(responseBody.ToString());
                                         }
                                     }
                                 }

                                 if (Contexto.RespuestaGeneraArchivo.EstatusExito == "true")
                                 {

                                     //  MessageBox.Show("genero archivo");
                                     Thread.Sleep(3000);
                                     VaMenuPrincipal.Start();
                                     /// aqui debe imprimir
                                 }
                                 else

                                 {
                                     Thread.Sleep(4000);
                                     TimerHome.Start();

                                 }
                             }

                             catch (Exception ex)
                             {
                                 VaMenuPrincipal.Stop();
                                 TimerHome.Stop();
                                 Consumo.Logger.Error(ex, "Servicio web de impresión método ImprimirTickets no disponible error: " + ex.Message.ToString());
                                 Utilitario.PintarImagen(IError, 0, 0, 1024, 768, "MN302", false, true);
                                 await Task.Delay(5000);
                                 NavigationService.Navigate(new MenuPrincipal(Contexto));
                             } 
                             
                    }
                    break;
                case "Finalizar":
                    {
                        Timeout.Stop();

                        VaMenuPrincipal.Stop();
                        TimerHome.Stop();
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(200);
                        NavigationService.Navigate(new MenuEncuesta(Contexto));
                    }
                    break;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);

            Consumo.InsertarEstadisticaServer("Menu Recaudo", $"Inciando recaudo {Consumo.ValorPagar}", "OK", Consumo.Redondeo);

            //Contexto.AmountToPay = Contexto.AproximarMontoOperacion(Contexto.InfoFactura.ValorPagar);

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            Timeout.Stop();
            TimerHome.Stop();
            VaMenuPrincipal.Stop();
        }

        private void VaMenuPrincipal_Tick(object sender, EventArgs e)
        {
            Timeout.Stop();
            TimerHome.Stop();
            VaMenuPrincipal.Stop();
            Contexto.MenuAnt = "PageMenuImpresion";
            NavigationService.Navigate(new MenuEncuesta(Contexto));

        }

        private async void TimerHome_TickAsync(object sender, EventArgs e)
        {
            VaMenuPrincipal.Stop();
            TimerHome.Stop();
            Utilitario.PintarImagen(IError, 0, 0, 1024, 768, "MN300", false, true);
            await Task.Delay(3000);

            NavigationService.Navigate(new MenuPrincipal(Contexto));


        }

        private async void TimerTimeout_Tick(object sender, EventArgs e)
        {
            Timeout.Stop();
            Contexto.MenuAnt = "PageMenuImpresion";
            NavigationService.Navigate(new PageTimeOUT(Contexto));

        }

        private void MostrarMensaje(string titulo, string mensaje)
        {
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            Mensajes.IsOpen = true;
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
            if (ContinuarEnPago == 1)
            {
                Consumo.LoggerInfo($"USUARIO CONFIRMA SEGUIR EN EL PAGO");
                ContinuarEnPago = 0;
            }

            if (Contexto.EstadoTransaccionActual == Contexto.EstadoTransaccion.RecaudoCompleto)
            {
                TimerHome.Stop();
                ContinuarEnPago = 2;
                //btnCancelar.IsEnabled = false;
            }
            else
            {
                //btnCancelar.IsEnabled = true;
            }
        }

        private void LblCambio_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Label lblCambio = sender as Label;

            Dispatcher.Invoke(async () =>
            {
                if (string.IsNullOrEmpty(lblCambio.Content.ToString()))
                    return;

                if (lblCambio.Content.ToString() != 0.ToString("C", Consumo.InfoPais))
                {
                    await Task.Run(() => Task.Delay(TimeSpan.FromSeconds(3)));
//                    EntregandoCambio.Visibility = Visibility.Visible;
                }
            });
        }

        private void ImprimirClick(object sender, RoutedEventArgs e)
        {
            TimerHome.Stop();
            Button button = (Button)sender;

            string MenuSig = button.Tag.ToString();
            switch (MenuSig)
            {
                case "Imprimir":
                    {
                        Contexto.Entered = Contexto.InfoFactura.ValorPagar + 10000;
                        Contexto.Cambio = Convert.ToInt32(Contexto.Entered - Contexto.InfoFactura.ValorPagar);
                        Contexto.RespuestaPago = new BusinessEnel.PayResultClass() { Autorizacion = "79997641", Resultado = "0", Mensaje = "" };
                        Contexto.ImprimirRecibo(true);
                    }
                    break;
                case "Pagar":
                    {
                        Contexto.Entered = Contexto.InfoFactura.ValorPagar + 10000;
                        Contexto.Cambio = Convert.ToInt32(Contexto.Entered - Contexto.InfoFactura.ValorPagar);
                        Contexto.RespuestaPago = new BusinessEnel.PayResultClass() { Autorizacion = "79997641", Resultado = "0", Mensaje = "" };
                       
                    }
                    break;
            }
        }

        /// <summary>
        /// ///////////////////////////// no se usan 
        /// </summary>

        //private void VirtualCash_SS75BCRDisposed(object sender, EventArgs e)
        //{
        //    Consumo.LoggerInfo($"Se detecta deshabilitación de monedero");
        //    //if (this.FinalizacionTransaccion == EstadoTransaccion.TransaccionFallida)
        //    //{
        //    //    this.MonederoLibre = true;
        //    //    Consumo.LoggerInfo("Esperando por el monedero.");
        //    //}
        //}
    }
}
