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
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageDetallePagoFactura.xaml
    /// </summary>
    public sealed partial class MuestraCreditos : Page
    {
        private int Menu = 61;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();
        String ValorPagar = "";
        int ValorPagarMinimo = 0;

    public MuestraCreditos(Contexto contexto)
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
                //Utilitario.PintarBoton(Menu, 1, btnCancelar, 588, 648, 196, 78);
                if (Contexto.CualMenu == "Cliente")
                {

                    //Utilitario.PintarBoton(Menu, 2, btn1, 436, 198, 571, 88);
                    //Utilitario.PintarBoton(Menu, 3, btn2, 436, 289, 571, 88);
                    //Utilitario.PintarBoton(Menu, 4, btn3, 437, 378, 571, 88);
                }
                else
                {
                    //Utilitario.PintarBoton(Menu, 2, btn1, 436, 198, 571, 88);

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
                case "Consulta":
                    {
                        Utilitario.ReiniciarTimer(TimerHome);

                        Contexto.AmountToPay = 12500;
                        Contexto.NumeroProducto = "1";
                        Contexto.NumeroCredito = "46055759";
                        PanelDescripcionPagos("46055759", "37790144", "Armando Enrique Barbosa", 12500);
                        break;
                    }
                case "Consulta1":
                    {
                        Utilitario.ReiniciarTimer(TimerHome);
                        Contexto.AmountToPay = 14800;
                        Contexto.NumeroProducto = "2";
                        Contexto.NumeroCredito = "46055760";
                        PanelDescripcionPagos("46055760", "37790144", "Armando Enrique Barbosa", 14800);

                        break;
                    }
                case "Consulta2":
                    {
                        Utilitario.ReiniciarTimer(TimerHome);
                        Contexto.AmountToPay = 26500;
                        Contexto.NumeroProducto = "3";
                        Contexto.NumeroCredito = "46055795";
                        PanelDescripcionPagos("46055795", "37790144", "Armando Enrique Barbosa", 26500);

                        break;
                    }
                case "Pagar1":
                    {
                        NavigationService.Navigate(new ValorPagar(Contexto));
                        break;
                    }
                case "Ocultar2":
                    {

                        NavigationService.Navigate(new MuestraCreditos(Contexto));
                        /*Lpaneld1NCre.Visibility = Visibility.Hidden;
                        Lpaneld1NuCli.Visibility = Visibility.Hidden;
                        Lpaneld1ValorPagar.Visibility = Visibility.Hidden;
                        Pagar.Visibility = Visibility.Hidden;
                        Ocultar.Visibility = Visibility.Hidden;
                        Paneldescrip.Visibility = Visibility.Hidden;*/
                        break;
                    }
                case "Cancelar":
                    {
                        TimerHome.Stop();
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

        private void PanelPagos(int numero)
        {


            
            int posicionTop = 194;

            if (numero == 1)
            {
                Utilitario.PintarControl(Lpanel1, 769, 221, 180, 45, 36, "L", "46055759", "B", true);
            }
            if (numero == 2)
            {
                Utilitario.PintarControl(Lpanel1, 769, 221, 180, 45, 36, "L", "46055759", "B", true);
                Utilitario.PintarControl(Lpanel2, 769, 312, 180, 45, 36, "L", "46055760", "B", true);
            }
            if (numero == 3)
            {
                Utilitario.PintarControl(Lpanel1, 769, 221, 180, 45, 36, "L", "46055759", "B", true);
                Utilitario.PintarControl(Lpanel2, 769, 312, 180, 45, 36, "L", "46055760", "B", true);
                Utilitario.PintarControl(Lpanel3, 769, 404, 180, 45, 36, "L", "46055795", "B", true);
            }



            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri($@"Util\PPagosImagen.jpg", UriKind.RelativeOrAbsolute));

            for (var i = 0; i < numero; i++) // The 5 here could be any number
            {
                posicionTop = posicionTop + 31;

                StackPanel sp = new StackPanel();
                sp.Width = 651;
                sp.Height = 91;
                sp.Name = i.ToString();
                Canvas.SetLeft(sp,225);
                Canvas.SetTop(sp, posicionTop);
                sp.Background = ib;
                sp.MouseLeftButtonDown += new MouseButtonEventHandler(StackPanel_MouseLeftButtonDown);
                Mipanel.Children.Add(sp);
                Mipanel.Visibility = Visibility.Visible;

            }


        }


        private void PanelDescripcionPagos(String Credito, String numero, String nombre, int valor)
        {

            //Utilitario.PintarBoton(Menu, 5, Pagar, 571, 533, 114, 46);
            //Utilitario.PintarBoton(Menu, 6, Ocultar, 746, 532, 114, 46);
            Utilitario.PintarControl(Lpaneld1NCre, 758, 339, 173, 44, 36, "L", Credito, "B", true);
            Utilitario.PintarControl(Lpaneld1NuCli, 470, 408, 500, 30, 22, "L", numero + " - " + nombre , "B", true);
            Utilitario.PintarControl(Lpaneld1ValorPagar, 470, 482, 200, 30, 22, "L", valor.ToString("C", Consumo.InfoPais) , "B", true);



            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri($@"Util\PPagosDescrip.jpg", UriKind.RelativeOrAbsolute));

            StackPanel sp = new StackPanel();
            sp.Width = 567;
            sp.Height = 269;
            sp.Background = ib;
            Paneldescrip.Children.Add(sp);
            Paneldescrip.Visibility = Visibility.Visible;

        }


        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
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
            Consumo.InsertarDetalleTransaccionServer(Contexto.DatoConsulta, "Referencia");
            Consumo.LoggerInfo("MENÚ DETALLE DE FACTURA");
        }
    }
}
