using BusinessEnel;
using FacturasEnel.Logica;
using FacturasEnel.Modelo;
using FacturasEnel.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using WindowsInput;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageOpcionesAdmin.xaml
    /// </summary>
    public sealed partial class PageOpcionesAdmin : Page
    {
        private int Menu = 30;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();
        private string UbicacionRecibos;

        public class Recibo
        {
            public int idVPS { get; set; }
            public int idVC { get; set; }
            public DateTime FechaHora { get; set; }
            public string Fecha { get; set; }
            public string Hora { get; set; }
            public string NombreArchivo { get; set; }
            public BitmapImage Boton { get; set; }
        }

        public PageOpcionesAdmin(Contexto contexto)
        {
            UbicacionRecibos = Environment.CurrentDirectory + @"\Recibos\";
            Contexto = contexto;

            Height = Contexto.Alto;
            Width = Contexto.Ancho;

            InitializeComponent();
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(Contexto.GetParams().TimeOutInactividadPantallas+30);
            //TimerHome.Start();
        }

        private void EstablecerImagenes()
        {
            try
            {
                Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                Utilitario.PintarBoton(Menu, 0, btnVolver, 41, 624, 283, 83, "Volver", false, true);
                //Utilitario.PintarBoton(Menu, 1, btn1, 537, 306, 288, 89);
                //Utilitario.PintarBoton(Menu, 2, btn2, 536, 437, 288, 89);
                //Utilitario.PintarControl(RecibosView, 910, 255, 360, 435,10,"C","","",false);
                Utilitario.PintarControl(FechaConsulta, 990, 205, 200, 50, 30, "C", "", "", false);
                //Utilitario.PintarBoton(Menu, 3, btn3, 655, 486, 56, 56);
            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "EstablecerImagenes");
            }
        }

        private async void Botones_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TimerHome.Stop();
            Storyboard story = (Storyboard)FindResource("EncogerImg");
            Image image = (Image)sender;
            image.BeginStoryboard(story);

            await Task.Delay(TimeSpan.FromMilliseconds(300));
            string MenuSig = image.Tag.ToString();
            switch (MenuSig)
            {
                case "Volver":
                    {
                        btnVolver.IsEnabled = false;
                        Consumo.LoggerInfo("VOLVER DE MENÚ ADMINISTRACIÓN");
                        NavigationService.Navigate(new MenuPrincipal(Contexto));
                        break;
                    }
                case "MenuAdmin":
                    {
                        btn1.IsEnabled = false;
                        Consumo.InsertarEstadisticaServer("Menú administración", "Ingreso Menú Administración", "OK", "0");
                        NavigationService.Navigate(new PageIngresoAdmin(Contexto, true));
                        break;
                    }
                case "Reimpresion":
                    {
                        Consumo.InsertarEstadisticaServer("Administración", "Reimpresión de recibos", "OK", "0");
                        FechaConsulta.SelectedDate = DateTime.Today;
                        FechaConsulta.Visibility = Visibility.Visible;
                        //Utilitario.PintarControl(RecibosView, 910, 255, 360, 435, 10, "C", "", "", false);
                        RecibosView.Width = 380;
                        RecibosView.Height = 435;
                        Canvas.SetTop(RecibosView, 255);
                        Canvas.SetLeft(RecibosView, 910);

                        ConsultarRecibos();
                        break;
                    }
                case "Ayuda":
                    {
                        Consumo.InsertarEstadisticaServer("Menú Lectura código de barras", "Ingreso Ayuda", "OK", Consumo.Redondeo);
                        NavigationService.Navigate(new PageAyuda(Contexto, Menu));
                        break;
                    }
                default:
                    {
                        string numero = image.Tag.ToString();
                        InputSimulator.SimulateTextEntry(numero);
                        break;
                    }
            }
        }

        private void ConsultarRecibos()
        {
            try
            {
                if (FechaConsulta.SelectedDate.Value != null)
                {
                    string[] Recibos = Directory.GetFiles(UbicacionRecibos, "*.pdf", SearchOption.TopDirectoryOnly);

                    if (Recibos.Length > 0)
                    {
                        List<Recibo> items = new List<Recibo>();
                        foreach (var file in Recibos)
                        {
                            FileInfo rec = new FileInfo(file);

                            if (rec.CreationTime.Date == FechaConsulta.SelectedDate.Value)
                            {
                                string[] InfoRecibo = rec.Name.Split('-');

                                int IdVPS = Convert.ToInt32(InfoRecibo[0].Replace("VPS", ""), Consumo.InfoPais);
                                int IdVC = Convert.ToInt32(InfoRecibo[1].Replace("VC", "").Replace(".pdf", ""), Consumo.InfoPais);

                                //Recibo itemPrueba = new Recibo()
                                //{
                                //    IdVPS = 99999999,
                                //    IdVC = 88888888,
                                //    Fecha = rec.CreationTime.Date.ToShortDateString(),
                                //    Hora = rec.CreationTime.TimeOfDay.ToString(@"hh\:mm\:ss", Consumo.InfoPais),
                                //    NombreArchivo = rec.Name,
                                //    Boton = Utilitario.EstablecerImagen(@"Util\submn\Imprimir.jpg")
                                //};
                                //items.Add(itemPrueba);

                                Recibo itemRec = new Recibo()
                                {
                                    idVPS = IdVPS,
                                    idVC = IdVC,
                                    FechaHora = rec.CreationTime,
                                    Fecha = rec.CreationTime.Date.ToShortDateString(),
                                    Hora = rec.CreationTime.TimeOfDay.ToString(@"hh\:mm\:ss", Consumo.InfoPais),
                                    NombreArchivo = rec.Name,
                                    Boton = Utilitario.EstablecerImagen(@"Util\submn\Imprimir.jpg")
                                };
                                items.Add(itemRec);
                            }
                        }
                        if (items.Count > 0)
                        {
                            //items.OrderBy(x => x.FechaHora);
                            //items.Sort(new Comparison<Recibo>((x, y) => string.Compare(x.Hora, y.Hora)));
                            //items.Sort((p, q) => p.Hora.CompareTo(q.Hora));
                            LstRecibos.ItemsSource = items.OrderByDescending(x => x.FechaHora);
                            RecibosView.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            RecibosView.Visibility = Visibility.Hidden;
                            LstRecibos.ItemsSource = null;
                            MostrarMensaje("Información no encontrada", "No hay recibos disponibles para reimprimir.");
                        }
                    }
                    else
                    {
                        RecibosView.Visibility = Visibility.Hidden;
                        LstRecibos.ItemsSource = null;
                        MostrarMensaje("Información no encontrada", "No hay recibos disponibles para reimprimir.");
                    }
                }
                else
                {
                    MostrarMensaje("Información incompleta", "Debe seleccionar una fecha válida.");
                }

            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "consultando recibos");
            }
        }

        private void BtnPrint_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string ArchivoImprimir = ((Image)sender).Tag.ToString();

            Process proc = new Process();
            try
            {
                Consumo.InsertarEstadisticaServer("Administración", $"Reimpresión recibo: {ArchivoImprimir}", "OK", "0");
                string File = UbicacionRecibos + ArchivoImprimir;

                proc.StartInfo.FileName = "SumatraPDF.exe";
                proc.StartInfo.Arguments = "-print-to-default " + File;
                proc.StartInfo.RedirectStandardError = false;
                proc.StartInfo.RedirectStandardOutput = false;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
                proc.WaitForExit();

            }
            catch (FileNotFoundException ex)
            {
                Consumo.Logger.Error(ex, "Archivo no encontrado");
            }
            catch (InvalidOperationException ex)
            {
                Consumo.Logger.Error(ex, "InvalidOperationException Reimprimiendo recibo");
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, "Reimprimiendo recibo");
            }
            finally
            {
                proc.Dispose();
            }
            Utilitario.ReiniciarTimer(TimerHome);
        }

        private void MostrarMensaje(string titulo, string mensaje)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            Mensajes.IsOpen = true;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
            Utilitario.ReiniciarTimer(TimerHome);
        }

        private void FechaConsulta_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            ConsultarRecibos();
        }
    }

}
