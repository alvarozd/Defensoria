using Microsoft.EntityFrameworkCore;
using FacturasEnel.Logica;
using FacturasEnel.Util;
using NLog;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using VirtualCash.Business;
using VirtualCash.Business.Services.Devices;
using System.Linq;
using ServiceReference;
using Renci.SshNet;
using System.IO;
using System.Collections.Generic;
using System.Net;
using Renci.SshNet.Sftp;
using System.Threading;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageCargando.xaml
    /// </summary>
    public sealed partial class PageConsultandoPago : Page
    {


        /// <Animación >
        private List<BitmapImage> Images = new List<BitmapImage>();
        private int ImageNumber = 0;
        private DispatcherTimer TimerAnima = new DispatcherTimer();
        /// </Animación>
        private int Menu = 200;
        private readonly Contexto Contexto;
        //public event EventHandler VCashReady;
        private DispatcherTimer TimerInicializar = new DispatcherTimer();
        String ValorTicket = "";
        String numeroTicket = "";

        public PageConsultandoPago(Contexto contexto)
        {
            TimerAnima.Tick += new EventHandler(TimerAnima_TickAsync);
            TimerAnima.Interval = TimeSpan.FromSeconds(1);
            Height = contexto.Alto;
            Width = contexto.Ancho;
            this.Contexto = contexto;
            InitializeComponent();
            EstablecerImagenes();

            TimerInicializar.Tick += new EventHandler(TimerInicializar_TickAsync);
            TimerInicializar.Interval = TimeSpan.FromSeconds(3);
        }

        private void EstablecerImagenes()
        {
            // anima
            DirectoryInfo dir_info = new DirectoryInfo(Environment.CurrentDirectory + @"\util\anima\");
            foreach (FileInfo file_info in dir_info.GetFiles())
            {
                if ((file_info.Extension.ToLower() == ".jpg") ||
                    (file_info.Extension.ToLower() == ".png"))
                {
                    Images.Add(new BitmapImage(new Uri(file_info.FullName)));
                }
            }

            // Display the first image.
            imgPicture.Source = Images[0];
            /// fin anima
            try
            {
                Background = Utilitario.ObtenerFondo(@"Util\MN200.jpg");
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TimerAnima.Start();

            await Task.Delay(1500);
            ConsumeServicio();



        }

        private async void TimerAnima_TickAsync(object sender, EventArgs e)
        {

            ImageNumber = (ImageNumber + 1) % Images.Count;
            ShowNextImage(imgPicture);


        }

        private async void ShowNextImage(System.Windows.Controls.Image img)
        {
            const double transition_time = 0.5;
            Storyboard sb = new Storyboard();

            // ***************************
            // Animate Opacity 1.0 --> 0.0
            // ***************************
            DoubleAnimation fade_out = new DoubleAnimation(1.0, 0.0,
                TimeSpan.FromSeconds(transition_time));
            fade_out.BeginTime = TimeSpan.FromSeconds(0);

            // Use the Storyboard to set the target property.
            Storyboard.SetTarget(fade_out, img);
            Storyboard.SetTargetProperty(fade_out,
                new PropertyPath(System.Windows.Controls.Image.OpacityProperty));

            // Add the animation to the StoryBoard.
            sb.Children.Add(fade_out);


            // *********************************
            // Animate displaying the new image.
            // *********************************
            ObjectAnimationUsingKeyFrames new_image_animation =
                new ObjectAnimationUsingKeyFrames();
            // Start after the first animation has finisheed.
            new_image_animation.BeginTime = TimeSpan.FromSeconds(transition_time);

            // Add a key frame to the animation.
            // It should be at time 0 after the animation begins.
            DiscreteObjectKeyFrame new_image_frame =
                new DiscreteObjectKeyFrame(Images[ImageNumber], TimeSpan.Zero);
            new_image_animation.KeyFrames.Add(new_image_frame);

            // Use the Storyboard to set the target property.
            Storyboard.SetTarget(new_image_animation, img);
            Storyboard.SetTargetProperty(new_image_animation,
                new PropertyPath(System.Windows.Controls.Image.SourceProperty));

            // Add the animation to the StoryBoard.
            sb.Children.Add(new_image_animation);


            // ***************************
            // Animate Opacity 0.0 --> 1.0
            // ***************************
            // Start when the first animation ends.
            DoubleAnimation fade_in = new DoubleAnimation(0.0, 1.0,
                TimeSpan.FromSeconds(transition_time));
            fade_in.BeginTime = TimeSpan.FromSeconds(transition_time);

            // Use the Storyboard to set the target property.
            Storyboard.SetTarget(fade_in, img);
            Storyboard.SetTargetProperty(fade_in,
                new PropertyPath(System.Windows.Controls.Image.OpacityProperty));

            // Add the animation to the StoryBoard.
            sb.Children.Add(fade_in);

            // Start the storyboard on the img control.
            sb.Begin(img);
        }

        private async void TimerInicializar_TickAsync(object sender, EventArgs e)
        {
            TimerAnima.Stop();
            TimerInicializar.Stop();

            Utilitario.PintarImagen(IError, 0, 0, 1024, 768, "MN301", false, true);
            await Task.Delay(10000);

            NavigationService.Navigate(new MenuPrincipal(Contexto));

        }

        private async void ConsumeServicio()
        {
            ///// solo para pruebas quitar

            ////TimerInicializar.Stop();
            ////TimerAnima.Stop();
            ///
            await Task.Delay(5000);
            NavigationService.Navigate(new PagoExitoso(Contexto));
            /////
           
            
            Consumo.Logger.Info("Valor enviado al abono " + Contexto.AmountToPay);

        /*    try
            {
                  Contexto.ConsultaAbono.montoAbono = Convert.ToInt32(Contexto.AmountToPay);
                  //            Contexto.ConsultaAbono.montoAbono = 193;

                  String JsonE = JsonConvert.SerializeObject(Contexto.ConsultaAbono);
                  var url1 = $"http://" + Contexto.IpAbono + ":9014/CajaMovil/Servicios/CajaMovil/wsCajaMovil.svc/wsProrrateoAbonoPM";
                  var request1 = (HttpWebRequest)WebRequest.Create(url1);
                  request1.Method = "POST";
                  request1.ContentType = "application/json; charset=utf-8";
                  request1.Accept = "application/json";
                  using (var streamWriter1 = new StreamWriter(request1.GetRequestStream()))
                  {
                      streamWriter1.Write(JsonE);
                      streamWriter1.Flush();
                      streamWriter1.Close();
                  }


                  using (WebResponse response1 = request1.GetResponse())
                  {
                      using (Stream strReader1 = response1.GetResponseStream())
                      {
                          if (strReader1 == null) return;
                          using (StreamReader objReader1 = new StreamReader(strReader1))
                          {
                              string responseBody1 = objReader1.ReadToEnd();
                              Contexto.retornoabono = JsonConvert.DeserializeObject(responseBody1.ToString());
                          }
                      }
                  }
                  if (Contexto.retornoabono.msjErr == "Generacion de prorrateo exitoso")
                  {
                      GeneraAbono();
                  }
                  else
                  {
                      Thread.Sleep(4000);
                      TimerInicializar.IsEnabled = true;

                  }

              }
              catch (Exception ex)
              {

                TimerInicializar.Stop();
                TimerAnima.Stop();
                Consumo.Logger.Error(ex, "Servicio web de abono métodowsProrrateoAbonoPM no disponible error: " + ex.Message.ToString());
                if (Contexto.ErrorEntrega == false)
                    Contexto.ImprimirRecibo(true);
                else
                    Contexto.ErrorEntrega = false;
                if (Contexto.VCash.AbonoTotal == false)
                {
                    Contexto.IMPCantidadIngresada = Contexto.Entered.ToString("C", Consumo.InfoPais);
                    Contexto.IMPCambioEntregado = Contexto.Cambio.ToString("C", Consumo.InfoPais);
                    Contexto.IMPCambioFaltante = 0.ToString("C", Consumo.InfoPais); ;

                    Contexto.ImprimirRecibo(true);


                }


                Utilitario.PintarImagen(IError, 0, 0, 1024, 768, "MN301", false, true);
                await Task.Delay(4000);
                NavigationService.Navigate(new MenuPrincipal(Contexto));
              } */
        }

        private async void GeneraAbono()
        {

            try
            {
                String JsonE = JsonConvert.SerializeObject(Contexto.retornoabono);
                var url1 = $"http://" + Contexto.IpAbono + ":9014/CajaMovil/Servicios/CajaMovil/wsCajaMovil.svc/wsAfectaAbonoPM";
                var request1 = (HttpWebRequest)WebRequest.Create(url1);
                request1.Method = "POST";
                request1.ContentType = "application/json; charset=utf-8";
                request1.Accept = "application/json";
                using (var streamWriter1 = new StreamWriter(request1.GetRequestStream()))
                {
                    streamWriter1.Write(JsonE);
                    streamWriter1.Flush();
                    streamWriter1.Close();
                }


                using (WebResponse response1 = request1.GetResponse())
                {
                    using (Stream strReader1 = response1.GetResponseStream())
                    {
                        if (strReader1 == null) return;
                        using (StreamReader objReader1 = new StreamReader(strReader1))
                        {
                            string responseBody1 = objReader1.ReadToEnd();
                            Contexto.retornoabono1 = JsonConvert.DeserializeObject(responseBody1.ToString());

                        }
                    }
                }

                if (Contexto.retornoabono1.msjErr == "OK")
                {

                    Contexto.ValorTicket = Contexto.retornoabono1.TicketXML;
                    GeneraTicket();
                }
                else
                {
                    Thread.Sleep(4000);
                    TimerInicializar.IsEnabled = true;
                }

            }
            catch (Exception ex)
            {
                TimerInicializar.Stop();
                TimerAnima.Stop();
                Consumo.Logger.Error(ex, "Servicio web de abono wsAfectaAbonoPM no disponible error: " + ex.Message.ToString());
                if (Contexto.ErrorEntrega == false)
                    Contexto.ImprimirRecibo(true);
                else
                    Contexto.ErrorEntrega = false;

                if (Contexto.VCash.AbonoTotal == false)
                {
                    Contexto.IMPCantidadIngresada = Contexto.Entered.ToString("C", Consumo.InfoPais);
                    Contexto.IMPCambioEntregado = Contexto.Cambio.ToString("C", Consumo.InfoPais);
                    Contexto.IMPCambioFaltante = 0.ToString("C", Consumo.InfoPais); ;

                    Contexto.ImprimirRecibo(true);


                }


                Utilitario.PintarImagen(IError, 0, 0, 1024, 768, "MN301", false, true);
                await Task.Delay(5000);
                NavigationService.Navigate(new MenuPrincipal(Contexto));
            }
        }

        public class Impresion
        {
            public List<JObject> ListaTickets = new List<JObject>();
        }

        private async void GeneraTicket()
        {
            try
            {
                var m = new Impresion();
                m.ListaTickets.Add(new JObject());
                m.ListaTickets[0]["Aplicacion"] = "AB-ABONO";
                m.ListaTickets[0]["Contenido"] = Contexto.ValorTicket;
                m.ListaTickets[0]["NoCopias"] = "1";

                var json = JsonConvert.SerializeObject(m);
                var url = $"http://localhost:9001/WSDesTecAppsLocal/GenerarTickets";
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                request.Accept = "application/json";
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            Contexto.respuestaImpresion = JsonConvert.DeserializeObject(responseBody.ToString());
                            Contexto.numeroTicket = Contexto.respuestaImpresion.Contenido;
                        }
                    }
                }
                if (numeroTicket != "")
                {
                    MessageBox.Show(Contexto.respuestaImpresion.Detalle.ToString());
                }
                else
                {
                    TimerInicializar.Stop();
                    TimerAnima.Stop();
                    if (Contexto.NumEstadoImpresora != 0)
                    {
                        NavigationService.Navigate(new MenuEncuesta(Contexto));

                    }
                    else 
                    {
                        NavigationService.Navigate(new PageMenuImpresion(Contexto));

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }


    }
}
