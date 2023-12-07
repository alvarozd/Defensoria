using Carulla.Logica;
using Carulla.Util;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Carulla.View
{
    /// <summary>
    /// Lógica de interacción para Page_Ayuda.xaml
    /// </summary>
    public partial class Page_PagoCodBarras : Page
    {
        private int Menu = 13;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public Page_PagoCodBarras(Contexto contexto)
        {
            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            this.Contexto = contexto;
            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(Contexto.Params.TimeOutInactividadPantallas);
            TimerHome.Start();
        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo(string.Format(@"Util\mn{0:00}.jpg", Menu));
                Utilitario.PintarBoton(Menu, 0, btnAyuda, 1228, 515, 129, 125, "Ayuda");
                Utilitario.PintarBoton(Menu, 1, btnCancelar, 524, 589, 319, 97);

                Utilitario.PintarControl(LblConvenio, 355, 258, 360, 60, 34, "R", Contexto.ConsultaResponse.Convenio);
                Utilitario.PintarControl(LblReferencia, 355, 330, 360, 60, 34, "R", Contexto.ConsultaResponse.Referencia);
                Utilitario.PintarControl(LblValorPagar, 355, 394, 360, 60, 34, "R", Contexto.ConsultaResponse.MontoOperacion.ToString("C", Consumo.InfoPais));
                Utilitario.PintarControl(LblValorIngresado, 355, 459, 360, 60, 34, "R", 0.ToString("C", Consumo.InfoPais));
                Utilitario.PintarControl(LblCambio, 355, 519, 360, 60, 34, "R", 0.ToString("C", Consumo.InfoPais));
            }
            catch (Exception ex)
            {

            }
        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            Storyboard story = (Storyboard)FindResource("EncogerImg");
            Image image = sender as Image;
            image.BeginStoryboard(story);

            await Task.Delay(TimeSpan.FromMilliseconds(300));

            string MenuSig = ((Image)sender).Tag.ToString();
            switch (MenuSig)
            {
                case "Ayuda":
                    {
                        this.NavigationService.Navigate(new Page_Ayuda(this.Contexto));
                        break;
                    }
                case "Cancelar":
                    {
                        this.NavigationService.Navigate(new Page_TransaccionCancelada(this.Contexto));
                        break;
                    }
            }
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            if (!Contexto.Consultando)
            {
                TimerHome.Stop();
                this.NavigationService.Navigate(new MenuPrincipal(this.Contexto));
            }
        }

    }
}
