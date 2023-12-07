using FacturasEnel.Util;
using System.Windows.Controls;

namespace FacturasEnel.View.Componentes
{
    /// <summary>
    /// Lógica de interacción para Cargando.xaml
    /// </summary>
    public sealed partial class EntregandoCambio : UserControl
    {
        public EntregandoCambio()
        {
            InitializeComponent();
            Fondo.Source = Utilitario.EstablecerImagen(@"Util\Mn03.jpg");
        }
    }
}
