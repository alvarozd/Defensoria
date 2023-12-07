using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Data;
using System.Xml.Linq;


namespace FacturasEnel.Util
{
    public class XmlUtil
    {
        //private string idConsulta = string.Empty;
        XmlDocument XMLParametros = null;
        XmlNode hijo = null;
        XmlElement nieto = null;
        XmlNode xmlnode = null;

        public void CreaXml(string nombreRaiz)
        {
            try
            {
                XMLParametros = new XmlDocument();
                XmlDeclaration xmldeclaracion;
                xmlnode = null;
                xmlnode = XMLParametros.CreateNode(XmlNodeType.XmlDeclaration, string.Empty, string.Empty);
                XMLParametros.AppendChild(xmlnode);

                // Le indico el encoding
                xmldeclaracion = (XmlDeclaration)XMLParametros.FirstChild;
                xmldeclaracion.Encoding = "iso-8859-1";

                // Añado el raiz 
                xmlnode = XMLParametros.CreateNode(XmlNodeType.Element, nombreRaiz, "");
                XMLParametros.AppendChild(xmlnode);
            }
            catch
            {
                throw;
            }
        }

        public void CreaHijo(string nombreHijo)
        {
            hijo = XMLParametros.CreateNode(XmlNodeType.Element, nombreHijo, "");
        }

        public void CreaNieto(string nombreNieto)
        {
            if (hijo != null)
            {
                nieto = XMLParametros.CreateElement(nombreNieto);
            }
        }

        public void CreaAtributosNieto(string nombre, string valor)
        {
            if (nieto != null)
            {
                nieto.SetAttribute(nombre, valor);
                hijo.AppendChild(nieto);
            }
        }

        public void AñadeHijo()
        {
            try
            {
                xmlnode.AppendChild(hijo);
            }
            catch
            {
                throw;
            }
        }

        public void GuardaXml(string ruta)
        {
            XMLParametros.Save(ruta);
        }
    }
}
