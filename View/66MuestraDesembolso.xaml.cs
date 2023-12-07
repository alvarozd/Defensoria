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
    public sealed partial class MuestraDesembolso : Page
    {
        private int Menu = 66;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();



        public MuestraDesembolso(Contexto contexto)
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
                //Utilitario.PintarBoton(Menu, 1, btnAceptar, 557, 505, 190, 72);
                //Utilitario.PintarBoton(Menu, 2, btnAyuda, 819, 342, 176, 192);
                //Utilitario.PintarBoton(Menu, 3, btnVolver, 11, 633, 218, 85);
                //Utilitario.PintarBoton(Menu, 4, btnMP, 798, 631, 218, 85);
                Utilitario.PintarControl(Solicitud, 230, 287, 509, 35, 22, "C", "FCO - 8987486721", "B", true);
                Utilitario.PintarControl(Cedula, 230, 360, 509, 35, 22, "C", Contexto.numeroDocumento, "B", true);
                Utilitario.PintarControl(Nombres, 230, 423, 509, 35, 22, "C", "María Salomé Jiménez Bustos", "B", true);
                Utilitario.PintarControl(Valor, 132, 530, 399, 35, 28, "C", 1000000.ToString("C", Consumo.InfoPais), "B", true);


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
                        Contexto.CualMenu = "Desembolso";
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
