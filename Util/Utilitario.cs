using FacturasEnel.Logica;
using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Text.RegularExpressions;
//using SpeechLib; // NTES SE USABA PARA REPRODUCIR TEXTO EN VOZ
using System.Speech.Synthesis;
using System.Runtime.CompilerServices;

namespace FacturasEnel.Util
{
    public  class Utilitario
    {
        private static SpeechSynthesizer Narrador = null;
        private static PromptBuilder builder = null;
        private static Dictionary<int, string> audioMenus = new Dictionary<int, string>();

 
       

        public static Dictionary<int, string> GetAudioMenus()
        {
            return audioMenus;
        }

        public static void SetAudioMenus(Dictionary<int, string> value)
        {
            audioMenus = value;
        }

        static readonly string RutaFonts = Environment.CurrentDirectory + @"\Util\Fonts\";

        public static ImageBrush ObtenerFondo(string nombreImagen)
        {
            BitmapImage bit = new BitmapImage();
            bit.BeginInit();
            bit.UriSource = new Uri(new FileInfo(nombreImagen).FullName);
            bit.EndInit();
            return new ImageBrush(bit);
        }

        public static BitmapImage EstablecerImagen(string rutaImagen)
        {
            string rutaTotalImagen = new FileInfo(rutaImagen).FullName;
            return new BitmapImage(new Uri(rutaTotalImagen));
        }

        public static void PintarBoton(int menu, int indice, Image btn, int posX, int posY, int largo, int alto, bool altocontraste)
        {


            if (altocontraste)
            {
                if (btn == null)
                    return;

                btn.Source = EstablecerImagen($@"Util\submn\mn{menu:00}{indice:00}01.jpg");
                btn.Width = largo;
                btn.Height = alto;
                Canvas.SetTop(btn, posY);
                Canvas.SetLeft(btn, posX);
                btn.Visibility = Visibility.Visible;
            }
            else
            {
                if (btn == null)
                    return;

                btn.Source = EstablecerImagen($@"Util\submn\al{menu:00}{indice:00}01.jpg");
                btn.Width = largo;
                btn.Height = alto;
                Canvas.SetTop(btn, posY);
                Canvas.SetLeft(btn, posX);
                btn.Visibility = Visibility.Visible;
            }
          
               
      
          
        }

        public static void PintarImagen(Image img, int posX, int posY, int largo, int alto, string name, bool png, bool visible)
        {
            if ((img == null) || (string.IsNullOrEmpty(name)))
                return;

            string ext = "jpg";

            if (png)
                ext = "png";

            img.Source = EstablecerImagen($@"Util\{name}.{ext}");
            img.Width = largo;
            img.Height = alto;
            Canvas.SetTop(img, posY);
            Canvas.SetLeft(img, posX);
            if (visible)
                img.Visibility = Visibility.Visible;
            else
                img.Visibility = Visibility.Hidden;
        }

        //public static void PintarBoton(int menu, int indice, Image btn, int posX, int posY, int largo, int alto, string img = "", bool png = false, bool visible = true)
        public static void PintarBoton(int menu, int indice, Image btn, int posX, int posY, int largo, int alto, string img, bool png, bool visible)
        {
            if (btn == null)
                return;

            string ext = "jpg";

            if (png)
                ext = "png";

            if (string.IsNullOrEmpty(img))
            {
                btn.Source = EstablecerImagen($@"Util\submn\mn{menu:00}{indice:00}01.{ext}");
            }
            else
            {
                btn.Source = EstablecerImagen($@"Util\submn\{img}.{ext}");
            }
            btn.Width = largo;
            btn.Height = alto;
            Canvas.SetTop(btn, posY);
            Canvas.SetLeft(btn, posX);
            if (visible)
                btn.Visibility = Visibility.Visible;
            else
                btn.Visibility = Visibility.Hidden;
        }

        public static void PintarControl(Control ctl, int posX, int posY, int largo, int alto, int size, string align, bool visible)
        {
            if (ctl == null)
                return;

            ctl.Width = largo;
            ctl.Height = alto;
            ctl.Padding = new Thickness(0);
            Canvas.SetTop(ctl, posY);
            Canvas.SetLeft(ctl, posX);

            if (visible)
                ctl.Visibility = Visibility.Visible;
            else
                ctl.Visibility = Visibility.Hidden;

            ctl.VerticalContentAlignment = VerticalAlignment.Center;

            if (size != 0)
                ctl.FontSize = size;

            ctl.FontFamily = new FontFamily(new Uri(RutaFonts), "./#HelveticaNeue");

            switch (align)
            {
                case "C": case "c": ctl.HorizontalContentAlignment = HorizontalAlignment.Center; break;
                case "R": case "r": ctl.HorizontalContentAlignment = HorizontalAlignment.Right; break;
                case "L": case "l": ctl.HorizontalContentAlignment = HorizontalAlignment.Left; break;
            }
            ctl.FontStyle = FontStyles.Normal;
        }

