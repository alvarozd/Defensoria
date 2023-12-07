using ENEL.Modelo;
using ENEL.View.Componentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ENEL.Logica
{

    public class Impresion
    {
        public void ImpresionServicioConsulta()
        {
            //CanvasImpresion win2 = new CanvasImpresion(this.Tramite);

            //win2.PintarInformacion();
            //win2.Show();



            ////win2.PintarInformacion();
            //PrintDialog dialog = new PrintDialog();
            ////win2.Left = -3000;
            ////var encoder = new BmpBitmapEncoder();
            ////win2.SaveUsingEncoder(win2.Ima_CodBarras_Horizontal_JPG, "Pruebakk.bmp", encoder);
            //dialog.PrintVisual(win2.ComponentesHorizontales, "");
            //win2.Close();



            //CanvasImpresion win2 = new CanvasImpresion(this.Tramite);
            //PrintDialog dialog = new PrintDialog();
            ////win2.Left = -3000;
            //if (Tramite.Servicio == TipoServicio.ImprimirCredito || Tramite.Servicio == TipoServicio.ImprimirPreLiquidacion || Tramite.Servicio == TipoServicio.ImprimirTarjetaComfandi)
            //{
            //    win2.ComponentesHorizontales.Visibility = Visibility.Visible;
            //    win2.PintarInformacion();
            //    win2.Show();
            //    dialog.PrintVisual(win2.ComponentesHorizontales, "");
            //}
            //else
            //{
            //    win2.ComponentesVerticales.Visibility = Visibility.Visible;
            //    win2.PintarInformacion();
            //    win2.Show();
            //    dialog.PrintVisual(win2.ComponentesVerticales, "");
            //}
            //win2.Close();



        }


        public void ImpersionTurno()
        {

        }

    }
}

//using Falabella_Demo.Conexion;
//using Falabella_Demo.Impresora;
//using Falabella_Demo.Modelo;
//using Falabella_Demo.Util;
//using LibreriaDepenciasApi_Sockets.Modelo;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using NLog;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Configuration;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;
//using VirtualCash.Business;
//using VirtualCash.Business.Services;

//namespace Falabella_Demo.View
//{
//    /// <summary>
//    /// Lógica de interacción para PaginaPago.xaml
//    /// </summary>
//    public partial class PaginaPago : Page
//    {
//        private readonly Contexto contexto;
//        private Thread HiloEspera;
//        private bool CancelarTransaccion = false;
//        private static Logger Logger = LogManager.GetCurrentClassLogger();
//        private bool desHabilitarHilo = true;
//        private decimal ValorAPagar { get; set; }
//        private decimal ValorRecibido { get; set; }
//        private decimal ValorDevolucion { get; set; }
//        public decimal AmountToPay { get; set; }
//        public Dictionary<decimal, int> InitBalance { get; set; } = new Dictionary<decimal, int>();
//        public Dictionary<decimal, int> BCRLoadBalance { get; set; } = new Dictionary<decimal, int>();
//        public event PropertyChangedEventHandler PropertyChanged;
//        private bool SeRealizoTransaccion;
//        private bool MonederoLibre;
//        private EstadoTransaccion FinalizacionTransaccion { get; set; }
//        private bool SeHaceDevolucionDinero { get; set; }
//        private bool EnUsoMonedero { get; set; }


//        public void NotifyPropertyChanged(string propName)
//        {
//            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
//        }


//        public PaginaPago(Contexto contexto)
//        {
//            Logger.Info("Inicializando Página Pago");
//            InitializeComponent();
//            this.contexto = contexto;
//            EstablecerProceso();
//        }

//        private void NavegarProcesandoTransaccion()
//        {
//            Thread.Sleep(5000);
//            this.Dispatcher.Invoke(() =>
//            {
//                this.NavigationService.Navigate(new Page_ProcesandoPago(contexto));
//            });
//        }

