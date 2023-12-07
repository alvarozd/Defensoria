using FacturasEnel.Util;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WindowsInput;
using FacturasEnel.Logica;
using FacturasEnel.Modelo;
using NLog;
using System.Windows.Threading;
using System.IO;
using System.Media;
using System.Collections.Generic;
using System.Windows.Media;
using Enel.Modelo;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageDetallePagoFactura.xaml
    /// </summary>
    public sealed partial class RegistroRup : Page
    {
        private int Menu = 2;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();

        public class Item
        {
            public string Nombre { get; set; }
            // Puedes agregar más propiedades según tus necesidades
        }

        private List<Item> listaItems = new List<Item>
        {
            new Item { Nombre = "Cedula" },
            new Item { Nombre = "Tarjeta de Identidad" },
            new Item { Nombre = "Pasaporte" }
            // Puedes agregar más elementos según sea necesario
        };



        public RegistroRup(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;

            InitializeComponent();
        ComboDocumentos.ItemsSource = listaItems;
            TxtNombres.Focus();

            Contexto = contexto;
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;
            Video.Source = new Uri(@"util\Banner.mp4", UriKind.RelativeOrAbsolute);
            Video.Width = 1280;
            Video.Height = 600;
            Video.Play();
            this.Video.Visibility = Visibility.Visible;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(116);
            TimerHome.Start();
            TxtNombres.TextChanged += TxtNombres_TextChanged;
            TxtApellidos.TextChanged += TxtApellidos_TextChanged;
            TxtDocumento.TextChanged += TxtDocumento_TextChanged;
            TxtNombres.Text =  Contexto.NombrePersona;
            TxtApellidos.Text= Contexto.ApellidoPersona;
            TxtDocumento.Text = Contexto.DocumentoPersona;


            ComboDocumentos.SelectionChanged += ComboDocumento_SelectionChanged;
            ComboDocumentos.ItemsSource = Contexto.ComboDocumento;
            ComboDocumentos.DisplayMemberPath = "NombreDocumento"; // Muestra el NombreSexo en el ComboBox
            ComboDocumentos.SelectedValuePath = "IdDocumento";

            ComboDocumentos.SelectedItem = contexto.IdDocumentoPersona;

        }

        private Documento documentoSeleccionado;

        private void ComboDocumento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboDocumentos.SelectedItem != null)
            {
                documentoSeleccionado = (Documento)ComboDocumentos.SelectedItem;
                Contexto.IdSexo = documentoSeleccionado.IdDocumento;
                // Ahora, sexoSeleccionado contiene el objeto Sexo seleccionado
                // Puedes acceder a sus propiedades como sexoSeleccionado.IdSexo y sexoSeleccionado.NombreSexo
            }
        }



        private void TxtNombres_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Obtener el texto actual del TextBox
            Contexto.NombrePersona = TxtNombres.Text;

          
        }
        private void TxtApellidos_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Obtener el texto actual del TextBox
            Contexto.ApellidoPersona = TxtApellidos.Text;

       
        }
        private void TxtDocumento_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Obtener el texto actual del TextBox
            Contexto.DocumentoPersona = TxtDocumento.Text;



        }


        private void Video_MediaEnded(object sender, RoutedEventArgs e)
        {
            Video.Position = new TimeSpan(0, 0, 1);
            Video.Play();
        }

        private void EstablecerImagenes()
        {
            try
            {

                if (Contexto.Altocontraste)
                {
                   
                    Brush Colorletra = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2a60a8"));
                    Brush ColorFondo = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                    Background = Utilitario.ObtenerFondo($@"Util\mn{Menu:00}.jpg");

                    TxtNombres.Foreground = Colorletra;
                    TxtNombres.Background = ColorFondo;
                    TxtNombres.BorderBrush = Colorletra;

                    TxtDocumento.Foreground = Colorletra;
                    TxtDocumento.Background = ColorFondo;
                    TxtDocumento.BorderBrush = Colorletra;

                    TxtApellidos.Background = ColorFondo;
                    TxtApellidos.Foreground = Colorletra;
                    TxtApellidos.BorderBrush = Colorletra;
                
                    ComboDocumentos.Foreground = Colorletra;
                    ComboDocumentos.Background = ColorFondo;

                }
                else
                {
                    Background = Utilitario.ObtenerFondo($@"Util\al{Menu:00}.jpg");
                    Brush Colorletra = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d59f56"));
                    Brush ColorFondo = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
                    Brush ColorFondoclaro = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                    TxtNombres.Foreground = Colorletra;
                    TxtNombres.Background = ColorFondo;
                    TxtNombres.BorderBrush = Colorletra;

                    TxtDocumento.Foreground = Colorletra;
                    TxtDocumento.Background = ColorFondo;
                    TxtDocumento.BorderBrush = Colorletra;

                    TxtApellidos.Background = ColorFondo;
                    TxtApellidos.Foreground = Colorletra;
                    TxtApellidos.BorderBrush = Colorletra;

                    ComboDocumentos.Foreground = Colorletra;
                    ComboDocumentos.Background = ColorFondo;
                }


                Utilitario.PintarBoton(Menu, 1, Altocontraste, 366, 925, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 2, Zoom, 548, 925, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 3, Silencio, 731, 925, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 4, Ayuda, 912, 923, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 5, AyudaDiscapa, 1095, 924, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 6, Atras, 14, 443, 98, 142, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 7, Siguiente, 1161, 442, 98, 142, Contexto.Altocontraste);

                Utilitario.PintarBoton(Menu, 8,  btn1, 867, 700, 68, 67,   Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 9,  btn2, 935, 700, 68, 67,   Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 10, btn3, 1005, 699, 68, 67,  Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 11, btn4, 867, 626, 68, 67,   Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 12, btn5, 935, 627, 68, 67,   Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 13, btn6, 1004, 628, 68, 67,  Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 14, btn7, 866, 555, 68, 67,   Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 15, btn8, 935, 557, 68, 67,   Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 16, btn9, 1004, 556, 68, 67,  Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 17, btn10, 1004, 772, 68, 67, Contexto.Altocontraste);



                Utilitario.PintarBoton(Menu, 18, btnQ, 163, 555, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 19, btnW, 228, 556, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 20, btnE, 295, 556, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 21, btnR, 360, 555, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 22, btnT, 425, 555, 68, 67,Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 23, btnY, 489, 556, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 24, btnU, 554, 556, 68, 67,Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 25, btnI, 620, 555, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 26, btnO, 685, 556, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 27, btnP, 751, 557, 68, 67, Contexto.Altocontraste);


      

                Utilitario.PintarBoton(Menu, 28, btnA, 190, 624, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 29, btnS, 255, 626, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 30, btnD, 321, 625, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 31, btnF, 387, 627, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 32, btnG, 450, 627, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 33, btnH, 515, 628, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 34, btnJ, 580, 627, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 35, btnK, 645, 629, 68, 67,Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 36, btnL, 711, 628, 68, 67, Contexto.Altocontraste);






                Utilitario.PintarBoton(Menu, 37, btnZ, 240, 700, 68, 67,  Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 38, btnX, 307, 700, 68, 67,  Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 39, btnC, 370, 700, 68, 67,  Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 40, btnV, 437, 701, 68, 67,  Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 41, btnB, 500, 699, 68, 67, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 42, btnN, 566, 700, 68, 67,  Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 43, btnM, 632, 701, 68, 67,  Contexto.Altocontraste );
                Utilitario.PintarBoton(Menu, 44, btnEspacio, 336, 770, 244, 71, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 45, btnBorrar, 867, 773, 136, 69, Contexto.Altocontraste);








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
            try
            {
                SoundPlayer simpleSound = new SoundPlayer(@"util\Click.wav");
                simpleSound.Play();
            }
            catch (Exception ex)
            {
                Consumo.LoggerInfo("No se puedo ejecutar el sonido");
            }



            switch (MenuSig)
            {
                case "Aceptar":
                    {

                        //if ((TextNumeroDocumento.Text == "") || (TextPin.Text == ""))
                        //{
                        //    MostrarMensaje("Datos incompletos","Por favor ingresar el número de documento y el número de pin");
                        //}
                        //else
                        //{
                        //    if ((TextNumeroDocumento.Text == "1234") && (TextPin.Text == "1234"))
                        //    {
                        //        Contexto.numeroDocumento = TextNumeroDocumento.Text;
                        //        Contexto.numeroPin = TextPin.Text;
                        //        Contexto.CualMenu = "Desembolso";
                        //        NavigationService.Navigate(new PageCosultando(Contexto));
                        //    }
                        //    else
                        //    {
                        //        Utilitario.PintarImagen(Consultando, 0, 0, 1024, 768, "mn100", false, true);
                        //        await Task.Delay(3000);
                        //        MostrarMensaje("Validación", "Los datos no se encuentran registrados");
                        //    }
                        //}
                        break;

                    }
                case "No":
                    {
                        break;
                    }
                case "Borrar":
                    {
                        TimerHome.Stop();
                        Utilitario.ReiniciarTimer(TimerHome);
                        if (TxtNombres.SelectionStart > 0)
                            InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        if (TxtApellidos.SelectionStart > 0)
                            InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        if (TxtDocumento.SelectionStart > 0)
                            InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        break;
                    }
                case "Atras":
                    {
                        TimerHome.Stop();
                        NavigationService.Navigate(new MenuPrincipal(Contexto));
                        break;
                    }
                case "Sig":
                    {
                        TimerHome.Stop();
                        NavigationService.Navigate(new RegistroRupHoja2(Contexto));
                        break;
                    }

                case "altocontraste":
                    {
                        if (Contexto.Altocontraste)
                        {
                            Contexto.Altocontraste = false;
                            NavigationService.Navigate(new RegistroRup(Contexto));
                        }
                        else
                        {
                            Contexto.Altocontraste = true;
                            NavigationService.Navigate(new RegistroRup(Contexto));
                        }

                        break;
                    }

                case "Zoom":
                    {


                        break;
                    }
                case "Silencio":
                    {
                        // NavigationService.Navigate(new Consultas(Contexto));
                        break;
                    }
                case "Ayuda":
                    {
                        //NavigationService.Navigate(new Consultas(Contexto));
                        break;
                    }

                default:
                    {
                        Utilitario.ReiniciarTimer(TimerHome);
                        string numero = image.Tag.ToString();
                        InputSimulator.SimulateTextEntry(numero);
                        break;
                    }
            }
        }

        private void TimerHome_Tick(object sender, EventArgs e)
        {
            TimerHome.Stop();
            Consumo.FinTransaccionTimeOut();
            NavigationService.Navigate(new MenuPrincipal(Contexto));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //if (TextNumeroDocumento.IsVisible)
            //    TextNumeroDocumento.Focus();
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
                NavigationService.Navigate(new RegistroRup(Contexto));
        }


        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            TimerHome.Stop();
        }
    }
}
