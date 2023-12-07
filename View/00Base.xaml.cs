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
    /// Lógica de interacción para PageBase.xaml
    /// </summary>
    public sealed partial class PageBase : Page
    {
        private int Menu = 0;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public PageBase(Contexto contexto)
        {
            Height = contexto.Alto;
            Width = contexto.Ancho;
            this.Contexto = contexto;

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
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                Utilitario.PintarBoton(Menu, 0, btnVolver, 6, 654, 357, 79, "Volver", false, true);
                Utilitario.PintarBoton(Menu, 0, btnAyuda, 1228, 515, 129, 125, "Ayuda", false, true);
                Utilitario.PintarBoton(Menu, 0, btnMenuPrincipal, 1007, 652, 355, 80, "MenuPrincipal", false, true);
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            Storyboard story = (Storyboard)FindResource("EncogerImg");
            Image image = (Image)sender;
            image.BeginStoryboard(story);

            await Task.Delay(TimeSpan.FromMilliseconds(300));

            string MenuSig = image.Tag.ToString();
            switch (MenuSig)
            {
                case "MenuPrincipal":
                    {
                        Consumo.FinTransaccionMenuPrincipal();
                        this.NavigationService.Navigate(new MenuPrincipal(Contexto));
                        break;
                    }
                case "Ayuda":
                    {
                        this.NavigationService.Navigate(new PageAyuda(Contexto, Menu));
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
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }

    }
}