//        private void EstablecerProceso()
//        {
//            Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_PAGO_Fondo_Procesando.ToString());
//            lb_Procesando.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_PAGO_Mensaje_Procesando.ToString());
//            flashPlayer_Pago.SourcePath = new FileInfo(Properties.Settings.Default.Ima_PAGO_Animacion.ToString()).FullName;
//            flashPlayer_Pago.Visibility = Visibility.Hidden;
//            AnimacionProcesandoTransaccion.SourcePath = new FileInfo(Properties.Settings.Default.Flash_RealizandoTransaccion.ToString()).FullName;
//            AnimacionProcesandoTransaccion.Visibility = Visibility.Hidden;
//            AnimacionDevolucion.SourcePath = new FileInfo(Properties.Settings.Default.Ima_PAGO_Animacion_Devolucion.ToString()).FullName;
//        }

//        public void EstablecerImagenes()
//        {
//            var flujoTarjeta = contexto.FlujoTarjeta;
//            string fondoPantalla = "";
//            if (flujoTarjeta)
//            {
//                fondoPantalla = Properties.Settings.Default.Ima_PAGO_Fondo_Tarjeta.ToString();
//            }
//            else
//            {
//                fondoPantalla = Properties.Settings.Default.Ima_PAGO_Fondo.ToString();
//            }
//            Background = Utilitario.ObtenerFondo(fondoPantalla);
//            btn_Cancelar.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_PAGO_Cancelar.ToString());
//            btn_AceptoErrores.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_CONSULTACONESTADOCUENTA_Aceptar.ToString());
//            this.btn_FinalizarImpresion.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_PAGO_btn_FinzalizarImpresion.ToString());
//            flashPlayer_Pago.Visibility = Visibility.Visible;
//        }

//        public void EstablecerValorAPagar()
//        {
//            var valorAPagar = contexto.parametros.ValorFinalAPagar;
//            lb_ValorAPagar.Content = contexto.parametros.RedondearValorMinimo(valorAPagar);
//            lb_ValorRecibido.Content = contexto.parametros.RedondearValorMinimo(0);
//            lb_ValorFaltante.Content = contexto.parametros.RedondearValorMinimo(0);
//        }

//        private void btn_Imprimir(object sender, RoutedEventArgs e)
//        {
//            this.contexto.Impresora.UpdateEvent += Impresora_UpdateEvent;
//            this.contexto.Impresora.FinishEvent += Impresora_FinishEvent;
//            this.contexto.Impresora.ErrorEvent += Impresora_ErrorEvent;
//            var imprimir = new Imprimir()
//            {
//                Titulo = "Prueba",
//                Subtitulo = "Subtitulo",
//                Fecha = "Fecha",
//                TextoCajero = "TextoCajero",
//                NumeroTarjeta = "123456XXXXX4120",//$"{this.contexto.datosconsulta.NumeroTarjetaAImprimir}",
//                NombreCliente = "NombreCliente",
//                MontoAPagar = $"{lb_ValorAPagar.Content}",
//                Vuelto = "456.60",
//                MontoRecibido = "Efectivo",
//            };
//            imprimir.AgregarMensajeFinal($"Solicito Repactación En: 1 cuota ok de CAmilo beltran".ToUpper());
//            imprimir.AgregarMensajeFinal($"Verifique que su pago sea igual al impote de maquina registradora".ToUpper());
//            imprimir.AgregarMensajeFinal($"Este documento no es un ticket bajo el reglamento de comprobantes de pago".ToUpper());
//            var datosImprimir = imprimir.ObtenerDatosImpresion();
//            this.contexto.Impresora.Imprimir(datosImprimir);
//        }

//        private void FinalizarSuscripcionImpresora()
//        {
//            this.contexto.Impresora.UpdateEvent -= Impresora_UpdateEvent;
//            this.contexto.Impresora.FinishEvent -= Impresora_FinishEvent;
//            this.contexto.Impresora.ErrorEvent -= Impresora_ErrorEvent;
//        }

//        private void Impresora_ErrorEvent(object sender, string e)
//        {
//            //MessageBox.Show(e.ToString() + "Err");
//        }

