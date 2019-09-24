using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Comfandi_.Util
{
    public class Utilitario
    {
        public static ImageBrush ObtenerFondo(string rutaImagen)
        {
            BitmapImage bit = new BitmapImage();
            bit.BeginInit();
            bit.UriSource = new Uri(new FileInfo(rutaImagen).FullName);
            bit.EndInit();
            return new ImageBrush(bit);
        }

        public static BitmapImage EstablecerImagen(string rutaImagen)
        {
            string rutaTotalImagen = new FileInfo(rutaImagen).FullName;
            return new BitmapImage(new Uri(rutaTotalImagen));
        }


        public static string FormatearMensaje(string mensaje)
        {
            if (mensaje.Length > 36)
            {
                var primeraPArte = mensaje.Substring(0, 36);
                var segundaParte = mensaje.Substring(36, ((mensaje.Length) - 36));
                mensaje = primeraPArte + "\n" + segundaParte;
            }
            return mensaje;
        }
    }
}
