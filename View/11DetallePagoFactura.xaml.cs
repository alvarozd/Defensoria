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
    public sealed partial class PageDetallePagoFactura : Page
    {
        private int Menu = 11;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public PageDetallePagoFactura(Contexto contexto)
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
///                Utilitario.PintarBoton(Menu, 0, btnVolver, 41, 624, 283, 83, "Volver", false, true);
///                Utilitario.PintarBoton(Menu, 0, btnPagar, 987, 417, 279, 280, "Pagar", false, true);

///                Utilitario.PintarControl(LblReferencia, 580, 349, 342, 53, 30, "R", Contexto.InfoFactura.ReferenciaPago,"",true);
///                Utilitario.PintarControl(LblValorPagar, 580, 432, 342, 53, 45, "R", Contexto.InfoFactura.ValorPagar.ToString("C",Consumo.InfoPais), "B",true);

                //// para demo 
                //Utilitario.PintarBoton(Menu, 1, btnAtras, 51, 50, 149, 77);
                //Utilitario.PintarBoton(Menu, 2, btnCancela, 316, 582, 199, 83);
                //Utilitario.PintarBoton(Menu, 3, btnAceptar, 513, 578, 199, 83);


                Utilitario.PintarControl(LblNombreUsuario, 465, 395, 572, 53, 20, "L", Contexto.NombreCliente, "B", true);
                Utilitario.PintarControl(LblNumeroCLiente, 465, 458, 572, 53, 20, "L", Contexto.InfoFactura1, "B", true);
//                Contexto.NroCliente = "Andres felipe Garagoa";
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
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(300);
                        NavigationService.Navigate(new PageValoresFactura(Contexto));
                        break;
                    }
                case "Atras":
                    {
                        btnAtras.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("VOLVER DE MENÚ DETALLE DE FACTURA");
                        if (Contexto.TipoConsulta == TipoConsulta.CodigodeBarras)
                        {
                            Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                            await Task.Delay(200);

                            NavigationService.Navigate(new PageLeerCodigobarras(Contexto));

                        }
                        else if ((Contexto.TipoConsulta == TipoConsulta.NumerodeCliente) || (Contexto.TipoConsulta == TipoConsulta.NumeroTarjeta))
                        {
                            Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                            await Task.Delay(200);
                            NavigationService.Navigate(new PageDigitarNroCliente(Contexto));

                        }
                        break;
                    }
                case "Cancelar":
                    {
                        btnCancela.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("VOLVER A MENÚ PRINCIPAL");
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
            Contexto.MenuAnt = "PageDetallePagoFactura";
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
            Consumo.LoggerInfo("MENÚ DETALLE DE FACTURA");
//            Consumo.LoggerInfo($"Valor a pagar: {Contexto.InfoFactura.ValorPagar.ToString("C", Consumo.InfoPais)}, Referencia: {Contexto.InfoFactura.ReferenciaPago}");
/*            Consumo.InsertarDetalleTransaccionServer($"Valor a pagar: {Contexto.InfoFactura.ValorPagar.ToString("C", Consumo.InfoPais)}, Referencia: {Contexto.InfoFactura.ReferenciaPago}", "Factura consultada");
            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);*/ // Volver a colocar jose 
        }
    }
}