//        private void Impresora_FinishEvent(object sender, string e)
//        {
//            //MessageBox.Show(e.ToString()+"Fini");
//        }

//        private void Impresora_UpdateEvent(object sender, OposPrinterLib.STATUS_UPDATE e)
//        {
//            //MessageBox.Show(e.ToString()+"Upda");
//            //throw new NotImplementedException();
//        }

//        private void btn_Cancelar_Click(object sender, RoutedEventArgs e)
//        {
//            Logger.Info("Transacción Cancelada");
//            this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", $"El usuario ha cancelado la transacción, validando devolución.", $"{contexto.parametros.ValorFinalAPagar}", "Cancelando Transacción");
//            this.btn_Cancelar.IsEnabled = false;
//            Task<bool>.Run(() =>
//            {
//                Logger.Info("Virtual Cash Inicializa Cancelación Transacción");
//                return this.contexto.VirtualCash.CancelTransaction();
//            }).ContinueWith((respuetaTarea) =>
//            {
//                if (respuetaTarea.IsFaulted)
//                {
//                    Logger.Error($"Error btn_Cancelar_Click: {respuetaTarea.Exception.Message}");
//                }
//                if (respuetaTarea.Result)
//                {
//                    Logger.Info("Transacción Cancelada");
//                    CancelarTransaccion = true;
//                    this.FinalizacionTransaccion = EstadoTransaccion.TransaccionCancelada;
//                }
//                else
//                {
//                    this.Dispatcher.Invoke(() =>
//                    {
//                        this.btn_Cancelar.IsEnabled = false;
//                    });
//                }
//            });
//        }

//        private async void btn_Pagar_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                this.btn_Pagar.IsEnabled = false;
//                this.btn_Cancelar.IsEnabled = false;
//                await this.contexto.VirtualCash.EnableToAcept(contexto.parametros.ValorFinalAPagar);
//            }
//            catch (Exception ex)
//            {
//                Logger.Error($"Error Window_Loaded: {ex.ToString()}");
//            }
//        }

//        private async void Page_Loaded(object sender, RoutedEventArgs e)
//        {
//            try
//            {

//                HiloEspera = new Thread(AnimarProcesando);
//                HiloEspera.Start();
//                Logger.Info("Inicia el proceso de recaudo");
//                await Task.Run(() => { this.EsperarPorMonedero(); });
//                this.contexto.VirtualCash.TransactionFinished += VirtualCash_TransactionFinished;
//                this.contexto.VirtualCash.MoneyInTransactionChanged += VirtualCash_MoneyInTransactionChanged;
//                this.contexto.VirtualCash.AcceptedCompleted += VirtualCash_AcceptedCompleted;
//                this.contexto.VirtualCash.SS75BCRDisposed += VirtualCash_SS75BCRDisposed;
//                await Task.Run(() => { return this.contexto.VirtualCash.EnableToAcept(contexto.parametros.ValorFinalAPagar); })
//                    .ContinueWith((task) =>
//                    {
//                        if (task.IsCompleted)
//                        {
//                            var idVirtualcash = task.Result;
//                            Logger.Info($"El id de virtual cash es {idVirtualcash}");
//                            this.FinalizacionTransaccion = EstadoTransaccion.TransaccionExitosa;
//                            this.contexto.Transaccion.IdVirtualCash = idVirtualcash;
//                            this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", $"El id de Transacción se virtual cash se ha establecido", $"{this.ValorAPagar}", "Entregando Cambio");
//                        }
//                        else
//                        {
//                            Logger.Error($"Se genero un error al obtener el Id de virtual Cash.");
//                        }

//                    });
//                HabilitarInterfaz();
//            }
//            catch (Exception ex)
//            {
//                Logger.Error($"Se genero un error {ex}");
//                Logger.Error($"Error Window_Loaded: {ex.ToString()}");
//            }
//        }

