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
    public sealed partial class DigitaServicioPublico : Page
    {
        private int Menu = 74;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();



        public DigitaServicioPublico(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Contexto = contexto;
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;


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
                //Utilitario.PintarBoton(Menu, 1, btn1, 496, 294, 89, 66);
                //Utilitario.PintarBoton(Menu, 2, btn2, 589, 294, 89, 66);
                //Utilitario.PintarBoton(Menu, 3, btn3, 681, 294, 89, 66);
                //Utilitario.PintarBoton(Menu, 4, btn4, 499, 363, 89, 66);
                //Utilitario.PintarBoton(Menu, 5, btn5, 588, 362, 89, 66);
                //Utilitario.PintarBoton(Menu, 6, btn6, 684, 364, 89, 66);
                //Utilitario.PintarBoton(Menu, 7, btn7, 501, 430, 89, 66);
                //Utilitario.PintarBoton(Menu, 8, btn8, 591, 426, 89, 66);
                //Utilitario.PintarBoton(Menu, 9, btn9, 685, 431, 89, 66);
                //Utilitario.PintarBoton(Menu, 10, btn10, 498, 498, 89, 66);
                //Utilitario.PintarBoton(Menu, 11, btnBorrar, 592, 498, 182, 69);
                //Utilitario.PintarBoton(Menu, 12, btnAceptar, 471, 589, 325, 69);
                //Utilitario.PintarBoton(Menu, 13, btnAyuda, 826, 332, 168, 209);
                //Utilitario.PintarBoton(Menu, 14, btnVolver, 14, 638, 211, 79);
                //Utilitario.PintarBoton(Menu, 15, btnMP, 802, 640, 211, 79);
                Utilitario.PintarControl(TextNumeroDocumento, 61, 431, 358, 40,  32, "C", true);
                TextNumeroDocumento.Focus();

                
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

                        if (TextNumeroDocumento.Text == "")
                        {
                            MostrarMensaje("Datos incompletos","Por favor ingresar el número de referencia de pago");
                        }
                        else
                        {
                            if (TextNumeroDocumento.Text == "1234")
                            {
                                Contexto.numeroDocumento = TextNumeroDocumento.Text;
                                Contexto.CualMenu = "Servicio";
                                NavigationService.Navigate(new PageCosultando(Contexto));
                            }
                            else
                            {
                                Utilitario.PintarImagen(Consultando, 0, 0, 1024, 768, "mn100", false, true);
                                await Task.Delay(3000);
                                MostrarMensaje("Validación", "Los datos no se encuentran registrados");
                            }
                        }
                        break;

                    }
                case "No":
                    {
                        break;
                    }
                case "Borrar":
                    {
                        TimerHome.Stop();
                        Utilitario.ReiniciarTimer(TimerHome);
                        if (TextNumeroDocumento.SelectionStart > 0)
                            InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        break;
                    }
                case "Volver":
                    {
                        TimerHome.Stop();
                        NavigationService.Navigate(new ServiciosPublicos(Contexto));
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
            if (TextNumeroDocumento.IsVisible)
                TextNumeroDocumento.Focus();
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
                NavigationService.Navigate(new DigitaServicioPublico(Contexto));
        }


        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            TimerHome.Stop();
        }
    }
}
