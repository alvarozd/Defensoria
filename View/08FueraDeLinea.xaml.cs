using FacturasEnel.Logica;
using FacturasEnel.Util;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageFueradeLinea.xaml
    /// </summary>
    public sealed partial class PageFueradeLinea : Page
    {
        private int Menu = 8;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerMenuPrincipal = new DispatcherTimer();
        private int Clics = 1;
        private DispatcherTimer TimerClics = new DispatcherTimer();

        public PageFueradeLinea(Contexto contexto, string page, string descripcion, bool enviarCorreo)
        {
            Consumo.LoggerInfo("INGRESO A PANTALLA FUERA DE LÍNEA");
            this.Contexto = contexto;
            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            EstablecerImagenes();

            TimerMenuPrincipal.Tick += new EventHandler(TimerMenuPrincipal_Tick);
            TimerMenuPrincipal.Interval = new TimeSpan(0, 0, Contexto.GetParams().TimeOutInactividadPantallas);

            TimerClics.Tick += new EventHandler(TimerClics_Tick);
            TimerClics.Interval = TimeSpan.FromSeconds(Contexto.GetParams().Params.TimeOutInactividadBotonesOcultos);

            //TimerMenuPrincipal.Start();
            Consumo.InsertarEstadisticaServer(page, descripcion.Replace("'", "''"), "Fuera de línea", "");
            if (enviarCorreo)
            {
                Consumo.EnviarEmail("Fuera de línea", $"Menú: {page}<br>Descripción: {descripcion.Replace("'", "''")}", page);
            }

        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
        //        Utilitario.PintarBoton(Menu, 1, volverFL, 398, 568, 263, 96);
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            string MenuSig = image.Tag.ToString();

            switch (MenuSig)
            {
                case "MenuAdmin":
                    {
                        if (Clics == Contexto.GetParams().Params.ClickBotonOculto)
                        {
                            Clics = 1;
                            TimerClics.Stop();
                            Consumo.InsertarEstadisticaServer("Fuera de línea", "Ingreso Menú Administración", "OK", "0");
                            NavigationService.Navigate(new PageIngresoAdmin(Contexto, false));
                        }
                        else
                        {
                            Utilitario.ReiniciarTimer(TimerClics);
                            Clics++;
                        }
                        break;
                    }
                case "volverfl":
                    {
                        TimerMenuPrincipal.Stop();
                        Contexto.MuestraFinaliza = false;
                        Consumo.LoggerInfo("VOLVER DE MENÚ PRINCIPAL DESDE FUERA DE LINEA");
                        NavigationService.Navigate(new MenuPrincipal(Contexto));

                        break;

                    }
            }
            }

        private void TimerClics_Tick(object sender, EventArgs e)
        {
            TimerClics.Stop();
            Clics = 1;
        }

        private void TimerMenuPrincipal_Tick(object sender, EventArgs e)
        {
            TimerMenuPrincipal.Stop();
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);
        }
    }
}