//        private void EsperarPorMonedero()
//        {
//            var EstaEnUsoMonedero = this.contexto.VirtualCash.IsBCRRunning();
//            do
//            {
//                if (EstaEnUsoMonedero)
//                {
//                    EstaEnUsoMonedero = this.contexto.VirtualCash.IsBCRRunning();
//                    Thread.Sleep(1000);
//                    Logger.Info($"Esperando a que el monedero se detenga");
//                }
//            } while (EstaEnUsoMonedero);
//        }

//        private void VirtualCash_SS75BCRDisposed(object sender, EventArgs e)
//        {
//            Logger.Info($"Se detecta deshabilitación de monedero");
//            if (this.FinalizacionTransaccion == EstadoTransaccion.TransaccionFallida)
//            {
//                this.MonederoLibre = true;
//                Logger.Info("Esperando por el monedero.");
//            }
//        }

//        private void VirtualCash_AcceptedCompleted(object sender, decimal e) //Des-Habilita el botón de cancelar transacción cuando el monto ingresado es igual o mayor al valor a pagar
//        {
//            Logger.Info("Se ha recaudado el Monto A Pagar, Se procederá a la devolución");
//            Dispatcher.Invoke(() =>
//            {
//                this.btn_Cancelar.IsEnabled = false;
//            });
//        }

//        private void VirtualCash_MoneyInTransactionChanged(object sender, VirtualCash.Business.Model.MoneyInTransaction ev)
//        {
//            try
//            {
//                if (ev.MovementType == VirtualCash.Business.Model.MovementType.OUT)//Se esta realizando la transacción y se esta devolviendo
//                {
//                    if (!this.SeHaceDevolucionDinero)
//                    {
//                        this.SeHaceDevolucionDinero = true;
//                        this.Dispatcher.Invoke(() =>
//                        {
//                            this.flashPlayer_Pago.Visibility = Visibility.Hidden;
//                            this.AnimacionDevolucion.Visibility = Visibility.Visible;
//                        });
//                    }
//                    this.ValorDevolucion = ev.ToReturn;
//                    Logger.Info("Los dispositivos estan realizando devolución de dinero");
//                    Logger.Info($"Monto Pagado:{this.ValorAPagar}, Monto Recibido: {this.ValorRecibido}, Monto Pendiente: {ev.Remaining}, Monto a Devolver: {ev.ToReturn}, Monto Retornado: {ev.Returned}");
//                    this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", $"Monto a Devolver: {ev.ToReturn}, Monto Retornado: {ev.Returned}", $"{this.ValorAPagar}", "Entregando Cambio");
//                    Dispatcher.Invoke(() =>
//                    {
//                        DeshabilitarEtiquetasAceptacion();
//                        HabilitarEtiquetasDevolucion(true);
//                        this.lb_ValorARetornar.Content = contexto.parametros.RedondearValorMinimo(ev.ToReturn);
//                        this.lb_ValorPendientePorDevolver.Content = contexto.parametros.RedondearValorMinimo(ev.Returned);
//                        this.btn_Cancelar.IsEnabled = ev.CanCancel;
//                    });
//                }
//                if (ev.MovementType == VirtualCash.Business.Model.MovementType.IN)//Se esta realizando la transacción y se esta recaudando
//                {
//                    Logger.Info("Los dispositivos estan realizando recepciónn de dinero");
//                    this.ValorAPagar = ev.AmountToAccept;
//                    this.ValorRecibido = ev.Entered;
//                    this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", $"Monto Recibido: {this.ValorRecibido}, Monto Pendiente: {ev.Remaining}", $"{this.ValorAPagar}", "Recaudando");
//                    Logger.Info($"Monto A Pagar:{this.ValorAPagar}, Monto Recibido: {this.ValorRecibido}, Monto Pendiente: {ev.Remaining}");
//                    Dispatcher.Invoke(() =>
//                    {
//                        this.lb_ValorAPagar.Content = contexto.parametros.RedondearValorMinimo(ev.AmountToAccept);
//                        this.lb_ValorRecibido.Content = contexto.parametros.RedondearValorMinimo(ev.Entered);
//                        this.lb_ValorFaltante.Content = contexto.parametros.RedondearValorMinimo(ev.Remaining);
//                        this.btn_Cancelar.IsEnabled = ev.CanCancel;
//                    });
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Error($"Se ha generado un error al modificar los montos de pago:  {ex } ");
//            }
//        }

