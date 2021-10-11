using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BibliotecaCliente;
using System.Windows.Media;
using System.Net.Http;

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>

    public partial class MainWindow : MetroWindow
    {
        Dictionary<string, string> usuarios = new Dictionary<string, string>()
        {
            { "lucas", "lucas123" },
            { "tomas", "tomas123" },
            { "profesor", "profe123" }
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TxtUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsuario.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void TxtContraseña_GotFocus(object sender, RoutedEventArgs e)
        {
            txtContraseña.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private async void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtContraseña.Password;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"http://localhost:8080/api/{usuario}");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    message.Content = await response.Content.ReadAsStringAsync();
                    Menu ventana = new Menu();
                    ventana.Show();
                }
                else
                {
                    message.Content = $"Server error code {response.StatusCode}";
                }
            }
        }

        private void TxtContraseña_LostFocus(object sender, RoutedEventArgs e)
        {
            txtContraseña.Foreground = new SolidColorBrush(Colors.LightGray);
        }

        private void TxtUsuario_LostFocus(object sender, RoutedEventArgs e)
        {
            txtUsuario.Foreground = new SolidColorBrush(Colors.LightGray);
        }

        private void highContrast()
        {
            LinearGradientBrush back = (LinearGradientBrush)fondo.BorderBrush;
            back.GradientStops[0].Color = Colors.Green;
            back.GradientStops[1].Color = Colors.Black;
            lblTitulo.Foreground = new SolidColorBrush(Colors.Green);
            lblIngreso.Foreground = new SolidColorBrush(Colors.Green);
            lblUsuario.Foreground = new SolidColorBrush(Colors.Green);
            lblContraseña.Foreground = new SolidColorBrush(Colors.Green);
            fondoInterno.Background = new SolidColorBrush(Colors.Black);
            borde.BorderBrush = new SolidColorBrush(Colors.Green);
            BtnEntrar.Background = new SolidColorBrush(Colors.Green);
            BtnEntrar.Foreground = new SolidColorBrush(Colors.Black);
            //Info.highContrast = true;
        }

        private void lowContrast()
        {
            LinearGradientBrush back = (LinearGradientBrush)fondo.BorderBrush;
            Color miColor = Color.FromRgb(17, 144, 203);
            back.GradientStops[0].Color = miColor;
            back.GradientStops[1].Color = Colors.White;
            lblTitulo.Foreground = new SolidColorBrush(miColor);
            lblIngreso.Foreground = new SolidColorBrush(miColor);
            lblUsuario.Foreground = new SolidColorBrush(miColor);
            lblContraseña.Foreground = new SolidColorBrush(miColor);
            fondoInterno.Background = new SolidColorBrush(Colors.White);
            borde.BorderBrush = new SolidColorBrush(miColor);
            BtnEntrar.Background = new SolidColorBrush(Colors.Gray);
            BtnEntrar.Foreground = new SolidColorBrush(miColor);
            //Info.highContrast = false;
        }

        //private void HighContrast_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!Info.highContrast)
        //    {
        //        highContrast();
        //    }
        //    else
        //    {
        //        lowContrast();
        //    }
        //}
    }
}