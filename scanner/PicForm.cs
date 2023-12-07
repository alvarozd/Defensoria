using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

using GdiPlusLib;

namespace TwainGui
{

    public class PicForm : System.Windows.Forms.Form
    {
        //public string NombreImagen;
        private IContainer components;
        private PictureBox ImagenEscaneada;

        public PicForm() //IntPtr dibhandp
        {

            InitializeComponent();


            //SetStyle(ControlStyles.DoubleBuffer, false);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.Opaque, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.UserPaint, true);

            //bmprect = new Rectangle(0, 0, 0, 0);
            //dibhand = dibhandp;
            //bmpptr = GlobalLock(dibhand);
            //pixptr = GetPixelInfo(bmpptr);

            //this.AutoScrollMinSize = new System.Drawing.Size(bmprect.Width, bmprect.Height);
        }

        public void CargarImagen(string NombreArchivo)
        {
            Image tempImage = Image.FromFile(NombreArchivo);
            Bitmap tempBitmap = new Bitmap(tempImage);
            ImagenEscaneada.Image = tempBitmap;
            tempImage.Dispose();
            //tempBitmap.Dispose();
            Top = (Screen.PrimaryScreen.Bounds.Height - 1024) / 2;
            Left = (Screen.PrimaryScreen.Bounds.Width - 1280) / 2;
            Height = 1024;
            Width = 1280;

            Show();
            Visible = true;
            BringToFront();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dibhand != IntPtr.Zero)
                {
                    GlobalFree(dibhand);
                    dibhand = IntPtr.Zero;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ImagenEscaneada = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ImagenEscaneada)).BeginInit();
            this.SuspendLayout();
            // 
            // ImagenEscaneada
            // 
            this.ImagenEscaneada.Location = new System.Drawing.Point(1, -2);
            this.ImagenEscaneada.Name = "ImagenEscaneada";
            this.ImagenEscaneada.Size = new System.Drawing.Size(233, 111);
            this.ImagenEscaneada.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ImagenEscaneada.TabIndex = 0;
            this.ImagenEscaneada.TabStop = false;
            // 
            // PicForm
            // 
            this.AutoScroll = true;
            this.AutoScrollMinSize = new System.Drawing.Size(256, 256);
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(464, 200);
            this.Controls.Add(this.ImagenEscaneada);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(80, 80);
            this.Name = "PicForm";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.Text = "PicForm";
            this.Load += new System.EventHandler(this.PicForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImagenEscaneada)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        //protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        //{
        //    Rectangle cltrect = ClientRectangle;
        //    Rectangle clprect = e.ClipRectangle;
        //    Point scrol = AutoScrollPosition;

        //    Rectangle realrect = clprect;
        //    realrect.X -= scrol.X;
        //    realrect.Y -= scrol.Y;

        //    SolidBrush brbg = new SolidBrush(Color.Black);
        //    if (realrect.Right > bmprect.Width)
        //    {
        //        Rectangle bgri = clprect;
        //        int ovri = bmprect.Width - realrect.X;
        //        if (ovri > 0)
        //        {
        //            bgri.X += ovri;
        //            bgri.Width -= ovri;
        //        }
        //        e.Graphics.FillRectangle(brbg, bgri);
        //    }

        //    if (realrect.Bottom > bmprect.Height)
        //    {
        //        Rectangle bgbo = clprect;
        //        int ovbo = bmprect.Height - realrect.Y;
        //        if (ovbo > 0)
        //        {
        //            bgbo.Y += ovbo;
        //            bgbo.Height -= ovbo;
        //        }
        //        e.Graphics.FillRectangle(brbg, bgbo);
        //    }

        //    realrect.Intersect(bmprect);
        //    if (!realrect.IsEmpty)
        //    {
        //        int bot = bmprect.Height - realrect.Bottom;
        //        IntPtr hdc = e.Graphics.GetHdc();
        //        SetDIBitsToDevice(hdc, clprect.X, clprect.Y, realrect.Width, realrect.Height,
        //                realrect.X, bot, 0, bmprect.Height, pixptr, bmpptr, 0);
        //        e.Graphics.ReleaseHdc(hdc);
        //    }
        //}

        //protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        //{
        //}

        //protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        //{
        //    this.Menu.MenuItems.Clear();
        //    base.OnClosing(e);
        //}



        private void menuItemClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void menuItemInfo_Click(object sender, System.EventArgs e)
        {
            InfoForm iform = new InfoForm(bmi);
            iform.ShowDialog(this);
        }

        private void menuItemSaveAs_Click(object sender, System.EventArgs e)
        {
            Gdip.SaveDIBAs(this.Text, bmpptr, pixptr);
        }


        protected IntPtr GetPixelInfo(IntPtr bmpptr)
        {
            bmi = new BITMAPINFOHEADER();
            Marshal.PtrToStructure(bmpptr, bmi);

            bmprect.X = bmprect.Y = 0;
            bmprect.Width = bmi.biWidth;
            bmprect.Height = bmi.biHeight;

            if (bmi.biSizeImage == 0)
                bmi.biSizeImage = ((((bmi.biWidth * bmi.biBitCount) + 31) & ~31) >> 3) * bmi.biHeight;

            int p = bmi.biClrUsed;
            if ((p == 0) && (bmi.biBitCount <= 8))
                p = 1 << bmi.biBitCount;
            p = (p * 4) + bmi.biSize + (int)bmpptr;
            return (IntPtr)p;
        }

        BITMAPINFOHEADER bmi;
        Rectangle bmprect;
        IntPtr dibhand;
        IntPtr bmpptr;
        IntPtr pixptr;

        [DllImport("gdi32.dll", ExactSpelling = true)]
        internal static extern int SetDIBitsToDevice(IntPtr hdc, int xdst, int ydst,
                                            int width, int height, int xsrc, int ysrc, int start, int lines,
                                            IntPtr bitsptr, IntPtr bmiptr, int color);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalLock(IntPtr handle);
        [DllImport("kernel32.dll", ExactSpelling = true)]
        internal static extern IntPtr GlobalFree(IntPtr handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void OutputDebugString(string outstr);

        private void PicForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            Top = (Screen.PrimaryScreen.Bounds.Height - 1024) / 2;
            Left = (Screen.PrimaryScreen.Bounds.Width - 1280) / 2;
            Height = 1024;
            Width = 1280;
        }
    } // class PicForm


    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    internal class BITMAPINFOHEADER
    {
        public int biSize;
        public int biWidth;
        public int biHeight;
        public short biPlanes;
        public short biBitCount;
        public int biCompression;
        public int biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public int biClrUsed;
        public int biClrImportant;
    }

} // namespace TwainGui