//        private void VirtualCash_TransactionFinished(object sender, VirtualCash.Business.Model.TransactionMovements e)
//        {
//            try
//            {
//                this.contexto.VirtualCash.MoneyInTransactionChanged -= VirtualCash_MoneyInTransactionChanged;
//                switch (FinalizacionTransaccion)
//                {
//                    case EstadoTransaccion.TransaccionExitosa:
//                        Logger.Info("El proceso de recaudo y devolución de dinero ha finalizado.");
//                        Logger.Info($"Los valores Monto Pagado: {this.ValorAPagar}, Monto Ingresado: {this.ValorRecibido}, Devolución: {this.ValorDevolucion}");
//                        this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", "El recaudo ha finalizado exitosamente", $"{this.ValorAPagar}", "Recaudo Finalizado");
//                        var task = Task.Run(() => { Thread.Sleep(3000); });
//                        task.Wait();
//                        RealizarTransaccion();
//                        break;
//                    case EstadoTransaccion.TransaccionFallida:
//                        Logger.Info("El proceso de devolución de dinero ha finalizado, por transacción Fallida.");
//                        Logger.Info($"Los valores Monto Pagado: {this.ValorAPagar}, Monto Ingresado: {this.ValorRecibido}, Devolución: {this.ValorDevolucion}");
//                        this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", "La devolución ha sido exitosa", $"{this.ValorAPagar}", "Devolución Finalizada");
//                        this.Dispatcher.Invoke(() =>
//                        {
//                            DeshabilitarAnimaciónDePago();
//                        });
//                        break;
//                    case EstadoTransaccion.TransaccionCancelada:
//                        Logger.Info("La transacción ha finalizado exitosamente por cancelación del usuario.");
//                        Logger.Info($"Los valores Monto Pagado: {this.ValorAPagar}, Monto Ingresado: {this.ValorRecibido}, Devolución: {this.ValorDevolucion}");
//                        this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", $"La transacción ha sido finalizada por el usuario", $"{this.ValorAPagar}", "Transacción Cancelada");
//                        this.contexto.Transaccion.NombreUsuario = "Usuario Pruebas";
//                        this.contexto.Transaccion.NumeroTarjeta = this.contexto.datosconsulta.NumeroTarjetaAImprimir;
//                        this.contexto.Transaccion.ValorAPagar = this.contexto.parametros.RedondearValorMinimo(this.ValorAPagar);
//                        this.contexto.Transaccion.ValorRecibido = this.contexto.parametros.RedondearValorMinimo(this.ValorRecibido);
//                        this.contexto.Transaccion.ValorDevolucion = this.contexto.parametros.RedondearValorMinimo(this.ValorDevolucion);

//                        this.Dispatcher.Invoke(() =>
//                        {
//                            this.AnimacionDevolucion.Visibility = Visibility.Hidden;
//                            this.Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_PAGO_Fondo_Cancelar.ToString());
//                            this.DeshabilitarPorCancelacion();
//                            this.Imprimir();
//                            //Task.Run(() => Imprimir());
//                            //var HiloCANCELACION = new Thread(DetenerIn);
//                            //HiloCANCELACION.Start();
//                        });
//                        break;
//                }
//            }
//            catch (Exception ex)
//            {
//                Logger.Error("Error Al finalizar la transacción: {@ex},  {@t}", ex, this.contexto.Transaccion);
//            }
//        }

//        private void RealizarTransaccion()
//        {
//            try
//            {
//                this.HabilitarEtiquetasDevolucion(false);
//                this.HabilitarAnimacion();
//                this.GenerarConsultaFalabella();
//            }
//            catch (Exception ex)
//            {
//                Logger.Error("Se ha generado un error al realizar la transacción, Exactamente: " + ex);
//            }
//        }

