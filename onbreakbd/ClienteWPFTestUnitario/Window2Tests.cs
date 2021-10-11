using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClienteWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaCliente;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;

//using System.Windows.Controls;
namespace ClienteWPF.Tests

{

    [TestClass()]
    public class Window2Tests

    {
        private object cboactividad;
        private object txtrut;
        private object txtrazons;
        private object txtnombrec;
        private object txtmail;
        private object txtfono;
        private object cbotipo;
        private object txtdir;





        [TestMethod()]
        public void Window2Test()
        {
            ClienteDatos.Cliente testcliente = new ClienteDatos.Cliente();
            ClienteWPF.Window1 guardar = new ClienteWPF.Window1();
            testcliente.Telefono = "123123123";
            testcliente.RutCliente = "16361834-6";
            testcliente.RazonSocial = "empresatest";
            testcliente.Direccion = "avenida simpre viva ";


            return; 
        }

        [TestMethod()]
        public void cargarCombosTest()

        {
            ClienteDatos.Cliente testcliente = new ClienteDatos.Cliente();

            TipoEmpresa objTipoEmpresa = new TipoEmpresa();


            foreach (TipoEmpresa dato in objTipoEmpresa.ReadAll())
            {



                //   cbotipo.ToString.Add(dato.Descripcion.ToString());
            }

            ActividadEmpresa objActividadEmpresa = new ActividadEmpresa();
            foreach (ActividadEmpresa dato in objActividadEmpresa.ReadAll())
            {
                //   cboactividad.Items.Add(dato.Descripcion.ToString());
            }


            return;
        }



        [TestMethod()]
        public void Btneliminar_ClickTest()
        {
            ClienteDatos.Cliente testcliente = new ClienteDatos.Cliente();
            ClienteWPF.Window1 guardar = new ClienteWPF.Window1();

            try
            {
                Cliente cli = new Cliente();

                /*MessageBoxResult eliminar = MessageBox.Show("Realmente desea Eliminar?",
                    "Estas Seguro?",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);*/

                if (cli.Delete(txtrut.ToString()))
                {
                    MessageBox.Show("Datos eliminados");
                }
                else
                {
                    MessageBox.Show("Datos no eliminados");
                }

                limpiarTest();
                mostrarClientesTest(cli.ReadAll());
            }
            catch (Exception ex)
            {

                MessageBox.Show("no hay clientes con el rut");
            }




            return; 
        }



        [TestMethod()]
        public void limpiarTest()
        {
            int cargarCombos = 2;

            // cboactividad
            //ClienteWPF.Window2.cboactividad 
            ClienteDatos.Cliente testcliente = new ClienteDatos.Cliente();
            ClienteWPF.Window2 limpiar = new ClienteWPF.Window2();
            testcliente.RutCliente = String.Empty;
            testcliente.RazonSocial = String.Empty;
            testcliente.MailContacto = String.Empty;
            testcliente.Direccion = String.Empty;
            testcliente.Telefono = String.Empty;
            // testcliente.ActividadEmpresa.Items.Clear();
            int SelectedIndex = 0;
            //testcliente.IdActividadEmpresa = (cboactividad.SelectedIndex + 1);
            //cboactividad.Item.Clear();
            cargarCombosTest();


            return;
        }

        [TestMethod()]
        public void mostrarClientesTest(List<Cliente> listaClientes)
        {
            ClienteDatos.Cliente testcliente = new ClienteDatos.Cliente();
            ClienteWPF.Window1 guardar = new ClienteWPF.Window1();

            Cliente objCliente = new Cliente();
            TipoEmpresa tipoEmpresa = new TipoEmpresa();
            ActividadEmpresa actividadEmpresa = new ActividadEmpresa();

            int dgClientes = 0;
            int ItemsSource = 0;
            int Items = 0;
            //dgClientes.ItemsSource = null;
            //dgClientes.Items.Clear();
            //dgClientes.Columns.Clear();

            DataTable dt = new DataTable();



            foreach (Cliente dato in listaClientes)
            {
                DataRow row = dt.NewRow();

                actividadEmpresa.Read(dato.IdActividadEmpresa);
                tipoEmpresa.Read(dato.IdTipoEmpresa);

                /*             row["Rut Cliente"] = dato.RutCliente;
                             row["Razón social"] = dato.RazonSocial;
                             row["Nombre contacto"] = dato.NombreContacto;
                             row["Mail contacto"] = dato.MailContacto;
                             row["Dirección"] = dato.Direccion;
                             row["Telefono"] = dato.Telefono;
                             row["Actividad Empresa"] = actividadEmpresa.Descripcion;
                             row["Tipo Empresa"] = tipoEmpresa.Descripcion;

                             */
                dt.Rows.Add(row);
            }

            // dgClientes.ClearValue(ItemsControl.ItemsSourceProperty);
            //  dgClientes.ItemsSource = dt.DefaultView;
            // dgClientes.UpdateLayout();
            /*Cliente objCliente = new Cliente();
            dgClientes.ItemsSource = objCliente.ReadAll();
            dgClientes.Items.Refresh();*/



         //   Assert.Fail();
            return; 
        }



