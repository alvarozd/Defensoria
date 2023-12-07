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
    public enum TipoDeCampo
    {
        CampoIdUsuario,
        CampoPassUsuario
    }

    public partial class TecladoNumericoLogOn : UserControl
    {
        private TextBox CampoIdUsuario { get; set; }
        private PasswordBox CampoContraseña { get; set; }
        private TipoDeCampo Campo;
        public Action<string> MostrarMensajeDesdeElTeclado { get; private set; }
        private string RutaFiles = Environment.CurrentDirectory + "\\";

        public TecladoNumericoLogOn()
        {
            InitializeComponent();
            EstablecerImagenes();
        }

        private void EstablecerImagenes()
        {
            //string ruta = RutaFiles + Properties.Settings.Default.Ima_AUTENTICACION_btn_Eliminar_Estado_1.ToString();
            //ImagenEliminar.Source = new BitmapImage(new Uri(ruta, UriKind.Absolute));
        }

        public void EscribirEnCampo(TextBox campo, Action<string> mostrarMensajeDesdeElTeclado)
        {
            this.CampoIdUsuario = campo;
            this.Campo = TipoDeCampo.CampoIdUsuario;
            this.MostrarMensajeDesdeElTeclado = mostrarMensajeDesdeElTeclado;
        }

        public void EscribirEnPass(PasswordBox campopass)
        {
            this.CampoContraseña = campopass;
            this.Campo = TipoDeCampo.CampoPassUsuario;
        }


        private void AgregarTecla(object sender, RoutedEventArgs e)
        {
            var tecla = (Button)sender;
            var valorTeclaSeleccionada = tecla.Content.ToString();
            if (this.Campo == TipoDeCampo.CampoIdUsuario) // El campo de texto id Usuario esta seleccionado
            {
                var nuevoValorDelCampo = CampoIdUsuario.Text + valorTeclaSeleccionada;
                var cantidadCaracteres = nuevoValorDelCampo.Length;
                if (cantidadCaracteres < 11)
                {
                    CampoIdUsuario.Text = nuevoValorDelCampo;
                }
                else
                {
                    MostrarMensajeDesdeElTeclado("El número de documento no puede ser mayor a 11 dígitos.");
                }
            }
            else // El campo de contraseña esta seleccionado
            {
                var valorCampoActual = CampoContraseña.Password.ToString();
                this.CampoContraseña.Password = valorCampoActual + valorTeclaSeleccionada;
            }
        }

        private void EliminarTecla(object sender, RoutedEventArgs e)
        {
            if (this.Campo == TipoDeCampo.CampoIdUsuario) // El campo de texto id Usuario esta seleccionado
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
            else
            {
                if (CampoContraseña.Password.Length > 0)
                {
                    var nuevovalor = CampoContraseña.Password.Remove(CampoContraseña.Password.Length - 1, 1);
                    CampoContraseña.Password = nuevovalor;
                }
            }

        }

        private void btn_Eliminar_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //string ruta = RutaFiles + Properties.Settings.Default.Ima_AUTENTICACION_btn_Eliminar_Estado_2.ToString();
            //ImagenEliminar.Source = new BitmapImage(new Uri(ruta, UriKind.Absolute));
        }

        private void btn_Eliminar_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //string ruta = RutaFiles + Properties.Settings.Default.Ima_AUTENTICACION_btn_Eliminar_Estado_1.ToString();
            //ImagenEliminar.Source = new BitmapImage(new Uri(ruta, UriKind.Absolute));
        }
    }
}
