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
using ClienteWPF.Models;
using Newtonsoft.Json;
using System.Text;

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>

    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void TxtUser_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUser.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void TxtPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private async void BtnEntrar_Click(object sender, RoutedEventArgs e)
        {
            string user = txtUser.Text;
            string pass = txtPassword.Password;

            var userObject = new
            {
                correo = user,
                contrasena = pass
            };

            var json = JsonConvert.SerializeObject(userObject);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://localhost:8080/api/auth";


            using (HttpClient client = new HttpClient())

            {
                var response = await client.PostAsync(url, data);
                response.EnsureSuccessStatusCode();
                var res = await response.Content.ReadAsStringAsync();
                var userList = JsonConvert.DeserializeObject<dynamic>(res);

                //validacion mientras la api responde el cod correcto
                var usuario_info = userList.idUsuario.ToObject<int>();

                //if (response.IsSuccessStatusCode)
                if (usuario_info != 0)
                {
                    //message.Content = await response.Content.ReadAsStringAsync();
                    Menu ventana = new Menu();
                    ventana.Show();
                }
                else
                {
                    message.Content = $"Server error code {response.StatusCode}";
                }
            }
        }

        private void TxtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Foreground = new SolidColorBrush(Colors.LightGray);
        }

        private void TxtUser_LostFocus(object sender, RoutedEventArgs e)
        {
            txtUser.Foreground = new SolidColorBrush(Colors.LightGray);
        }

    }
}