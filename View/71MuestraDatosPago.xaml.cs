using FacturasEnel.Util;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WindowsInput;
using FacturasEnel.Logica;
using FacturasEnel.Modelo;
using NLog;
using System.Windows.Threading;
using System.IO;
using System.Media;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageDetallePagoFactura.xaml
    /// </summary>
    public sealed partial class MuestraDatosPago : Page
    {
        private int Menu = 71;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();



        public MuestraDatosPago(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Contexto = contexto;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(116);
            TimerHome.Start();


        }

        private void EstablecerImagenes()
        {
            try
            {

                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                //Utilitario.PintarBoton(Menu, 1, btnAceptar, 337, 494, 191, 76);
                //Utilitario.PintarBoton(Menu, 2, btnAyuda, 813, 332, 185, 220);
                //Utilitario.PintarBoton(Menu, 3, btnVolver, 12, 636, 219, 79);
                //Utilitario.PintarBoton(Menu, 4, btnMP, 797, 636, 219, 79);
                Utilitario.PintarControl(Solicitud, 366, 287, 390, 35, 22, "C", "FCO - 321564654", "B", true);
                Utilitario.PintarControl(Cedula, 366, 339, 390, 35, 22, "C", "María Salomé Jiménez Bustos", "B", true);
                Utilitario.PintarControl(Nombres, 366, 387, 390, 35, 28, "C", 184000.ToString("C", Consumo.InfoPais), "B", true);
                Utilitario.PintarControl(Valor, 366, 438, 390, 35, 22, "C", DateTime.Now.ToString("MM/dd/yyyy"), "B", true);

            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TimerHome.Stop();
            Storyboard story = (Storyboard)FindResource("EncogerImg");
            Image image = (Image)sender;
            image.BeginStoryboard(story);

            await Task.Delay(TimeSpan.FromMilliseconds(300));

            string MenuSig = image.Tag.ToString();
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
                case "Aceptar":
                    {
                        Contexto.CualMenu = "Documento";
                        NavigationService.Navigate(new HaceDesembolso(Contexto));
                        break;

                    }
                case "Volver":
                    {
                        TimerHome.Stop();
                     
                        break;
                    }
                case "MP":
                    {
                        TimerHome.Stop();
                        NavigationService.Navigate(new MenuPrincipal(Contexto));
                        break;
                    }

                default:
                    {
                        Utilitario.ReiniciarTimer(TimerHome);
                        string numero = image.Tag.ToString();
                        InputSimulator.SimulateTextEntry(numero);
                        break;
                    }
            }
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Consumo.FinTransaccionTimeOut();
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //if (TextNumeroDocumento.IsVisible)
            //    TextNumeroDocumento.Focus();
        }


        private void MostrarMensaje(string titulo, string mensaje)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            Mensajes.IsOpen = true;
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
                TimerHome.Stop();
              
        }


        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            TimerHome.Stop();
        }
    }
}
