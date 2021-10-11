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

namespace ClienteWPF
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : MetroWindow
    {

        public double total;
        List<Contrato> miContrato = new List<Contrato>();
        public Window1()
        {
            InitializeComponent();
            //txtnro.Text = DateTime.Now.ToString("yyyyMMddHHmm");
            List<Contrato> listaContrato = new List<Contrato>();
            txtfecha.Text = DateTime.Now.ToString("G");
            cargaCombos();
            mostrarContrato(listaContrato);
        }

        public Window1(Contrato contrato) {
            InitializeComponent();
            //txtnro.Text = DateTime.Now.ToString("yyyyMMddHHmm");
            List<Contrato> listaContrato = new List<Contrato>();
            listaContrato.Add(contrato);
            txtfecha.Text = DateTime.Now.ToString("G");
            cargaCombos();
            mostrarContrato(listaContrato);
            llenarDatosBuscados(listaContrato);
        }

        private void cargaCombos()
        {
            cboEstado.Items.Add("Vigente");
            cboEstado.Items.Add("No vigente");

            TipoEvento objTipoEvento = new TipoEvento();
            foreach (TipoEvento dato in objTipoEvento.ReadAll()) {
                cbotipoevento.Items.Add(dato.Descripcion.ToString().Trim());
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtnro.Text != String.Empty)
            {
                Contrato objContrato = new Contrato();

                mostrarContrato(objContrato.ReadAllByNumeroContrato(txtnro.Text));
                llenarDatosBuscados(objContrato.ReadAllByNumeroContrato(txtnro.Text));
            }
        }

        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {

            Contrato con = new Contrato();
            if (txtfecha.Text != String.Empty && txtrut.Text != String.Empty && dtpFecha.Text != String.Empty && dtpInicio.Text != String.Empty && cboEstado.Text != String.Empty && cbotipoContrato.Text != String.Empty && cbotipoevento.Text != String.Empty)
            {
                ModalidadServicio modalidad = new ModalidadServicio();
                bool verificado = false;
                Valorizador val = new Valorizador();

                bool verificarNumero = true;

                foreach (Contrato dato in con.ReadAll()) {
                    if (dato.Numero.Equals(txtnro.Text)) {
                        verificarNumero = false;
                    }
                }

                if (verificarNumero)
                {
                    con.Numero = txtnro.Text = DateTime.Now.ToString("yyyyMMddHHmm");
                    con.Creacion = (DateTime)dtpInicio.SelectedDate;
                    con.Termino = (DateTime)dtpFecha.SelectedDate;
                    con.RutCliente = txtrut.Text.ToString();
                    con.IdModalidad = obtenerModalidad();
                    con.IdTipoEvento = (cbotipoevento.SelectedIndex + 1) * 10;
                    con.FechaHoraInicio = (DateTime)dtpInicio.SelectedDate;
                    con.FechaHoraTermino = (DateTime)dtpFecha.SelectedDate;

                    modalidad.ReadById(obtenerModalidad());

                    con.Asistentes = modalidad.PersonalBase;
                    con.PersonalAdicional = int.Parse(txtPerAdicional.Text.ToString());

                    if (cboEstado.SelectedIndex == 0)
                    {
                        verificado = true;
                    }

                    con.Realizado = verificado;
                    con.ValorTotalContrato = val.CalcularValorEvento(modalidad.ValorBase, int.Parse(txtPerAdicional.Text.ToString()), modalidad.PersonalBase);
                    con.Observaciones = txtObservacion.Text.ToString();

                    if (con.Create())
                    {
                        this.ShowMessageAsync("Datos guardados", ":)");
                        mostrarContrato(con.ReadAllByNumeroContrato(txtnro.Text));
                    }
                    else
                    {
                        this.ShowMessageAsync("Error al guardar datos", ":(");
                    }

                }
                else {
                    this.ShowMessageAsync("Número de contrato ya existe", ":(");
                }
            }

        }

        private void limpiar()
        {
            txtnro.Text = string.Empty;
            txtfecha.Text = String.Empty;
            txtrut.Text = String.Empty;
            dtpFecha.SelectedDate = DateTime.Now;
            dtpInicio.SelectedDate = DateTime.Today;
            txtObservacion.Text = String.Empty;
            txtrut.Text = String.Empty;
            cbotipoevento.Items.Clear();
            cbotipoContrato.Items.Clear();
            cboEstado.Items.Clear();
            cargaCombos();
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

            foreach (Contrato dato in listaContrato) {
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

                if (dato.Realizado) {
                    row["Vigencia"] = "Vigente";
                }
                if (dato.Realizado == false) {
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

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            Contrato objContrato = new Contrato();

            if (objContrato.quitarVigencia(txtnro.Text)) {
                this.ShowMessageAsync("Contrato eliminado", ":)");
                mostrarContrato(objContrato.ReadAllByNumeroContrato(txtnro.Text));
                llenarDatosBuscados(objContrato.ReadAllByNumeroContrato(txtnro.Text));
                txtnro.Text = "";
            }
            else {
                this.ShowMessageAsync("Error al eliminar", ":(");

            }
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtHora_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Contrato objContrato = new Contrato();

            String mensaje = ":(";

            ModalidadServicio modalidad = new ModalidadServicio();
            bool verificado = false;
            Valorizador val = new Valorizador();



            objContrato.Numero = txtnro.Text;
            objContrato.RutCliente = txtrut.Text;
            objContrato.IdModalidad = obtenerModalidad();
            objContrato.IdTipoEvento = (cbotipoevento.SelectedIndex + 1) * 10;
            objContrato.FechaHoraInicio = DateTime.Now;
            objContrato.FechaHoraTermino = DateTime.Now;

            modalidad.ReadById(obtenerModalidad());

            objContrato.Asistentes = modalidad.PersonalBase;

            try {
                objContrato.PersonalAdicional = int.Parse(txtPerAdicional.Text);
                objContrato.Creacion = (DateTime)dtpInicio.SelectedDate;
                objContrato.Termino = (DateTime)dtpFecha.SelectedDate;
                objContrato.ValorTotalContrato = val.CalcularValorEvento(modalidad.ValorBase, int.Parse(txtPerAdicional.Text.ToString()), modalidad.PersonalBase);
            }
            catch (Exception ex) {
                mensaje = "Campos vacios";
            }

            if (cboEstado.SelectedIndex == 0)
            {
                verificado = true;
            }

            objContrato.Realizado = verificado;
            
            objContrato.Observaciones = txtObservacion.Text;

            if (objContrato.Update(objContrato.Numero))
            {
                this.ShowMessageAsync("Datos actualizados", ":)");
                mostrarContrato(objContrato.ReadAllByNumeroContrato(txtnro.Text));
            }
            else {
                this.ShowMessageAsync("Error al actualizar los datos", mensaje);
            }
        }

        private void Cbotipoevento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbotipoContrato.Items.Clear();
            ModalidadServicio modalidad = new ModalidadServicio();
            foreach (ModalidadServicio dato in modalidad.Read((cbotipoevento.SelectedIndex + 1) * 10))
            {
                cbotipoContrato.Items.Add(dato.Nombre);
            }

        }


        private void Cbocantidad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Cbomas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();

        }

        private void CbotipoContrato_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbocantidad.Items.Clear();

            ModalidadServicio modalidad = new ModalidadServicio();

            if (modalidad.ReadById(obtenerModalidad()))
            {
                cbocantidad.Items.Add(modalidad.PersonalBase);
            }
        }

        private String obtenerModalidad() {
            String idMod = String.Empty;
            if ((cbotipoevento.SelectedIndex + 1) * 10 == 10)
            {
                idMod = "CB00" + (cbotipoContrato.SelectedIndex + 1);
            }
            if ((cbotipoevento.SelectedIndex + 1) * 10 == 20)
            {
                idMod = "CO00" + (cbotipoContrato.SelectedIndex + 1);
            }
            if ((cbotipoevento.SelectedIndex + 1) * 10 == 30)
            {
                idMod = "CE00" + (cbotipoContrato.SelectedIndex + 1);
            }
            return idMod;
        }

        private int llenarModalidad(String modalidad) {
            int index = -1;

            if (modalidad.Equals("CB001")) {
                index = 0;
            }
            if (modalidad.Equals("CB002"))
            {
                index = 1;
            }
            if (modalidad.Equals("CB003"))
            {
                index = 2;
            }
            if (modalidad.Equals("CO001"))
            {
                index = 0;
            }
            if (modalidad.Equals("C0002"))
            {
                index = 1;
            }
            if (modalidad.Equals("CE001"))
            {
                index = 0;
            }
            if (modalidad.Equals("CE002"))
            {
                index = 1;
            }

            return index;
        }

        private void TxtPerAdicional_TextChanged(object sender, TextChangedEventArgs e)
        {
            ModalidadServicio objModalidad = new ModalidadServicio();
            objModalidad.ReadById(obtenerModalidad());

            Valorizador objValorizador = new Valorizador();

            int personalAdicional = 0;

            if (txtPerAdicional.Text.Equals("") == false) {
                personalAdicional = int.Parse(txtPerAdicional.Text);
            }

            txtvalor.Text = Convert.ToString(objValorizador.CalcularValorEvento(objModalidad.ValorBase,
                personalAdicional, objModalidad.PersonalBase));
        }

        private void Txtvalor_TextChanged(object sender, TextChangedEventArgs e)
        {
            try {
                int.Parse(txtPerAdicional.Text);
            }
            catch (Exception ex) {
                this.ShowMessageAsync("Por favor introduzca numeros", ":(");
            }
        }

        private void BtnListarCltes_Click(object sender, RoutedEventArgs e)
        {
            ListaCltes window = new ListaCltes();
            window.Show();
            if (txtrut.Text.Equals("") == false)
            {
                window.setGrid(txtrut.Text);
            }

        }

        private void BtnContrato_Click(object sender, RoutedEventArgs e)
        {
            ListaContra window = new ListaContra(this);
            window.Show();

        }

        private void llenarDatosBuscados(List<Contrato> listadoContrato) {
            foreach (Contrato dato in listadoContrato)
            {
                txtnro.Text = dato.Numero;
                dtpInicio.SelectedDate = dato.Creacion;
                dtpFecha.SelectedDate = dato.Termino;

                int verificar = 1;
                if (dato.Realizado == true)
                {
                    verificar = 0;
                }

                cboEstado.SelectedIndex = verificar;
                txtObservacion.Text = dato.Observaciones;
                txtfecha.Text = DateTime.Now.ToString("G");
                txtrut.Text = dato.RutCliente;
                cbotipoContrato.SelectedIndex = llenarModalidad(dato.IdModalidad);
                cbotipoevento.SelectedIndex = (dato.IdTipoEvento / 10) - 1;
                cbocantidad.SelectedIndex = 0;
                txtPerAdicional.Text = dato.PersonalAdicional.ToString();
                txtvalor.Text = dato.ValorTotalContrato.ToString();
            }
        }

        public void setGrid(List<Contrato> listaContrato) {
            mostrarContrato(listaContrato);
            llenarDatosBuscados(listaContrato);
        }

        private void BtnGuardar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnGuardar.Background = Brushes.LightBlue;

        }

        private void BtnGuardar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnGuardar.Background = (Brush)new BrushConverter().ConvertFrom("#FF0047AE");
        }

        private void BtnModificar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnModificar.Background = Brushes.LightBlue;

        }

        private void BtnModificar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnModificar.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");
        }

        private void BtnEliminar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnEliminar.Background = Brushes.LightBlue;

        }

        private void BtnEliminar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnEliminar.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");
        }

        private void BtnLimpiar_MouseEnter(object sender, MouseEventArgs e)
        {
            btnLimpiar.Background = Brushes.LightBlue;

        }

        private void BtnLimpiar_MouseLeave(object sender, MouseEventArgs e)
        {
            btnLimpiar.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void BtnListarContra_MouseEnter(object sender, MouseEventArgs e)
        {
            btnListarContra.Background = Brushes.LightBlue;

        }

        private void BtnListarContra_MouseLeave(object sender, MouseEventArgs e)
        {
            btnListarContra.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");
        }

        private void BtnBuscarContra_MouseEnter(object sender, MouseEventArgs e)
        {
            btnBuscarContra.Background = Brushes.LightBlue;

        }

        private void BtnBuscarContra_MouseLeave(object sender, MouseEventArgs e)
        {
            btnBuscarContra.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void BtnBuscarRut_MouseEnter(object sender, MouseEventArgs e)
        {
            btnBuscarRut.Background = Brushes.LightBlue;

        }

        private void BtnBuscarRut_MouseLeave(object sender, MouseEventArgs e)
        {
            btnBuscarRut.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void BtnVolver_MouseEnter(object sender, MouseEventArgs e)
        {
            btnVolver.Background = Brushes.LightBlue;

        }

        private void BtnVolver_MouseLeave(object sender, MouseEventArgs e)
        {
            btnVolver.Background = (Brush)new BrushConverter().ConvertFrom("#FF549AFF");

        }

        private void BtnLimpiar_Click(object sender, RoutedEventArgs e)
        {
            txtnro.Text = "";
            dtpInicio.SelectedDate = DateTime.Now;
            dtpFecha.SelectedDate = DateTime.Now;
            cboEstado.SelectedIndex = -1;
            txtObservacion.Text = "";
            txtrut.Text = "";
            cbotipoevento.SelectedIndex = -1;
            cbotipoContrato.SelectedIndex = -1;
            cbocantidad.SelectedIndex = -1;
            txtPerAdicional.Text = "";
            txtvalor.Text = "";
        }

        private void Gr2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)gr2.SelectedItem;

            Contrato objContrato = new Contrato();

            if (row[0].ToString().Equals(""))
            {

            }
            else {
                txtnro.Text = row[0].ToString();
                dtpInicio.SelectedDate = Convert.ToDateTime(row[1].ToString());
                dtpFecha.SelectedDate = Convert.ToDateTime(row[2].ToString());
                txtrut.Text = row[3].ToString();
                cbotipoContrato.SelectedIndex = indiceModalidad(row[4].ToString());
                cbotipoevento.SelectedIndex = obtenerIdEvento(row[5].ToString());

                cbocantidad.Items.Clear();
                cbocantidad.Items.Add(int.Parse(row[8].ToString()));
                cbocantidad.SelectedIndex = 0;

                txtPerAdicional.Text = row[9].ToString();
                cboEstado.SelectedItem = (Object)row[10].ToString();

                txtvalor.Text = row[11].ToString();
                txtObservacion.Text = row[12].ToString();
            }

        }

        private int indiceModalidad(String nombre) {
            int indice = -1;
            if (nombre.Equals("Light Break")) {
                indice = 0;
            }
            if (nombre.Equals("Journal Break")) {
                indice = 1;
            }
            if (nombre.Equals("Day Break")) {
                indice = 2;
            }
            if (nombre.Equals("Ejecutiva")) {
                indice = 0;
            }
            if (nombre.Equals("Celebración")) {
                indice = 1;
            }
            if (nombre.Equals("Quick Cocktail")) {
                indice = 0;
            }
            if (nombre.Equals("Ambient Cocktail")) {
                indice = 1;
            }

            return indice;
        }

        private int obtenerIdEvento(String evento)
        {
            int idTipoEvento = -1;

            if (evento.Equals("Coffee Break"))
            {
                idTipoEvento = 0;
            }
            if (evento.Equals("Cocktail"))
            {
                idTipoEvento = 1;
            }
            if (evento.Equals("Cenas"))
            {
                idTipoEvento = 2;
            }

            return idTipoEvento;
        }
    }
}
