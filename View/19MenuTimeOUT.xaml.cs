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
    public sealed partial class PageTimeOUT : Page
    {
        private int Menu = 19;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();



        public PageTimeOUT(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Contexto = contexto;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(15);
            TimerHome.Start();


        }

        private void EstablecerImagenes()
        {
            try
            {

                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                //Utilitario.PintarBoton(Menu, 2, btnMas, 201, 589, 291, 131);
                //Utilitario.PintarBoton(Menu, 3, btnNo, 532, 590, 291, 131);


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
                case "Mas":
                    TimerHome.Stop();
                    {
                        Consumo.LoggerInfo("TIME OUT BOTON SI");

                        switch (Contexto.MenuAnt)
                        {
                            case "PageLeerCodigobarras":
                                {
                                    TimerHome.Stop();
                                    Contexto.MuestraFinaliza = false;
                                    NavigationService.Navigate(new PageLeerCodigobarras(Contexto));
                                    break;
                                }
                            case "PageDigitarNroCliente":
                                {
                                    TimerHome.Stop();
                                    Contexto.MuestraFinaliza = false;
                                    NavigationService.Navigate(new PageDigitarNroCliente(Contexto));
                                    break;
                                }
                            case "PageDetallePagoFactura":
                                {
                                    TimerHome.Stop();
                                    Contexto.MuestraFinaliza = true;
                                    NavigationService.Navigate(new PageDetallePagoFactura(Contexto));
                                    break;
                                }
                            case "PageValoresFactura":
                                {
                                    TimerHome.Stop();
                                    Contexto.MuestraFinaliza = true;
                                    NavigationService.Navigate(new PageValoresFactura(Contexto));
                                    break;
                                }
                            case "PageDigitaValorPagar":
                                {
                                    TimerHome.Stop();
                                    Contexto.MuestraFinaliza = true;
                                    NavigationService.Navigate(new PageDigitaValorPagar(Contexto));
                                    break;
                                }

                            case "PageMuestraValorPagar":
                                {
                                    TimerHome.Stop();
                                    Contexto.MuestraFinaliza = true;
                                    NavigationService.Navigate(new PageMuestraValorPagar(Contexto));
                                    break;
                                }

                            case "PageMenuImpresion":
                                {
                                    TimerHome.Stop();
                                    Contexto.MuestraFinaliza = true;
                                    NavigationService.Navigate(new PageMenuImpresion(Contexto));
                                    break;
                                }
                            case "MenuEncuesta":
                                {
                                    TimerHome.Stop();
                                    Contexto.MuestraFinaliza = true;
                                    NavigationService.Navigate(new MenuEncuesta(Contexto));
                                    break;
                                }
                            case "MenuInicio":
                                {
                                    TimerHome.Stop();
                                    NavigationService.Navigate(new MenuPrincipal(Contexto));
                                    break;
                                }
                            default:
                                {
                                    TimerHome.Stop();
                                    NavigationService.Navigate(new MenuPrincipal(Contexto));
                                    break;
                                }
                        }
                        
                        break;
                    }
                case "No":
                    {
                        if ((Contexto.MenuAnt == "PageLeerCodigobarras") || (Contexto.MenuAnt == "PageDigitarNroCliente"))
                            Contexto.MuestraFinaliza = false;
                        else
                            Contexto.MuestraFinaliza = true;


                        if (Contexto.MenuAnt == "MenuEncuesta")
                        {
                            Consumo.InsertarEstadisticaServer("Encuesta", "No contestó", "OK", Consumo.Redondeo);
                            Consumo.InsertarDetalleTransaccionServer("No contestó", "Respuesta encuesta");
                        }

                        TimerHome.Stop();
                        Consumo.LoggerInfo("TIME OUT BOTON NO");
                        NavigationService.Navigate(new MenuPrincipal(Contexto));
                        break;
                    }
                default:
                    {
                        //MessageBox.Show(image.Name, "NOMBRE BOTÓN");
                        break;
                    }
            }
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Consumo.FinTransaccionTimeOut();
            if ((Contexto.MenuAnt == "PageLeerCodigobarras") || (Contexto.MenuAnt == "PageDigitarNroCliente"))
                Contexto.MuestraFinaliza = false;
            else
                Contexto.MuestraFinaliza = true;


            if (Contexto.MenuAnt == "MenuEncuesta")
            {
                Consumo.InsertarEstadisticaServer("Encuesta", "No contestó", "OK", Consumo.Redondeo);
                Consumo.InsertarDetalleTransaccionServer("No contestó", "Respuesta encuesta");
            }

            TimerHome.Stop();
            Consumo.LoggerInfo("TIME OUT BOTON NO");
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }


    }
}
