using BusinessEnel;
using FacturasEnel.Logica;
using FacturasEnel.Modelo;
using FacturasEnel.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using WindowsInput;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageDigitarNroCliente.xaml
    /// </summary>
    public sealed partial class PageDigitarNroCliente : Page
    {
        private int Menu = 20;
        private readonly Contexto Contexto;
        bool BackToMenuPrincipal = false;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public PageDigitarNroCliente(Contexto contexto)
        {
            Contexto = contexto;
            Contexto.MuestraFinaliza = false;
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
                if (Contexto.CualMenu == "Cliente")
                {

                    Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");
                }
                else
                {
                    Background = Utilitario.ObtenerFondo($@"Util\mn65.jpg");
                }

                //Utilitario.PintarBoton(Menu, 1, btn1, 418, 343, 74, 71);
                //Utilitario.PintarBoton(Menu, 2, btn2, 493, 342, 74, 71);
                //Utilitario.PintarBoton(Menu, 3, btn3, 568, 343, 74, 71);
                //Utilitario.PintarBoton(Menu, 4, btn4, 419, 415, 74, 71);
                //Utilitario.PintarBoton(Menu, 5, btn5, 492, 415, 74, 71);
                //Utilitario.PintarBoton(Menu, 6, btn6, 567, 417, 74, 71);
                //Utilitario.PintarBoton(Menu, 7, btn7, 418, 489, 74, 71);
                //Utilitario.PintarBoton(Menu, 8, btn8, 493, 491, 74, 71);
                //Utilitario.PintarBoton(Menu, 9, btn9, 568, 488, 74, 71);
                //Utilitario.PintarBoton(Menu, 10, btn10, 419, 562, 74, 71);
                //Utilitario.PintarBoton(Menu, 11, btnBorrar, 493, 564, 155, 71);
                //Utilitario.PintarBoton(Menu, 12, btnAceptar, 725, 369, 246, 92);
                //Utilitario.PintarBoton(Menu, 13, btnCancelar, 720, 468, 246, 92);

                Utilitario.PintarControl(TextNumeroDocumento, 544, 190, 352, 55, 32, "C", true);
              //  TextNumeroDocumento.Text = "37790144";
                TextNumeroDocumento.Focus();
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

            await Task.Delay(TimeSpan.FromMilliseconds(100));
            string MenuSig = image.Tag.ToString();
            switch (MenuSig)
            {

                case "Consultar":
                    {
                          ConsultarNumeroDeCliente();
                        break;
                    }
                case "Borrar":
                    {
                        TimerHome.Stop();
                        Utilitario.ReiniciarTimer(TimerHome);
                        if (TextNumeroDocumento.SelectionStart > 0)
                            InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        break;
                    }
                case "Cancelar":
                    {
                        TimerHome.Stop();
                        NavigationService.Navigate(new MenuPrincipal(Contexto));
                        break;
                    }
                default:
                    {
                        TimerHome.Stop();
                        Utilitario.ReiniciarTimer(TimerHome);
                        string numero = image.Tag.ToString();
                        InputSimulator.SimulateTextEntry(numero);
                        break;
                    }
            }
        }

        private async void ConsultarNumeroDeCliente()
        {
            if (TextNumeroDocumento.Text == "")
            {
                MostrarMensaje("Datos Usuario", "Por favor digitar un número de documento");
            }
            else
            {
                if ((TextNumeroDocumento.Text == "37790144") || (TextNumeroDocumento.Text == "46055759")) 
                {
                    if (TextNumeroDocumento.Text == "37790144")
                        Contexto.CualMenu = "Cliente";
                    NavigationService.Navigate(new PageCosultando(Contexto));
                }
            }
        }

        private async Task ConsultarPorNumeroCliente(string NumeroCliente)
        {
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Consumo.LoggerInfo("MENÚ DIGITAR NÚMERO DE CLIENTE");

            if (Utilitario.GetAudioMenus().ContainsKey(Menu))
                Utilitario.Hablar(Utilitario.GetAudioMenus()[Menu]);

            if (TextNumeroDocumento.IsVisible)
                TextNumeroDocumento.Focus();
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Contexto.MenuAnt = "PageDigitarNroCliente";
            NavigationService.Navigate(new MenuPrincipal(Contexto));

        }

        private void MostrarMensaje(string titulo, string mensaje)
        {
            Utilitario.ReiniciarTimer(TimerHome);
            txt_Error_Titulo.Text = titulo;
            txt_Error_Campos.Text = mensaje;
            Mensajes.IsOpen = true;
        }

        private void Btn_AceptoErrores_Click(object sender, RoutedEventArgs e)
        {
            TimerHome.Stop();
            NavigationService.Navigate(new PageDigitarNroCliente(Contexto));
        }


        private void NroCliente_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox ElTxt = sender as TextBox;

        }
    }

}
