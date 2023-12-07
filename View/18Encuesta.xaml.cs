using FacturasEnel.Logica;
using FacturasEnel.Util;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para MenuPrincipal.xaml
    /// </summary>
    public sealed partial class MenuEncuesta : Page
    {
        private int Menu = 18;
        private Contexto Contexto;
        private int Clics = 1;
        private DispatcherTimer TimerClics = new DispatcherTimer();
        private DispatcherTimer VaMenuPrincipal = new DispatcherTimer();
        String ValorEncuesta = "Sin responder";


        //private DispatcherTimer TimerConciliacion = new DispatcherTimer();

        public MenuEncuesta(Contexto contexto)
        {
            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            this.Contexto = contexto;
            EstablecerImagenes();

            TimerClics.Tick += new EventHandler(TimerClics_Tick);
            TimerClics.Interval = TimeSpan.FromSeconds(Contexto.GetParams().Params.TimeOutInactividadBotonesOcultos);

            VaMenuPrincipal.Tick += new EventHandler(VaMenuPrincipal_Tick);
            VaMenuPrincipal.Interval = TimeSpan.FromSeconds(Contexto.GetParams().TimeOutInactividadPantallas);
            VaMenuPrincipal.Start();


        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");

                //Utilitario.PintarBoton(Menu, 1, btnMala, 158, 250, 179, 213);
                //Utilitario.PintarBoton(Menu, 2, btnRegular, 435, 259, 179, 213);
                //Utilitario.PintarBoton(Menu, 3, btnBuena, 703, 253, 179, 213);
   //             Utilitario.PintarBoton(Menu, 4, btnFinalizar, 419, 613, 196, 92);


            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Consumo.LoggerInfo("MENÚ Encuesta");
        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            string MenuSig = image.Tag.ToString();
            VaMenuPrincipal.Stop();

            if (image.Tag.ToString() != "MenuAdmin")
            {
                MiniCargando.Visibility = Visibility.Visible;
                Storyboard story = (Storyboard)FindResource("EncogerImg");
                image.BeginStoryboard(story);
            }
            await Task.Delay(TimeSpan.FromMilliseconds(100));

            switch (MenuSig)
            {
                case "1":
                    {
                        VaMenuPrincipal.Stop();

                        Utilitario.PintarImagen(ImagenCarita, 171, 251, 158, 216, "mala", false, true);
                        btnMala.Visibility = Visibility.Hidden;
                        btnRegular.Visibility = Visibility.Visible;
                        btnBuena.Visibility = Visibility.Visible;
                        ValorEncuesta = "Mala";
                        Consumo.LoggerInfo("Encuesta Mala");
                        Consumo.InsertarEstadisticaServer("Encuesta", ValorEncuesta, "OK", Consumo.Redondeo);
                        Consumo.InsertarDetalleTransaccionServer(ValorEncuesta, "Respuesta encuesta");

                        btnMala.Visibility = Visibility.Hidden;
                        btnRegular.Visibility = Visibility.Hidden;
                        btnBuena.Visibility = Visibility.Hidden;
                        NavigationService.Navigate(new MenuPrincipal(Contexto));


                        break;
                    }
                case "2":
                    {
                        VaMenuPrincipal.Stop();

                        btnMala.Visibility = Visibility.Visible;
                        btnRegular.Visibility = Visibility.Hidden;
                        btnBuena.Visibility = Visibility.Visible;

                        Utilitario.PintarImagen(ImagenCarita, 435, 251, 173, 232, "regular", false, true);
                        ValorEncuesta = "Regular";

                        Consumo.LoggerInfo("Encuesta Regular");
                        Consumo.InsertarEstadisticaServer("Encuesta", ValorEncuesta, "OK", Consumo.Redondeo);
                        Consumo.InsertarDetalleTransaccionServer(ValorEncuesta, "Respuesta encuesta");

                        btnMala.Visibility = Visibility.Hidden;
                        btnRegular.Visibility = Visibility.Hidden;
                        btnBuena.Visibility = Visibility.Hidden;
                        NavigationService.Navigate(new MenuPrincipal(Contexto));


                        break;
                    }
                case "3":
                    {
                        VaMenuPrincipal.Stop();

                        btnMala.Visibility = Visibility.Visible;
                        btnRegular.Visibility = Visibility.Visible;
                        btnBuena.Visibility = Visibility.Hidden;

                        Utilitario.PintarImagen(ImagenCarita, 699, 248, 163, 222, "buena", false, true);
                        ValorEncuesta = "Buena";
                        Consumo.LoggerInfo("Encuesta Buena");
                        Consumo.InsertarEstadisticaServer("Encuesta", ValorEncuesta, "OK", Consumo.Redondeo);
                        Consumo.InsertarDetalleTransaccionServer(ValorEncuesta, "Respuesta encuesta");

                        btnMala.Visibility = Visibility.Hidden;
                        btnRegular.Visibility = Visibility.Hidden;
                        btnBuena.Visibility = Visibility.Hidden;
                        NavigationService.Navigate(new MenuPrincipal(Contexto));


                        break;
                    }
                case "4":
                    {
                        VaMenuPrincipal.Stop();

                        Consumo.LoggerInfo("Finaliza la encuesta");
                        Consumo.InsertarEstadisticaServer("Encuesta", ValorEncuesta, "OK", Consumo.Redondeo);
                        Consumo.InsertarDetalleTransaccionServer(ValorEncuesta, "Respuesta encuesta");
                        btnMala.Visibility = Visibility.Hidden;
                        btnRegular.Visibility = Visibility.Hidden;
                        btnBuena.Visibility = Visibility.Hidden;
                        NavigationService.Navigate(new MenuPrincipal(Contexto));


                        break;
                    }
                default:
                    {
                        VaMenuPrincipal.Stop();

                        MessageBox.Show(image.Name, "NOMBRE BOTÓN");
                        break;
                    }
            }

        }

        private void TimerClics_Tick(object sender, EventArgs e)
        {
            TimerClics.Stop();
            Clics = 1;
        }

        private void VaMenuPrincipal_Tick(object sender, EventArgs e)
        {
            VaMenuPrincipal.Stop();
            Contexto.MenuAnt = "MenuEncuesta";
            NavigationService.Navigate(new PageTimeOUT(Contexto));
        }

        private void MostrarErrores(string titulo, string mensaje)
        {
            Btn_AceptoErrores.Content = "Aceptar";
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            ModalErro.IsOpen = true;
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
            ModalErro.IsOpen = false;
        }
    }
}
