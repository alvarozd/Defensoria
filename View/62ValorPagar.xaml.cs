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
    public sealed partial class ValorPagar : Page
    {
        private int Menu = 62;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        bool Edito = false;
        int ValorValida = 0;

        public ValorPagar(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Contexto = contexto;
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(116);
            TimerHome.Start();


        }

        private void EstablecerImagenes()
        {
            try
            {

                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                //Utilitario.PintarBoton(Menu, 12, btnEditar, 759, 561, 176, 50);
                //Utilitario.PintarBoton(Menu, 13, btnCancelar, 469, 652, 178, 72);
                //Utilitario.PintarBoton(Menu, 14, btnAceptar, 717, 651, 178, 72);
                Utilitario.PintarControl(LNumeroCredito, 720, 169, 170, 45, 32, "L", Contexto.NumeroCredito, "B", true);
                Utilitario.PintarControl(LNumeroProducto, 895, 300, 33, 38, 28, "L", Contexto.NumeroProducto, "B", true);
                Utilitario.PintarControl(LValorPagar, 753, 491, 180, 25, 28, "C", Contexto.AmountToPay.ToString("C",Consumo.InfoPais ), "B", true);

                if (Contexto.NumeroProducto == "1")
                {
                    Utilitario.PintarControl(LSaldototal, 753, 391, 180, 25, 28, "C", "$ 287.500.00", "B", true);
                    Contexto.PagoLiquidar = 287500;

                }
                if (Contexto.NumeroProducto == "2")
                {
                    Utilitario.PintarControl(LSaldototal, 753, 391, 180, 25, 28, "C", "$ 177.600.00", "B", true);
                    Contexto.PagoLiquidar = 177600;

                }
                if (Contexto.NumeroProducto == "3")
                {
                    Utilitario.PintarControl(LSaldototal, 753, 391, 180, 25, 28, "C", "$ 424.700.00", "B", true);
                    Contexto.PagoLiquidar = 424700;

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



            switch (MenuSig)
            {
                case "Editar":
                    {
                        Editar();


                        break;

                    }
                case "Aceptar":
                    {
                        TimerHome.Stop();


                        if (Edito)
                        {
                            if ((string.IsNullOrEmpty(TextOtroValor.Text)))
                            {
                                MostrarMensaje("Valor a pagar", "Por favor ingresar un valor pagar");
                            }
                            else
                            {
                                ValorValida = Int32.Parse(TextOtroValor.Text);
                                if (ValorValida <= 99)
                                {
                                    MostrarMensaje("Valor a pagar", "El valor ingresado debe ser mayor a 99 y debe ser múltiplos de 100");
                                    TextOtroValor.Text = "";

                                }
                                else
                                {
                                    if (ValorValida >Contexto.PagoLiquidar) /// aqui se debe validar que el usuario no coloque un valor mayor al pago total de la deuda  
                                    {

                                        MostrarMensaje("Valor a pagar", "El valor ingresado es mayor al saldo total");
                                        TextOtroValor.Text = "";

                                    }
                                    else
                                    {
                                        Contexto.AmountToPay = ValorValida;
                                        Consumo.ValorPagar = Contexto.AmountToPay.ToString("C", Consumo.InfoPais);
                                        NavigationService.Navigate(new PageMenuPago(Contexto));

                                    }
                                }
                            }
                        }
                        else
                        {
                            Consumo.ValorPagar = Contexto.AmountToPay.ToString("C", Consumo.InfoPais);
                            NavigationService.Navigate(new PageMenuPago(Contexto));
                        }


                        break;
                    }
                case "Cancelar":
                    {
                        TimerHome.Stop();
                        NavigationService.Navigate(new MenuPrincipal(Contexto));
                        break;
                    }
                case "Borrar":
                    {
                        TimerHome.Stop();
                        if (TextOtroValor.SelectionStart > 0)
                            InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        break;
                    }

                default:
                    {
                        TimerHome.Stop();
                        Utilitario.ReiniciarTimer(TimerHome);
                        string numero = image.Tag.ToString();
                        InputSimulator.SimulateTextEntry(numero);

                        break;
                    }
            }
        }

        private void Editar()
        {
            Edito = true;
            btnEditar.IsEnabled = false;
            //Utilitario.PintarBoton(Menu, 1, btn1, 419, 342, 72, 71);
            //Utilitario.PintarBoton(Menu, 2, btn2, 493, 342, 72, 71);
            //Utilitario.PintarBoton(Menu, 3, btn3, 568, 341, 72, 71);
            //Utilitario.PintarBoton(Menu, 4, btn4, 419, 416, 72, 71);
            //Utilitario.PintarBoton(Menu, 5, btn5, 493, 415, 72, 71);
            //Utilitario.PintarBoton(Menu, 6, btn6, 568, 417, 72, 71);
            //Utilitario.PintarBoton(Menu, 7, btn7, 419, 489, 72, 71);
            //Utilitario.PintarBoton(Menu, 8, btn8, 493, 490, 72, 71);
            //Utilitario.PintarBoton(Menu, 9, btn9, 568, 488, 72, 71);
            //Utilitario.PintarBoton(Menu, 10, btn10, 420, 565, 72, 71);
            //Utilitario.PintarBoton(Menu, 11, btnBorrar, 494, 562, 150, 77);
            Utilitario.PintarControl(TextOtroValor, 459, 298, 166, 35, 26, "C", true);
            TextOtroValor.Focus();

        }



        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Consumo.FinTransaccionTimeOut();
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //if (TextNumeroDocumento.IsVisible)
            //    TextNumeroDocumento.Focus();
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
            //            NavigationService.Navigate(new ValorPagar(Contexto));
            Mensajes.Visibility = Visibility.Hidden;
            Mensajes.IsOpen = false;

        }



        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            TimerHome.Stop();
        }

        private void TextOtroValor_TextChanged(object sender, TextChangedEventArgs e)
        {
            int ValorLabel = 0;
            try
            {

                if (TextOtroValor.Text != "")
                {
                    ValorLabel = Convert.ToInt32(TextOtroValor.Text);
                    LValorPagar.Content = ValorLabel.ToString("C", Consumo.InfoPais);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex.Message.ToString());
           
            }

        }
    }
}
