using FacturasEnel.Logica;
using FacturasEnel.Util;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WindowsInput;
using System.Windows.Threading;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageIngresoAdmin.xaml
    /// </summary>
    public sealed partial class PageIngresoAdmin : Page
    {
        private int Menu = 40;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();
        private bool DesdeMenuPrincipal;

        public PageIngresoAdmin(Contexto contexto, bool desdeMenuPrincipal)
        {
            DesdeMenuPrincipal = desdeMenuPrincipal;
            Contexto = contexto;

            Height = Contexto.Alto;
            Width = Contexto.Ancho;

            InitializeComponent();
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(Contexto.GetParams().TimeOutInactividadPantallas);
            TimerHome.Start();
        }
        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                Utilitario.PintarControl(Teclado, 190, 330, 650, 330, 0, "C", true);
                //Utilitario.PintarBoton(Menu, 1, btnMenuPrincipal, 10, 674, 305, 92);

                //Utilitario.PintarBoton(Menu, 2, btnIngresar, 705, 669, 305, 92);

                Utilitario.PintarControl(TxtUsuario, 326, 148, 570, 75, 56, "C", true);
                Utilitario.PintarControl(TxtPassword, 326, 250, 570, 75, 56, "C", true);

            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            Storyboard story = (Storyboard)FindResource("EncogerImg");
            Image image = (Image)sender;
            image.BeginStoryboard(story);

            await Task.Delay(TimeSpan.FromMilliseconds(100));

            string MenuSig = image.Tag.ToString();
            switch (MenuSig)
            {
                case "MenuPrincipal":
                    {

                        TimerHome.Stop();
                        Contexto.MuestraFinaliza = false;
                        Consumo.LoggerInfo("VOLVER DE MENÚ PRINCIPAL");
                        NavigationService.Navigate(new MenuPrincipal(Contexto));



                     /*   Contexto.MuestraFinaliza = false;
                        btnMenuPrincipal.IsEnabled = false;
                        TimerHome.Stop();
                        Consumo.InsertarEstadisticaServer("Menú Administración", "Regresando Menú de Administración", "OK", "0");
                        //NavigationService.GoBack();
                        NavigationService.Navigate(new MenuPrincipal(Contexto));*/
                        break;
                    }
                case "Borrar":
                    {
                        if (TxtUsuario.SelectionStart > 0)
                            InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        break;
                    }
                case "Limpiar":
                    {
                        TxtUsuario.Text = "";
                        break;
                    }
                case "Ingresar":
                    {
                        TimerHome.Stop();
//                        btnIngresar.IsEnabled = false;
                        if (!string.IsNullOrEmpty(TxtUsuario.Text) && !string.IsNullOrEmpty(TxtPassword.Password))
                        {
                            try
                            {
                                Consultando.Visibility = Visibility.Visible;
                                string idUsuario = Consumo.AutenticarUsuario(TxtUsuario.Text, TxtPassword.Password);

                                if (string.IsNullOrEmpty(idUsuario) || idUsuario.IndexOf("ERROR") >= 0)
                                {
                                    //MostrarMensaje("Datos incorrectos", $"Por favor verifique la información suministrada.");
                                    MessageBox.Show("Datos incorrectos -  Por favor verifique la información suministrada");

                                    btnIngresar.IsEnabled = true;
                                }
                                else
                                {
                                    btnIngresar.IsEnabled = false;
                                    TimerHome.Stop();
                                    string RutaExeMisional;
                                    RutaExeMisional = Directory.GetCurrentDirectory() + @"\VirtualCashManager\";
                                    try
                                    {
                                        Contexto.VCash.UninitializeService();
                                        Consumo.InsertarEstadisticaServer("Menú Administración", $"Ingreso usuario {TxtUsuario.Text}", "Abriendo aplicación", "");
                                        Consumo.LoggerInfo("ABRIENDO APLICACIÓN DE ADMINISTRACIÓN");
                                        Process OpenExe = new Process();
                                        OpenExe.StartInfo.UseShellExecute = false;
                                        OpenExe.StartInfo.FileName = RutaExeMisional + @"VirtualCashManager.exe";
                                        OpenExe.StartInfo.CreateNoWindow = false;
                                        OpenExe.StartInfo.Arguments = TxtUsuario.Text + " " + Consumo.IdKiosco;
                                        OpenExe.StartInfo.WorkingDirectory = RutaExeMisional;
                                        OpenExe.Start();
                                        Thread.Sleep(2000);
                                        Application.Current.Shutdown();
                                    }
                                    catch (FileNotFoundException ex)
                                    {
                                        Consumo.InsertarEstadisticaServer("Menú Administración", $"Módulo administración no encontrado", "Error", "");
                                        Consumo.Logger.Error(ex, "ERROR ABRIENDO APLICACIÓN DE ADMINISTRACIÓN");
                                        MostrarMensaje("Error", $"No se pudo ejecutar la herramienta de administración.");
                                    }
                                    catch (Exception ex)
                                    {
                                        Consumo.InsertarEstadisticaServer("Menú Administración", $"Error abriendo módulo administración", "Error", "");
                                        Consumo.Logger.Error(ex, "ERROR ABRIENDO APLICACIÓN DE ADMINISTRACIÓN");
                                        MostrarMensaje("Error", $"No se pudo ejecutar la herramienta de administración.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Consumo.InsertarLogServer("Menú Administración", "Autenticación", $"Excepción: {ex.Message}");
                                MostrarMensaje("Exception", $"Error: {ex.Message}");
                                Consumo.Logger.Error($"Exception: {ex.Message}");
                            }
                            finally
                            {
                                Consultando.Visibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
//                            MostrarMensaje("Información Incompleta", "Debe ingresar los datos de ingreso.");
                            MessageBox.Show("Información Incompleta", "Debe ingresar los datos de ingreso.");
                        }
                        break;
                    }
            }
        }

        private void MostrarMensaje(string titulo, string mensaje)
        {
            TimerHome.Stop();
            Btn_AceptoErrores.Content = "Aceptar";
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            Mensajes.IsOpen = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Teclado.CampoFocused(TxtUsuario);
            if (TxtUsuario.IsVisible)
                TxtUsuario.Focus();
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            if (DesdeMenuPrincipal)
            {
                Consumo.InsertarEstadisticaServer("Menú Administración", "Regreso a menú principal", "Navegando", "");
                NavigationService.Navigate(new MenuPrincipal(Contexto));
            }
            else
            {
                Consumo.InsertarEstadisticaServer("Menú Administración", "Regreso a fuera de línea", "Navegando", "");
                NavigationService.Navigate(new MenuPrincipal(Contexto));
            }
        }

        private void TextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            Teclado.CampoFocused(sender);
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
            Btn_AceptoErrores.Content = "Aceptar";
            Utilitario.ReiniciarTimer(TimerHome);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Utilitario.ReiniciarTimer(TimerHome);
        }

        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Utilitario.ReiniciarTimer(TimerHome);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            TimerHome.Stop();
        }
    }
}
