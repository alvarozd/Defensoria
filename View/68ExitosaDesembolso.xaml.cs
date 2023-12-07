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
    public sealed partial class ExitosaDesembolso : Page
    {
        private int Menu = 68;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();
        private DispatcherTimer TimerNumeros = new DispatcherTimer();

        private int Vdispensado = 0;
        private int VRestante = 1000000;


        public ExitosaDesembolso(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Contexto = contexto;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(20);
            TimerHome.Start();
            TimerNumeros.Tick += new EventHandler(TimerNumeros_Tick);
            TimerNumeros.Interval = TimeSpan.FromSeconds(1);


        }

        private void EstablecerImagenes()
        {
            try
            {

                if (!Contexto.VieneDesembolso)
                    Background = Utilitario.ObtenerFondo(@"Util\mn100.jpg");
                else
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
//                        NavigationService.Navigate(new PageDigitarNroCliente(Contexto));
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

        private async void TimerNumeros_Tick(object sender, EventArgs e)
        {

            try
            {



                TimerNumeros.Stop();
                TimerHome.Stop();


                if (!Contexto.VieneDesembolso)
                {

                    if (Contexto.CualMenu == "Servicios")
                    {
                        Contexto.AmountToPay = 97000;
                        Contexto.Entered = 100000;
                        Contexto.Cambio = 3000;

                    }
                    else
                    {
                        Contexto.AmountToPay = 184000;
                        Contexto.Entered = 200000;
                        Contexto.Cambio = 16000;

                    }


                    Utilitario.PintarImagen(Imprimiendo, 0, 0, 1024, 768, "mn68", false, true);
                    await Task.Delay(4000);
                    Utilitario.PintarImagen(Imprimiendo, 0, 0, 1024, 768, "imprimiendo", false, true);
                    imprimir();
                    await Task.Delay(4000);
                    Utilitario.PintarImagen(Gracias, 0, 0, 1024, 768, "Gracias", false, true);

                }
                else
                {
                    Utilitario.PintarImagen(Imprimiendo, 0, 0, 1024, 768, "imprimiendo", false, true);
                    imprimir();
                    await Task.Delay(4000);
                    Utilitario.PintarImagen(Gracias, 0, 0, 1024, 768, "Gracias", false, true);
                }


                await Task.Delay(3000);
                NavigationService.Navigate(new MenuPrincipal(Contexto));
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error("Error en volver de la pantalla gracias: " + ex.ToString());

            }
        }
        private async void imprimir()
        {
            Contexto.NombreCliente = "María Salomé Jiménez Bustos";
            Contexto.InfoFactura1 = "FCO - 8987486721";
            Contexto.InfoFactura1 = "FCO - 8987486721";
            Contexto.IMPCantidadIngresada = Contexto.Entered.ToString("C", Consumo.InfoPais); // VALOR RECIBIDO
            Contexto.IMPCambioEntregado = Contexto.Cambio.ToString("C", Consumo.InfoPais);  // VALOR PAGAR
            Contexto.IMPCambioFaltante = "";  // VALOR CAMBIO
            Contexto.RespuestaPago1 = "1657468481"; // NÚMERO RECIBO

            /// 

            Contexto.ImprimirRecibo(true);



        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(4000);
            TimerNumeros.Start();


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
