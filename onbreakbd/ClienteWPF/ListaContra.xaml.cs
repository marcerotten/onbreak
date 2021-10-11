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
    /// Lógica de interacción para ListaContra.xaml
    /// </summary>
    public partial class ListaContra : MetroWindow
    {
        Window1 ventMantenedor;
        public ListaContra()
        {
            Contrato objContrato = new Contrato();
            InitializeComponent();
            cargarCombos();
            mostrarContrato(objContrato.ReadAll());
            ventMantenedor = null;
        }

        public ListaContra(Window1 window) {
            Contrato objContrato = new Contrato();
            InitializeComponent();
            cargarCombos();
            mostrarContrato(objContrato.ReadAll());
            ventMantenedor = window;
        }

        private void cargarCombos() {
            cbotipoevento.Items.Add("-Ninguno-");

            TipoEvento objTipoEvento = new TipoEvento();
            foreach (TipoEvento dato in objTipoEvento.ReadAll())
            {
                cbotipoevento.Items.Add(dato.Descripcion.ToString());
            }
        }

        private void Txtrut_TextChanged(object sender, TextChangedEventArgs e)
        {
            buscar();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            buscar();
        }

        private void Cbotipoevento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            buscar();
        }

        private void mostrarContrato(List<Contrato> listaContrato)
        {
            ModalidadServicio objModalidadServicio = new ModalidadServicio();
            TipoEvento objTipoEvento = new TipoEvento();

            gr2.ItemsSource = null;
            gr2.Items.Clear();
            gr2.Columns.Clear();

            DataTable dt = new DataTable();

            dt.Columns.Add("Numero");
            dt.Columns.Add("Creacion");
            dt.Columns.Add("Termino");
            dt.Columns.Add("Rut cliente");
            dt.Columns.Add("Modalidad");
            dt.Columns.Add("Tipo evento");
            dt.Columns.Add("Fecha hora inicio");
            dt.Columns.Add("Fecha hora termino");
            dt.Columns.Add("Asistentes");
            dt.Columns.Add("Personal adicional");
            dt.Columns.Add("Vigencia");
            dt.Columns.Add("Valor total contrato (UF)");
            dt.Columns.Add("Observaciones");

            foreach (Contrato dato in listaContrato)
            {
                DataRow row = dt.NewRow();

                objModalidadServicio.ReadById(dato.IdModalidad);
                objTipoEvento.Read(dato.IdTipoEvento);

                row["Numero"] = dato.Numero;
                row["Creacion"] = dato.Creacion;
                row["Termino"] = dato.Termino;
                row["Rut cliente"] = dato.RutCliente;
                row["Modalidad"] = objModalidadServicio.Nombre.Trim();
                row["Tipo evento"] = objTipoEvento.Descripcion;
                row["Fecha hora inicio"] = dato.FechaHoraInicio;
                row["Fecha hora termino"] = dato.FechaHoraTermino;
                row["Asistentes"] = dato.Asistentes;
                row["Personal adicional"] = dato.PersonalAdicional;

                if (dato.Realizado)
                {
                    row["Vigencia"] = "Vigente";
                }
                if (dato.Realizado==false) {
                    row["Vigencia"] = "No vigente";
                }
                
                row["Valor total contrato (UF)"] = dato.ValorTotalContrato;
                row["Observaciones"] = dato.Observaciones;

                dt.Rows.Add(row);
            }

            gr2.ClearValue(ItemsControl.ItemsSourceProperty);
            gr2.ItemsSource = dt.DefaultView;
            gr2.UpdateLayout();
        }

        private void Gr2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)gr2.SelectedItem;

            Contrato objContrato = new Contrato();

            if (row[0].ToString().Equals("")) {

            }
            else {
                objContrato.Numero = row[0].ToString();
                objContrato.Creacion = Convert.ToDateTime(row[1].ToString());
                objContrato.Termino = Convert.ToDateTime(row[2].ToString());
                objContrato.RutCliente = row[3].ToString();
                objContrato.IdModalidad = obtenerIdModalidad(row[4].ToString());
                objContrato.IdTipoEvento = obtenerIdEvento(row[5].ToString());
                objContrato.FechaHoraInicio = Convert.ToDateTime(row[6].ToString());
                objContrato.FechaHoraTermino = Convert.ToDateTime(row[7].ToString());
                objContrato.Asistentes = int.Parse(row[8].ToString());
                objContrato.PersonalAdicional = int.Parse(row[9].ToString());

                bool vigencia = false;
                if (row[10].ToString().Equals("Vigente"))
                {
                    vigencia = true;
                }

                objContrato.Realizado = vigencia;
                objContrato.ValorTotalContrato = Convert.ToDouble(row[11].ToString());
                objContrato.Observaciones = row[12].ToString();

                if (ventMantenedor != null)
                {
                    List<Contrato> listaContrato = new List<Contrato>();
                    listaContrato.Add(objContrato);

                    ventMantenedor.setGrid(listaContrato);
                    this.Close();
                }
                else
                {
                    Window1 ventana = new Window1(objContrato);
                    ventana.Show();
                    this.Close();
                }
            }
        }

        private String obtenerIdModalidad(String modalidad) {
            String idModalidad = "";

            if (modalidad.Equals("Light Break")) {
                idModalidad = "CB001";
            }
            if (modalidad.Equals("Journal Break"))
            {
                idModalidad = "CB002";
            }
            if (modalidad.Equals("Day Break"))
            {
                idModalidad = "CB003";
            }
            if (modalidad.Equals("Ejecutiva"))
            {
                idModalidad = "CE001";
            }
            if (modalidad.Equals("Celebración"))
            {
                idModalidad = "CE002";
            }
            if (modalidad.Equals("Quick Cocktail"))
            {
                idModalidad = "CO001";
            }
            if (modalidad.Equals("Ambient Cocktail"))
            {
                idModalidad = "CO002";
            }

            return idModalidad;
        }

        private int obtenerIdEvento(String evento) {
            int idTipoEvento = 0;

            if (evento.Equals("Coffee Break")) {
                idTipoEvento = 10;
            }
            if (evento.Equals("Cocktail"))
            {
                idTipoEvento = 20;
            }
            if (evento.Equals("Cenas"))
            {
                idTipoEvento = 30;
            }

            return idTipoEvento;
        }

        private void buscar() {
            Contrato objContrato = new Contrato();

            int opcion = 0;
            if (txtnro.Text.Equals("") == false)
            {
                opcion = opcion + 1;
            }
            if (txtrut.Text.Equals("") == false)
            {
                opcion = opcion + 2;
            }
            if (cbotipoevento.SelectedIndex > 0)
            {
                opcion = opcion + 4;
            }

            switch (opcion) {
                case 1:
                    mostrarContrato(objContrato.ReadAllByNumeroContrato(txtnro.Text));
                    break;
                case 2:
                    mostrarContrato(objContrato.ReadAllByRutCliente(txtrut.Text));
                    break;
                case 3:
                    List<Contrato> listaNroRut = new List<Contrato>();
                    foreach (Contrato dato in objContrato.ReadAll()) {
                        if (txtnro.Text.Equals(dato.Numero) && txtrut.Text.Equals(dato.RutCliente)) {
                            listaNroRut.Add(dato);
                        }
                    }

                    mostrarContrato(listaNroRut);
                    break;
                case 4:
                    mostrarContrato(objContrato.ReadAllByTipo(cbotipoevento.SelectedIndex * 10));
                    break;
                case 5:
                    List<Contrato> listaTipoNro = new List<Contrato>();
                    foreach (Contrato dato in objContrato.ReadAll()) {
                        if (dato.IdTipoEvento == cbotipoevento.SelectedIndex * 10 &&
                            dato.Numero.Equals(txtnro.Text)) {
                            listaTipoNro.Add(dato);
                        }
                    }

                    mostrarContrato(listaTipoNro);
                    break;
                case 6:
                    List<Contrato> listaRutTipo = new List<Contrato>();
                    foreach (Contrato dato in objContrato.ReadAll()) {
                        if (dato.RutCliente.Equals(txtrut.Text) && dato.IdTipoEvento * 10 ==
                            cbotipoevento.SelectedIndex) {
                            listaRutTipo.Add(dato);
                        }
                    }

                    mostrarContrato(listaRutTipo);
                    break;
                case 7:
                    List<Contrato> listaNroRutTipo = new List<Contrato>();
                    foreach (Contrato dato in objContrato.ReadAll()) {
                        if (dato.IdTipoEvento == cbotipoevento.SelectedIndex * 10 &&
                            dato.Numero.Equals(txtnro.Text) && dato.RutCliente.Equals(txtrut.Text)) {
                            listaNroRutTipo.Add(dato);
                        }
                    }

                    mostrarContrato(listaNroRutTipo);
                    break;
                default:
                    List<Contrato> listaContrato = new List<Contrato>();

                    mostrarContrato(listaContrato);
                    break;

            }
            opcion = 0;
        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtnro.Text = "";
            txtrut.Text = "";
            cbotipoevento.SelectedIndex = -1;
        }

        private void BtnLimpiar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnLimpiar.Background = Brushes.LightBlue;

        }

        private void BtnLimpiar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnLimpiar.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }
    }
}
