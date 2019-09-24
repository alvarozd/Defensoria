using Comfandi_.Modelo;
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
    /// Lógica de interacción para Page_Secundaria.xaml
    /// </summary>
    public partial class Page_Servicios : Page
    {
        private readonly Contexto Contexto;

        public Page_Servicios(Contexto contexto)
        {
            InitializeComponent();
            EstablecerImagenes();
            this.Contexto = contexto;
        }

        private void EstablecerImagenes()
        {
            try
            {
                this.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_SERVICIOS_Fondo.ToString());
                this.btn_ImprimirCredito.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_SERVICIOS_btn_ImprimirCreditos.ToString());
                this.btn_Pre_Liquidacion.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_SERVICIOS_btn_PreLiquidacion.ToString());
                this.btn_Imprimir_Tarjeta.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_SERVICIOS_btn_ImprimirTarjeta.ToString());
                this.btn_Consulta_PreAprobado.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_SERVICIOS_btn_Consulta_PreAprobado.ToString());
                this.btn_ConsultaSaldo.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_SERVICIOS_btn_Consulta_Saldo.ToString());
                this.btn_Consulta_UltimoPago.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_SERVICIOS_btn_Consulta_VisualizarUltimoPago.ToString());
                this.btn_ActualizacionDatos.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_SERVICIOS_btn_ActualizarDatos.ToString());
                this.btn_Volver_IMA.Source= Utilitario.EstablecerImagen(Properties.Settings.Default.Ima_SERVICIOS_btn_Volver.ToString());
                this.btn_Autenticar_IMA.Source = Utilitario.EstablecerImagen(Properties.Settings.Default.Ima_AUTENTICACION_btn_Login.ToString());
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_Volver_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Page_Principal(this.Contexto));
        }

        private void btn_ImprimirCredito_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCickServicio(object sender, RoutedEventArgs e)
        {
            var Servicio = (Button)sender;
            var opcion = Servicio.Name.ToString();
            switch (opcion) {
                case "btn_ImprimirCredito":
                    this.Contexto.Tramite.Servicio = TipoServicio.ImprimirCredito;
                    break;
                case "btn_Pre_Liquidacion":
                    this.Contexto.Tramite.Servicio = TipoServicio.ImprimirPreLiquidacion;
                    this.RedireccionAlTramite();
                    break;
                case "btn_Imprimir_Tarjeta":
                    this.Contexto.Tramite.Servicio = TipoServicio.ImprimirTarjetaComfandi;
                    this.RedireccionAlTramite();
                    break;
                case "btn_Consulta_PreAprobado":
                    this.Contexto.Tramite.Servicio = TipoServicio.ConsultaPreAprobado;
                    break;
                case "btn_ConsultaSaldo":
                    this.Contexto.Tramite.Servicio = TipoServicio.ConsultaSaldo;
                    this.ConsultarContraseña();
                    break;
                case "btn_Consulta_UltimoPago":
                    this.Contexto.Tramite.Servicio = TipoServicio.ConsultaUltimoPago;
                    this.RedireccionAlTramite();
                    break;
                case "btn_ActualizacionDatos":
                    this.Contexto.Tramite.Servicio = TipoServicio.ActualizaciónDatos;
                    this.ConsultarContraseña();
                    break;
            }
        }

        private void RedireccionAlTramite() {
            this.NavigationService.Navigate(new Page_Servicio_Componente(this.Contexto));
        }

        private void ConsultarContraseña() {
            this.DeshabilitarServicios();
            this.HabilitarSolicitudContraseña();
        }

        private void HabilitarSolicitudContraseña()
        {

            this.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_PRINCIPAL_Fondo.ToString());
            this.Teclado.Visibility = Visibility.Visible;
            this.lb_Contraseña.Visibility = Visibility.Visible;
            this.txt_PassUsuario.Visibility = Visibility.Visible;
            this.btn_Autenticar.Visibility = Visibility.Visible;
        }

        private void DeshabilitarServicios()
        {
            this.btn_ImprimirCredito.Visibility = Visibility.Hidden;
            this.btn_Pre_Liquidacion.Visibility = Visibility.Hidden;
            this.btn_Imprimir_Tarjeta.Visibility = Visibility.Hidden;
            this.btn_Consulta_PreAprobado.Visibility = Visibility.Hidden;
            this.btn_ConsultaSaldo.Visibility = Visibility.Hidden;
            this.btn_Consulta_UltimoPago.Visibility = Visibility.Hidden;
            this.btn_ActualizacionDatos.Visibility = Visibility.Hidden;
        }

        private void btn_Autenticar_Click(object sender, RoutedEventArgs e)
        {
            var passUsuario = txt_PassUsuario.Password.ToString();
            if (string.IsNullOrEmpty(passUsuario))
            {
                this.MostrarErrores("Credenciales Insuficientes", "Todas las credenciales deben de ser diligenciadas.");
            }
            else
            {
                var autenticar = this.Contexto.Consultar.Autenticar(this.Contexto.Tramite.IdUsuario, passUsuario);
                if (autenticar)
                {
                    this.NavigationService.Navigate(new Page_Servicio_Componente(Contexto));
                }
                else
                {
                    this.MostrarErrores("Error de autenticación", "Las credenciales dígitadas son inválidas.");
                }
            }
        }

        private void MostrarErrores(string titulo, string mensaje)
        {
            throw new NotImplementedException();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Teclado.EscribirEnPass(txt_PassUsuario);
        }

        private void txt_Pass_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Teclado.EscribirEnPass((PasswordBox)sender);
        }
    }
}
