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
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;



namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageCargando.xaml
    /// </summary>
    public sealed partial class PageCosultando : Page
    {

        /// <Animación >
        private List<BitmapImage> Images = new List<BitmapImage>();
        private int ImageNumber = 0;
        private DispatcherTimer TimerAnima = new DispatcherTimer();
        /// </Animación>


        private int Menu = 100;
        private readonly Contexto Contexto;
        //public event EventHandler VCashReady;
        private DispatcherTimer TimerInicializar = new DispatcherTimer();

        public PageCosultando(Contexto contexto)
        {
            Height = contexto.Alto;
            Width = contexto.Ancho;
            this.Contexto = contexto;
            InitializeComponent();
            EstablecerImagenes();
            Contexto.MuestraFinaliza = true;

            TimerAnima.Tick += new EventHandler(TimerAnima_TickAsync);
            TimerAnima.Interval = TimeSpan.FromSeconds(1);


            TimerInicializar.Tick += new EventHandler(TimerInicializar_TickAsync);
            TimerInicializar.Interval = TimeSpan.FromSeconds(3);


        }

        private void EstablecerImagenes()
        {

            try
            {
                Background = Utilitario.ObtenerFondo(@"Util\MN100.jpg");
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
//            TimerAnima.Start();
            await Task.Delay(1000);
            TimerInicializar.Start();
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
            TimerInicializar.Stop();
            TimerAnima.Stop();

            if (Contexto.CualMenu == "Desembolso")
            {
                NavigationService.Navigate(new MuestraDesembolso(Contexto));
            }
            else if (Contexto.CualMenu == "Pago")
            {
                NavigationService.Navigate(new MuestraDatosPago(Contexto));
            }
            else if (Contexto.CualMenu == "Servicio")
            {
                NavigationService.Navigate(new MuestraDatosServicio(Contexto));
            }


        }

        private void CargarMenuPrincipal()
        {
            Dispatcher.Invoke(() =>
            {
                Consumo.LoggerInfo("CARGANDO MENÚ PRINCIPAL");
                if (Contexto.VCash != null)
//                    Contexto.VCash.VcashError -= Cargando_VcashError;

                Consumo.InsertarEstadisticaServer("Cargando", "Cargando Menú Principal", "OK", "");
                NavigationService.Navigate(new MenuPrincipal(Contexto));
            });
        }

        public class Abono
        {

            public string buscar { get; set; }
            public string workStationK { get; set; }
        }

        private async void ConsumeServicio(String NumeroEnviar)
        {
            
            //ojo ojo solo para pruebas sin servicio 
            //Contexto.PagoSemanal = 400;
            //Contexto.PagoSugerido = 800;
            //Contexto.PagoLiquidar = 60000;
            //Contexto.NombreCliente = "OSCAR EDUARDO RODRIGUEZ DIMATE SENSEI";
            //NavigationService.Navigate(new PageDetallePagoFactura(Contexto));
            //
            
           
        }


    }
}