        [TestMethod()]
        public void Btnbuscar_ClickTest()
        {
            ClienteDatos.Cliente testcliente = new ClienteDatos.Cliente();
            ClienteWPF.Window2 mostar = new ClienteWPF.Window2();
            BibliotecaCliente.Cliente Clientetest = new BibliotecaCliente.Cliente();


            if (txtrut != String.Empty)
            {
               
                mostrarClientesTest(Clientetest.ReadAllByRut(txtrut.ToString()));
                foreach (Cliente dato in Clientetest.ReadAllByRut(txtrut.ToString()))
                {
                    txtrut = dato.RutCliente;
                    txtrazons = dato.RazonSocial;
                    txtnombrec = dato.NombreContacto;
                    txtmail = dato.MailContacto;
                    txtdir = dato.Direccion;
                    txtfono = dato.Telefono;
                    //  cboactividad.SelectedIndex = dato.IdActividadEmpresa - 1;
                    //  cbotipo.SelectedIndex = (dato.IdTipoEmpresa / 10) - 1;
                }
            }

            return;
        }

        [TestMethod()]
        public void BtonModificar_ClickTest()
        {
            ClienteDatos.Cliente testcliente = new ClienteDatos.Cliente();
            ClienteWPF.Window1 guardar = new ClienteWPF.Window1();

            ClienteDatos.OnBreakEntities bbdd = new ClienteDatos.OnBreakEntities();
            BibliotecaCliente.Cliente Clientetest = new BibliotecaCliente.Cliente();
            Cliente objCliente = new Cliente();

            objCliente.RutCliente = txtrut.ToString();
            objCliente.RazonSocial = txtrazons.ToString();
            objCliente.NombreContacto = txtnombrec.ToString();
            objCliente.MailContacto = txtmail.ToString();
            objCliente.Direccion = txtdir.ToString();
            objCliente.Telefono = txtfono.ToString();
            //Se suma +1 porque el index de un combobox parte de 0 al largo-1, y se multiplica por 10 ya que los id en la BD van de 10 en 10
            //objCliente.IdActividadEmpresa = (cboactividad.SelectedIndex + 1);
            //   objCliente.IdTipoEmpresa = (cbotipo.SelectedIndex + 1) * 10;

            return;
        }

        [TestMethod()]
        public void BtnListar_ClickTest()
        {

            Cliente objCliente = new Cliente();
            objCliente.ReadAll();
            return; 
        }

        [TestMethod()]
        public void Btnguardar_Test()
        {
            txtrut = "16361834-6";
            txtrazons = "Tests";
            txtnombrec = "adrian";
            txtmail = "test@test.cl";
            txtdir = "calle en el suelo";
            txtfono = "5555555";
            cboactividad = "cualquier cosa";
            cbotipo = "venta";

            if (txtrut != String.Empty && txtrazons != String.Empty && txtnombrec != String.Empty && txtmail != String.Empty && txtdir != String.Empty && txtfono != String.Empty && cboactividad != String.Empty && cbotipo != String.Empty)
            {
                Cliente objCliente = new Cliente();

                objCliente.RutCliente = txtrut.ToString();
                objCliente.RazonSocial = txtrazons.ToString();
                objCliente.NombreContacto = txtnombrec.ToString();
                objCliente.MailContacto = txtmail.ToString();
                objCliente.Direccion = txtdir.ToString();
                objCliente.Telefono = txtfono.ToString();
                //Se suma +1 porque el index de un combobox parte de 0 al largo-1, y se multiplica por 10 ya que los id en la BD van de 10 en 10
                // objCliente.IdActividadEmpresa = (cboactividad.SelectedIndex + 1);
                //  objCliente.IdTipoEmpresa = (cbotipo.SelectedIndex + 1) * 10;

                if (objCliente.Create())
                {
                    MessageBox.Show("Datos guardados");
                }
                else
                {
                    MessageBox.Show("Datos no guardados");
                }

            }


        }

    }
}