//        private async void GenerarConsultaFalabella()
//        {
//            var consulta = new ConexionApi();
//            Logger.Info("Iniciando proceso de grabación de la transacción en el core de falabella.");
//            await Task.Run(() =>
//            {
//                return consulta.GrabarTransaccion();
//            })
//                .ContinueWith((task) =>
//                {
//                    if (task.IsCompleted && task.Status == TaskStatus.RanToCompletion)
//                    {
//                        Logger.Info("La consulta a falabella ha sido exita");
//                        SeRealizoTransaccion = task.Result;
//                        this.Dispatcher.Invoke(() =>
//                        {
//                            DeshabilitarAnimaciónDePago();
//                        });
//                    }
//                    else if (task.IsCanceled || task.IsFaulted)
//                    {
//                        Logger.Error("Se ha generado un error al grabar la transacción en el core de falabella");
//                        this.FinalizacionTransaccion = EstadoTransaccion.TransaccionFallida;
//                        this.Dispatcher.Invoke(() =>
//                        {
//                            this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", "Se ha generado un error al registrar la transacción se procederá a la dispensación del dinero.", $"{this.ValorAPagar}", "Transacción Exitosa");
//                            MostrarError(this.contexto.parametros.Mensajes["10024"], this.contexto.parametros.Mensajes["10023"]);
//                        });
//                    }
//                });
//        }

//        private void MostrarError(string titulo, string mensaje)
//        {
//            txt_Error_Titulo.Text = titulo;
//            txt_Error_Campos.Text = mensaje;
//            ModalErro.IsOpen = true;
//        }

