using FacturasEnel.Util;
using System.Windows.Controls;

namespace FacturasEnel.View.Componentes
{
    /// <summary>
    /// Lógica de interacción para Cargando.xaml
    /// </summary>
    public sealed partial class Consultando : UserControl
    {
        public Consultando()
        {
            InitializeComponent();
            Fondo.Source = Utilitario.EstablecerImagen(@"Util\Mn02.jpg");

            Canvas.SetTop(Carga, 235);
            Canvas.SetLeft(Carga, 840);

        }
    }
}
