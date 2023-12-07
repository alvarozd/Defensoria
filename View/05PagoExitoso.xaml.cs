using FacturasEnel.Logica;
using FacturasEnel.Util;
using FacturasEnel.View.Componentes;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PagePagoExitoso.xaml
    /// </summary>
    public sealed partial class PagePagoExitoso : Page
    {
        private int Menu = 5;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public PagePagoExitoso(Contexto contexto)
        {
            this.Contexto = contexto;
            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(8); // this.Contexto.Params.TimeOutInactividadPantallas
        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Consumo.FinTransaccion("Transacción finalizada exitosamente", "Exitosa", Contexto.NroCliente);
            Consumo.LoggerInfo($"NAVEGANDO A MENÚ PRINCIPAL - FIN TRASACCIÓN EXITOSA");
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Consumo.LoggerInfo($"INGRESO MENÚ PAGO EXITOSO");

            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);

            Contexto.ImprimirRecibo(true);

            //Contexto.CrearArchivoConciliacion(int Cuales, String nombreArchivo, String Fecha, String Hora, String ValorTransaccion, String NumeroAprobacion, String NumeroFactura)

            TimerHome.Start();
        }
    }
}
