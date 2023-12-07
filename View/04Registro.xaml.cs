using BusinessEnel;
using FacturasEnel.Logica;
using FacturasEnel.Modelo;
using FacturasEnel.Util;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Net;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageProcesandoPago.xaml
    /// </summary>
    public sealed partial class Registro : Page
    {

        /// <Animación >
        private List<BitmapImage> Images = new List<BitmapImage>();
        private int ImageNumber = 0;
        private DispatcherTimer TimerAnima = new DispatcherTimer();
        /// </Animación>



        private int Menu = 4;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerMenuPrincipal = new DispatcherTimer();
        private bool SiPago = false;

        public Registro(Contexto contexto)
        {
            this.Contexto = contexto;
            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;

            EstablecerImagenes();
            TimerAnima.Tick += new EventHandler(TimerAnima_TickAsync);
            TimerAnima.Interval = TimeSpan.FromSeconds(1);


            TimerMenuPrincipal.Tick += new EventHandler(TimerMenuPrincipal_Tick);
            TimerMenuPrincipal.Interval = new TimeSpan(0, 0, 5);

            // NO SE USA EL TIMER
            //TimerMenuPrincipal.Start(); // NO SE USA

            // TODO
            // comportamiento cuando no se realiza el pago exitoso.

        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
             
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(100);
            ConsumeServicio();

        }

        private async void TimerMenuPrincipal_Tick(object sender, EventArgs e)
        {

            TimerMenuPrincipal.Stop();

            Utilitario.PintarImagen(IError, 0, 0, 1024, 768, "MN301", false, true);
            await Task.Delay(3000);

            NavigationService.Navigate(new MenuPrincipal(Contexto));


        }


        private async void TimerAnima_TickAsync(object sender, EventArgs e)
        {
            ImageNumber = (ImageNumber + 1) % Images.Count;
            ShowNextImage(imgPicture);
        }
        private void ShowNextImage(System.Windows.Controls.Image img)
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

        private void ConsumeServicio()
        {
        }

    }
}
