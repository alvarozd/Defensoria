using Comfandi_.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Comfandi_.View
{
    /// <summary>
    /// Lógica de interacción para Page_Principal.xaml
    /// </summary>
    public partial class Page_Principal : Page
    {
        private readonly Contexto Contexto;

        public Page_Principal(Contexto contexto)
        {
            InitializeComponent();
            EstablecerImagenes();
            this.Contexto = contexto;
        }

        private void EstablecerImagenes()
        {
            try
            {
                this.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_PRINCIPAL_Fondo.ToString());
                this.btn_Turnos.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_PRINCIPAL_Turno.ToString());
                this.btn_Otros_Servicios.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_PRINCIPAL_OtrosServicios.ToString());
            }
            catch (Exception ex)
            {
                
            }
        }

        private void btn_Turnos_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Otros_Servicios_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Page_Autenticacion(this.Contexto));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
