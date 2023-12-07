using FacturasEnel.Util;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FacturasEnel.Teclado
{
    /// <summary>
    /// Lógica de interacción para tecladoNumerico.xaml
    /// </summary>
    public partial class TecladoAlfaNumericoCaracteres : UserControl, INotifyPropertyChanged
    {
        public object CampoConTexto { get; set; }
        //public TipoDeCampo Campo { get; set; }
        //public TextBox campoAEscribir { get; set; }
        //public PasswordBox CampoContraseña { get; set; }

        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }
        public string E { get; set; }
        public string F { get; set; }
        public string G { get; set; }
        public string H { get; set; }
        public string I { get; set; }
        public string J { get; set; }
        public string K { get; set; }
        public string L { get; set; }
        public string M { get; set; }
        public string N { get; set; }
        public string Ñ { get; set; }
        public string O { get; set; }
        public string P { get; set; }
        public string Q { get; set; }
        public string R { get; set; }
        public string S { get; set; }
        public string T { get; set; }
        public string U { get; set; }
        public string V { get; set; }
        public string W { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
        private bool Mayuscula { get; set; }

        public TecladoAlfaNumericoCaracteres()
        {
            InitializeComponent();
            this.DataContext = this;
            EstablecerImagenes();
        }

        private void EstablecerImagenes()
        {
            //ImagenEliminar_Alfa.Source = Utilitario.EstablecerImagen(@"Util\submn\btn_Eliminar02.png");
            //ImagenEliminar.Source = Utilitario.EstablecerImagen(@"Util\submn\btn_Eliminar02.png");

            //ImagenEliminar_Alfa.Source = new BitmapImage(new Uri(new FileInfo(ENEL.Properties.Settings.Default.Ima_AUTENTICACION_btn_Eliminar_Estado_2.ToString()).FullName));
            //ImagenEliminar.Source = new BitmapImage(new Uri(new FileInfo(ENEL.Properties.Settings.Default.Ima_AUTENTICACION_btn_Eliminar_Estado_2.ToString()).FullName));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void CampoFocused(object campo)
        {
            if (campo is TextBox textBox)
                CampoConTexto = textBox;
            else if (campo is PasswordBox passwordBox)
                CampoConTexto = passwordBox;
            else if (campo is TextBlock textBlock)
                CampoConTexto = textBlock;
            else if (campo is AccessText accessText)
                CampoConTexto = accessText;
            else if (campo is RichTextBox richTextBox)
                CampoConTexto = richTextBox;
        }

        private void TeclaPresionada(object sender, RoutedEventArgs e)
        {
            if (CampoConTexto == null)
                return;

            string tecla = (sender as Button).Content.ToString();
            if (string.IsNullOrEmpty(tecla))
                return;

            if ((CampoConTexto is TextBox) || (CampoConTexto is TextBlock) || (CampoConTexto is AccessText) || (CampoConTexto is RichTextBox))
            {
                var Control = CampoConTexto as TextBox;

                switch (tecla)
                {
                    case "Borrar":
                        {
                            if (Control.Text.Length > 0)
                            {
                                var posicionActualCampo = Control.SelectionStart - 1;
                                if (posicionActualCampo >= 0)
                                {
                                    Control.Text = Control.Text.Remove(posicionActualCampo, 1);
                                    Control.SelectionStart = posicionActualCampo;
                                }
                                if (posicionActualCampo == -1)
                                {
                                    Control.Text = Control.Text.Remove(Control.Text.Length - 1, 1);
                                    Control.SelectionStart = Control.Text.Length;
                                }
                            }
                        }
                        break;
                    case "Espacio":
                    default:
                        {
                            if (tecla == "Espacio")
                                tecla = " ";

                            var posicionActualCampo = Control.SelectionStart;
                            if (posicionActualCampo >= 0)
                            {
                                Control.Text = Control.Text.Insert(posicionActualCampo, tecla);
                                Control.SelectionStart = Control.Text.Length;
                            }
                        }
                        break;
                }
            }
            else // El campo de contraseña esta seleccionado
            {
                var Control = CampoConTexto as PasswordBox;

                switch (tecla)
                {
                    case "Espacio": Control.Password += " "; break;
                    case "Borrar":
                        {
                            if (Control.Password.Length > 0)
                            {
                                var nuevovalor = Control.Password.Remove(Control.Password.Length - 1, 1);
                                Control.Password = nuevovalor;
                            }
                        }
                        break;
                    default: Control.Password += tecla; break;
                }
            }
        }


        private void Btn_Mayusculas(object sender, RoutedEventArgs e)
        {
            if (this.Mayuscula)
            {
                Mayuscula = false;
                this.A = this.A.ToLower();
                this.B = this.B.ToLower();
                this.C = this.C.ToLower();
                this.D = this.D.ToLower();
                this.E = this.E.ToLower();
                this.F = this.F.ToLower();
                this.G = this.G.ToLower();
                this.H = this.H.ToLower();
                this.I = this.I.ToLower();
                this.J = this.J.ToLower();
                this.K = this.K.ToLower();
                this.L = this.L.ToLower();
                this.M = this.M.ToLower();
                this.N = this.N.ToLower();
                this.Ñ = this.Ñ.ToLower();
                this.O = this.O.ToLower();
                this.P = this.P.ToLower();
                this.Q = this.Q.ToLower();
                this.R = this.R.ToLower();
                this.S = this.S.ToLower();
                this.T = this.T.ToLower();
                this.U = this.U.ToLower();
                this.V = this.V.ToLower();
                this.W = this.W.ToLower();
                this.X = this.X.ToLower();
                this.Y = this.Y.ToLower();
                this.Z = this.Z.ToLower();
            }
            else
            {
                this.Mayuscula = true;
                this.A = this.A.ToUpper();
                this.B = this.B.ToUpper();
                this.C = this.C.ToUpper();
                this.D = this.D.ToUpper();
                this.E = this.E.ToUpper();
                this.F = this.F.ToUpper();
                this.G = this.G.ToUpper();
                this.H = this.H.ToUpper();
                this.I = this.I.ToUpper();
                this.J = this.J.ToUpper();
                this.K = this.K.ToUpper();
                this.L = this.L.ToUpper();
                this.M = this.M.ToUpper();
                this.N = this.N.ToUpper();
                this.Ñ = this.Ñ.ToUpper();
                this.O = this.O.ToUpper();
                this.P = this.P.ToUpper();
                this.Q = this.Q.ToUpper();
                this.R = this.R.ToUpper();
                this.S = this.S.ToUpper();
                this.T = this.T.ToUpper();
                this.U = this.U.ToUpper();
                this.V = this.V.ToUpper();
                this.W = this.W.ToUpper();
                this.X = this.X.ToUpper();
                this.Y = this.Y.ToUpper();
                this.Z = this.Z.ToUpper();
            }
            this.NotificarCambio();
        }

        private void Btn_Numeros_Click(object sender, RoutedEventArgs e)
        {
            this.Fila_AlfaBetico.Visibility = Visibility.Collapsed;
            this.Fila_TecladoCaracteres.Visibility = Visibility.Visible;
        }

        private void Btn_Abc_Click(object sender, RoutedEventArgs e)
        {
            this.Fila_AlfaBetico.Visibility = Visibility.Visible;
            this.Fila_TecladoCaracteres.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.EstablecerTecladoAlfabetico();
            Mayuscula = true;
            Btn_Mayusculas(null, null);
            this.NotificarCambio();
        }

        private void EstablecerTecladoAlfabetico()
        {
            this.A = "A";
            this.B = "B";
            this.C = "C";
            this.D = "D";
            this.E = "E";
            this.F = "F";
            this.G = "G";
            this.H = "H";
            this.I = "I";
            this.J = "J";
            this.K = "K";
            this.L = "L";
            this.M = "M";
            this.N = "N";
            this.Ñ = "Ñ";
            this.O = "O";
            this.P = "P";
            this.Q = "Q";
            this.R = "R";
            this.S = "S";
            this.T = "T";
            this.U = "U";
            this.V = "V";
            this.W = "W";
            this.X = "X";
            this.Y = "Y";
            this.Z = "Z";
        }

        private void NotificarCambio()
        {
            NotifyPropertyChanged(this.A);
            NotifyPropertyChanged(this.B);
            NotifyPropertyChanged(this.C);
            NotifyPropertyChanged(this.D);
            NotifyPropertyChanged(this.E);
            NotifyPropertyChanged(this.F);
            NotifyPropertyChanged(this.G);
            NotifyPropertyChanged(this.H);
            NotifyPropertyChanged(this.I);
            NotifyPropertyChanged(this.J);
            NotifyPropertyChanged(this.K);
            NotifyPropertyChanged(this.L);
            NotifyPropertyChanged(this.M);
            NotifyPropertyChanged(this.N);
            NotifyPropertyChanged(this.Ñ);
            NotifyPropertyChanged(this.O);
            NotifyPropertyChanged(this.P);
            NotifyPropertyChanged(this.Q);
            NotifyPropertyChanged(this.R);
            NotifyPropertyChanged(this.S);
            NotifyPropertyChanged(this.T);
            NotifyPropertyChanged(this.U);
            NotifyPropertyChanged(this.V);
            NotifyPropertyChanged(this.W);
            NotifyPropertyChanged(this.X);
            NotifyPropertyChanged(this.Y);
            NotifyPropertyChanged(this.Z);
        }


        #region FUNCIONES ANTERIORES DE DON BREYNER GARZON 
        //private void Button_Click_2(object sender, RoutedEventArgs e)
        //{
        //    if (!campoAEscribir.Text.Contains("."))
        //    {
        //        campoAEscribir.Text += ".";
        //    }
        //}

        //public void EscribirEnCampoBreyner(TextBox campo)
        //{
        //    this.campoAEscribir = campo;
        //    this.Campo = TipoDeCampo.CampoIdUsuario;
        //}

        //public void EscribirEnPassBreyner(PasswordBox campopass)
        //{
        //    this.CampoContraseña = campopass;
        //    this.Campo = TipoDeCampo.CampoPassUsuario;
        //}

        //private void AgregarTeclaBreyner(object sender, RoutedEventArgs e)
        //{
        //    if (campoAEscribir != null)
        //    {
        //        var valorTeclaSeleccionada = (Button)sender;
        //        if (this.Campo == TipoDeCampo.CampoIdUsuario) // El campo de texto id Usuario esta seleccionado
        //        {
        //            var valorCampoActual = campoAEscribir.Text;
        //            //var nuevoValorDelCampo = campoAEscribir.Text + valorTeclaSeleccionada.Content;
        //            if (string.IsNullOrEmpty(valorTeclaSeleccionada.Content.ToString()))
        //            {
        //                this.campoAEscribir.Text = valorCampoActual + " ";
        //            }
        //            else
        //            {
        //                this.campoAEscribir.Text = valorCampoActual + valorTeclaSeleccionada.Content;
        //            }
        //            //campoAEscribir.Text = nuevoValorDelCampo;
        //        }
        //        else // El campo de contraseña esta seleccionado
        //        {
        //            var valorCampoActual = CampoContraseña.Password.ToString();
        //            this.CampoContraseña.Password = valorCampoActual + valorTeclaSeleccionada.Content;
        //        }


        //        //var tecla = (Button)sender;
        //        //var valorTeclaSeleccionada = tecla.Content.ToString();
        //        //var valorCampoActual = campoAEscribir.Text;
        //        //if (string.IsNullOrEmpty(valorTeclaSeleccionada))
        //        //{
        //        //    this.campoAEscribir.Text = valorCampoActual + " ";
        //        //}
        //        //else
        //        //{
        //        //    this.campoAEscribir.Text = valorCampoActual + valorTeclaSeleccionada;
        //        //}
        //    }
        //}

        //private void BorrarTeclaBreyner(object sender, RoutedEventArgs e)
        //{
        //    if (this.Campo == TipoDeCampo.CampoIdUsuario) // El campo de texto id Usuario esta seleccionado
        //    {
        //        if (campoAEscribir.Text.Length > 0)
        //        {
        //            var posicionActualCampo = campoAEscribir.SelectionStart - 1;
        //            if (posicionActualCampo >= 0)
        //            {
        //                campoAEscribir.Text = campoAEscribir.Text.Remove(posicionActualCampo, 1);
        //                campoAEscribir.SelectionStart = posicionActualCampo;
        //            }
        //            if (posicionActualCampo == -1)
        //            {
        //                campoAEscribir.Text = campoAEscribir.Text.Remove(campoAEscribir.Text.Length - 1, 1);
        //                campoAEscribir.SelectionStart = campoAEscribir.Text.Length;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (CampoContraseña.Password.Length > 0)
        //        {
        //            var nuevovalor = CampoContraseña.Password.Remove(CampoContraseña.Password.Length - 1, 1);
        //            CampoContraseña.Password = nuevovalor;
        //        }
        //    }



        //    //if (campoAEscribir.Text.Length > 0)
        //    //{
        //    //    var posicionActualCampo = campoAEscribir.SelectionStart - 1;

        //    //    if (posicionActualCampo >= 0)
        //    //    {
        //    //        var nuevovalor = campoAEscribir.Text.Remove(posicionActualCampo, 1);
        //    //        campoAEscribir.Text = nuevovalor;
        //    //        campoAEscribir.SelectionStart = campoAEscribir.Text.Length;
        //    //    }
        //    //    if (posicionActualCampo == -1)
        //    //    {
        //    //        var nuevovalor = campoAEscribir.Text.Remove(campoAEscribir.Text.Length - 1, 1);
        //    //        campoAEscribir.Text = nuevovalor;
        //    //        campoAEscribir.SelectionStart = campoAEscribir.Text.Length;
        //    //    }
        //    //}
        //}

        //private void BorrarTecla(object sender, RoutedEventArgs e)
        //{
        //    if (this.Campo == TipoDeCampo.CampoIdUsuario) // El campo de texto id Usuario esta seleccionado
        //    {
        //        if (campoAEscribir.Text.Length > 0)
        //        {
        //            var posicionActualCampo = campoAEscribir.SelectionStart - 1;
        //            if (posicionActualCampo >= 0)
        //            {
        //                campoAEscribir.Text = campoAEscribir.Text.Remove(posicionActualCampo, 1);
        //                campoAEscribir.SelectionStart = posicionActualCampo;
        //            }
        //            if (posicionActualCampo == -1)
        //            {
        //                campoAEscribir.Text = campoAEscribir.Text.Remove(campoAEscribir.Text.Length - 1, 1);
        //                campoAEscribir.SelectionStart = campoAEscribir.Text.Length;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (CampoContraseña.Password.Length > 0)
        //        {
        //            var nuevovalor = CampoContraseña.Password.Remove(CampoContraseña.Password.Length - 1, 1);
        //            CampoContraseña.Password = nuevovalor;
        //        }
        //    }
        //}


        #endregion
    }
}
