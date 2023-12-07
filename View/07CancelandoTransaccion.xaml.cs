using CTM;
using FacturasEnel.Logica;
using FacturasEnel.Util;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;



namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageCancelandoTransaccion.xaml
    /// </summary>
    public sealed partial class PageCancelandoTransaccion : Page
    {
        private int Menu = 7;
        private readonly Contexto Contexto;
        private string RazonCancelacion;
        private DispatcherTimer Espera = new DispatcherTimer();
        private DispatcherTimer Ejecutar = new DispatcherTimer();

        public PageCancelandoTransaccion(Contexto contexto, string razonCancelacion, int CualMenu)
        {

            Menu = CualMenu;
            RazonCancelacion = razonCancelacion;
            Height = contexto.Alto;
            Width = contexto.Ancho;
            Contexto = contexto;

            InitializeComponent();
            EstablecerImagenes();
            Espera.Tick += new EventHandler(Espera_Tick);
            Espera.Interval = new TimeSpan(0, 0, 4);

            Ejecutar.Tick += new EventHandler(Ejecutar_Tick);
            Ejecutar.Interval = new TimeSpan(0, 0, 2);
        }

        private void EstablecerImagenes()
        {
            try
            {

                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                Utilitario.PintarControl(Mensaje, 0, 520, Contexto.Ancho, 80, 70, "C", "Transacción cancelada por el usuario", "N", true);


                /*           if (Menu < 10)

                               Background = Utilitario.ObtenerFondo($@"Util\mn0" + Menu.ToString() + ".jpg");
                           else
                               Background = Utilitario.ObtenerFondo($@"Util\mn" + Menu.ToString() + ".jpg");*/
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private void Espera_Tick(object sender, EventArgs e)
        {
            Espera.Stop();
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }

        private void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            Contexto.VCash.TransactionComplete += VirtualCash_TransactionFinished;
            Contexto.VCash.CashAccept += VirtualCash_MoneyInTransactionChanged;

            Ejecutar.Start();
            //            Contexto.VCash.FaltanteDelPago += VirtualCash_FaltanteDelPago;

        }

        private void VirtualCash_FaltanteDelPago(object sender, decimal e)
        {
            Consumo.LoggerInfo($"Entró a VirtualCash_FaltanteDelPago con Transacción {Contexto.EstadoTransaccionActual}");
            Consumo.LoggerInfo($"Faltante Del Pago: {e.ToString("C", Consumo.InfoPais)}");
        }

        private void Ejecutar_Tick(object sender, EventArgs e)
        {
            Ejecutar.Stop();
            if (Contexto.EstadoTransaccionActual == Contexto.EstadoTransaccion.Rechazada)
            {
                try
                {
                    Consumo.LoggerInfo($"Reversando transacción: {Contexto.IdUltimaTransaccion}");
                    //                  Contexto.VCash.Reverse_Transaction(Contexto.IdUltimaTransaccion);
                    Consumo.LoggerInfo($"Transacción reversada: {Contexto.IdUltimaTransaccion}");
                }
                catch (Exception ex)
                {
                    Consumo.Logger.Error(ex, "Cancelando Transacción - VCash.Reverse_Transaction");
                }
            }

            if (Contexto.EstadoTransaccionActual == Contexto.EstadoTransaccion.Cancelada)
            {
                try
                {
                    Consumo.LoggerInfo($"Cancelando transacción: {Consumo.IdVCash}");
                    Contexto.VCash.CancelTransaction();
                    Consumo.LoggerInfo($"Transacción cancelada: {Contexto.IdUltimaTransaccion}");
                }
                catch (Exception ex)
                {
                    Consumo.Logger.Error(ex, "Cancelando Transacción - VCash.CancelTransaction");
                }
            }
        }

        private void VirtualCash_MoneyInTransactionChanged(object sender, CTMAcceptEvent ev)
        {
            Consumo.LoggerInfo($"Entró a VirtualCash_MoneyInTransactionChanged con Transacción {Contexto.EstadoTransaccionActual}");
            try
            {
                //if (ev.MovementType == MovementType.OUT)// SALIDA DE DINERO DE LOS DISPOSITIVOS
                //{
                //    Dispatcher.Invoke(() =>
                //    {
                //        Contexto.Cambio = Convert.ToInt32(ev.ToReturn);
                //        Consumo.LoggerInfo($"Retornado: {ev.Returned} de {ev.ToReturn.ToString("C", Consumo.InfoPais)}");
                //        Consumo.InsertarDetalleTransaccionServer($"Retornado: {ev.Returned.ToString("C", Consumo.InfoPais)} de {ev.ToReturn.ToString("C", Consumo.InfoPais)}", "Retornando dinero");
                //    });
                //}
                //if (ev.MovementType == MovementType.IN)// INGRESÓ DINERO Y NO DEBÍA
                //{
                //    Dispatcher.Invoke(() =>
                //    {
                //        Consumo.LoggerInfo("Recepción de dinero mientras se retornaba el dinero ingresado.");
                //    });
                //}
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Cancelando Transaccion VirtualCash_MoneyInTransactionChanged");
            }
        }

        private void VirtualCash_TransactionFinished(object sender, EventArgs e)
        {
            Consumo.LoggerInfo($"Entró a VirtualCash_TransactionFinished con Transacción {Contexto.EstadoTransaccionActual}");
            try
            {
                Dispatcher.Invoke(() =>
                {
                    //  Contexto.ImprimirRecibo(false);
                });
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, $"Cancelando Transacción - ImprimirRecibo, Transacción {Contexto.EstadoTransaccionActual}");
            }

            try
            {
                Contexto.VCash.CashAccept -= VirtualCash_MoneyInTransactionChanged;
                Contexto.VCash.TransactionComplete -= VirtualCash_TransactionFinished;
                //Contexto.VCash.FaltanteDelPago -= VirtualCash_FaltanteDelPago;
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Cancelando Transaccion VirtualCash_TransactionFinished");
            }

            Dispatcher.Invoke(() =>
            {
                Consumo.FinTransaccion("Transacción finalizada", $"Transacción {Contexto.EstadoTransaccionActual}", Contexto.NroCliente);
                NavigationService.Navigate(new MenuPrincipal(Contexto));
            });

        }



    }
}