//        private async void DeshabilitarAnimaciónDePago()
//        {
//            this.AnimacionProcesandoTransaccion.Visibility = Visibility.Hidden;
//            switch (this.FinalizacionTransaccion)
//            {
//                case EstadoTransaccion.TransaccionExitosa:
//                    if (SeRealizoTransaccion)
//                    {
//                        Logger.Info("La transacción fue exitosa y ha sido registrada en el core de Falabella", $"{this.ValorAPagar}");
//                        this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", "La transacción fue exitosa y ha sido registrada en el core de Falabella", $"{this.ValorAPagar}", "Transacción Exitosa");
//                        this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", "Pago registrado exitosamente  en Falabella.", $"{this.ValorAPagar}", "Transacción Exitosa");
//                        Background = Utilitario.ObtenerFondo(Properties.Settings.Default.Ima_DEVOLUCION_FondoExi.ToString());
//                        this.contexto.Transaccion.NombreUsuario = "Usuario Pruebas";
//                        this.contexto.Transaccion.NumeroTarjeta = this.contexto.datosconsulta.NumeroTarjetaAImprimir;
//                        this.contexto.Transaccion.ValorAPagar = this.contexto.parametros.RedondearValorMinimo(this.ValorAPagar);
//                        this.contexto.Transaccion.ValorRecibido = this.contexto.parametros.RedondearValorMinimo(this.ValorRecibido);
//                        this.contexto.Transaccion.ValorDevolucion = this.contexto.parametros.RedondearValorMinimo(this.ValorDevolucion);
//                        this.Imprimir();
//                        //await Task.Run(() =>
//                        //{
//                        //    this.Imprimir();
//                        //    Thread.Sleep(3000);
//                        //    this.Dispatcher.Invoke(() =>
//                        //    {
//                        //        if (this.contexto.FlujoTarjeta)
//                        //        {
//                        //            Logger.Info("---------------------------------------------------Finalizando Transacción Con Tarjeta CRM Exitosamente----------------------------------------------------------------------------");
//                        //        }
//                        //        else
//                        //        {
//                        //            Logger.Info("---------------------------------------------------Finalizando Transacción Con Estado de Cuenta Exitosamente------------------------------------------------------------------------");
//                        //        }
//                        //        NavigationService.Navigate(new PaginaPrincipal(contexto));
//                        //    });
//                        //});
//                    }
//                    else
//                    {
//                        Logger.Info("La transacción fue fallida, y no ha podido ser registrada en el core de Falabella");
//                        this.FinalizacionTransaccion = EstadoTransaccion.TransaccionFallida;
//                        this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", $"Pago no registrado en Falabella, Se procederá a la devolución de {this.ValorAPagar}", $"{this.ValorAPagar}", "Pago Rechazado");
//                        //this.HabilitarEtiquetasDevolucion(true);
//                        this.Dispatcher.Invoke(() =>
//                        {
//                            this.lb_Espere.Visibility = Visibility.Visible;
//                            this.AnimacionDevolucion.Visibility = Visibility.Visible;
//                            MostrarError(this.contexto.parametros.Mensajes["10024"], this.contexto.parametros.Mensajes["10025"] + " " + this.contexto.parametros.RedondearValorMinimo(this.ValorAPagar));
//                        });
//                    }
//                    break;
//                case EstadoTransaccion.TransaccionFallida:
//                    this.contexto.Transaccion.NombreUsuario = "Usuario Pruebas";
//                    this.contexto.Transaccion.NumeroTarjeta = this.contexto.datosconsulta.NumeroTarjetaAImprimir;
//                    this.contexto.Transaccion.ValorAPagar = this.contexto.parametros.RedondearValorMinimo(this.ValorAPagar);
//                    this.contexto.Transaccion.ValorRecibido = this.contexto.parametros.RedondearValorMinimo(this.ValorRecibido);
//                    this.contexto.Transaccion.ValorDevolucion = this.contexto.parametros.RedondearValorMinimo(this.ValorDevolucion + this.ValorAPagar);
//                    this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", "La transacción fue exitosa y ha sido registrada en el core de Falabella", $"{this.ValorAPagar}", "Transacción Exitosa");
//                    this.contexto.VPS.InsertarDetalle(this.contexto.Transaccion.Id, $"{this.contexto.Transaccion.IdVirtualCash}", "Pago registrado exitosamente  en Falabella.", $"{this.ValorAPagar}", "Transacción Exitosa");
//                    this.Dispatcher.Invoke(() =>
//                    {
//                        this.lb_Mensaje.Text = "Devolución exitosa.";
//                    });
//                    Imprimir();
//                    //await Task.Run(() =>
//                    //{
//                    //    Imprimir();
//                    //    Thread.Sleep(3000);
//                    //    this.Dispatcher.Invoke(() =>
//                    //    {
//                    //        Logger.Info("Finalizando proceso de impresion");
//                    //        if (this.contexto.FlujoTarjeta)
//                    //        {
//                    //            Logger.Info("---------------------------------------------------Finalizando Transacción Con Tarjeta CRM - ESTADO FALLIDA----------------------------------------------------------------------------");
//                    //        }
//                    //        else
//                    //        {
//                    //            Logger.Info("---------------------------------------------------Finalizando Transacción Con Estado de Cuenta - ESTADO FALLIDA------------------------------------------------------------------------");
//                    //        }
//                    //        NavigationService.Navigate(new PaginaPrincipal(contexto));
//                    //    });
//                    //});
//                    break;
//                case EstadoTransaccion.TransaccionCancelada:
//                    break;
//            }
//        }

//        private void HabilitarAnimacion()
//        {
//            try
//            {
//                Dispatcher.Invoke(() =>
//                {
//                    this.btn_Cancelar.Visibility = Visibility.Hidden;
//                    this.AnimacionDevolucion.Visibility = Visibility.Hidden;
//                    this.flashPlayer_Pago.Visibility = Visibility.Hidden;
//                    this.AnimacionProcesandoTransaccion.Visibility = Visibility.Visible;
//                });

//            }
//            catch (Exception ex)
//            {
//                Logger.Error($"Se ha generado un error al habilitar la animación de procesando pago, Exactamente : {ex}");
//            }
//        }

