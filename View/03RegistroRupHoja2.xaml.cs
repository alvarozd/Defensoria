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
using Enel.Logica;
using Newtonsoft.Json;

namespace FacturasEnel.View
{
    /// <summary>
    /// Lógica de interacción para PageDetallePagoFactura.xaml
    /// </summary>
    public sealed partial class RegistroRupHoja2 : Page
    {
        private int Menu = 3;
        private readonly Contexto Contexto;
        private DispatcherTimer TimerHome = new DispatcherTimer();


        public List<Municipio> ComboMunicipio = new List<Municipio>();

        public class Item
        {
            public string Nombre { get; set; }
            // Puedes agregar más propiedades según tus necesidades
        }

    





        public RegistroRupHoja2(Contexto contexto)
        {


            Height = contexto.Alto;
            Width = contexto.Ancho;


        InitializeComponent();
            //ComboDocumentos.ItemsSource = listaItems;
            ComboSexo.SelectionChanged += ComboSexo_SelectionChanged;
            ComboDepartamento.SelectionChanged += ComboDepartamento_SelectionChanged;
            ComboCiudad.SelectionChanged += ComboMunicipio_SelectionChanged;
            Contexto = contexto;
            Mensajes.Height = Contexto.Alto;
            Mensajes.Width = Contexto.Ancho;
            Video.Source = new Uri(@"util\Banner.mp4", UriKind.RelativeOrAbsolute);
            ComboSexo.ItemsSource = Contexto.Combosexo;
            ComboSexo.DisplayMemberPath = "NombreSexo"; // Muestra el NombreSexo en el ComboBox
            ComboSexo.SelectedValuePath = "IdSexo";

            ComboDepartamento.ItemsSource = Contexto.ComboDepartamento;
            ComboDepartamento.DisplayMemberPath = "NombreDepartamento"; // Muestra el NombreSexo en el ComboBox
            ComboDepartamento.SelectedValuePath = "IdDepartamento";

           


            Video.Width = 1280;
            Video.Height = 600;
            Video.Play();
            this.Video.Visibility = Visibility.Visible;

            EstablecerImagenes();
            TimerHome.Tick += new EventHandler(TimerHome_Tick);
            TimerHome.Interval = TimeSpan.FromSeconds(116);
            TimerHome.Start();
        
        }

        private Sexo sexoSeleccionado;

        private void ComboSexo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboSexo.SelectedItem != null)
            {
                sexoSeleccionado = (Sexo)ComboSexo.SelectedItem;
                Contexto.IdDocumentoPersona = sexoSeleccionado.IdSexo;
                // Ahora, sexoSeleccionado contiene el objeto Sexo seleccionado
                // Puedes acceder a sus propiedades como sexoSeleccionado.IdSexo y sexoSeleccionado.NombreSexo
            }
        }


        private Municipio MunicipioSeleccionado;

        private void ComboMunicipio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboCiudad.SelectedItem != null)
            {
                MunicipioSeleccionado = (Municipio)ComboCiudad.SelectedItem;
                Contexto.IdMunicipio = MunicipioSeleccionado.IdMunicipio;
                // Ahora, sexoSeleccionado contiene el objeto Sexo seleccionado
                // Puedes acceder a sus propiedades como sexoSeleccionado.IdSexo y sexoSeleccionado.NombreSexo
            }
        }


        private Departamento DepartamentoSeleccionado;
        private async void ComboDepartamento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboDepartamento.SelectedItem != null)
            {
                ComboMunicipio.Clear();
                ComboCiudad.ItemsSource = null;
                DepartamentoSeleccionado = (Departamento)ComboDepartamento.SelectedItem;
                Contexto.IdDepartamento = DepartamentoSeleccionado.IdDepartamento;
                Api api = new Api();

                var MunicipiosResultado = await api.ObtenerMunicipio(Contexto.Token,Contexto.IdDepartamento);

                Dictionary<string, string> datosMunicipio = JsonConvert.DeserializeObject<Dictionary<string, string>>(MunicipiosResultado);

                foreach (var kvp in datosMunicipio)
                {
                    ComboMunicipio.Add(new Municipio { IdMunicipio = kvp.Key, NombreMunicipio = kvp.Value });
                }

                Contexto.ComboMunicipio = ComboMunicipio;

                


            }
           
            ComboCiudad.ItemsSource = Contexto.ComboMunicipio;
            ComboCiudad.DisplayMemberPath = "NombreMunicipio"; // Muestra el NombreSexo en el ComboBox
            ComboCiudad.SelectedValuePath = "IdMunicipio";
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

                    ComboSexo.Foreground = Colorletra;
                    ComboDepartamento.Foreground = Colorletra;
                    ComboCiudad.Foreground = Colorletra;



                }
                else
                {
                    Background = Utilitario.ObtenerFondo($@"Util\al{Menu:00}.jpg");
                    Brush Colorletra = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d59f56"));
                    Brush ColorFondo = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#000000"));
                    ComboSexo.Foreground = Colorletra;
                    ComboDepartamento.Foreground = Colorletra;
                    ComboCiudad.Foreground = Colorletra;



                }


                Utilitario.PintarBoton(Menu, 1, Altocontraste, 366, 925, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 2, Zoom, 548, 925, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 3, Silencio, 731, 925, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 4, Ayuda, 912, 923, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 5, AyudaDiscapa, 1095, 924, 171, 55, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 6, Atras, 14, 443, 98, 142, Contexto.Altocontraste);
                Utilitario.PintarBoton(Menu, 7, Siguiente, 1161, 442, 98, 142, Contexto.Altocontraste);









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
                        //TimerHome.Stop();
                        //Utilitario.ReiniciarTimer(TimerHome);
                        //if (TxtNombres.SelectionStart > 0)
                        //    InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        //if (TxtApellidos.SelectionStart > 0)
                        //    InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        //if (TxtDocumento.SelectionStart > 0)
                        //    InputSimulator.SimulateKeyDown(VirtualKeyCode.BACK);
                        break;
                    }
                case "Atras":
                    {
                        TimerHome.Stop();
                        NavigationService.Navigate(new RegistroRup(Contexto));
                        break;
                    }
                case "Sig":
                    {
                        TimerHome.Stop();
                       NavigationService.Navigate(new RegistroRupHoja3(Contexto));
                        break;
                    }


                case "altocontraste":
                    {
                        if (Contexto.Altocontraste)
                        {
                            Contexto.Altocontraste = false;
                            NavigationService.Navigate(new RegistroRupHoja2(Contexto));
                        }
                        else
                        {
                            Contexto.Altocontraste = true;
                            NavigationService.Navigate(new RegistroRupHoja2(Contexto));
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
                        //Utilitario.ReiniciarTimer(TimerHome);
                        //string numero = image.Tag.ToString();
                        //InputSimulator.SimulateTextEntry(numero);
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

        private void ComboDepartamento_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
