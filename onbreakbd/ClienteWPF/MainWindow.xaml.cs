using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BibliotecaCliente;

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        List<Cliente> miCliente = new List<Cliente>();
        

        public MainWindow()
        {
            InitializeComponent();

        }

   

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Window2 ventana2 = new Window2();
            ventana2.Owner = this;
            ventana2.ShowDialog();
        }


        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Hide();
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Window1 ventana= new Window1();
            ventana.Show();
            this.Close();
        }

        private void BtnCliente_Click(object sender, RoutedEventArgs e)
        {
            Window2 window = new Window2();
            window.Show();
            this.Close();
        }

        private void BtnContrato_Click(object sender, RoutedEventArgs e)
        {
            ListaContra window = new ListaContra();
            window.Show();

        }

        private void BtnCliente_Click_1(object sender, RoutedEventArgs e)
        {
            ListaCltes window = new ListaCltes();
            window.Show();

        }
    }
}