//        private void AnimarProcesando()
//        {
//            var activarInicio = false;
//            while (desHabilitarHilo)
//            {
//                if (activarInicio)
//                {
//                    this.Dispatcher.Invoke(() =>
//                        lb_Procesando.Visibility = Visibility.Collapsed
//                    );
//                }
//                else
//                {
//                    this.Dispatcher.Invoke(() =>
//                        lb_Procesando.Visibility = Visibility.Visible
//                    );
//                }
//                activarInicio = !activarInicio;
//                Thread.Sleep(1000);
//            }
//            this.Dispatcher.Invoke(() =>
//            lb_Procesando.Visibility = Visibility.Collapsed
//            );
//        }

//        private void Page_Unloaded(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                this.contexto.VirtualCash.TransactionFinished -= VirtualCash_TransactionFinished;
//                this.contexto.VirtualCash.MoneyInTransactionChanged -= VirtualCash_MoneyInTransactionChanged;
//                this.contexto.VirtualCash.AcceptedCompleted -= VirtualCash_AcceptedCompleted;
//                this.contexto.VirtualCash.SS75BCRDisposed -= VirtualCash_SS75BCRDisposed;
//                this.contexto.VirtualCash.DisableLogTx();
//                Logger.Info("Página dode pago deshabilitada.");
//            }
//            catch (Exception ex)
//            {
//                Logger.Error($"Error Window_Loaded: {ex.ToString()}");
//            }
//        }

//        private void DeshabilitarEtiquetasAceptacion()
//        {
//            this.lb_ValorAPagar.Visibility = Visibility.Collapsed;
//            this.lb_ValorRecibido.Visibility = Visibility.Collapsed;
//            this.lb_ValorFaltante.Visibility = Visibility.Collapsed;
//            this.lb_texto_ValorAPagar.Visibility = Visibility.Collapsed;
//            this.lb_texto_ValorIngresado.Visibility = Visibility.Collapsed;
//            this.lb_texto_ValorPendiente.Visibility = Visibility.Collapsed;
//        }

//        private async void btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
//        {
//            try
//            {
//                var estaOcupadoMonedero = this.contexto.VirtualCash.IsBCRRunning();
//                if (estaOcupadoMonedero)
//                {
//                    while (!MonederoLibre)
//                    {
//                        await Task.Delay(TimeSpan.FromSeconds(1));
//                        Logger.Info($"Esperando a deshabilitación de monedero.");
//                    }
//                }
//                this.lb_Mensaje.Text = "En proceso de devolución.";
//                Logger.Info($"Virtual Cash empieza el proceso de devolución de dinero para la transacción {this.contexto.Transaccion.IdVirtualCash}");
//                await Task.Run(() => this.contexto.VirtualCash.ReverseTx(this.contexto.Transaccion.IdVirtualCash));
//            }
//            catch (Exception ex)
//            {
//                Logger.Info($"Se genero un error al mandar a pagar Virtual Cash, Error: {ex}");
//            }
//        }

//        private void RedireccionarPaginaPrincipal()
//        {
//            var mensaje = "";
//            if (this.contexto.FlujoTarjeta)
//            {
//                mensaje = "Finalizando Transacción Con Tarjeta CRM ";
//            }
//            else
//            {
//                mensaje = "Finalizando Transacción Con Estado de Cuenta ";
//            }
//            switch (this.FinalizacionTransaccion)
//            {
//                case EstadoTransaccion.TransaccionExitosa:
//                    mensaje += "- Transacción Exitosa";
//                    break;
//                case EstadoTransaccion.TransaccionFallida:
//                    mensaje += "- Transacción Fallida";
//                    break;
//                case EstadoTransaccion.TransaccionCancelada:
//                    mensaje += "- Transacción Cancelada";
//                    break;
//            };
//            Logger.Info($"---------------------------------------------------{mensaje}----------------------------------------------------------------------------");
//            this.Dispatcher.Invoke(() =>
//            {
//                this.NavigationService.Navigate(new PaginaPrincipal(contexto));
//            });

//        }
//    }
//}