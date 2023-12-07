using FacturasEnel.Logica;
using FacturasEnel.Util;
using FacturasEnel.View;
using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace FacturasEnel
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private Contexto Contexto;
//        private DispatcherTimer TimerActualizaArchivosSFTP = new DispatcherTimer();
        private DateTime DiaInicio = DateTime.Now;
        private TimeSpan horaSubida; //TimeSpan.Parse("17:04");
        private TimeSpan horaActual;

        public MainWindow()
        {
            Width = 1280;
            Height = 1024;
         //  this.Topmost = true;
            Contexto = new Contexto();
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                if (Width >= SystemParameters.PrimaryScreenWidth)
                {
                    Left = ((Width - SystemParameters.PrimaryScreenWidth) / 2) * - 1;
                    Contenido.Margin = new Thickness(0, 0, 0, 0);
                    //Contenido.Margin = new Thickness(-120, 0, 0, 0);
                }
                else
                {
                    Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
                }

                if (Height >= SystemParameters.PrimaryScreenHeight)
                    Top = (Height - SystemParameters.PrimaryScreenHeight) / 2;
                else
                    Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;

                try
                {
                    WindowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
                    HwndSource.FromHwnd(WindowHandle)?.AddHook(new HwndSourceHook(HandleMessages));
                    await Contexto.ConsultarParametros();
                    FramePrincipal.Content = new MenuPrincipal(Contexto);//// volv er a colocar la pagina de cargando jose ojo
                    Contexto.NumerodeCreditos = 3; /// ojo ojo borrar
                   // FramePrincipal.Content = new MuestraCreditos(Contexto); //Solo para pruebas
//                    horaSubida = Datetime ;
                }
                catch (Exception ex)
                {
                    Consumo.Logger.Error(ex, "MAIN WINDOW");
                }
            };

        }

        public static IntPtr WindowHandle { get; private set; }

        internal void HandleParameter(string[] argumentos)
        {
            if (argumentos != null)
            {
                if (Application.Current?.MainWindow is MainWindow mainWindow)
                {
                    mainWindow.Title = "Finanaciera Comultrasan - Autoservicio";
                }
            }
        }

        private IntPtr HandleMessages(IntPtr handle, int message, IntPtr wParameter, IntPtr lParameter, ref Boolean handled)
        {
            if (handle != WindowHandle)
                return IntPtr.Zero;

            var data = UnsafeNativeMethods.GetMessage(message, lParameter);

            if (data != null)
            {
                if (Application.Current.MainWindow == null)
                    return IntPtr.Zero;

                if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;

                var respu = UnsafeNativeMethods.SetForegroundWindow(new WindowInteropHelper(Application.Current.MainWindow).Handle);

                var args = data.Split(' ');
                HandleParameter(args);
                handled = true;
            }

            return IntPtr.Zero;
        }

        private void FramePrincipal_Navigated(object sender, NavigationEventArgs e)
        {
            //var pagina = e.Uri;
            var nombrePgina = e.Content.GetType();
/*            if (nombrePgina == typeof(MenuPrincipal))
            {
                Utilitario.ReiniciarTimer(TimerActualizaArchivosSFTP);
            }*/
        }

        private async void Window_ContentRendered(object sender, EventArgs e)
        {
            //try
            //{
            //    WindowHandle = new WindowInteropHelper(Application.Current.MainWindow).Handle;
            //    HwndSource.FromHwnd(WindowHandle)?.AddHook(new HwndSourceHook(HandleMessages));
            //    await Contexto.ConsultarParametros().ConfigureAwait(false);
            //    FramePrincipal.Content = new PageCargando(Contexto);
            //    horaSubida = TimeSpan.Parse(Contexto.GetParams().Params.HoraFinOperacion,Consumo.InfoPais);

            //    TimerActualizaArchivosSFTP.Tick += new EventHandler(TimerActualizarArchivosSFTP_Tick);
            //    TimerActualizaArchivosSFTP.Interval = TimeSpan.FromMinutes(50);
            //    TimerActualizaArchivosSFTP.Start(); // ACTIVAR LA ACTUALIZACIÓN DIARIA DE ARCHIVOS

            //}
            //catch (Exception ex)
            //{
            //    Consumo.Logger.Error(ex, "MAIN WINDOW");
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Consumo.Logger.Info($"CERRANDO VENTANA PRINCIPAL, sender: {sender.ToString()}");
        }

    }
}
