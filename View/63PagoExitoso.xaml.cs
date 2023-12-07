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

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageDetallePagoFactura.xaml
    /// </summary>
    public sealed partial class PagoExitoso : Page
    {
        private int Menu = 63;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();



        public PagoExitoso(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Contexto = contexto;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(4);
            TimerHome.Start();


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

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TimerHome.Stop();
            Storyboard story = (Storyboard)FindResource("EncogerImg");
            Image image = (Image)sender;
            image.BeginStoryboard(story);

            await Task.Delay(TimeSpan.FromMilliseconds(300));

            string MenuSig = image.Tag.ToString();



            switch (MenuSig)
            {
                case "NumeroDocumento":
                    {
                        NavigationService.Navigate(new PageDigitarNroCliente(Contexto));


                        break;

                    }
                case "NumeroCredito":
                    {
                        break;
                    }
                case "Cancelar":
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

        private async void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Utilitario.PintarImagen(Consultando, 0, 0, 1024, 768, "mn64", false, true);


            /// variables impresion
            Contexto.NombreCliente = "Armando Enrique Barbosa";
            Contexto.InfoFactura1 = Contexto.NumeroCredito;
            Contexto.InfoFactura1 = Contexto.NumeroCredito;
            Contexto.IMPCantidadIngresada = Contexto.Entered.ToString("C", Consumo.InfoPais); // VALOR RECIBIDO
            Contexto.IMPCambioEntregado = Contexto.Cambio.ToString("C", Consumo.InfoPais);  // VALOR PAGAR
            Contexto.IMPCambioFaltante = "";  // VALOR CAMBIO
            Contexto.RespuestaPago1 = "1657468481"; // NÚMERO RECIBO

            /// 

            Contexto.ImprimirRecibo(true);
            await Task.Delay(5000);
            Utilitario.PintarImagen(Consultando, 0, 0, 1024, 768, "finaliza", false, true);
            await Task.Delay(5000);
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
