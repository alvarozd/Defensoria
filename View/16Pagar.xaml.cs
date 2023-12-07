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
    public sealed partial class PagePagar : Page
    {
        private int Menu = 16;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();
        String ValorPagar = "";

        public PagePagar(Contexto contexto, String ValorAPagar)
        {

            ValorPagar = ValorAPagar;
            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Contexto = contexto;
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(Contexto.GetParams().TimeOutInactividadPantallas);
            TimerHome.Start();
        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                //Utilitario.PintarBoton(Menu, 1, btnCancelar, 286, 681, 194, 78);
                //Utilitario.PintarBoton(Menu, 2, btnAceptar, 542, 677, 194, 78);

                ///                Utilitario.PintarControl(LblReferencia, 580, 349, 342, 53, 30, "R", Contexto.InfoFactura.ReferenciaPago,"",true);
                ///                Utilitario.PintarControl(LblValorPagar, 580, 432, 342, 53, 45, "R", Contexto.InfoFactura.ValorPagar.ToString("C",Consumo.InfoPais), "B",true);

                //// para demo 
                //                Utilitario.PintarBoton(Menu, 1, btnAtras, 51, 24, 197, 116);

                Utilitario.PintarControl(LblValor, 329, 443, 473, 90, 32, "L", "Pago " + ValorPagar, "B", true);
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
                case "Aceptar":
                    {
                        NavigationService.Navigate(new PageMenuImpresion(Contexto));
                        break;
                    }
                case "Cancelar":
                    {
                        //btnCancela.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("PAGAR OTRO VALOR");

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

        private void MostrarMensaje(string titulo, string mensaje)
        {
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            Mensajes.IsOpen = true;
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new PagePagoExitoso(Contexto));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Consumo.LoggerInfo("MENÚ DETALLE DE FACTURA");
//            Consumo.LoggerInfo($"Valor a pagar: {Contexto.InfoFactura.ValorPagar.ToString("C", Consumo.InfoPais)}, Referencia: {Contexto.InfoFactura.ReferenciaPago}");
/*            Consumo.InsertarDetalleTransaccionServer($"Valor a pagar: {Contexto.InfoFactura.ValorPagar.ToString("C", Consumo.InfoPais)}, Referencia: {Contexto.InfoFactura.ReferenciaPago}", "Factura consultada");
            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);*/ // Volver a colocar jose 
        }
    }
}
