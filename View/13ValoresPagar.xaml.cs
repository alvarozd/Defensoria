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
    public sealed partial class PageValoresFactura : Page
    {
        private int Menu = 13;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public PageValoresFactura(Contexto contexto)
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
                //// para demo 
                //Utilitario.PintarBoton(Menu, 1, btnAtras, 48, 54, 154, 65);
                if (Contexto.PagoSemanal != 0) ;
                //Utilitario.PintarBoton(Menu, 2, btnPagoSemanal, 181, 383, 323, 140);
                else
                    Utilitario.PintarImagen(Psemanal, 180, 381, 325, 143, "psemanal", false, true);

                //Utilitario.PintarBoton(Menu, 3, btnPagoSugerido, 519, 382, 323, 140);
                //Utilitario.PintarBoton(Menu, 4, btnPagoTotal, 179, 537, 323, 140);
                //Utilitario.PintarBoton(Menu, 5, btnPagoOtro, 519, 536, 323, 140);
                Utilitario.PintarControl(LblSemanal, 216, 472, 100, 46, 22, "L", Contexto.PagoSemanal.ToString("C", Consumo.InfoPais), "B", true);
                Utilitario.PintarControl(LblSugerido, 554, 472, 100, 46, 22, "L",Contexto.PagoSugerido.ToString("C", Consumo.InfoPais), "B", true);
                Utilitario.PintarControl(LblTotal, 216, 616, 100, 46, 22, "L",Contexto.PagoLiquidar.ToString("C", Consumo.InfoPais), "B", true);
              //  MessageBox.Show(Contexto.stuff);

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
                        break;
                    }
                case "Atras":
                    {
                       // btnVolver.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("VOLVER DE MENÚ INICIAL");
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(200);
                        NavigationService.Navigate(new PageDetallePagoFactura(Contexto));

                        break;
                    }
                case "Otro":
                    {
                        //btnCancela.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("PAGAR OTRO VALOR");
                        Contexto.pagoTotal = true;
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(200);
                        NavigationService.Navigate(new PageDigitaValorPagar(Contexto));
                        break;
                    }
                case "Semanal":
                    {
                        /*
        public int PagoSemanal { get; set; }
        public int PagoSugerido { get; set; }
        public int PagoLiquidar { get; set; }

                         */



                        //btnCancela.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("PAGAR VALOR SEMANAL");
                        Contexto.pagoTotal = false;
                        Contexto.AmountToPay = Contexto.PagoSemanal;
                        Consumo.ValorPagar = Contexto.AmountToPay.ToString("C", Consumo.InfoPais);
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(200);
                        NavigationService.Navigate(new PageMenuPago(Contexto));
                        break;
                    }
                case "Sugerido":
                    {
                        //btnCancela.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("PAGAR VALOR SUGERIDO");
                        Contexto.pagoTotal = false;
                        Contexto.AmountToPay = Contexto.PagoSugerido;
                        Consumo.ValorPagar = Contexto.AmountToPay.ToString("C", Consumo.InfoPais);
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(200);
                        NavigationService.Navigate(new PageMenuPago(Contexto));

                        break;
                    }
                case "Total":
                    {
                        //btnCancela.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.LoggerInfo("PAGAR VALOR Total");
                        Contexto.pagoTotal = false;
                        Contexto.AmountToPay = Contexto.PagoLiquidar;
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(200);

                        Consumo.ValorPagar = Contexto.AmountToPay.ToString("C", Consumo.InfoPais);

                        NavigationService.Navigate(new PageMenuPago(Contexto));
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
            Contexto.MenuAnt = "PageValoresFactura";
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
