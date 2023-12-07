using FacturasEnel.Util;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.ComponentModel;
using FacturasEnel.Logica;
using System.Threading;
using System.IO;
using CTM;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageMenuPago.xaml
    /// </summary>
    public sealed partial class PageMenuPago : Page
    {
        private int Menu = 12;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();
        private DispatcherTimer TimerProcesar = new DispatcherTimer();
        //public byte[] StatusEvent = new byte[1];
        private int ContinuarEnPago = 0;
        private int PagoSinDevolucion = 0;

        public PageMenuPago(Contexto contexto)
        {
            Contexto = contexto;
            Height = Contexto.Alto;
            Width = Contexto.Ancho;

            InitializeComponent();
            //Mensajes.Height = Contexto.Alto;
            //Mensajes.Width = Contexto.Ancho;
            EstablecerImagenes();

            TimerHome.Tick += new EventHandler(TimerHome_TickAsync);
            TimerHome.Interval = TimeSpan.FromSeconds(Contexto.GetParams().TimeOutInactividadPantallas);

            TimerProcesar.Tick += new EventHandler(TimerProcesar_TickAsync);
            TimerProcesar.Interval = TimeSpan.FromSeconds(18);


        }

        private async void TimerProcesar_TickAsync(object sender, EventArgs e)
        {
            TimerProcesar.Stop();
            Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.RecaudoCompleto;
            Consumo.LoggerInfo("Usuario continua procesando pago abono sin devolución por time out");
            Contexto.AmountToPay = Contexto.Entered;
            Contexto.VCash.TransactionComplete += VirtualCash_TransactionFinished;
            Contexto.VCash.Procesarpago();
        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                Utilitario.PintarBoton(Menu, 0, btnCancelar, 587, 641, 200, 88, "Cancelar", false, true);

                //Utilitario.PintarControl(LblReferencia, 713, 304, 344, 60, 30, "R", Contexto.DatoConsulta,"",true);
                Utilitario.PintarControl(LblValorIngresado, 690, 292, 282, 48, 22, "L", 0.ToString("C", Consumo.InfoPais), "B", true);
                Utilitario.PintarControl(LblRestan, 690, 389, 282, 48, 22, "L", 0.ToString("C", Consumo.InfoPais), "B", true);
                Utilitario.PintarControl(LblCambio, 690, 485, 282, 48, 22, "L", 0.ToString("C", Consumo.InfoPais), "B", true);
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            btnCancelar.IsEnabled = false;
            //TimerHome.Stop();
            Storyboard story = (Storyboard)FindResource("EncogerImg");
            Image image = (Image)sender;
            image.BeginStoryboard(story);

            await Task.Delay(TimeSpan.FromMilliseconds(300));

            string MenuSig = image.Tag.ToString();
            switch (MenuSig)
            {
                case "Aceptar":
                    {
                        TimerHome.Stop();
                        TimerProcesar.Stop();
                        Contexto.IMPTipoFlujo = "IngresoIgualSinCambio";

                        Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.RecaudoCompleto;
                        Consumo.LoggerInfo("Usuario continua procesando pago abono sin devolución e ingresando cantidad inferior ");
                        Contexto.AmountToPay = Contexto.Entered;
                        Contexto.VCash.TransactionComplete += VirtualCash_TransactionFinished;
                        Contexto.VCash.Procesarpago();
                    }
                    break;
                case "Cancelar":
                    {
                        Consumo.LoggerInfo("CANCELANDO POR BOTÓN CANCELAR");
                        await CancelarTransaccionAsync(12);
                    }
                    break;
                case "Continuar":
                    {
                        if (ContinuarEnPago == 1)
                        {
                            Consumo.LoggerInfo($"USUARIO CONFIRMA SEGUIR EN EL PAGO");
                            ContinuarEnPago = 0;
                            CancelarImage.Visibility = Visibility.Hidden;
                            btnContinuar.Visibility = Visibility.Hidden;
                            btnCancelar2.Visibility = Visibility.Hidden;
                            Utilitario.ReiniciarTimer(TimerHome);
                        }

                        if (Contexto.EstadoTransaccionActual == Contexto.EstadoTransaccion.RecaudoCompleto)
                        {
                            TimerHome.Stop();
                            ContinuarEnPago = 2;
                            btnCancelar.IsEnabled = false;
                        }
                        else
                        {
                            btnCancelar.IsEnabled = true;
                        }
                    }
                    break;
                case "Cancelar2":
                    {
                        Consumo.LoggerInfo("CANCELANDO POR BOTÓN CANCELAR EN POPUP");
                        await CancelarTransaccionAsync(19);
                    }
                    break;


                case "Procesar":
                    {
                        TimerProcesar.Stop();

                        Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.RecaudoCompleto;
                        Consumo.LoggerInfo("Usuario continua procesando pago abono sin devolución ");
                        Contexto.AmountToPay = Contexto.Entered;
                        Contexto.VCash.TransactionComplete += VirtualCash_TransactionFinished;
                        Contexto.VCash.Procesarpago();
                    }
                    break;
                case "Cancelar3":
                    {

                        //                        Contexto.Entered = 0;
                        TimerHome.Stop();
                        TimerProcesar.Stop();
                        Consumo.LoggerInfo("CANCELANDO POR BOTÓN CANCELAR EN POPUP");
                        await CancelarTransaccionAsync(32);

                        /*                        Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.CanceladaNoCambio;
                                                Contexto.VCash.TransactionComplete += VirtualCash_TransactionFinished;
                                                Contexto.VCash.Procesarpago();
                                                Consumo.LoggerInfo("CANCELANDO POR BOTÓN CANCELAR");
                                                Consumo.ValorPagar = 0.ToString("C");
                                                //Contexto.Cambio = 0;

                                                Contexto.ImprimirRecibo(true);
                                                Utilitario.PintarImagen(Finaliza, 0, 0, 1024, 768, "Finaliza", false, true);
                                                await Task.Delay(10000);*/

                    }
                    break;



            }
        }


        #region METODOS VIRTUAL CASH

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);

            Consumo.InsertarEstadisticaServer("Menu Recaudo", $"Inciando recaudo {Consumo.ValorPagar}", "OK", Consumo.Redondeo);

            Utilitario.PintarControl(LblValorPagar, 705, 186, 228, 48, 32, "C", Consumo.ValorPagar , "B", true);

            TimerHome.Start();

            if (Contexto.PagoReal)
            {
                try
                {
                    Consumo.LoggerInfo("INICIA EL PROCESO DE RECAUDO DE DINERO");

                    AgregarEventosVCash(true);

                    await Task.Run(() => { return Contexto.VCash.CTM_AcceptCash(Contexto.AmountToPay); })
                        .ContinueWith((task) =>
                        {
                            if (task.IsCompleted && !task.IsFaulted)
                            {
                                Contexto.IdUltimaTransaccion = task.Result;
                                Consumo.LoggerInfo($"ID VirtualCash: {Contexto.IdUltimaTransaccion}");
                                Consumo.IdVCash = Contexto.IdUltimaTransaccion.ToString(Consumo.InfoPais);
                                Consumo.InsertarDetalleTransaccionServer($"ID Transacción Virtual Cash: {Contexto.IdUltimaTransaccion}", "ID VirtualCash creado");
                            }
                            else
                            {
                                Consumo.Logger.Error(task.Exception.InnerException, "Error generando ID VirtualCash.");
                                Dispatcher.Invoke(() =>
                                {
                                    TimerHome.Stop();
                                    AgregarEventosVCash(false);
                                    NavigationService.Navigate(new PageFueradeLinea(Contexto, Title, "Error generando ID VirtualCash", true));
                                });
                            }

                        });
                }
                catch (Exception ex)
                {
                    Consumo.Logger.Error(ex, "Menú Pagos Iniciando Aceptación");
                }
            }
        }

        private void AgregarEventosVCash(bool agregar)
        {
            if (agregar)
            {
                if (!Contexto.VCash.AbonoTotal)
                {
                    Contexto.VCash.CashAcceptComplete += VirtualCash_AcceptedCompleted;
                    Contexto.VCash.TransactionComplete += VirtualCash_TransactionFinished;
                }
                Contexto.VCash.CashAccept += VirtualCash_MoneyInTransactionChanged;
                Contexto.VCash.DispenseError += VCash_DispenseError;
                //Contexto.VCash.DeviceError += Ctm_DeviceError;
                //Contexto.VCash.DeviceStatus += Ctm_DeviceStatus;  
                Consumo.LoggerInfo("Eventos agregados.");
            }
            else
            {
                if (!Contexto.VCash.AbonoTotal)
                {
                    Contexto.VCash.CashAcceptComplete -= VirtualCash_AcceptedCompleted;
                    Contexto.VCash.TransactionComplete -= VirtualCash_TransactionFinished;
                }
                Contexto.VCash.CashAccept -= VirtualCash_MoneyInTransactionChanged;
                Contexto.VCash.DispenseError -= VCash_DispenseError;
                Consumo.LoggerInfo("Eventos retirados.");
            }
        }

        private void VCash_DispenseError(object sender, (int cambio, int dispensado) e)
        {
            Consumo.Logger.Error($"VCash_DispenseError Cambio: {e.cambio}, entregado: {e.dispensado}, faltante: {e.cambio - e.dispensado}");
            PagoSinDevolucion = 17;
            TimerProcesar.Interval = TimeSpan.FromMilliseconds(10);
            Utilitario.ReiniciarTimer(TimerProcesar);
        }

        private void VirtualCash_MoneyInTransactionChanged(object sender, CTMAcceptEvent ev)
        {
            try
            {
                Dispatcher.Invoke(() =>
                {
                    Contexto.Entered += ev.cashUnit.denomination;
                    Utilitario.ReiniciarTimer(TimerHome);
                    LblValorIngresado.Content = Contexto.Entered.ToString("C", Consumo.InfoPais);

                    if (Contexto.Entered >= Contexto.AmountToPay)
                    {
                        Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.RecaudoCompleto;
                        TimerHome.Stop();
                                    // ESCONDE EL POPUP POQUE MIENTRAS ESTABA VISIBLE SE RECAUDÓ EL DINERO
                                    CancelarImage.Visibility = Visibility.Hidden;
                        btnContinuar.Visibility = Visibility.Hidden;
                        btnCancelar2.Visibility = Visibility.Hidden;
                        btnCancelar.IsEnabled = false;

                        if (Contexto.VCash.AbonoTotal)
                        {
                            if (Contexto.Entered == Contexto.AmountToPay)
                            {

                                Contexto.IMPTipoFlujo = "IngresoIgualSinCambio";
                                TimerHome.Stop();
                                Consumo.LoggerInfo("Procesando pago por recaudo completo y sin cambio");
                                TimerProcesar.Interval = TimeSpan.FromSeconds(1);
                                TimerProcesar.Start();

                                            //                                Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.RecaudoCompleto;
                                            //                                Contexto.VCash.TransactionComplete += VirtualCash_TransactionFinished;
                                            //                                Contexto.VCash.Procesarpago();
                                        }
                            else
                            {
                                Contexto.IMPTipoFlujo = "IngresoMayorSinCambio";
                                            /*                                AsignaValoresImpresion();
                                                                            Contexto.ImprimirRecibo(true);
                                                                            LblMensajeCambio.Visibility = Visibility.Hidden;
                                                                            IMensaje.Visibility = Visibility.Hidden;
                                                                            PagoSinDevolucion = 17;
                                                                            Contexto.VCash.CashAcceptComplete -= VirtualCash_AcceptedCompleted;
                                            */
                                            //TimerProcesar.Interval = TimeSpan.FromSeconds(1);
                                            TimerProcesar.Start();

                                ProcesarImage.Visibility = Visibility.Visible;

                                LblCambio.Visibility = Visibility.Visible;
                                LblCambio.BringIntoView();
                                btnProcesar.Visibility = Visibility.Visible;
                                btnProcesarCancelar3.Visibility = Visibility.Visible;
                                TimerProcesar.Start();
                            }

                        }

                        Contexto.Cambio = Convert.ToInt32(Contexto.Entered - Contexto.AmountToPay);

                        LblCambio.Content = Contexto.Cambio.ToString("C", Consumo.InfoPais);
                        LblRestan.Content = 0.ToString("C", Consumo.InfoPais);
                        if (Contexto.Entered > Contexto.AmountToPay)
                        {
                            LblCambio.Width = LblCambio.Width - 1;
                        }
                    }
                    else
                    {
                                    // AUN FALTA PLATA
                        LblCambio.Content = 0.ToString("C", Consumo.InfoPais);
                        LblRestan.Content = Convert.ToInt32(Contexto.AmountToPay - Contexto.Entered).ToString("C", Consumo.InfoPais);
                    }
                    Consumo.InsertarDetalleTransaccionServer($"Valor a pagar: {Contexto.AmountToPay.ToString("C", Consumo.InfoPais)}, Dinero recibido: {Contexto.Entered.ToString("C", Consumo.InfoPais)}", "Recaudando");
                    Consumo.LoggerInfo($"\r\nValor a pagar: {Contexto.AmountToPay.ToString("C", Consumo.InfoPais)},\r\nDinero recibido: {Contexto.Entered.ToString("C", Consumo.InfoPais)}");
                });
                //}
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Exception: VirtualCash_MoneyInTransactionChanged");
            }
        }

        private void VirtualCash_AcceptedCompleted(object sender, CurrentStatus e)
        {
            Dispatcher.Invoke(() =>
            {
                Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.RecaudoCompleto;
                Consumo.LoggerInfo($"Transacción marcada como: {Contexto.EstadoTransaccionActual}.");
                TimerHome.Stop();
                btnCancelar.IsEnabled = false;
                ContinuarEnPago = 2;
                Consumo.LoggerInfo("Se ha recaudado el Monto A Pagar, Se procederá a la devolución");
            });
        }

        private void VirtualCash_TransactionFinished(object sender, EventArgs e)
        {
            Consumo.LoggerInfo($"Entró a VirtualCash_TransactionFinished con Transacción {Contexto.EstadoTransaccionActual}");
            try
            {
                Dispatcher.Invoke(() =>
                {
                    //             if (EntregandoCambio.Visibility == Visibility.Visible)
                    //                 EntregandoCambio.Visibility = Visibility.Hidden;

                    TimerHome.Stop();

                    Contexto.VCash.TransactionComplete -= VirtualCash_TransactionFinished;
                    switch (Contexto.EstadoTransaccionActual)
                    {
                        case Contexto.EstadoTransaccion.RecaudoCompleto:
                            TimerHome.Stop();
                            TimerProcesar.Stop();

                            NavigationService.Navigate(new PageConsultandoPago(Contexto));
                            break;
                        case Contexto.EstadoTransaccion.CanceladaNoCambio:
                            TimerHome.Stop();
                            TimerProcesar.Stop();
                            NavigationService.Navigate(new MenuPrincipal(Contexto));
                            break;
                        default:
                            break;
                    }
                });
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, $"Exception VirtualCash_TransactionFinished");
            }
        }

        private async Task CancelarTransaccionAsync(int menuva)
        {

            Dispatcher.Invoke(() =>
            {
                TimerHome.Stop();
                btnCancelar.IsEnabled = false;
                if (Contexto.EstadoTransaccionActual == Contexto.EstadoTransaccion.RecaudoCompleto)
                    return;
            });

            if (Contexto.VCash == null)
            {
                Dispatcher.Invoke(() =>
                {
                    Consumo.FinTransaccion("Transacción cancelada por el usuario", "Transacción cancelada", Contexto.NroCliente);
                    NavigationService.Navigate(new MenuPrincipal(Contexto));
                });
            }
            else
            {
                await Task.Run(() =>
                {
                    try
                    {
                        Dispatcher.Invoke(() =>
                        {
                            //bool cancelable = Contexto.VCash.CancelTransaction(); // VALIDA QUE SI PUEDE CANCELAR LA TRANSACCIÓN
                            bool cancelable = true; // VALIDA QUE SI PUEDE CANCELAR LA TRANSACCIÓN
                            if (cancelable)
                            {
                                //Consumo.LoggerInfo($"Transacción puede ser cancelada");
                                Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.Cancelada;
                                Consumo.LoggerInfo($"Transacción marcada como: {Contexto.EstadoTransaccionActual}.");

                                Consumo.InsertarDetalleTransaccionServer("Transacción cancelada por el usuario", "Cancelando transacción");

                                AgregarEventosVCash(false);

                                if (Contexto.Entered > 0)
                                {
                                    Consumo.LoggerInfo($"Navegando a cancelar");
                                    NavigationService.Navigate(new PageCancelandoTransaccion(Contexto, "Transacción cancelada", menuva));
                                }
                                else
                                {
                                    Contexto.VCash.CancelTransaction();
                                    Consumo.LoggerInfo($"Transacción cancelada en VCash, navegando a Menú Principal");
                                    Task.Run(() => { Thread.Sleep(1000); }).Wait();
                                    //var task = Task.Run(() => { Thread.Sleep(1000); });
                                    //task.Wait();
                                    Consumo.FinTransaccion("Transacción finalizada", "Transacción cancelada sin recaudo", Contexto.NroCliente);
                                    NavigationService.Navigate(new MenuPrincipal(Contexto));
                                }
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            //Contexto.VCash.TransactionFinished += VirtualCash_TransactionFinished;
                            //Contexto.VCash.MoneyInTransactionChanged += VirtualCash_MoneyInTransactionChanged;
                            //Contexto.VCash.AcceptedCompleted += VirtualCash_AcceptedCompleted;
                            //Contexto.EstadoTransaccionActual = Contexto.EstadoTransaccion.SinIniciar;
                            Consumo.Logger.Error(ex.Message, "Menu Pagos Cancelar_Click");
                            //Utilitario.ReiniciarTimer(TimerHome);
                            btnCancelar.IsEnabled = true;
                            MostrarMensaje("Adventencia", ex.Message);
                        });
                    }
                });
            }
        }

        private void VirtualCash_FaltanteDelPago(object sender, decimal e)
        {
            Consumo.LoggerInfo($"Entró a VirtualCash_FaltanteDelPago con Transacción {Contexto.EstadoTransaccionActual}");
            Consumo.LoggerInfo($"Faltante Del Pago: {e.ToString("C", Consumo.InfoPais)}");
        }

        #endregion

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Contexto.EstadoTransaccionActual != Contexto.EstadoTransaccion.Cancelada)
                {
                    AgregarEventosVCash(false);
                }
                Consumo.LoggerInfo("Página de pago deshabilitada.");
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Error MenuPagos Page_Unloaded");
            }
        }

        private async void TimerHome_TickAsync(object sender, EventArgs e)
        {
            switch (ContinuarEnPago)
            {
                case 0: // SE CUMPLIÓ EL TIEMPO, MOSTRAR ADVERTENCIA
                    {
                        if (Contexto.EstadoTransaccionActual == Contexto.EstadoTransaccion.RecaudoCompleto)
                        {
                            Dispatcher.Invoke(() =>
                            {
                                TimerHome.Stop();
                                ContinuarEnPago = 2;
                            });
                        }
                        else
                        {
                            Consumo.LoggerInfo($"CANCELAR, ESPERAR POR CONFIRMACIÓN DEL USUARIO");
                            TimerHome.Interval = TimeSpan.FromSeconds(15);
                            Utilitario.ReiniciarTimer(TimerHome);
                            ContinuarEnPago = 1;
                            MostrarMensaje("", "");
                            //MostrarMensaje("Inactividad ingresando dinero", "¿Desea continuar con el pago?");
                        }
                    }
                    break;
                case 1: // NO CONTINUARON, YA PASARON LOS 15 SEGUNDOS SE DEBE CANCELAR Y CERRAR
                    {
                        Consumo.LoggerInfo("CANCELANDO POR TIEMPO DE INACTIVIDAD");
                        if (Contexto.Entered == 0)
                        {
                            Consumo.LoggerInfo("Cancela transaccion por tiempo de inactividad");
                            await CancelarTransaccionAsync(12);


                        }
                        else
                        {
                            Consumo.LoggerInfo("Procesa pago por tiempo de inactividad");
                            Contexto.IMPTipoFlujo = "IngresoIgualSinCambio";
                            TimerProcesar.Interval = TimeSpan.FromSeconds(1);
                            Utilitario.ReiniciarTimer(TimerProcesar);

                        }
                    }
                    break;
                case 7: // NO CONTINUARON, YA PASARON LOS 15 SEGUNDOS SE DEBE CANCELAR Y CERRAR
                    {
                        TimerHome.Stop();


                    }
                    break;
            }
        }

        private void MostrarMensaje(string titulo, string mensaje)
        {
            //txt_Error_Titulo.Text = titulo;
            //txt_Error_Campos.Text = mensaje;
            LblCambioM.Visibility = Visibility.Hidden;
            Finaliza.Visibility = Visibility.Hidden;
            CancelarImage.Visibility = Visibility.Visible;
            btnContinuar.Visibility = Visibility.Visible;
            btnCancelar2.Visibility = Visibility.Visible;
        }

        private void LblCambio_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Label lblCambio = sender as Label;

            Dispatcher.Invoke(async () =>
            {
                if (string.IsNullOrEmpty(lblCambio.Content.ToString()))
                    return;

                if (lblCambio.Content.ToString() != 0.ToString("C", Consumo.InfoPais))
                {
                    //await Task.Run(() => Task.Delay(TimeSpan.FromSeconds(3)));
                    //EntregandoCambio.Visibility = Visibility.Visible;
                }
            });
        }
    }
}
