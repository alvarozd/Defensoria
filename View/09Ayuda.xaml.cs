using FacturasEnel.Logica;
using FacturasEnel.Util;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageAyuda.xaml
    /// </summary>
    public sealed partial class PageAyuda : Page
    {
        private readonly Contexto Contexto;
        private int Menu = 9;
        private int MenuAyuda;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public PageAyuda(Contexto contexto, int menuayuda)
        {
            this.Contexto = contexto;
            Height = Contexto.Alto;
            Width = Contexto.Ancho;
            MenuAyuda = menuayuda;

            InitializeComponent();
            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(Contexto.GetParams().TimeOutInactividadPantallas);
            TimerHome.Start();
        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\Ayuda\mn{MenuAyuda:00}.jpg");
                Utilitario.PintarBoton(Menu, 0, btnVolver, 6, 654, 357, 79, "Volver", false, true);
                //Utilitario.PintarBoton(Menu, 0, btnPrintOK, 6, 0, 357, 79, "Volver");
                //Utilitario.PintarBoton(Menu, 0, btnPrintError, 6, 300, 357, 79, "Volver");
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
                case "ERROR":
                    {
                        break;
                    }
                case "OK":
                    {
                        //Contexto.ImprimirReciboPendiente();
                        break;
                    }
                case "Volver":
                    {
                        NavigationService.GoBack();
                        break;
                    }
                default:
                    {
                        MessageBox.Show(image.Name, "NOMBRE BOTÓN");
                        break;
                    }
            }
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            NavigationService.GoBack();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);
        }
    }
}
