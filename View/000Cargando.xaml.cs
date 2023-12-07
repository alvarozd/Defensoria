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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageCargando.xaml
    /// </summary>
    public sealed partial class PageCargando : Page
    {

        /// <Animación >
        private List<BitmapImage> Images = new List<BitmapImage>();
        private int ImageNumber = 0;
        private DispatcherTimer TimerAnima = new DispatcherTimer();
        /// </Animación>


        private int Menu = 0;
        private readonly Contexto Contexto;
        //public event EventHandler VCashReady;
        private DispatcherTimer TimerInicializar = new DispatcherTimer();

        int anima = 1;

        public PageCargando(Contexto contexto)
        {

            Height = contexto.Alto;
            Width = contexto.Ancho;
            this.Contexto = contexto;
            InitializeComponent();
            EstablecerImagenes();

            TimerAnima.Tick += new EventHandler(TimerAnima_TickAsync);
            TimerAnima.Interval = TimeSpan.FromSeconds(1);

            TimerInicializar.Tick += new EventHandler(TimerInicializar_TickAsync);
            TimerInicializar.Interval = TimeSpan.FromSeconds(6);

        }

        private void EstablecerImagenes()
        {
            // anima
            /**DirectoryInfo dir_info = new DirectoryInfo(Environment.CurrentDirectory + @"\util\anima\");
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
//            TimerAnima.Start();
            */

            // anima





            try
            {
                Background = Utilitario.ObtenerFondo(@"Util\Cargando.jpg");
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);

            TimerInicializar.Start();
        }

        private async void TimerAnima_TickAsync(object sender, EventArgs e)
        {

            ImageNumber = (ImageNumber + 1) % Images.Count;
            ShowNextImage(imgPicture);


        }

        private async void ShowNextImage(System.Windows.Controls.Image img)
        {
            const double transition_time = 0.9;
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
            TimerInicializar.Stop();
            try
            {
                if (Contexto.PagoReal)
                {
                    Consumo.LoggerInfo($" + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + +");
                    Consumo.LoggerInfo($"MODO RECAUDO REAL");
                    Consumo.LoggerInfo($"+ + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + + +");
                    await Task.Run(() => Contexto.InicializarDispositivosPago());
                    Cargando_VCashReady(null, null);
                    /*                    Contexto.VCash.VcashReady += Cargando_VCashReady;
                                        Contexto.VCash.VcashError += Cargando_VcashError;
                                        Contexto.VCash.ServiceError += Cargando_ServiceError;*/
                }
                else
                {
                    Consumo.LoggerInfo($"- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                    Consumo.LoggerInfo($"MODO RECAUDO PRUEBAS");
                    Consumo.LoggerInfo($"- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -");
                    Cargando_VCashReady(null, null);
                }
                //await Task.Run(() => Contexto.InicializarDispositivosPago());
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, $"Cargando Page_Loaded");
                Dispatcher.Invoke(() =>
                {
                    NavigationService.Navigate(new PageFueradeLinea(Contexto, Title, ex.Message, false));
                });
            }
        }

        private void Cargando_VCashReady(object sender, EventArgs e)
        {
            Consumo.LoggerInfo($"Cargando VCash Ready - Virtual Cash Inicializado");
            Contexto.Cargado = true;

            //            var Respu = Task.Run(async () => await Contexto.VerificarCantidades("Cargando"));

            //          string result = Respu.Result;
            CargarMenuPrincipal();

            /*            if (string.IsNullOrEmpty(result))
                        {
                            var TaskFiles = Task.Run(() => Contexto.DescargarArchivosFacturaConsultaLocal());

                            if (string.IsNullOrEmpty(TaskFiles.Result))
                            {
                                if (Contexto.VCash != null)
                                {
                                    Contexto.VCash.VcashReady -= Cargando_VCashReady;
                                    Contexto.VCash.VcashError -= Cargando_VcashError;
                                    Contexto.VCash.ServiceError -= Cargando_ServiceError;
                                }
                                CargarMenuPrincipal();
                            }
                            else
                            {
                                NavigationService.Navigate(new PageFueradeLinea(Contexto, Title, "Descargando archivos", true));
                            }
                        }
                        else
                        {
                            NavigationService.Navigate(new PageFueradeLinea(Contexto, Title, "Cantidades insuficientes", true));
                        }*/

        }

        private void Cargando_ServiceError(object sender, Exception e)
        {
            Consumo.Logger.Error(e, $"Cargando VCash ServiceError");
            Dispatcher.Invoke(() =>
            {
                //Contexto.VCash.VcashError -= Cargando_VcashError;
                NavigationService.Navigate(new PageFueradeLinea(Contexto, Title, e.Message, true));
            });
        }

        private void Cargando_VcashError(object sender, Exception e)
        {
            Consumo.Logger.Error(e, $"Cargando VCash Error");
            Dispatcher.Invoke(() =>
            {
                //Contexto.VCash.VcashError -= Cargando_VcashError;
                NavigationService.Navigate(new PageFueradeLinea(Contexto, Title, e.Message, true));
            });
        }

        private void CargarMenuPrincipal()
        {
            Dispatcher.Invoke(() =>
            {
                Consumo.LoggerInfo("CARGANDO MENÚ PRINCIPAL");
                if (Contexto.VCash != null)
                    //                    Contexto.VCash.VcashError -= Cargando_VcashError;

                    Consumo.InsertarEstadisticaServer("Cargando", "Cargando Menú Principal", "OK", "");
             //   TimerAnima.Start();
                Contexto.MuestraFinaliza = false;
                NavigationService.Navigate(new MenuPrincipal(Contexto));
            });
        }


    }
}
