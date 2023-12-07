using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using TwainDotNet;
using FacturasEnel.Util;

namespace TwainGui
{
    using CrearPDF;
    using System.Collections.Generic;
    using System.Threading;
    using TwainDotNet.TwainNative;
    using TwainDotNet.WinFroms;

    public class EscaneoForm : Form
    {
        private static readonly AreaSettings AreaSettings = new AreaSettings(Units.Centimeters, 0.1f, 5.7f, 0.1F + 2.6f, 5.7f + 2.6f);
        private readonly Twain _twain;
        private ScanSettings _settings;

        private readonly IContainer components;
        public List<string> ListaImagenes = new List<string>();

        public string NombreEscaner = "";
        public string NombreArchivoPDF = "";
        private string NombreArchivo = "";
        private string _rutaGuardar = "";
        private readonly string br = Environment.NewLine;
        private string RutaFiles = AppDomain.CurrentDomain.BaseDirectory;

//        public IntPtr HandleApp;
        private Label LblEscanear;
        private Label LblCancelar;
        private Label LblEscanearOtra;
        private Label LblFinalizarProceso;
        private Panel PanelMensaje;
        private Label LblMensajes;
        private Label LblSeleccion;
        private PictureBox ImgEscaneada;

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblEscanear = new System.Windows.Forms.Label();
            this.LblCancelar = new System.Windows.Forms.Label();
            this.LblEscanearOtra = new System.Windows.Forms.Label();
            this.LblFinalizarProceso = new System.Windows.Forms.Label();
            this.PanelMensaje = new System.Windows.Forms.Panel();
            this.LblMensajes = new System.Windows.Forms.Label();
            this.ImgEscaneada = new System.Windows.Forms.PictureBox();
            this.LblSeleccion = new System.Windows.Forms.Label();
            this.PanelMensaje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgEscaneada)).BeginInit();
            this.SuspendLayout();
            // 
            // LblEscanear
            // 
            this.LblEscanear.BackColor = System.Drawing.Color.Transparent;
            this.LblEscanear.Location = new System.Drawing.Point(10, 9);
            this.LblEscanear.Name = "LblEscanear";
            this.LblEscanear.Size = new System.Drawing.Size(114, 53);
            this.LblEscanear.TabIndex = 5;
            this.LblEscanear.Text = "ESCANEAR";
            this.LblEscanear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblEscanear.Visible = false;
            this.LblEscanear.Click += new System.EventHandler(this.Escanear_Click);
            // 
            // LblCancelar
            // 
            this.LblCancelar.BackColor = System.Drawing.Color.Transparent;
            this.LblCancelar.Location = new System.Drawing.Point(281, 12);
            this.LblCancelar.Name = "LblCancelar";
            this.LblCancelar.Size = new System.Drawing.Size(114, 53);
            this.LblCancelar.TabIndex = 7;
            this.LblCancelar.Text = "CANCELAR TODO";
            this.LblCancelar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblCancelar.Visible = false;
            this.LblCancelar.Click += new System.EventHandler(this.Cancelar_Click);
            // 
            // LblEscanearOtra
            // 
            this.LblEscanearOtra.BackColor = System.Drawing.Color.Transparent;
            this.LblEscanearOtra.Location = new System.Drawing.Point(20, 134);
            this.LblEscanearOtra.Name = "LblEscanearOtra";
            this.LblEscanearOtra.Size = new System.Drawing.Size(114, 53);
            this.LblEscanearOtra.TabIndex = 9;
            this.LblEscanearOtra.Text = "ESCANEAR OTRA HOJA";
            this.LblEscanearOtra.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblEscanearOtra.Visible = false;
            this.LblEscanearOtra.Click += new System.EventHandler(this.LblEscanearOtra_Click);
            // 
            // LblFinalizarProceso
            // 
            this.LblFinalizarProceso.BackColor = System.Drawing.Color.Transparent;
            this.LblFinalizarProceso.Location = new System.Drawing.Point(377, 134);
            this.LblFinalizarProceso.Name = "LblFinalizarProceso";
            this.LblFinalizarProceso.Size = new System.Drawing.Size(114, 53);
            this.LblFinalizarProceso.TabIndex = 8;
            this.LblFinalizarProceso.Text = "GUARDAR PDF Y SALIR";
            this.LblFinalizarProceso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblFinalizarProceso.Visible = false;
            this.LblFinalizarProceso.Click += new System.EventHandler(this.LblFinalizarProceso_Click);
            // 
            // PanelMensaje
            // 
            this.PanelMensaje.AutoScroll = true;
            this.PanelMensaje.Controls.Add(this.LblMensajes);
            this.PanelMensaje.Location = new System.Drawing.Point(13, 214);
            this.PanelMensaje.Margin = new System.Windows.Forms.Padding(0);
            this.PanelMensaje.Name = "PanelMensaje";
            this.PanelMensaje.Size = new System.Drawing.Size(718, 367);
            this.PanelMensaje.TabIndex = 10;
            this.PanelMensaje.Visible = false;
            // 
            // LblMensajes
            // 
            this.LblMensajes.BackColor = System.Drawing.Color.Transparent;
            this.LblMensajes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.LblMensajes.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensajes.ForeColor = System.Drawing.Color.White;
            this.LblMensajes.Location = new System.Drawing.Point(0, 82);
            this.LblMensajes.Margin = new System.Windows.Forms.Padding(0);
            this.LblMensajes.Name = "LblMensajes";
            this.LblMensajes.Size = new System.Drawing.Size(718, 285);
            this.LblMensajes.TabIndex = 12;
            this.LblMensajes.Text = "Ayuda";
            this.LblMensajes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ImgEscaneada
            // 
            this.ImgEscaneada.Location = new System.Drawing.Point(524, 12);
            this.ImgEscaneada.Name = "ImgEscaneada";
            this.ImgEscaneada.Size = new System.Drawing.Size(197, 91);
            this.ImgEscaneada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImgEscaneada.TabIndex = 11;
            this.ImgEscaneada.TabStop = false;
            this.ImgEscaneada.Visible = false;
            // 
            // LblSeleccion
            // 
            this.LblSeleccion.BackColor = System.Drawing.Color.Transparent;
            this.LblSeleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblSeleccion.ForeColor = System.Drawing.Color.White;
            this.LblSeleccion.Location = new System.Drawing.Point(0, 162);
            this.LblSeleccion.Margin = new System.Windows.Forms.Padding(0);
            this.LblSeleccion.Name = "LblSeleccion";
            this.LblSeleccion.Size = new System.Drawing.Size(718, 119);
            this.LblSeleccion.TabIndex = 13;
            this.LblSeleccion.Text = "Que desea hacer";
            this.LblSeleccion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LblSeleccion.Visible = false;
            // 
            // EscaneoForm
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(743, 608);
            this.Controls.Add(this.LblSeleccion);
            this.Controls.Add(this.ImgEscaneada);
            this.Controls.Add(this.PanelMensaje);
            this.Controls.Add(this.LblEscanearOtra);
            this.Controls.Add(this.LblFinalizarProceso);
            this.Controls.Add(this.LblCancelar);
            this.Controls.Add(this.LblEscanear);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "EscaneoForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Escaneo de documentos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EscaneoForm_FormClosing);
            this.Load += new System.EventHandler(this.EscaneoForm_Load);
            this.PanelMensaje.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgEscaneada)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        public EscaneoForm(string ruta)
        {
            _rutaGuardar = ruta;

            InitializeComponent();

            _twain = new Twain(new WinFormsWindowMessageHook(this));
            _twain.TransferImage += _twain_TransferImage;
            _twain.ScanningComplete += _twain_ScanningComplete;

            // OERD NOVIEMBRE 2023 CAMBIA EVENTOS DELEGATE POR SUBSCRIPCIÓN A EVENTHANDLERS
            //_twain.TransferImage += delegate (object sender, TransferImageEventArgs args)
            //{
            //    if (args.Image != null)
            //    {
            //        DateTime h = DateTime.Now;

            //        NombreArchivo = _rutaGuardar + string.Format(@"Img{0}{1}.jpg", "Scan", h.ToString("yyyyMMdd-HHmmss"));

            //        args.Image.Save(NombreArchivo, System.Drawing.Imaging.ImageFormat.Jpeg);
            //        ListaImagenes.Add(NombreArchivo);

            //        /// SI SE QUIERE MOSTRAR LA IMAGEN SE PUEDE CARGAR ACÁ EN PANTALLA
            //        /// DESPUES QUE ES ESCANEADA CADA HOJA
            //    }
            //};
            //_twain.ScanningComplete += delegate
            //{
            //    Enabled = true;
            //};

            CargarEscanear(true);
        }

        private void _twain_TransferImage(object sender, TransferImageEventArgs e)
        {
            if (e.Image != null)
            {
                DateTime h = DateTime.Now;

                NombreArchivo = _rutaGuardar + string.Format(@"Img{0}{1}.jpg", "Scan", h.ToString("yyyyMMdd-HHmmss"));

                e.Image.Save(NombreArchivo, System.Drawing.Imaging.ImageFormat.Jpeg);
                ListaImagenes.Add(NombreArchivo);

                /// SI SE QUIERE MOSTRAR LA IMAGEN SE PUEDE CARGAR ACÁ EN PANTALLA
                /// DESPUES QUE ES ESCANEADA CADA HOJA
            }
        }

        private void _twain_ScanningComplete(object sender, ScanningCompleteEventArgs e)
        {
            Enabled = true;
        }

        private void EscaneoForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Top = (Screen.PrimaryScreen.Bounds.Height - 1024) / 2;
            Left = (Screen.PrimaryScreen.Bounds.Width - 1280) / 2;
            Height = 1024;
            Width = 1280;
            PanelMensaje.BackgroundImage = Image.FromFile(RutaFiles + @"util\PopUp.jpg");
            PanelMensaje.Width = 996;
            PanelMensaje.Height = 524;
            PanelMensaje.Top = ((Height - PanelMensaje.Height) / 2) + 20;
            PanelMensaje.Left = ((Width - PanelMensaje.Width) / 2) + 10;
        }

        private void CargarEscanear(bool mostrar)
        {
            if (mostrar)
            {
                BackgroundImage = Image.FromFile(RutaFiles + @"util\MN10.jpg");
                LblEscanear.Text = "";
                LblEscanear.Left = 37;
                LblEscanear.Top = 900;
                LblEscanear.Height = 108;
                LblEscanear.Width = 289;

                LblCancelar.Text = "";
                LblCancelar.Left = 960;
                LblCancelar.Top = 904;
                LblCancelar.Height = 108;
                LblCancelar.Width = 289;
            }
            LblEscanear.Visible = mostrar;
            LblCancelar.Visible = mostrar;
        }

        private void CargarAgregarOtra(bool mostrar)
        {
            if (mostrar)
            {
                PanelMensaje.Visible = false;
                BackgroundImage = Image.FromFile(RutaFiles + @"util\MN11.jpg");

                LblSeleccion.Top = 477;
                LblSeleccion.Left = 207;
                LblSeleccion.Width = 900;
                LblSeleccion.Height = 180;
                LblSeleccion.Text = "Hoja escaneada." + br + br + "Seleccione una de estas opciones:";

                LblEscanearOtra.Text = "";
                LblEscanearOtra.Left = 207;
                LblEscanearOtra.Top = 678;
                LblEscanearOtra.Height = 93;
                LblEscanearOtra.Width = 235;

                LblCancelar.Text = "";
                LblCancelar.Left = 540;
                LblCancelar.Top = 679;
                LblCancelar.Height = 93;
                LblCancelar.Width = 235;

                LblFinalizarProceso.Text = "";
                LblFinalizarProceso.Left = 874;
                LblFinalizarProceso.Top = 678;
                LblFinalizarProceso.Height = 93;
                LblFinalizarProceso.Width = 235;
            }

            LblSeleccion.Visible = mostrar;
            LblEscanearOtra.Visible = mostrar;
            LblFinalizarProceso.Visible = mostrar;
            LblCancelar.Visible = mostrar;
        }

        private void Escanear_Click(object sender, EventArgs e)
        {
            LblMensajes.Text = $"Iniciando el proceso{br}por favor espere...";
            PanelMensaje.Visible = true;
            //LblMensajes.Text = "Validando estado del escánner...";

            if (!_twain.SourceNames.Contains(NombreEscaner))
            {
                LblMensajes.Text = $"El controlador para el escáner: {NombreEscaner}{br}{br}no está instalado!";
                Enabled = true;
            }
            else
            {
                _twain.SelectSource(NombreEscaner);
                Enabled = false;

                _settings = new ScanSettings
                {
                    AbortWhenNoPaperDetectable = true,
                    ShowProgressIndicatorUI = true,
                    UseDocumentFeeder = true,
                    ShowTwainUI = false,
                    UseDuplex = false,
                    Resolution = null
                };
                _settings.Resolution = new ResolutionSettings()
                {
                    ColourSetting = ColourSetting.Colour,
                    Dpi = 100
                };

                //_settings.Area = !checkBoxArea.Checked ? null : AreaSettings;
                _settings.Area = AreaSettings;
                _settings.Page = null;

                //_settings.Page = new PageSettings()
                // {
                //    Orientation = Orientation.Portrait,
                //    Size = PageType.None

                //};
                _settings.ShouldTransferAllPages = true;

                _settings.Rotation = new RotationSettings()
                {
                    AutomaticRotate = false,
                    AutomaticBorderDetection = true
                };

                try
                {
                    _twain.StartScanning(_settings);

                    CargarEscanear(false);
                    CargarAgregarOtra(true);
                }
                catch (TwainException ex)
                {
                    string ErrorEspecifico;
                    switch (ex.ReturnCode)
                    {
                        case null: ErrorEspecifico = "Ubique la hoja en la ranura del escáner" + br + br + "y presione el botón Escanear."; break;
                        case TwainResult.Success: ErrorEspecifico = LblMensajes.Text; break;
                        case TwainResult.Failure:
                        case TwainResult.DataNotAvailable:
                        case TwainResult.InfoNotSupported:
                        case TwainResult.CheckStatus:
                            ErrorEspecifico = $"No fue posible conectar con el escáner{br}{br}verifique el cable."; break;
                        case TwainResult.DSEvent: ErrorEspecifico = ex.ReturnCode.ToString(); break;
                        //case TwainResult.NotDSEvent: ErrorEspecifico = ex.ReturnCode.ToString(); break;
                        //case TwainResult.EndOfList: ErrorEspecifico = ex.ReturnCode.ToString(); break;
                        //case TwainResult.XferDone: ErrorEspecifico = ex.ReturnCode.ToString(); break;
                        //case TwainResult.Cancel: ErrorEspecifico = ex.ReturnCode.ToString(); break;
                        default: ErrorEspecifico = $"Error accediendo al escáner{br}{br}por favor solicite ayuda."; break;
                    }
                    LblMensajes.Text = ErrorEspecifico;
                    //LblMensajes.Text = $"No fue posible conectar con el escáner{br}{br}verifique el cable.";
                    //LblMensajes.Text = "Ubique la hoja en la ranura del escáner" + br + br + "y presione el botón Escanear.";
                    PanelMensaje.Visible = true;
                    if (ex.ReturnCode != null)
                    {
                       // Contexto.Logger.Error(ex, $"Iniciando Escaneo. ErrorCode: {ex.ReturnCode}");
                    }
                }
                finally
                {
                    Enabled = true; // HABILITA NUEVAMENTE LA FORMA PARA QUE SE PUEDAN USAR LOS BOTONES
                }
            }
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            NombreArchivoPDF = "";
            Close();
        }

        private void LblEscanearOtra_Click(object sender, EventArgs e)
        {
            CargarAgregarOtra(false);
            CargarEscanear(true);
        }

        private void LblFinalizarProceso_Click(object sender, EventArgs e)
        {
            PanelMensaje.Visible = true;
            LblMensajes.Text = "Generando archivo...";
            LblMensajes.Update();
            PanelMensaje.Update();
            Thread.Sleep(10);
            CrearArchivoPDF();
        }

        public void CrearArchivoPDF()
        {
            ITextPDF pdf = new ITextPDF();
            DateTime h = DateTime.Now;
            NombreArchivoPDF = _rutaGuardar + string.Format(@"Img{0}{1}.pdf", "Scan", h.ToString("yyyyMMdd-HHmmss"));
            pdf.ArchivoDestino = NombreArchivoPDF;
            pdf.CrearPDFconImagenes(ListaImagenes);
            Close();
        }

        private void Enviar_Click(object sender, EventArgs e)
        {
            // Crear el Mail
            using (System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage())
            {
                //mail.To.Add(new System.Net.Mail.MailAddress("fpinilla@virtual.com.co"));
                //mail.From = new System.Net.Mail.MailAddress("fpinilla@virtual.com.co", "Fredy Pinilla", System.Text.Encoding.UTF8);
                //mail.Subject = "Escan PQR";
                //mail.SubjectEncoding = System.Text.Encoding.UTF8;
                //mail.Body = "Petición pqr";
                //mail.BodyEncoding = System.Text.Encoding.UTF8;
                //mail.IsBodyHtml = true;

                // Agregar el Adjunto si deseamos hacerlo
                //mail.Attachments.Add(new Attachment(@"C:\\Scan_1.jpg"));

                // Configuración SMTP
                //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.virtual.com.co", 25);

                // Crear Credencial de Autenticacion
                //            smtp.Credentials = new System.Net.NetworkCredential("fpinilla@virtual.com.co", "fpinilla");
                //            smtp.EnableSsl = false;

                /*try
                { 
                    //smtp.Send(mail);
                MessageBox.Show("Mensaje Enviado");
                }
                catch (Exception ex)
                { throw ex;
                  MessageBox.Show("Mensaje Fallido");
                }*/
            } // end using mail

        }

        private void EscaneoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _twain.TransferImage -= _twain_TransferImage;
            _twain.ScanningComplete -= _twain_ScanningComplete;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

    } 




}