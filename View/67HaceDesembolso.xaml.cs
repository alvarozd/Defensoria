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
    public sealed partial class HaceDesembolso : Page
    {
        private int Menu = 67;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();
        private DispatcherTimer TimerNumeros = new DispatcherTimer();

        private int Vdispensado = 0;
        private int VRestante = 1000000;


        public HaceDesembolso(Contexto contexto)
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


                if ((Contexto.CualMenu == "Documento") || (Contexto.CualMenu == "Credito") || (Contexto.CualMenu == "Servicios"))
                {
                    Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}a.jpg");
                    if (Contexto.CualMenu == "Servicios")
                        Utilitario.PintarControl(Total, 310, 404, 398, 34, 28, "C", 97000.ToString("C", Consumo.InfoPais), "B", true);
                    else
                        Utilitario.PintarControl(Total, 310, 404, 398, 34, 28, "C", 184000.ToString("C", Consumo.InfoPais), "B", true);
                    Utilitario.PintarControl(Dispensado, 310, 470, 398, 34, 28, "C", 0.ToString("C", Consumo.InfoPais), "B", true);
                    Utilitario.PintarControl(Pendiente, 310, 558, 398, 34, 28, "C", 0.ToString("C", Consumo.InfoPais), "B", true);
                    VRestante = 184000;
                }
                else
                {
                    Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                    Utilitario.PintarControl(Total, 310, 404, 398, 34, 28, "C", 1000000.ToString("C", Consumo.InfoPais), "B", true);
                    Utilitario.PintarControl(Dispensado, 310, 470, 398, 34, 28, "C", 0.ToString("C", Consumo.InfoPais), "B", true);
                    Utilitario.PintarControl(Pendiente, 310, 558, 398, 34, 28, "C", 1000000.ToString("C", Consumo.InfoPais), "B", true);
                    VRestante = 1000000;

                }


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
            TimerHome.Stop();


            if ((Contexto.CualMenu == "Documento") || (Contexto.CualMenu == "Credito"))
            {
                Contexto.VieneDesembolso = false;

                Vdispensado = Vdispensado + 20000;

                Dispensado.Content = Vdispensado.ToString("C", Consumo.InfoPais);

                if (Vdispensado >= 184000)
                {
                    VRestante = Vdispensado - 184000;
                    Pendiente.Content = VRestante.ToString("C", Consumo.InfoPais);
                    TimerHome.Stop();
                    TimerNumeros.Stop();
                    await Task.Delay(4000);
                    NavigationService.Navigate(new ExitosaDesembolso(Contexto));


                }
                else
                    Utilitario.ReiniciarTimer(TimerNumeros);


            }
            else if (Contexto.CualMenu == "Desembolso")
            {
                Contexto.VieneDesembolso = true;

                Vdispensado = Vdispensado + 100000;
                VRestante = VRestante - 100000;

                Dispensado.Content = Vdispensado.ToString("C", Consumo.InfoPais);
                Pendiente.Content = VRestante.ToString("C", Consumo.InfoPais);

                if (Vdispensado == 1000000)
                {
                    TimerHome.Stop();
                    TimerNumeros.Stop();
                    await Task.Delay(1500);
                    NavigationService.Navigate(new ExitosaDesembolso(Contexto));


                }
                else
                    Utilitario.ReiniciarTimer(TimerNumeros);

            }
            else if (Contexto.CualMenu == "Servicios")
            {
                Contexto.VieneDesembolso = false;

                Vdispensado = Vdispensado + 5000;

                Dispensado.Content = Vdispensado.ToString("C", Consumo.InfoPais);

                if (Vdispensado >= 97000)
                {
                    VRestante = Vdispensado - 97000;
                    Pendiente.Content = VRestante.ToString("C", Consumo.InfoPais);
                    TimerHome.Stop();
                    TimerNumeros.Stop();
                    await Task.Delay(4000);
                    NavigationService.Navigate(new ExitosaDesembolso(Contexto));


                }
                else
                    Utilitario.ReiniciarTimer(TimerNumeros);


            }


        }



        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(2000);
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
