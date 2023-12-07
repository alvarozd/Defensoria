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
    public sealed partial class ValidaCodigoBarras : Page
    {
        private int Menu = 31;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();



        public ValidaCodigoBarras(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            Contexto = contexto;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(15);
            TimerHome.Start();


        }

        private void EstablecerImagenes()
        {
            try
            {

                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                //Utilitario.PintarBoton(Menu, 1, btnSi, 217, 560, 287, 144);
                //Utilitario.PintarBoton(Menu, 2, btnNo, 523, 560, 287, 144);


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
                case "Si":
                    {

                        TimerHome.Stop();
                        NavigationService.Navigate(new PageLeerCodigobarras(Contexto));

                        break;

                    }
                case "No":
                    {
                        TimerHome.Stop();
                        Consumo.LoggerInfo("TIME OUT BOTON NO");
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

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Consumo.FinTransaccionTimeOut();
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }


    }
}
