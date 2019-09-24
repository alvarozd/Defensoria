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
    /// Lógica de interacción para Page_Servicio_Componente.xaml
    /// </summary>
    public partial class Page_Servicio_Componente : Page
    {
        private readonly Contexto Contexto;

        public Page_Servicio_Componente(Contexto contexto)
        {
            InitializeComponent();
            this.Contexto = contexto;
            EstablecerImagenes();
        }

        private void EstablecerImagenes()
        {
            try
            {
                this.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_SERVICIO_Fondo.ToString());
                this.btn_Volver_IMA.Source= Utilitario.EstablecerImagen(Properties.Settings.Default.Ima_SERVICIOS_btn_Volver.ToString());
                this.btn_Imprimir_IMA.Source = Utilitario.EstablecerImagen(Properties.Settings.Default.Ima_SERVICIO_btn_Imprimir.ToString());
                this.btn_MenuPrincipal_IMA.Source = Utilitario.EstablecerImagen(Properties.Settings.Default.Ima_SERVICIO_btn_MenuPrincipal.ToString());
                this.AnimaciónEspera.SourcePath = new FileInfo(Properties.Settings.Default.Ima_PRINCIPAL_Animacion_Espera.ToString()).FullName;
            }
            catch (Exception ex)
            {

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ConsultarInformacion();
            //Poner la animación de Consulta
            //Esperar Respuesta
            //Si hay error
            //Si es correcto
        }

        private async void ConsultarInformacion()
        {
            await Task.Run(() =>
            {
                return this.ConsultaServicio();
            }).ContinueWith((task) =>
            {
                if (task.IsCanceled || task.IsFaulted)
                {
                    MostrarError();
                }
                else
                {
                    var respuesta = task.Result;
                    this.Contexto.Tramite.RespuestaConsumo = respuesta;
                    PintarControl();
                }
            });
        }

        private Respuesta ConsultaServicio()
        {
            switch (this.Contexto.Tramite.Servicio)
            {
                case TipoServicio.ImprimirCredito:
                    return this.Contexto.Consultar.ConsultarUltimoPago();
                case TipoServicio.ImprimirPreLiquidacion:
                    return this.Contexto.Consultar.ConsultarUltimoPago();
                case TipoServicio.ImprimirTarjetaComfandi:
                    return this.Contexto.Consultar.ConsultarUltimoPago();
                case TipoServicio.ConsultaPreAprobado:
                    return this.Contexto.Consultar.ConsultarPreAprobado();
                case TipoServicio.ConsultaSaldo:
                    return this.Contexto.Consultar.ConsultarUltimoPago();
                case TipoServicio.ConsultaUltimoPago:
                    return this.Contexto.Consultar.ConsultarUltimoPago();
                case TipoServicio.ActualizaciónDatos:
                    return this.Contexto.Consultar.ActualizarDatos();
                default:
                    return new Respuesta();
            }
        }

        private void PintarControl()
        {
            this.Dispatcher.Invoke(() =>
            {
                this.AnimaciónEspera.Visibility = Visibility.Hidden;
                switch (this.Contexto.Tramite.Servicio)
                {
                    case TipoServicio.ImprimirCredito:
                        this.ImprimirCredito.Visibility = Visibility.Visible;
                        break;
                    case TipoServicio.ImprimirPreLiquidacion:
                        this.ImprimirPreliquidacion.Visibility = Visibility.Visible;
                        break;
                    case TipoServicio.ImprimirTarjetaComfandi:
                        this.ImprimirTarjetaComfandi.Visibility = Visibility.Visible;                        
                        break;
                    case TipoServicio.ConsultaPreAprobado:
                        this.ConsultaPreAprobado.Visibility = Visibility.Visible;
                        this.ConsultaPreAprobado.PintarInformacion(this.Contexto.Tramite.RespuestaConsumo);
                        this.ActivarBotones();
                        break;
                    case TipoServicio.ConsultaSaldo:
                        this.ConsultaSaldo.Visibility = Visibility.Visible;
                        this.ConsultaSaldo.PintarInformacion(this.Contexto.Tramite.RespuestaConsumo);
                        this.ActivarBotones();
                        break;
                    case TipoServicio.ConsultaUltimoPago:
                        this.ConsultaUltimoPago.Visibility = Visibility.Visible;
                        this.ConsultaUltimoPago.PintarInformacion(this.Contexto.Tramite.RespuestaConsumo);
                        this.ActivarBotones();
                        break;
                    case TipoServicio.ActualizaciónDatos:
                        this.ActualizarDatos.Visibility = Visibility.Visible;
                        this.ActualizarDatos.PintarInformacion(this.Contexto.Tramite.RespuestaConsumo);
                        break;
                }
            });
        }

        private void ActivarBotones() {

            this.btn_Imprimir.Visibility = Visibility.Visible;
            this.btn_Volver.Visibility = Visibility.Visible;
            this.btn_MenuPrincipal.Visibility = Visibility.Visible;
        }

        private void MostrarError()
        {

        }

        private void btn_Volver_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Page_Servicios(this.Contexto));
        }

        private void btn_Imprimir_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_MenuPrincipal_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Page_Principal(this.Contexto));
        }
    }
}
