using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using BibliotecaCliente;
using System.Data;
using MahApps.Metro.Controls.Dialogs;
using System.Net.Http;

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para Window2.xaml
    /// </summary>
    public partial class Clientes : MetroWindow
    {

        public Clientes()
        {
            InitializeComponent();
            cargarCombos();
            Cliente cliente = new Cliente();
            List<Cliente> listaCliente = new List<Cliente>();
            listaCliente.Add(cliente);

            mostrarClientes(listaCliente);
        }

        public Clientes(Cliente objCliente) {
            InitializeComponent();
            cargarCombos();

            List<Cliente> listaCliente = new List<Cliente>();
            listaCliente.Add(objCliente);
            
            mostrarClientes(listaCliente);

            dgClientes.SelectedIndex = 0;
            llenarCamposSeleccionados();
        }

        private void cargarCombos()
        {
            TipoEmpresa objTipoEmpresa = new TipoEmpresa();
            foreach (TipoEmpresa dato in objTipoEmpresa.ReadAll()) {
                cbotipo.Items.Add(dato.Descripcion.ToString());
            }

            ActividadEmpresa objActividadEmpresa = new ActividadEmpresa();
            foreach (ActividadEmpresa dato in objActividadEmpresa.ReadAll()) {
                cboactividad.Items.Add(dato.Descripcion.ToString());
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btneliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cliente cli = new Cliente();


                if (cli.Delete(txtrut.Text.ToString()))
                {
                    this.ShowMessageAsync("Datos eliminados", ":)");
                    Cliente objCliente = new Cliente();
                    List<Cliente> listaCliente = new List<Cliente>();
                    listaCliente.Add(objCliente);
                    mostrarClientes(listaCliente);
                }
                else {

                    this.ShowMessageAsync("Error al eliminar los datos", ":(");
                    Cliente objCliente = new Cliente();
                    List<Cliente> listaCliente = new List<Cliente>();
                    listaCliente.Add(objCliente);

                    mostrarClientes(listaCliente);
                }

                limpiar();
                mostrarClientes(cli.ReadAllByRut(txtrut.Text));
            }
            catch (Exception ex)
            {

                this.ShowMessageAsync("No se encuentran clientes", ":(");

            }
        }

        private void Btnguardar_Click(object sender, RoutedEventArgs e)
        {
            if (txtrut.Text != String.Empty && txtrazons.Text != String.Empty && txtnombrec.Text != String.Empty && txtmail.Text != String.Empty && txtdir.Text != String.Empty && txtfono.Text != String.Empty && cboactividad.Text != String.Empty && cbotipo.Text != String.Empty)
            {
                Cliente objCliente = new Cliente();

                objCliente.RutCliente = txtrut.Text.ToString();
                objCliente.RazonSocial = txtrazons.Text.ToString();
                objCliente.NombreContacto = txtnombrec.Text.ToString();
                objCliente.MailContacto = txtmail.Text.ToString();
                objCliente.Direccion = txtdir.Text.ToString();
                objCliente.Telefono = txtfono.Text.ToString();
                //Se suma +1 porque el index de un combobox parte de 0 al largo-1, y se multiplica por 10 ya que los id en la BD van de 10 en 10
                objCliente.IdActividadEmpresa = (cboactividad.SelectedIndex + 1);
                objCliente.IdTipoEmpresa = (cbotipo.SelectedIndex + 1) * 10;

                if (objCliente.Create())
                {
                    this.ShowMessageAsync("Datos guardados", ":)");
                    mostrarClientes(objCliente.ReadAllByRut(txtrut.Text)); ;
                }
                else
                {
                    this.ShowMessageAsync("Error al guardar los datos", ":(");

                }


            }
            else {

                this.ShowMessageAsync("No se ingresaron todos los datos", ":(");

            }
        }

        private void limpiar()
        {

            txtrut.Text = String.Empty;
            txtrazons.Text = String.Empty;
            txtnombrec.Text = String.Empty;
            txtmail.Text = String.Empty;
            txtdir.Text = String.Empty;
            txtfono.Text = String.Empty;
            cboactividad.Items.Clear();
            cbotipo.Items.Clear();
            cargarCombos();


        }

        private async void mostrarClientes(List<Cliente> listaClientes)
        {
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            ActividadEmpresa actividadEmpresa = new ActividadEmpresa();

            dgClientes.ItemsSource = null;
            dgClientes.Items.Clear();
            dgClientes.Columns.Clear();

            DataTable dt = new DataTable();

            dt.Columns.Add("Nombre");
            dt.Columns.Add("Apellido Paterno");
            dt.Columns.Add("Apellido Materno");
            dt.Columns.Add("DNI");
            dt.Columns.Add("Dirección");
            dt.Columns.Add("Código Postal");
            dt.Columns.Add("Correo");
            dt.Columns.Add("Pais");
            dt.Columns.Add("Rol");
            dt.Columns.Add("Estado");
            dt.Columns.Add("Términos y Condiciones");

            //var json = JsonConvert.SerializeObject(userObject);
            //var data = new StringContent(json, Encoding.UTF8, "application/json");
            var url = "http://localhost:8080/api/usuario/1";

            using (HttpClient client = new HttpClient())

            {
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    message.Content = await response.Content.ReadAsStringAsync();
                    
                }
                else
                {
                    message.Content = $"Server error code {response.StatusCode}";
                }
            }

            //foreach (Cliente dato in listaClientes) {
            //    DataRow row = dt.NewRow();

            //    actividadEmpresa.Read(dato.IdActividadEmpresa);
            //    tipoEmpresa.Read(dato.IdTipoEmpresa);

            //    row["Rut Cliente"] = dato.RutCliente;
            //    row["Razón social"] = dato.RazonSocial;
            //    row["Nombre contacto"] = dato.NombreContacto;
            //    row["Mail contacto"] = dato.MailContacto;
            //    row["Dirección"] = dato.Direccion;
            //    row["Telefono"] = dato.Telefono;
            //    row["Actividad Empresa"] = actividadEmpresa.Descripcion;
            //    row["Tipo Empresa"] = tipoEmpresa.Descripcion;

            //    dt.Rows.Add(row);
            //}

            dgClientes.ClearValue(ItemsControl.ItemsSourceProperty);
            dgClientes.ItemsSource = dt.DefaultView;
            dgClientes.UpdateLayout();
     
        }

        private void Cbotipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Btnbuscar_Click(object sender, RoutedEventArgs e)
        {
            if (txtrut.Text != String.Empty)
            {
                Cliente objCliente = new Cliente();
                mostrarClientes(objCliente.ReadAllByRut(txtrut.Text.ToString()));
                llenarCamposBuscados(objCliente.ReadAllByRut(txtrut.Text.ToString()));
            }
        }

        private void BtonModificar_Click(object sender, RoutedEventArgs e)
        {
            Cliente objCliente = new Cliente();

            objCliente.RutCliente = txtrut.Text.ToString();
            objCliente.RazonSocial = txtrazons.Text.ToString();
            objCliente.NombreContacto = txtnombrec.Text.ToString();
            objCliente.MailContacto = txtmail.Text.ToString();
            objCliente.Direccion = txtdir.Text.ToString();
            objCliente.Telefono = txtfono.Text.ToString();
            //Se suma +1 porque el index de un combobox parte de 0 al largo-1, y se multiplica por 10 ya que los id en la BD van de 10 en 10
            objCliente.IdActividadEmpresa = (cboactividad.SelectedIndex + 1);
            objCliente.IdTipoEmpresa = (cbotipo.SelectedIndex + 1) * 10;

            if (objCliente.Update(txtrut.Text.ToString()))
            {

                this.ShowMessageAsync("Modificación exitosa", ":)");
                mostrarClientes(objCliente.ReadAllByRut(txtrut.Text));

            }
            else {

                this.ShowMessageAsync("Error al modificar", ":(");

            }
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void BtnListar_Click(object sender, RoutedEventArgs e)
        {
            Cliente objCliente = new Cliente();
            mostrarClientes(objCliente.ReadAll());
        }

        private void DgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DgClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            llenarCamposSeleccionados();
        }

        private void BtnListarCltes_Click(object sender, RoutedEventArgs e) { 
                ListaCltes window = new ListaCltes(this);
                window.Show();
            
        }

        private void llenarCamposSeleccionados() {
            DataRowView row = (DataRowView)dgClientes.SelectedItem;

            if (row[0].ToString().Equals("") || dgClientes.Items.Count==0)
            {

            }
            else {
                txtrut.Text = row[0].ToString();
                txtrazons.Text = row[1].ToString();
                txtnombrec.Text = row[2].ToString();
                txtmail.Text = row[3].ToString();
                txtdir.Text = row[4].ToString();
                txtfono.Text = row[5].ToString();

                int indiceActividad = -1;
                if (row[6].ToString().Equals("Agropecuaria"))
                {
                    indiceActividad = 0;
                }
                if (row[6].ToString().Equals("Minería"))
                {
                    indiceActividad = 1;
                }
                if (row[6].ToString().Equals("Manufactura"))
                {
                    indiceActividad = 2;
                }
                if (row[6].ToString().Equals("Comercio"))
                {
                    indiceActividad = 3;
                }
                if (row[6].ToString().Equals("Hotelería"))
                {
                    indiceActividad = 4;
                }
                if (row[6].ToString().Equals("Alimentos"))
                {
                    indiceActividad = 5;
                }
                if (row[6].ToString().Equals("Transporte"))
                {
                    indiceActividad = 6;
                }
                if (row[6].ToString().Equals("Servicios"))
                {
                    indiceActividad = 7;
                }

                cboactividad.SelectedIndex = indiceActividad;

                int indiceTipo = -1;
                if (row[7].ToString().Equals("SPA"))
                {
                    indiceTipo = 0;
                }
                if (row[7].ToString().Equals("EIRL"))
                {
                    indiceTipo = 1;
                }
                if (row[7].ToString().Equals("Limitada"))
                {
                    indiceTipo = 2;
                }
                if (row[7].ToString().Equals("Sociedad Anónima"))
                {
                    indiceTipo = 3;
                }
                cbotipo.SelectedIndex = indiceTipo;
            }
        }

        private void llenarCamposBuscados(List<Cliente> listaCliente) {
            foreach (Cliente dato in listaCliente)
            {
                txtrut.Text = dato.RutCliente;
                txtrazons.Text = dato.RazonSocial;
                txtnombrec.Text = dato.NombreContacto;
                txtmail.Text = dato.MailContacto;
                txtdir.Text = dato.Direccion;
                txtfono.Text = dato.Telefono;
                cboactividad.SelectedIndex = dato.IdActividadEmpresa - 1;
                cbotipo.SelectedIndex = (dato.IdTipoEmpresa / 10) - 1;
            }
        }

        public void setGrid(List<Cliente> listaCliente) {
            mostrarClientes(listaCliente);
            llenarCamposBuscados(listaCliente);
        }

        private void Btnguardar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnguardar.Background = Brushes.LightBlue;

        }

        private void Btnguardar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnguardar.Background = (Brush)new BrushConverter().ConvertFrom("#FF0047AE");

        }

        private void BtonModificar_MouseEnter(object sender, MouseEventArgs e)
        {
            btonModificar.Background = Brushes.LightBlue;

        }

        private void BtonModificar_MouseLeave(object sender, MouseEventArgs e)
        {
            btonModificar.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void Btneliminar_MouseEnter(object sender, MouseEventArgs e)
        {
            btneliminar.Background = Brushes.LightBlue;

        }

        private void Btneliminar_MouseLeave(object sender, MouseEventArgs e)
        {
            btneliminar.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void BtnListarCltes_MouseEnter(object sender, MouseEventArgs e)
        {
            btnListarCltes.Background = Brushes.LightBlue;

        }

        private void BtnListarCltes_MouseLeave(object sender, MouseEventArgs e)
        {
            btnListarCltes.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void Btnbuscar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnbuscar.Background = Brushes.LightBlue;

        }

        private void Btnbuscar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnbuscar.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void BtnVolver_MouseEnter(object sender, MouseEventArgs e)
        {
            btnVolver.Background = Brushes.LightBlue;

        }

        private void BtnVolver_MouseLeave(object sender, MouseEventArgs e)
        {
            btnVolver.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void BtnLimpiar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnLimpiar.Background = Brushes.LightBlue;

        }

        private void BtnLimpiar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnLimpiar.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtrut.Text = "";
            txtrazons.Text = "";
            txtnombrec.Text = "";
            txtmail.Text = "";
            txtdir.Text = "";
            txtfono.Text = "";
            cboactividad.SelectedIndex = -1;
            cbotipo.SelectedIndex = -1;
        }
    }
}
