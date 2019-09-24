using Comfandi_.Logica;
using Comfandi_.Modelo;
using Comfandi_.Util;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para Page_Autenticacion.xaml
    /// </summary>
    public partial class Page_Autenticacion : Page
    {
        private readonly Contexto Contexto;

        public Page_Autenticacion(Contexto contexto)
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
                //this.btn_Autenticar.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_AUTENTICACION_btn_Login.ToString());
                this.btn_Volver_IMA.Source = Utilitario.EstablecerImagen(Properties.Settings.Default.Ima_SERVICIOS_btn_Volver.ToString());
                this.btn_Autenticar_IMA.Source = Utilitario.EstablecerImagen(Properties.Settings.Default.Ima_AUTENTICACION_btn_Login.ToString());
                this.btn_AceptoErrores_IMA.Source = Utilitario.EstablecerImagen(Properties.Settings.Default.Ima_SERVICIOS_btn_Aceptar.ToString());
            }
            catch (Exception ex)
            {

            }
        }

        private void txt_IdUsuario_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Teclado.EscribirEnCampo(txt_IdUsuario, MostrarMensajeDesdeElTeclado);
        }

        private void MostrarMensajeDesdeElTeclado(string mensaje)
        {
            txt_Error_Titulo.Text = "Credenciales Inválidas";
            txt_Error_Campos.Text = mensaje;
            ModalErro.IsOpen = true;
        }

        private void MostrarErrores(string titulo, string mensaje)
        {
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            ModalErro.IsOpen = true;
        }

        private void txt_Pass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Teclado.EscribirEnPass((PasswordBox)sender);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Teclado.EscribirEnCampo(txt_IdUsuario, MostrarMensajeDesdeElTeclado);
        }

        private void btn_Volver_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Page_Principal(Contexto));
        }

        private void btn_Volver_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Autenticar_Click(object sender, RoutedEventArgs e)
        {
            var idUsuario = txt_IdUsuario.Text;
            var passUsuario = txt_PassUsuario.Password.ToString();
            if (string.IsNullOrEmpty(idUsuario) || string.IsNullOrEmpty(passUsuario))
            {
                this.MostrarErrores("Credenciales Insuficientes", "Todas las credenciales deben de ser diligenciadas.");
            }
            else
            {
                this.Contexto.Consultar = new Consumo();
                var autenticar = this.Contexto.Consultar.Autenticar(idUsuario, passUsuario);
                if (autenticar)
                {
                    this.Contexto.Tramite = new Tramite() { IdUsuario = idUsuario, ContraseñaUsuario = passUsuario};
                    this.NavigationService.Navigate(new Page_Servicios(Contexto));
                }
                else
                {
                    this.MostrarErrores("Error de autenticación", "Las credenciales dígitadas son inválidas.");
                }
            }
        }

        private void btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
