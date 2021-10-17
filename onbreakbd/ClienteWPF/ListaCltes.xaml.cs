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

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para ListaCltes.xaml
    /// </summary>
    public partial class ListaCltes : MetroWindow
    {
        Clientes mantCliente;
        public ListaCltes()
        {
            Cliente objCliente = new Cliente();
            InitializeComponent();
            cargarCombos();
            mostrarClientes(objCliente.ReadAll());
            mantCliente = null;
        }

        public ListaCltes(Clientes window) {
            Cliente objCliente = new Cliente();
            InitializeComponent();
            cargarCombos();
            mostrarClientes(objCliente.ReadAll());
            mantCliente = window;
        }

        private void cargarCombos() {
            cbotipo.Items.Add("-Ninguno-");
            cboactividad.Items.Add("-Ninguno-");

            TipoEmpresa objTipoEmpresa = new TipoEmpresa();
            foreach (TipoEmpresa dato in objTipoEmpresa.ReadAll())
            {
                cbotipo.Items.Add(dato.Descripcion.ToString());
            }

            ActividadEmpresa objActividadEmpresa = new ActividadEmpresa();
            foreach (ActividadEmpresa dato in objActividadEmpresa.ReadAll())
            {
                cboactividad.Items.Add(dato.Descripcion.ToString());
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buscar();
        }

        private void Cbotipo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buscar();
        }

        private void Txtrut_TextChanged(object sender, TextChangedEventArgs e)
        {
            buscar();
        }

        private void mostrarClientes(List<Cliente> listaCliente) {
            Cliente objCliente = new Cliente();
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            ActividadEmpresa actividadEmpresa = new ActividadEmpresa();

            dgClientes.ItemsSource = null;
            dgClientes.Items.Clear();
            dgClientes.Columns.Clear();

            DataTable dt = new DataTable();

            dt.Columns.Add("Rut Cliente");
            dt.Columns.Add("Razón social");
            dt.Columns.Add("Nombre contacto");
            dt.Columns.Add("Mail contacto");
            dt.Columns.Add("Dirección");
            dt.Columns.Add("Telefono");
            dt.Columns.Add("Actividad Empresa");
            dt.Columns.Add("Tipo Empresa");

            foreach (Cliente dato in listaCliente)
            {
                DataRow row = dt.NewRow();

                actividadEmpresa.Read(dato.IdActividadEmpresa);
                tipoEmpresa.Read(dato.IdTipoEmpresa);

                row["Rut Cliente"] = dato.RutCliente;
                row["Razón social"] = dato.RazonSocial;
                row["Nombre contacto"] = dato.NombreContacto;
                row["Mail contacto"] = dato.MailContacto;
                row["Dirección"] = dato.Direccion;
                row["Telefono"] = dato.Telefono;
                row["Actividad Empresa"] = actividadEmpresa.Descripcion;
                row["Tipo Empresa"] = tipoEmpresa.Descripcion;

                dt.Rows.Add(row);
            }

            dgClientes.ClearValue(ItemsControl.ItemsSourceProperty);
            dgClientes.ItemsSource = dt.DefaultView;
            dgClientes.UpdateLayout();
        }

        private void DgClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)dgClientes.SelectedItem;
            Cliente objCliente = new Cliente();

            if (row[0].ToString().Equals(""))
            {

            }
            else {
                objCliente.RutCliente = row[0].ToString();
                objCliente.RazonSocial = row[1].ToString();
                objCliente.NombreContacto = row[2].ToString();
                objCliente.MailContacto = row[3].ToString();
                objCliente.Direccion = row[4].ToString();
                objCliente.Telefono = row[5].ToString();

                int indiceActividad = -1;
                if (row[6].ToString().Equals("Agropecuaria"))
                {
                    indiceActividad = 1;
                }
                if (row[6].ToString().Equals("Minería"))
                {
                    indiceActividad = 2;
                }
                if (row[6].ToString().Equals("Manufactura"))
                {
                    indiceActividad = 3;
                }
                if (row[6].ToString().Equals("Comercio"))
                {
                    indiceActividad = 4;
                }
                if (row[6].ToString().Equals("Hotelería"))
                {
                    indiceActividad = 5;
                }
                if (row[6].ToString().Equals("Alimentos"))
                {
                    indiceActividad = 6;
                }
                if (row[6].ToString().Equals("Transporte"))
                {
                    indiceActividad = 7;
                }
                if (row[6].ToString().Equals("Servicios"))
                {
                    indiceActividad = 8;
                }

                objCliente.IdActividadEmpresa = indiceActividad;

                int indiceTipo = -1;
                if (row[7].ToString().Equals("SPA"))
                {
                    indiceTipo = 10;
                }
                if (row[7].ToString().Equals("EIRL"))
                {
                    indiceTipo = 20;
                }
                if (row[7].ToString().Equals("Limitada"))
                {
                    indiceTipo = 30;
                }
                if (row[7].ToString().Equals("Sociedad Anónima"))
                {
                    indiceTipo = 40;
                }

                objCliente.IdTipoEmpresa = indiceTipo;

                if (mantCliente != null)
                {
                    List<Cliente> listaCliente = new List<Cliente>();
                    listaCliente.Add(objCliente);

                    mantCliente.setGrid(listaCliente);
                    this.Close();
                }
                else
                {
                    Clientes ventana = new Clientes(objCliente);
                    ventana.Show();
                    this.Close();
                }
            }
        }

        private void buscar() {
            int opcion = 0;

            if (txtrut.Text.Equals("") == false) {
                opcion = opcion + 1;
            }
            if (cboactividad.SelectedIndex > 0) {
                opcion = opcion + 2;
            }
            if (cbotipo.SelectedIndex > 0) {
                opcion = opcion + 4;
            }

            Cliente objCliente = new Cliente();

            switch (opcion) {
                case 1:
                    mostrarClientes(objCliente.ReadAllByRut(txtrut.Text));
                    break;
                case 2:
                    mostrarClientes(objCliente.ReadAllByActividad(cboactividad.SelectedIndex));
                    break;
                case 3:
                    List<Cliente> listaRutActividad = new List<Cliente>();
                    foreach (Cliente dato in objCliente.ReadAll()) {
                        if (dato.RutCliente.Equals(txtrut.Text) &&
                            dato.IdActividadEmpresa == cboactividad.SelectedIndex) {
                            listaRutActividad.Add(dato);
                        }
                    }

                    mostrarClientes(listaRutActividad);
                    break;
                case 4:
                    mostrarClientes(objCliente.ReadAllByTipoEmpresa(cbotipo.SelectedIndex * 10));
                    break;
                case 5:
                    List<Cliente> listaRutTipo = new List<Cliente>();
                    foreach (Cliente dato in objCliente.ReadAll()) {
                        if (dato.RutCliente.Equals(txtrut.Text) && dato.IdTipoEmpresa ==
                            cbotipo.SelectedIndex * 10) {
                            listaRutTipo.Add(dato);
                        }
                    }

                    mostrarClientes(listaRutTipo);
                    break;
                case 6:
                    List<Cliente> listaActividadTipo = new List<Cliente>();
                    foreach (Cliente dato in objCliente.ReadAll()) {
                        if (dato.IdActividadEmpresa == cboactividad.SelectedIndex && dato.IdTipoEmpresa ==
                            cbotipo.SelectedIndex * 10) {
                            listaActividadTipo.Add(dato);
                        }
                    }

                    mostrarClientes(listaActividadTipo);
                    break;
                case 7:
                    List<Cliente> listaRutActividadTipo = new List<Cliente>();
                    foreach (Cliente dato in objCliente.ReadAll()) {
                        if (dato.RutCliente.Equals(txtrut.Text) && dato.IdActividadEmpresa ==
                            cboactividad.SelectedIndex && dato.IdTipoEmpresa == 
                            cbotipo.SelectedIndex * 10) {
                            listaRutActividadTipo.Add(dato);
                        }
                    }

                    mostrarClientes(listaRutActividadTipo);
                    break;
                default:
                    List<Cliente> listaCliente = new List<Cliente>();

                    mostrarClientes(listaCliente);
                    break;
            }
        }

        private void DgClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtrut.Text = "";
            cboactividad.SelectedIndex = -1;
            cbotipo.SelectedIndex = -1;
        }

        private void BtnLimpiar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnLimpiar.Background = Brushes.LightBlue;

        }

        private void BtnLimpiar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnLimpiar.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        public void setGrid(String rut) {
            Cliente objCliente = new Cliente();
            mostrarClientes(objCliente.ReadAllByRut(rut));
        }
    }
}
