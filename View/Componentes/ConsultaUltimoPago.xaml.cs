using Comfandi_.Modelo;
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

namespace Comfandi_.View.Componentes
{
    /// <summary>
    /// Lógica de interacción para ConsultaUltimoPago.xaml
    /// </summary>
    public partial class ConsultaUltimoPago : UserControl
    { 
        public ConsultaUltimoPago()
        {
            InitializeComponent();
        }

        public void PintarInformacion(Respuesta respuestaConsumo)
        {
            this.lb_Identificacion.Content = respuestaConsumo.Id;
            this.lb_NombreUsuario.Content = respuestaConsumo.NombreUsuario;
            this.lb_Fecha.Content = respuestaConsumo.Fecha;
            this.lb_Valor.Content = respuestaConsumo.ValorConsulta;
        }
    }
}
