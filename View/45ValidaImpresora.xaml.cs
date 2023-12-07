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
    public sealed partial class ValidaImpresora : Page
    {
        private int Menu = 45;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();


        private string respuS = "";
        public ValidaImpresora(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Contexto = contexto;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(16);
            TimerHome.Start();


        }

        private void EstablecerImagenes()
        {
            try
            {

                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                //Utilitario.PintarBoton(Menu, 1, btnSiI, 216, 564, 285, 137);
                //Utilitario.PintarBoton(Menu, 2, btnNoI, 524, 563, 285, 137);


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
                case "Si":
                    {

                        TimerHome.Stop();
                        Consumo.LoggerInfo("Sigue proceso por falta de papel");
                        Contexto.TipoConsulta = TipoConsulta.CodigodeBarras;
                        //       Contexto.tieneCambio = true; /// ojo quitar pruebas locales
                        //       Contexto.VCash.AbonoTotal = false; // ojo ojo ojo 
                        if (Contexto.tieneCambio == false)
                        {
                            Utilitario.PintarImagen(Consultando, 0, 0, 1024, 768, "Consultando", false, true);
                            await Task.Delay(100);
                            NavigationService.Navigate(new NoTieneCambio(Contexto));
                        }

                        else
                        {
                            respuS = await Consumo.InsertarTransaccionServer("Pago de abono", "Iniciado");
                            if (respuS.LastIndexOf("ERROR") >= 0)
                            {
                                Utilitario.PintarImagen(Consultando, 0, 0, 1024, 768, "Consultando", false, true);
                                await Task.Delay(100);
                                NavigationService.Navigate(new PageFueradeLinea(Contexto, "NOtienePapel", "Error el insertar la transación en VPS", false));
                            }
                            else
                            {
                                //Consumo.InsertarEstadisticaServer("Menú Inicio", "Ingresa al menú principal", "OK", Consumo.Redondeo);
                                Contexto.TipoConsulta = TipoConsulta.CodigodeBarras;
                                Consumo.InsertarDetalleTransaccionServer("Consulta por código de barras", "Iniciada");
                                Utilitario.PintarImagen(Consultando, 0, 0, 1024, 768, "Consultando", false, true);
                                await Task.Delay(100);
                                NavigationService.Navigate(new PageLeerCodigobarras(Contexto));

                            }
                        }


                        break;

                    }
                case "No":
                    {
                        TimerHome.Stop();
                        Consumo.LoggerInfo("No sigue proceso por falta de papel");
                        Utilitario.PintarImagen(Consultando, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(500);
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
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }


    }
}
