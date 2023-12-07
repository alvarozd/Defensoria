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
    public sealed partial class PageMuestraValorPagar : Page
    {
        private int Menu = 15;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();
        String ValorPagar = "";
        int ValorPagarMinimo = 0;

        public PageMuestraValorPagar(Contexto contexto)
        {


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
                //Utilitario.PintarBoton(Menu, 1, btnAtras, 49, 48, 149, 79);
                //Utilitario.PintarBoton(Menu, 2, btnCancelar, 318, 582, 194, 83);
                //Utilitario.PintarBoton(Menu, 3, btnAceptar, 515, 582, 194, 83);

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
                        Consumo.ValorPagar = Contexto.AmountToPay.ToString("C", Consumo.InfoPais);
                        Consumo.InsertarDetalleTransaccionServer(Consumo.ValorPagar, "Valor a pagar");
                        TimerHome.Stop();
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(200);

                        NavigationService.Navigate(new PageMenuPago(Contexto)); 


                        break;
                    }
                case "Atras":
                    {
                        // btnVolver.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("VOLVER DE MENÚ DETALLE DE FACTURA");

                        if (Contexto.pagoTotal)
                        {
                            Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                            await Task.Delay(200);

                            NavigationService.Navigate(new PageDigitaValorPagar(Contexto));

                        }
                        break;
                    }
                case "Cancelar":
                    {
                        //btnCancela.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("PAGAR OTRO VALOR");
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(200);

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
            Contexto.MenuAnt = "PageMuestraValorPagar";
            NavigationService.Navigate(new PageTimeOUT(Contexto));

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
            Consumo.InsertarDetalleTransaccionServer(Contexto.DatoConsulta, "Referencia");
            Consumo.LoggerInfo("MENÚ DETALLE DE FACTURA");
//            Consumo.LoggerInfo($"Valor a pagar: {Contexto.InfoFactura.ValorPagar.ToString("C", Consumo.InfoPais)}, Referencia: {Contexto.InfoFactura.ReferenciaPago}");
/*            Consumo.InsertarDetalleTransaccionServer($"Valor a pagar: {Contexto.InfoFactura.ValorPagar.ToString("C", Consumo.InfoPais)}, Referencia: {Contexto.InfoFactura.ReferenciaPago}", "Factura consultada");
            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);*/ // Volver a colocar jose 
        }
    }
}
