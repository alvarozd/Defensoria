using Comfandi_.Util;
using Comfandi_.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Comfandi_
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            EstablecerImagenes();
            EstablecerPageInicio();
        }

        private void EstablecerPageInicio()
        {
        }

        private void EstablecerImagenes()
        {
            try
            {
                AnimaciónSuperior.SourcePath = new FileInfo(Properties.Settings.Default.Ima_BANNERSUPERIOR.ToString()).FullName;
            }
            catch (Exception ex)
            {
                
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.FramePrincipal.Content = new Page_Principal(new Contexto());
        }
    }
}
