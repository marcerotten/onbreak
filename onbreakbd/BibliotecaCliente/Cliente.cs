using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClienteDatos;

namespace BibliotecaCliente
{
    public class Cliente
    {
        public String RutCliente { get; set; }
        public String RazonSocial { get; set; }
        public String NombreContacto { get; set; }
        public String MailContacto { get; set; }
        public String Direccion { get; set; }
        public String Telefono { get; set; }
        public int IdActividadEmpresa { get; set; }
        public int IdTipoEmpresa { get; set; }

        public Cliente()
        {
            Init();
        }

        private void Init() {
            RutCliente = String.Empty;
            RazonSocial = String.Empty;
            NombreContacto = String.Empty;
            MailContacto = String.Empty;
            Direccion = String.Empty;
            Telefono = String.Empty;
            IdActividadEmpresa = 0;
            IdTipoEmpresa = 0;
        }

        public bool Create()
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            //Se llama a la clase clientes de la BD
            ClienteDatos.Cliente objCliente = new ClienteDatos.Cliente();

            try
            {
                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto cliente
                //CommonBC.Syncronize(this, objCliente);

                objCliente.RutCliente = this.RutCliente;
                objCliente.RazonSocial = this.RazonSocial;
                objCliente.NombreContacto = this.NombreContacto;
                objCliente.MailContacto = this.MailContacto;
                objCliente.Direccion = this.Direccion;
                objCliente.Telefono = this.Telefono;
                objCliente.IdActividadEmpresa = this.IdActividadEmpresa;
                objCliente.IdTipoEmpresa = this.IdTipoEmpresa;

                //Se guardan los datos en la BD
                bbdd.Cliente.Add(objCliente);
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                //Se borran los datos en caso de error
                bbdd.Cliente.Remove(objCliente);
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool Read(String rut)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se busca el primer resultado que coincida con el rut
                ClienteDatos.Cliente objCliente = bbdd.Cliente.First(e => e.RutCliente == rut);

                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto cliente
                //CommonBC.Syncronize(objCliente, this);

                this.RutCliente = objCliente.RutCliente;
                this.RazonSocial = objCliente.RazonSocial;
                this.NombreContacto = objCliente.NombreContacto;
                this.MailContacto = objCliente.MailContacto;
                this.Direccion = objCliente.Direccion;
                this.Telefono = objCliente.Telefono;
                this.IdActividadEmpresa = objCliente.IdActividadEmpresa;
                this.IdTipoEmpresa = objCliente.IdTipoEmpresa;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(String rut)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se busca el primer resultado que coincida con el rut
                ClienteDatos.Cliente objCliente = bbdd.Cliente.First(e => e.RutCliente == rut);

                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto cliente
                //CommonBC.Syncronize(this, objCliente);
                objCliente.RutCliente = this.RutCliente;
                objCliente.RazonSocial = this.RazonSocial;
                objCliente.NombreContacto = this.NombreContacto;
                objCliente.MailContacto = this.MailContacto;
                objCliente.Direccion = this.Direccion;
                objCliente.Telefono = this.Telefono;
                objCliente.IdActividadEmpresa = this.IdActividadEmpresa;
                objCliente.IdTipoEmpresa = this.IdTipoEmpresa;

                //Se guardan los cambios realizados
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(String rut)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {

                //Se busca el primer resultado que coincida con el rut
                ClienteDatos.Cliente objCliente = bbdd.Cliente.First(e => e.RutCliente == rut);

                List<ClienteDatos.Contrato> listaContratos = bbdd.Contrato.ToList();

                foreach (ClienteDatos.Contrato dato in listaContratos) {
                    if (dato.RutCliente.Equals(objCliente.RutCliente)) {
                        return false;
                    }
                }

                bbdd.Cliente.Remove(objCliente);
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Cliente> ReadAll()
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.Cliente> listaDatos = bbdd.Cliente.ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.Cliente a Cliente
                List<Cliente> listadoClientes = generarListado(listaDatos);

                return listadoClientes;
            }
            catch (Exception ex)
            {
                return new List<Cliente>();
            }
        }

        private List<Cliente> generarListado(List<ClienteDatos.Cliente> listaDatos)
        {
            List<Cliente> listadoClientes = new List<Cliente>();

            foreach (ClienteDatos.Cliente dato in listaDatos)
            {
                Cliente cliente = new Cliente();

                //Se sincroniza el dato de la lista con un objeto tipo Cliente
                //CommonBC.Syncronize(dato, cliente);
                cliente.RutCliente = dato.RutCliente;
                cliente.RazonSocial = dato.RazonSocial;
                cliente.NombreContacto = dato.NombreContacto;
                cliente.MailContacto = dato.MailContacto;
                cliente.Direccion = dato.Direccion;
                cliente.Telefono = dato.Telefono;
                cliente.IdActividadEmpresa = dato.IdActividadEmpresa;
                cliente.IdTipoEmpresa = dato.IdTipoEmpresa;

                listadoClientes.Add(cliente);
            }

            return listadoClientes;
        }

        public List<Cliente> ReadAllByRut(String rut)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.Cliente> listaDatos = bbdd.Cliente.SqlQuery("select * from dbo.Cliente where RutCliente=@rut", new SqlParameter("@rut", rut)).ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.Cliente a Cliente
                List<Cliente> listadoClientes = generarListado(listaDatos);

                return listadoClientes;
            }
            catch (Exception ex)
            {
                return new List<Cliente>();
            }
        }

        public List<Cliente> ReadAllByTipoEmpresa(int id)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.Cliente> listaDatos = bbdd.Cliente.SqlQuery("select * from dbo.Cliente where IdTipoEmpresa=@id", new SqlParameter("@id", id)).ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.Cliente a Cliente
                List<Cliente> listadoClientes = generarListado(listaDatos);

                return listadoClientes;
            }
            catch (Exception ex)
            {
                return new List<Cliente>();
            }
        }

        public List<Cliente> ReadAllByActividad(int id)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.Cliente> listaDatos = bbdd.Cliente.SqlQuery("select * from dbo.Cliente where IdActividadEmpresa=@id", new SqlParameter("@id", id)).ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.Cliente a Cliente
                List<Cliente> listadoClientes = generarListado(listaDatos);

                return listadoClientes;
            }
            catch (Exception ex)
            {
                return new List<Cliente>();
            }
        }

        public String toString() {
            String retornar = String.Empty;
            retornar = "RutCliente:"+this.RutCliente+ ", RazonSocial:"+this.RazonSocial+ ", NombreContrato:"+this.NombreContacto +
                ", MailContacto:"+this.MailContacto+ ", Direccion:"+this.Direccion+ ", Telefono:"+this.Telefono+ 
                ", IdActividadEmpresa:"+this.IdActividadEmpresa+ ", IdTipoEmpresa:"+this.IdTipoEmpresa;
            return retornar;
        }
    }
}