        public static void PintarControl(Control ctl, int posX, int posY, int largo, int alto, int size, string align, string texto, string estilo, bool visible)
        {
            if (ctl == null)
                return;

            ctl.Width = largo;
            ctl.Height = alto;
            ctl.Padding = new Thickness(0);
            Canvas.SetTop(ctl, posY);
            Canvas.SetLeft(ctl, posX);

            if (visible)
                ctl.Visibility = Visibility.Visible;
            else
                ctl.Visibility = Visibility.Hidden;

            ctl.VerticalContentAlignment = VerticalAlignment.Center;

            if (size != 0)
                ctl.FontSize = size;

            ctl.FontFamily = new FontFamily(new Uri(RutaFonts), "./#HelveticaNeue");

            switch (align)
            {
                case "C": case "c": ctl.HorizontalContentAlignment = HorizontalAlignment.Center; break;
                case "R": case "r": ctl.HorizontalContentAlignment = HorizontalAlignment.Right; break;
                case "L": case "l": ctl.HorizontalContentAlignment = HorizontalAlignment.Left; break;
            }

            switch (estilo)
            {
                case "B": case "b": case "N": case "n": ctl.FontWeight = FontWeights.DemiBold; break;
                case "I": case "i": case "K": case "k": ctl.FontStyle = FontStyles.Italic; break;
                case "U": case "u": case "S": case "s": ctl.FontStyle = FontStyles.Oblique; break;
                default: ctl.FontStyle = FontStyles.Normal; break;
            }

            if (string.IsNullOrEmpty(texto))
                return;

            if (ctl is TextBox textBox)
            {
                textBox.Text = texto;
            }
            else if (ctl is Label label)
            {
                label.Content = texto;
            }
            else if (ctl is Button button)
            {
                button.Content = texto;
            }
        }

        public static void PintarFlash(FlashPlayer ctl, int posX, int posY, int largo, int ancho, string file)
        {
            if (ctl == null)
                return;

            ctl.Width = largo;
            ctl.Height = ancho;
            ctl.Padding = new System.Windows.Thickness(0);
            Canvas.SetTop(ctl, posY);
            Canvas.SetLeft(ctl, posX);

            ctl.SourcePath = Environment.CurrentDirectory + $@"\swf\{file}.swf";
        }

        public static void ReiniciarTimer(DispatcherTimer elTimer)
        {
            //if (elTimer == null)
            //    return;

            elTimer.Stop();
            elTimer.Start();
        }

        //public static string FormatearMensaje(string mensaje)
        //{
        //    if (mensaje.Length > 36)
        //    {
        //        var primeraPArte = mensaje.Substring(0, 36);
        //        var segundaParte = mensaje.Substring(36, ((mensaje.Length) - 36));
        //        mensaje = primeraPArte + "\n" + segundaParte;
        //    }
        //    return mensaje;
        //}

        public static string FormatearAValorPesos(decimal valor)
        {
            return valor.ToString("C", Consumo.InfoPais);
        }

        public static bool EsNumero(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        public static void Hablar(string palabras)
        {
            return; //jose por si se coloca el audio

/*            if (Narrador == null)
            {
                Narrador = new SpeechSynthesizer();
                Narrador.SetOutputToDefaultAudioDevice(); // Configure the audio output.
                builder = new PromptBuilder(Consumo.InfoPais);
            }

            builder.StartVoice(new CultureInfo("es-MX"));
            builder.AppendText(palabras);
            builder.EndVoice();

            //builder.StartVoice(new CultureInfo("es-ES"));
            //builder.AppendText(palabras);
            //builder.EndVoice();

            //if (!esperar)

            Narrador.Rate = 0;
            Narrador.Volume = 100;
            Narrador.SpeakAsyncCancelAll(); // CANCELA LAS ÓRDENES DE HABLAR ANTERIORES
            Narrador.SpeakAsync(builder);   // HABLA EL TEXTO RECIBIDO CON LA VOZ DE LA CULTURA DEFINIDA O CON LA DEL SISTEMA OPERATIVO
            builder.ClearContent();*/
        }

        public static DataSet CargarXmltoDataSet(string rutaFiles)
        {
            DataSet ds = null;
            string myXMLfile = rutaFiles;

            // Crear un FileStream con el que leer el esquema.
            FileStream fsReadXml = new FileStream(myXMLfile, FileMode.Open);
            try
            {
                ds.ReadXml(fsReadXml);
                return ds;
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error(ex, $"Creando archivo: {rutaFiles}");
                return ds;
            }
            finally
            {
                fsReadXml.Close();
                ds.Dispose();
            }
        }

        public static void CargarXmltoDataSet(string rutaFiles, ref DataSet ds)
        {
            string myXMLfile = rutaFiles;
            ds.Clear();
            // Crear un FileStream con el que leer el esquema.
            FileStream fsReadXml = new FileStream(myXMLfile, FileMode.Open);
            try
            {
                ds.ReadXml(fsReadXml);
            }
            catch (Exception ex)
            {
                Consumo.Logger.Error($"Creando archivo: {rutaFiles}" + ex.ToString());
            }
            finally
            {
                fsReadXml.Close();
            }
        }

        //public static void Hablar_Esperar(string palabras)
        //{
        //    try
        //    {
        //        SpeechVoiceSpeakFlags SpFlags = SpeechVoiceSpeakFlags.SVSFDefault;
        //        SpVoice Voice = new SpVoice();
        //        Voice.Speak(palabras, SpFlags);

        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show(ex.ToString());
        //    }

        //}


    }
}
