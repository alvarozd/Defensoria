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
    /// Lógica de interacción para PageLeerCodigobarras.xaml
    /// </summary>
    public sealed partial class PageLeerCodigobarras : Page
    {
        private int Menu = 10;
        private readonly Contexto Contexto;
        private bool BackToMenuPrincipal = false;
        private DispatcherTimer TimerHome1 = new DispatcherTimer();

        public PageLeerCodigobarras(Contexto contexto)
        {
            Contexto = contexto;

            Height = Contexto.Alto;
            Width = Contexto.Ancho;

            InitializeComponent();
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;

            EstablecerImagenes();
            TimerHome1.Tick += new EventHandler(TimerHome1_Tick);
            TimerHome1.Interval = TimeSpan.FromSeconds(Contexto.GetParams().TimeOutInactividadPantallas);
            TimerHome1.Start();
        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                //Utilitario.PintarBoton(Menu, 1, btnConsultaCliente, 422, 67, 185, 150);
                //Utilitario.PintarBoton(Menu, 2, btnConsultaTarjeta, 623, 77, 127, 150);
                //Utilitario.PintarBoton(Menu, 3, btnAtras, 51, 50, 149, 77);


                Utilitario.PintarControl(Consultando, 0, 0, Contexto.Ancho, Contexto.Alto, 0, "C", false);


            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private void MetodoValidacionFormatoTextBox(string nada)
        {
        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TimerHome1.Stop();
            Storyboard story = (Storyboard)FindResource("EncogerImg");
            Image image = (Image)sender;
            image.BeginStoryboard(story);

            await Task.Delay(TimeSpan.FromMilliseconds(300));
            string MenuSig = image.Tag.ToString();
            switch (MenuSig)
            {
                case "Concli":
                    {
                        TimerHome1.Stop();

                        Consumo.InsertarDetalleTransaccionServer("Consulta por número de cliente", "Iniciada");
                        Contexto.TipoConsulta = TipoConsulta.NumerodeCliente;
                        Contexto.CualMenu = "Cliente";
                        Consumo.InsertarEstadisticaServer("Menú Principal", "Consulta por número de cliente", "OK", Consumo.Redondeo);
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(300);
                        NavigationService.Navigate(new PageDigitarNroCliente(Contexto));
                        break;
                    }
                case "ConTar":
                    {
                        TimerHome1.Stop();

                        Consumo.InsertarDetalleTransaccionServer("Consulta por número de Tarjeta", "Iniciada");
                        Contexto.CualMenu = "Tarjeta";
                        Contexto.TipoConsulta = TipoConsulta.NumeroTarjeta;
                        Consumo.InsertarEstadisticaServer("Menú Principal", "Consulta por número de Tarjeta", "OK", Consumo.Redondeo);
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(300);
                        NavigationService.Navigate(new PageDigitarNroCliente(Contexto));
                        break;
                    }
                case "atras":
                    {
                        TimerHome1.Stop();
                        Contexto.MuestraFinaliza = false;
                        Consumo.LoggerInfo("VOLVER DE MENÚ PRINCIPAL");
                        Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                        await Task.Delay(300);
                        NavigationService.Navigate(new MenuPrincipal(Contexto));
                        break;
                    }

                default:
                    {
                        TimerHome1.Stop();

                        MessageBox.Show(image.Name, "NOMBRE BOTÓN");
                        break;
                    }
                    
            }
        }

        private async void CodigoLeido_KeyUp(object sender, KeyEventArgs e)
        {

            String p1, p2, p3, p4, p5, p6, p7, unido = "";

            if (e.Key == Key.Enter)
            {
                TimerHome1.Stop();
                Consumo.Logger.Info("Antes" + CodigoLeido.Text);
                if (CodigoLeido.Text.Contains('$'))
                {
                    Contexto.InfoFactura1 = Contexto.DatoConsulta = CodigoLeido.Text.Trim('$', '%');
                    Contexto.InfoFactura1 = Contexto.DatoConsulta = Contexto.InfoFactura1.Substring(4);
                    if (Contexto.InfoFactura1.Length > 16)
                    {
                        Contexto.InfoFactura1 = Contexto.DatoConsulta = Contexto.InfoFactura1.Remove(8, 5);
                    }

                    if (Contexto.InfoFactura1.Length == 12)
                        Contexto.InfoFactura1 = string.Join("-", Enumerable.Range(0, Contexto.InfoFactura1.Length / 4).Select(i => Contexto.InfoFactura1.Substring(i * 4, 4)));
                    if (Contexto.InfoFactura1.Length == 13)
                    {

                        p1 = Contexto.InfoFactura1.Substring(0, 4);
                        p2 = Contexto.InfoFactura1.Substring(4, 4);
                        p3 = Contexto.InfoFactura1.Substring(8, 4);
                        p4 = Contexto.InfoFactura1.Substring(12, 1);

                        unido = p1 + "-" + p2 + "-" + p3 + "-" + p4;
                    }
                    if (Contexto.InfoFactura1.Length == 14)
                    {

                        p1 = Contexto.InfoFactura1.Substring(0, 4);
                        p2 = Contexto.InfoFactura1.Substring(4, 4);
                        p3 = Contexto.InfoFactura1.Substring(8, 4);
                        p4 = Contexto.InfoFactura1.Substring(12, 2);

                        unido = p1 + "-" + p2 + "-" + p3 + "-" + p4;
                    }
                    if (Contexto.InfoFactura1.Length == 15)
                    {

                        p1 = Contexto.InfoFactura1.Substring(0, 4);
                        p2 = Contexto.InfoFactura1.Substring(4, 4);
                        p3 = Contexto.InfoFactura1.Substring(8, 4);
                        p4 = Contexto.InfoFactura1.Substring(12, 3);

                        unido = p1 + "-" + p2 + "-" + p3 + "-" + p4;
                    }
                    if (Contexto.InfoFactura1.Length == 16)
                    {

                        p1 = Contexto.InfoFactura1.Substring(0, 4);
                        p2 = Contexto.InfoFactura1.Substring(4, 4);
                        p3 = Contexto.InfoFactura1.Substring(8, 4);
                        p4 = Contexto.InfoFactura1.Substring(12, 4);

                        unido = p1 + "-" + p2 + "-" + p3 + "-" + p4;
                    }
                    Contexto.InfoFactura1 = unido;

                    Consumo.Logger.Info("despues de hacer" + Contexto.InfoFactura1);
                    NavigationService.Navigate(new PageCosultando(Contexto));
                }
                else
                {
                    CodigoLeido.Text = "";
                    TimerHome1.Stop();
                    Utilitario.PintarImagen(Consultando1, 0, 0, 1024, 768, "Consultando", false, true);
                    await Task.Delay(300);
                    NavigationService.Navigate(new ValidaCodigoBarras(Contexto));
                }
            }
        }

        private async Task ConsultarPorCodigoBarras(string codigoBarras)
        {
            LeerCodigo.Visibility = Visibility.Collapsed;
            CamposPago FacturaConsultada = await Consumo.ConsultarPorCodigoBarras(codigoBarras);
            CodigoLeido.Text = "";
            Consumo.ValorPagar = 0.ToString("C", Consumo.InfoPais);

            if (FacturaConsultada == null)
            {
                MostrarMensaje("Error en la consulta", "No se obtuvo la información de la factura.");
                CodigoLeido.Text = "";
                CodigoLeido.IsEnabled = true;
                CodigoLeido.Focus();
                return;
            }

            if (FacturaConsultada.ValorPagar <= 0)
            {
                MostrarMensaje("Sin valor a pagar", "No hay deuda para esta factura.");
                CodigoLeido.Text = "";
                CodigoLeido.IsEnabled = true;
                CodigoLeido.Focus();
                return;
            }
            else
            {
                TimerHome1.Stop();
                Contexto.InfoFactura = FacturaConsultada;
                //Contexto.NroCliente = Contexto.InfoFactura.ReferenciaPago.Substring(2,8);
                //Contexto.NroCliente = Contexto.NroCliente.Insert(Contexto.NroCliente.Length - 1, "-");

                Consumo.ValorPagar = Contexto.InfoFactura.ValorPagar.ToString("C", Consumo.InfoPais);
                NavigationService.Navigate(new PageDetallePagoFactura(Contexto));
            }

            LeerCodigo.Visibility = Visibility.Visible;
        }

        private void MostrarMensaje(string titulo, string mensaje)
        {
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Consumo.LoggerInfo("MENÚ LECTURA CÓDIGO DE BARRAS");

            if (CodigoLeido.IsVisible)
                CodigoLeido.Focus();

            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);
        }

        private void TimerHome1_Tick(object sender, EventArgs e)
        {
            TimerHome1.Stop();
            Contexto.MenuAnt = "PageLeerCodigobarras";
            NavigationService.Navigate(new PageTimeOUT(Contexto));
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
        }


        private void CodigoLeido_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (((TextBox)e.Source).Text.Length == 58)
            //    InputSimulator.SimulateKeyPress(VirtualKeyCode.RETURN);
        }

        private void CodigoLeido_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Utilitario.ReiniciarTimer(TimerHome1);
      //      e.Handled = !Utilitario.EsNumero(e.Text);
        }

        //private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        //{
        //    if (e.DataObject.GetDataPresent(typeof(String)))
        //    {
        //        String text = (String)e.DataObject.GetData(typeof(String));
        //        if (!Utilitario.EsNumero(text))
        //        {
        //            e.CancelCommand();
        //        }
        //    }
        //    else
        //    {
        //        e.CancelCommand();
        //    }
        //}

        private void BotonesClick(object sender, RoutedEventArgs e)
        {
            Button boton = (Button)sender;

            TimerHome1.Stop();
            string MenuSig = boton.Tag.ToString();

            switch (MenuSig)
            {
                case "Demo":
                    {
                        if (!string.IsNullOrEmpty(ValorPagar.Text))
                        {
                            Contexto.AmountToPay = Convert.ToDecimal(ValorPagar.Text, Consumo.InfoPais);
                            Contexto.InfoFactura = new CamposPago()
                            {
                                CodigoIacFacturador = "",
                                ReferenciaPago = "",
                                ValorPagar = Convert.ToInt32(ValorPagar.Text, Consumo.InfoPais)
                            };
                            //Contexto.DatoConsulta = "4157707209914253802001089115705814618700390000000000015470";
                            NavigationService.Navigate(new PageDetallePagoFactura(Contexto));
                        }
                        else
                        {
                            MostrarMensaje("Información Incompleta", "Digite el valor a PAGAR para continuar.");
                        }

                        break;
                    }
                default:
                    {
                        MessageBox.Show(boton.Name, $"NOMBRE BOTÓN");
                        break;
                    }

            }

        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            TimerHome1.Stop();
        }
    }

}
