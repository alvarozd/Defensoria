using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Font;
using iText.IO.Font;
using iText.Layout.Borders;
using System.Globalization;
using iText.Kernel.Pdf.Canvas;
using System.Windows.Forms;
using System.Collections.Generic;

namespace CrearPDF
{
    class ITextPDF
    {
        public String ArchivoDestino;
        public String ArchivoPaginado;

        public String LogoEMSA { get; set; }
//        public String NombreCliente { get; set; }
        //public String Matricula { get; set; }
        public String Fecha { get; set; }
        //public String Sector { get; set; }
        //public long? Capital { get; set; }
        //public long? Intereses { get; set; }
        //public long? Alumbrado { get; set; }
        //public long? Total { get; set; }
        bool linea2 = false;

        public void CrearPDFconImagenes(List<string> ListaImages)
        {
            //// CREAR EL OBJETO QUE VA A ESCRIBIR EL DOCUMENTO CON EL TAMAÑO DE HOJA Y LAS MÁRGENES ESPECIFICADAS ////
            var writer = new PdfWriter(ArchivoDestino);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf, PageSize.LETTER);
            document.SetMargins(1, 1, 0, 1);

            if ((ListaImages.Count > 0) && (ListaImages != null))
            {
                foreach (string strUrl in ListaImages)
                {
                    //string ImagenFisica = Directory.GetCurrentDirectory() + @"\reportes\"+ strUrl;
                    var imgObj = new Image(ImageDataFactory.Create(strUrl));

                    //////// AJUSTA LA IMAGEN AL TAMAÑO DE LA HOJA
                    //float h = PageSize.LETTER.GetHeight() - ((PageSize.LETTER.GetHeight() * 6) / 100f);
                    //float w = PageSize.LETTER.GetWidth() - ((PageSize.LETTER.GetWidth() * 6) / 100f);
                    //var pLogo = new Paragraph().Add(imgLogo.SetHeight(h).SetWidth(w));

                    imgObj.SetHeight(PageSize.LETTER.GetHeight() - ((PageSize.LETTER.GetHeight() * 6) / 100f));
                    imgObj.SetWidth(PageSize.LETTER.GetWidth() - ((PageSize.LETTER.GetWidth() * 6) / 100f));
                    var pLogo = new Paragraph().Add(imgObj);
                    document.Add(pLogo);
                }
            }

            //// CIERRA EL DOCUMENTO DESPUÉS DE CREARLO /////////
            document.Close();
        }

        private void AgregarCelda(Table table, String linea, PdfFont font, int tamano, bool header=false, bool borde = false)
        {
            var TextoCelda = new Paragraph(linea).SetFont(font).SetFontSize(tamano).SetFixedLeading(6);
            if (header)
            {
                table.AddHeaderCell(new Cell().Add(TextoCelda));
            }
            else
            {
                if (!borde)
                    table.AddCell(new Cell().Add(TextoCelda).SetBorder(iText.Layout.Borders.Border.NO_BORDER));
                else
                    table.AddCell(new Cell().Add(TextoCelda));
            }
        }

        private void AgregarCeldaDetalle(Table table, String linea, PdfFont font, int tamano, int align = 2, bool header = false)
        {

            iText.Layout.Properties.TextAlignment al;

            switch (align)
            {
                case 0: al = iText.Layout.Properties.TextAlignment.LEFT; break;
                case 1: al = iText.Layout.Properties.TextAlignment.CENTER; break;
                case 2: al = iText.Layout.Properties.TextAlignment.RIGHT; break;
                case 3: al = iText.Layout.Properties.TextAlignment.JUSTIFIED; break;
                case 4: al = iText.Layout.Properties.TextAlignment.JUSTIFIED_ALL; break;
                default: al = iText.Layout.Properties.TextAlignment.RIGHT; break;
            }

            var TextoCelda = new Paragraph(linea).SetFont(font).SetFontSize(tamano).SetFixedLeading(6);
            if (header)
            {
                if (linea2)
                {
                    table.AddHeaderCell(
                        new Cell().Add(
                            TextoCelda.SetFixedLeading(3))
                            .SetBorder(Border.NO_BORDER)
                            .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.TOP)
                            .SetTextAlignment(al));
                }
                else
                {
                    table.AddHeaderCell(
                        new Cell().Add(
                            TextoCelda.SetFixedLeading(3))
                            .SetBorder(Border.NO_BORDER)
                            .SetVerticalAlignment(iText.Layout.Properties.VerticalAlignment.BOTTOM)
                            .SetTextAlignment(al));
                }
            }
            else
            {
                table.AddCell(
                    new Cell().Add(TextoCelda)
                        .SetBorder(Border.NO_BORDER)
                        .SetTextAlignment(al));
            }
        }

        private string FormatearNumero(long? valor)
        {
            string conFormato = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "{0:#,##0}", valor);
            return conFormato;
        }

        private void ModificarPDF()
        {
            ArchivoPaginado = ArchivoDestino.Substring(0, ArchivoDestino.IndexOf(".")) + "p.pdf";
            PdfDocument pdfDoc = new PdfDocument(new PdfReader(ArchivoDestino), new PdfWriter(ArchivoPaginado));
            Document document = new Document(pdfDoc);
            Rectangle pageSize;
            PdfCanvas canvas;
            int n = pdfDoc.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                PdfPage page = pdfDoc.GetPage(i);
                pageSize = page.GetPageSize();
                canvas = new PdfCanvas(page);
                //ESCRIBE EL NÚMERO DE PAGINAS
                canvas.BeginText()
                    .SetFontAndSize(PdfFontFactory.CreateFont(FontConstants.HELVETICA), 8)
                    .MoveText(pageSize.GetWidth() / 2 + 82, pageSize.GetHeight() - 105)
                    .ShowText(i.ToString())
                    .ShowText(" de ")
                    .ShowText(n.ToString())
                    .EndText();

                /*
                //ESCRIBE LOS TEXTOS FALTANTES EN EL HEADER
                canvas.BeginText()
                    .SetFontAndSize(PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD), 7)
                    .MoveText(pageSize.GetWidth() / 2 + 140, pageSize.GetHeight() - 135)
                    .ShowText("Lecturas del Mes")
                    .EndText();
                    */
            }
            pdfDoc.Close();
        }

        private string FormatearDinero(long? valor)
        {
            string conFormato = string.Format(CultureInfo.CreateSpecificCulture("en-US"), "${0:#,##0}", valor);
            return conFormato;
        }

        private string FormatearFecha(String fc)
        {
            DateTime fecha = Convert.ToDateTime(fc.Substring(0,10));
            string strfc = fecha.ToString("dd-") + fecha.ToString("MMM").ToUpper() + fecha.ToString("-yy");
            return strfc;
        }
    }

}
