using FacturasEnel.Logica;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Security.Permissions;

[assembly: CLSCompliant(false)]
namespace FacturasEnel
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    /// 
    public static class Program
    {
        [STAThread]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = Consumo.InfoPais;
            CultureInfo.DefaultThreadCurrentCulture = Consumo.InfoPais;
            CultureInfo.DefaultThreadCurrentUICulture = Consumo.InfoPais;
            Thread.CurrentThread.CurrentCulture = Consumo.InfoPais;
            Thread.CurrentThread.CurrentUICulture = Consumo.InfoPais;

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionHandler);

            //currentDomain.ThreadException += new ThreadExceptionEventHandler(Program.Form1_UIThreadException);
            //Application.SetUnhandledExceptionMode

            Consumo.LoggerInfo("INICIANDO APLICACIÓN");
            var proc = Process.GetCurrentProcess();
            var processName = proc.ProcessName;
            var runningProcess = Process.GetProcesses().FirstOrDefault(x => (x.ProcessName == processName) && x.Id != proc.Id);

            if (runningProcess == null)
            {
                try
                {
                    var app = new App();
                    app.InitializeComponent();
                    //                    var window = new MainWindow();
                    var window = new MainWindow();
                    window.HandleParameter(args);
                    app.Run(window);
                    Consumo.InsertarEstadisticaServer("Main Program", "Cargando Aplicación", "OK", "");
                    return; // In this case we just proceed on loading the program
                }
                catch (Exception ex)
                {
                    Consumo.Logger.Error(ex, "Main Program");
                }
            }
            else
            {
                Consumo.Logger.Error("APLICACIÓN YA ESTABA ABIERTA");
            }

            if (args != null)
            {
                if (args.Length > 0)
                {
                    UnsafeNativeMethods.SendMessage(runningProcess.MainWindowHandle, string.Join(" ", args));
                }
            }
        }

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            Consumo.LoggerInfo($"Runtime terminating: {args.IsTerminating}");
            Consumo.Logger.Error(e, "UnhandledExceptionHandler");
        }

    }

    public partial class App : Application
    {
        private void Application_Exit(object sender, ExitEventArgs e)
        {
            Consumo.Logger.Info($"CERRANDO APLICACIÓN, sender: {sender.ToString()}, e:{e.ApplicationExitCode}");
        }
        //    //public App()
        //    //{
        //    //    string procName = Process.GetCurrentProcess().ProcessName;
        //    //    Process[] processes = Process.GetProcessesByName(procName);
        //    //    var argumentos = Environment.GetCommandLineArgs();
        //    //    if (processes.Length > 1)//EL proceso ya esta corriendo osea debo cerrar esta instancia
        //    //    {
        //    //        System.Windows.Application.Current.Shutdown();
        //    //        Process.GetCurrentProcess().Kill();
        //    //        return;
        //    //    }
        //    //    else
        //    //    {
        //    //        var window = new MainWindow();
        //    //        MainWindow.HandleParameter(argumentos);
        //    //        this.Run(window);
        //    //        // Application.Run(...);
        //    //    }

        //    //}


        //    // get the list of all processes by the "procName"       
    }
}
