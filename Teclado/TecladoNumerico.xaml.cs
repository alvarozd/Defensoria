using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FacturasEnel.Teclado
{
    /// <summary>
    /// Lógica de interacción para tecladoNumerico.xaml
    /// </summary>
    /// 

    public sealed partial class TecladoNumerico : UserControl
    {
        private TextBox CampoIdUsuario { get; set; }
        public Action<string> MostrarMensajeDesdeTeclado { get; private set; }
        public int CantidadCaracteres { get; set; }
        //private string RutaFiles = Environment.CurrentDirectory + "\\";

        public TecladoNumerico()
        {
            InitializeComponent();
            EstablecerImagenes();
            //btn_Eliminar_MouseLeave(null, null);
        }

        private void EstablecerImagenes()
        {
            //string ruta = Properties.Settings.Default.Ima_AUTENTICACION_btn_Eliminar_Estado_1.ToString();
            //ImagenEliminar.Source = new BitmapImage(new Uri(ruta, UriKind.RelativeOrAbsolute));
        }

        public void EscribirCampo(TextBox campo, Action<string> mostrarMensajeDesdeTeclado, int cantidadCaracteres)
        {
            this.CampoIdUsuario = campo;
            this.CantidadCaracteres = cantidadCaracteres;
            this.MostrarMensajeDesdeTeclado = mostrarMensajeDesdeTeclado;
        }

        private void AgregarTecla(object sender, RoutedEventArgs e)
        {
            var tecla = (Button)sender;
            var valorTeclaSeleccionada = tecla.Content.ToString();
            var nuevoValorDelCampo = CampoIdUsuario.Text + valorTeclaSeleccionada;
            var cantidadCaracteres = nuevoValorDelCampo.Length;
            if (cantidadCaracteres < this.CantidadCaracteres)
            {
                CampoIdUsuario.Text = nuevoValorDelCampo;
            }
            else
            {
                MostrarMensajeDesdeTeclado($"El campo actual no permite más de {CantidadCaracteres-1} caracteres.");
            }

        }

        private void EliminarTecla(object sender, RoutedEventArgs e)
        {
            if (CampoIdUsuario.Text.Length > 0)
            {
                var posicionActualCampo = CampoIdUsuario.SelectionStart - 1;
                if (posicionActualCampo >= 0)
                {
                    CampoIdUsuario.Text = CampoIdUsuario.Text.Remove(posicionActualCampo, 1);
                    CampoIdUsuario.SelectionStart = posicionActualCampo;
                }
                if (posicionActualCampo == -1)
                {
                    CampoIdUsuario.Text = CampoIdUsuario.Text.Remove(CampoIdUsuario.Text.Length - 1, 1);
                    CampoIdUsuario.SelectionStart = CampoIdUsuario.Text.Length;
                }
            }
        }

        private void Btn_Eliminar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //string ruta = RutaFiles + Properties.Settings.Default.Ima_AUTENTICACION_btn_Eliminar_Estado_2.ToString();
            //ImagenEliminar.Source = new BitmapImage(new Uri(ruta, UriKind.Absolute));
        }

        private void Btn_Eliminar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //string ruta = RutaFiles + Properties.Settings.Default.Ima_AUTENTICACION_btn_Eliminar_Estado_1.ToString();
            //ImagenEliminar.Source = new BitmapImage(new Uri(ruta, UriKind.Absolute));
        }
    }
}
