using BusinessEnel;
using FacturasEnel.Logica;
using FacturasEnel.Modelo;
using FacturasEnel.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using WindowsInput;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageDigitarNroCliente.xaml
    /// </summary>
    public sealed partial class PageDigitarNroTarjeta : Page
    {
        private int Menu = 50;
        private readonly Contexto Contexto;
        bool BackToMenuPrincipal = false;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public PageDigitarNroTarjeta(Contexto contexto)
        {
            Contexto = contexto;

            Height = Contexto.Alto;
            Width = Contexto.Ancho;

            InitializeComponent();
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(Contexto.GetParams().TimeOutInactividadPantallas+30);
            TimerHome.Start();
        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                //Utilitario.PintarBoton(Menu, 1, btnConsultaCodigo, 199, 108, 140, 198);
                //Utilitario.PintarBoton(Menu, 2, btnConsultaCliente, 412, 98, 208, 205);


                /*    Utilitario.PintarBoton(Menu, 0, btnVolver, 41, 624, 283, 83, "Volver", false, true);
                    Utilitario.PintarBoton(Menu, 1, btn0, 599, 545, 56, 56);
                    Utilitario.PintarBoton(Menu, 2, btn1, 599, 486, 56, 56);
                    Utilitario.PintarBoton(Menu, 3, btn2, 655, 486, 56, 56);
                    Utilitario.PintarBoton(Menu, 4, btn3, 711, 486, 56, 56);
                    Utilitario.PintarBoton(Menu, 5, btn4, 599, 428, 56, 56);
                    Utilitario.PintarBoton(Menu, 6, btn5, 655, 427, 56, 56);
                    Utilitario.PintarBoton(Menu, 7, btn6, 711, 427, 56, 56);
                    Utilitario.PintarBoton(Menu, 8, btn7, 599, 369, 56, 56);
                    Utilitario.PintarBoton(Menu, 9, btn8, 655, 369, 56, 56);
                    Utilitario.PintarBoton(Menu, 10, btn9, 711, 369, 56, 56);
                    Utilitario.PintarBoton(Menu, 11, btnBorrar, 656, 545, 111, 56);
                    Utilitario.PintarBoton(Menu, 12, btnConsultar, 1043, 623, 284, 84);
                    */
                //Utilitario.PintarBoton(Menu, 0, btnAyuda, 1228, 515, 129, 125, "Ayuda");
                Utilitario.PintarControl(Consultando, 0, 0, Contexto.Ancho, Contexto.Alto, 0, "C", "", "",false);
                Utilitario.PintarControl(NroCliente, 537, 307, 292, 42, 28, "C", true);
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

            await Task.Delay(TimeSpan.FromMilliseconds(100));
            string MenuSig = image.Tag.ToString();
            switch (MenuSig)
            {
                case "ConCod":
                    {
                        Consumo.InsertarEstadisticaServer("Menú consulta por número de tarjeta", "Ingreso a consulta por Código de barras", "OK", Consumo.Redondeo);
                        NavigationService.Navigate(new PageLeerCodigobarras(Contexto));
                        break;
                    }
                case "ConCli":
                    {
                        Consumo.InsertarEstadisticaServer("Menú consulta por número de tarjeta", "Ingreso a consulta por Cliente", "OK", Consumo.Redondeo);
                        NavigationService.Navigate(new PageDigitarNroCliente(Contexto));
                        break;
                    }
                case "Volver":
                    {
                        btnVolver.IsEnabled = false;
                        Consumo.FinTransaccionMenuPrincipal();
                        NavigationService.Navigate(new MenuPrincipal(Contexto));
                        break;
                    }
                case "Consultar":
                    {
                        btnConsultar.IsEnabled = false;
                        Utilitario.Hablar("");

                        ConsultarNumeroDeCliente();
                        break;
                    }
                case "Borrar":
                    {
                        Utilitario.ReiniciarTimer(TimerHome);
                        if (NroCliente.SelectionStart > 0)
                            InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        break;
                    }
                case "Ayuda":
                    {
                        Consumo.InsertarEstadisticaServer("Menú Lectura código de barras", "Ingreso Ayuda", "OK", Consumo.Redondeo);
                        NavigationService.Navigate(new PageAyuda(Contexto, Menu));
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

        private async void ConsultarNumeroDeCliente()
        {
            TimerHome.Stop();
            if (string.IsNullOrEmpty(NroCliente.Text))
            {
                Utilitario.ReiniciarTimer(TimerHome);
                MostrarMensaje("Información incompleta", "Debe digitar el número de cliente");
                btnConsultar.IsEnabled = true;
            }
            else
            {
                try
                {
                    NroCliente.Text = NroCliente.Text.TrimStart('0');
                    Contexto.TipoConsulta = TipoConsulta.NumerodeCliente;
                    Consumo.LoggerInfo($"Número de cliente: {NroCliente.Text}");
                    Contexto.DatoConsulta = NroCliente.Text;
                    Consultando.Visibility = Visibility.Visible;

                    Consumo.InsertarDetalleTransaccionServer($"Número de Cliente: {NroCliente.Text}", "Número de cliente digitado");
                    Consumo.InsertarEstadisticaServer("Menú digitar número de cliente", $"Número digitado: {NroCliente.Text}", "OK", Consumo.Redondeo);

                    if (Contexto.ConsultaReal)
                        await ConsultarPorNumeroCliente(NroCliente.Text);
                    else
                        NavigationService.Navigate(new PageDetallePagoFactura(Contexto));
                }
                catch (Exception ex)
                {
                    Consumo.Logger.Error(ex, "ConsultarNumeroDeCliente");
                    MostrarMensaje("Advertencia", $"No se encontró el número de cliente digitado.");
                    //MostrarMensaje("Exception", $"Error: {ex.Message}");
                }
                finally
                {
                    TimerHome.Stop();
                    Consultando.Visibility = Visibility.Collapsed;
                    NroCliente.Text = "";
                    NroCliente.Focus();
                    btnConsultar.IsEnabled = true;
                }
            }
        }

        private async Task ConsultarPorNumeroCliente(string NumeroCliente)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            CamposPago FacturaConsultada = await Consumo.ConsultarPorNumeroCliente(NumeroCliente);
            NroCliente.Text = "";
            Consumo.ValorPagar = 0.ToString("C", Consumo.InfoPais);

            if (FacturaConsultada == null)
            {
                MostrarMensaje("Error en la consulta", "No se pudo obtener la información de la factura.");
                NroCliente.Text = "";
                NroCliente.Focus();
                return;
            }

            if (FacturaConsultada.ValorPagar <= 0)
            {
                if (string.IsNullOrEmpty(FacturaConsultada.ReferenciaPago))
                {
                    MostrarMensaje("Información", "Número de cliente no encontrado.");
                }
                else
                {
                    MostrarMensaje("Información", "No hay deuda para el número de cliente consultado.");
                }
                NroCliente.Text = "";
                NroCliente.Focus();
                return;
            }
            else
            {
                //FacturaConsultada.ValorPagar = FacturaConsultada.ValorPagar / 100;
                TimerHome.Stop();
                Contexto.InfoFactura = FacturaConsultada;
                Contexto.NroCliente = NumeroCliente;
                //Contexto.NroCliente = Contexto.NroCliente.Insert(Contexto.NroCliente.Length - 1, "-");
                Consumo.ValorPagar = Contexto.InfoFactura.ValorPagar.ToString("C", Consumo.InfoPais);
                NavigationService.Navigate(new PageDetallePagoFactura(Contexto));
            }
        }

        private void MostrarMensaje(string titulo, string mensaje)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            Mensajes.IsOpen = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Consumo.LoggerInfo("MENÚ DIGITAR NÚMERO DE CLIENTE");

            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);

            if (NroCliente.IsVisible)
                NroCliente.Focus();
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Consumo.FinTransaccionTimeOut();
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
            if (BackToMenuPrincipal)
            {
                TimerHome.Stop();
                NavigationService.Navigate(new MenuPrincipal(Contexto));
            }
            else
            {
                Utilitario.ReiniciarTimer(TimerHome);
            }
        }

    }

}
