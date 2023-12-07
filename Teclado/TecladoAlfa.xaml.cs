
using System;
using System.Windows;
using System.Windows.Controls;

namespace FacturasEnel.Teclado
{
    /// <summary>
    /// Lógica de interacción para tecladoNumerico.xaml
    /// </summary>
    public partial class TecladoAlfa : UserControl
    {
        public TextBox campoAEscribir { get; set; }
        private string RutaFiles = Environment.CurrentDirectory + "\\";

        public TecladoAlfa()
        {
            InitializeComponent();
        }
                
        public void EscribirEnCampo(TextBox campo)
        {
            this.campoAEscribir = campo;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button tecla = (Button)sender;
            string valorTeclaSeleccionada = tecla.Content.ToString();
            var valorCampoActual = campoAEscribir.Text;
            if (string.IsNullOrEmpty(valorTeclaSeleccionada))
            {
                this.campoAEscribir.Text = valorCampoActual + " ";
            }
            else
            {
                this.campoAEscribir.Text = valorCampoActual + valorTeclaSeleccionada;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (campoAEscribir.Text.Length > 0)
            {
                var posicionActualCampo = campoAEscribir.SelectionStart - 1;

                if (posicionActualCampo >= 0)
                {
                    var nuevovalor = campoAEscribir.Text.Remove(posicionActualCampo, 1);
                    campoAEscribir.Text = nuevovalor;
                    campoAEscribir.SelectionStart = campoAEscribir.Text.Length;
                }
                if (posicionActualCampo == -1)
                {
                    var nuevovalor = campoAEscribir.Text.Remove(campoAEscribir.Text.Length - 1, 1);
                    campoAEscribir.Text = nuevovalor;
                    campoAEscribir.SelectionStart = campoAEscribir.Text.Length;
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!campoAEscribir.Text.Contains("."))
            {
                campoAEscribir.Text += ".";
            }
        }
    }
}
