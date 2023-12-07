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
    public sealed partial class PageDigitaValorPagar : Page
    {
        private int Menu = 14;
        private readonly Contexto Contexto;
        bool BackToMenuPrincipal = false;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public PageDigitaValorPagar(Contexto contexto)
        {
            Contexto = contexto;

            Height = Contexto.Alto;
            Width = Contexto.Ancho;

            InitializeComponent();
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
                //                Utilitario.PintarBoton(Menu, 0, btnVolver, 41, 624, 283, 83, "Volver", false, true);
                //Utilitario.PintarBoton(Menu, 1, btn1, 393, 409, 51, 47);
                //Utilitario.PintarBoton(Menu, 2, btn2, 446, 409, 51, 47);
                //Utilitario.PintarBoton(Menu, 3, btn3, 499, 410, 51, 47);
                //Utilitario.PintarBoton(Menu, 4, btn4, 393, 458, 51, 47);
                //Utilitario.PintarBoton(Menu, 5, btn5, 445, 458, 51, 47);
                //Utilitario.PintarBoton(Menu, 6, btn6, 499, 458, 51, 47);
                //Utilitario.PintarBoton(Menu, 7, btn7, 393, 507, 51, 47);
                //Utilitario.PintarBoton(Menu, 8, btn8, 446, 507, 51, 47);
                //Utilitario.PintarBoton(Menu, 9, btn9, 498, 508, 51, 47);
                //Utilitario.PintarBoton(Menu, 10, btn0, 446, 557, 51, 47);
                //Utilitario.PintarBoton(Menu, 11, btnBorrar, 551, 409, 82, 52);
                //Utilitario.PintarBoton(Menu, 12, btnCancelar, 318, 657, 190, 72);
                //Utilitario.PintarBoton(Menu, 13, btnConsultar, 518, 657, 190, 72);
                //Utilitario.PintarBoton(Menu, 14, btnAtras, 32, 49, 162, 77);

                //Utilitario.PintarBoton(Menu, 0, btnAyuda, 1228, 515, 129, 125, "Ayuda");
                //                Utilitario.PintarControl(Consultando, 0, 0, Contexto.Ancho, Contexto.Alto, 0, "C", "", "",false);
                Utilitario.PintarControl(TextValorPagar, 401, 346, 224, 36, 24, "L", true);
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
                case "Atras":
                    {
                        TimerHome.Stop();
                        btnVolver.IsEnabled = false;
                        Consumo.FinTransaccionMenuPrincipal();
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(200);

                        NavigationService.Navigate(new PageValoresFactura(Contexto));
                        break;
                    }
                case "Consultar":
                    {
                        TimerHome.Stop();
                        btnConsultar.IsEnabled = false;
                        Utilitario.Hablar("");

                        ValidaValor();
                        break;
                    }
                case "Borrar":
                    {
                        TimerHome.Stop();
                        Utilitario.ReiniciarTimer(TimerHome);
                        if (TextValorPagar.SelectionStart > 0)
                            InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
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
                        Utilitario.ReiniciarTimer(TimerHome);
                        string numero = image.Tag.ToString();
                        InputSimulator.SimulateTextEntry(numero);
                        break;
                    }
            }
        }

        private async void ValidaValor()
        {
            TimerHome.Stop();
            int ValorValida = 0;

            if ((string.IsNullOrEmpty(TextValorPagar.Text)))
            {

                Utilitario.PintarImagen(IMensaje, 314, 385, 24, 23, "IMensaje", false, true);
                Utilitario.PintarControl(LblMensaje, 346, 387, 429, 20, 18, "L", "Por favor ingresar un valor", "B", true);

                btnConsultar.IsEnabled = true;
            }
            else
            {
                ValorValida = Int32.Parse(TextValorPagar.Text);

                if (ValorValida <= 0)
                {
                    Utilitario.PintarImagen(IMensaje, 314, 385, 24, 23, "IMensaje", false, true);
                    Utilitario.PintarControl(LblMensaje, 346, 387, 429, 20, 18, "L", "El valor ingresado debe ser mayor a cero", "B", true);
                    TextValorPagar.Text = "";
                    TextValorPagar.Focus();
                    btnConsultar.IsEnabled = true;
                }
                else
                {

                    if (ValorValida > Contexto.PagoLiquidar) /// aqui se debe validar que el usuario no coloque un valor mayor al pago total de la deuda  
                    {

                        Utilitario.PintarImagen(IMensaje, 314, 385, 24, 23, "IMensaje", false, true);
                        Utilitario.PintarControl(LblMensaje, 346, 387, 429, 20, 18, "L", "El valor ingresado es mayor al pago total", "B", true);
                        TextValorPagar.Text = "";
                        TextValorPagar.Focus();
                        btnConsultar.IsEnabled = true;
                    }
                    else
                    {

                           try
                            {
                            Contexto.AmountToPay = ValorValida;
                            Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                            await Task.Delay(200);

                            NavigationService.Navigate(new PageMuestraValorPagar(Contexto));

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
                            TextValorPagar.Text = "";
                            TextValorPagar.Focus();
                            btnConsultar.IsEnabled = true;
                        }


                    }
                }

            }
        }

        private async Task ConsultarPorNumeroCliente(string NumeroCliente)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            CamposPago FacturaConsultada = await Consumo.ConsultarPorNumeroCliente(NumeroCliente);
            TextValorPagar.Text = "";
            Consumo.ValorPagar = 0.ToString("C", Consumo.InfoPais);

            if (FacturaConsultada == null)
            {
                MostrarMensaje("Error en la consulta", "No se pudo obtener la información de la factura.");
                TextValorPagar.Text = "";
                TextValorPagar.Focus();
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
                TextValorPagar.Text = "";
                TextValorPagar.Focus();
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

            if (TextValorPagar.IsVisible)
                TextValorPagar.Focus();
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Contexto.MenuAnt = "PageDigitaValorPagar";
            NavigationService.Navigate(new PageTimeOUT(Contexto));

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

        private void TextValorPagar_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TextValorPagar.Text = string.Format("{0:#,##0.00}", double.Parse(TextValorPagar.Text));


        }

    }

}
