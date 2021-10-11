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
    public class Contrato
    {
        public String Numero { get; set; }
        public DateTime Creacion { get; set; }
        public DateTime Termino { get; set; }
        public String RutCliente { get; set; }
        public String IdModalidad { get; set; }
        public int IdTipoEvento { get; set; }
        public DateTime FechaHoraInicio { get; set; }
        public DateTime FechaHoraTermino { get; set; }
        public int Asistentes { get; set; }
        public int PersonalAdicional { get; set; }
        public bool Realizado { get; set; }
        public double ValorTotalContrato { get; set; }
        public String Observaciones { get; set; }

        public Contrato()
        {
            Init();
        }

        private void Init()
        {
            Numero = String.Empty;
            Creacion = DateTime.MinValue;
            Termino = DateTime.MinValue;
            RutCliente = String.Empty;
            IdModalidad = String.Empty;
            IdTipoEvento = 0;
            FechaHoraInicio = DateTime.MinValue;
            FechaHoraTermino = DateTime.MinValue;
            Asistentes = 0;
            PersonalAdicional = 0;
            Realizado = false;
            ValorTotalContrato = 0;
            Observaciones = String.Empty;
        }

        public bool Create()
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            //Se llama a la clase contrato de la BD
            ClienteDatos.Contrato objContrato = new ClienteDatos.Contrato();

            try
            {
                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto contrato
                //CommonBC.Syncronize(this, objContrato);
                objContrato.Numero = this.Numero;
                objContrato.Creacion = this.Creacion;
                objContrato.Termino = this.Termino;
                objContrato.RutCliente = this.RutCliente;
                objContrato.IdModalidad = this.IdModalidad;
                objContrato.IdTipoEvento = this.IdTipoEvento;
                objContrato.FechaHoraInicio = this.FechaHoraInicio;
                objContrato.FechaHoraTermino = this.FechaHoraTermino;
                objContrato.Asistentes = this.Asistentes;
                objContrato.PersonalAdicional = this.PersonalAdicional;
                objContrato.Realizado = this.Realizado;
                objContrato.ValorTotalContrato = this.ValorTotalContrato;
                objContrato.Observaciones = this.Observaciones;

                //Se guardan los datos en la BD
                bbdd.Contrato.Add(objContrato);
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                //Se borran los datos en caso de error
                bbdd.Contrato.Remove(objContrato);
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public bool Read(String nroContrato)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se busca el primer resultado que coincida con el numero
                ClienteDatos.Contrato objContrato = bbdd.Contrato.First(e => e.Numero == nroContrato);

                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto contrato
                CommonBC.Syncronize(this, objContrato);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(String nroContrato)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se busca el primer resultado que coincida con el numero
                ClienteDatos.Contrato objContrato = bbdd.Contrato.First(e => e.Numero == nroContrato);

                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto contrato
                //CommonBC.Syncronize(this, objContrato);

                objContrato.Numero = this.Numero;
                objContrato.Creacion = this.Creacion;
                objContrato.Termino = this.Termino;
                objContrato.RutCliente = this.RutCliente;
                objContrato.IdModalidad = this.IdModalidad;
                objContrato.IdTipoEvento = this.IdTipoEvento;
                objContrato.FechaHoraInicio = this.FechaHoraInicio;
                objContrato.FechaHoraTermino = this.FechaHoraTermino;
                objContrato.Asistentes = this.Asistentes;
                objContrato.PersonalAdicional = this.PersonalAdicional;
                objContrato.Realizado = this.Realizado;
                objContrato.ValorTotalContrato = this.ValorTotalContrato;
                objContrato.Observaciones = this.Observaciones;

                //Se guardan los cambios realizados
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(String nroContrato)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se busca el primer resultado que coincida con el numero
                ClienteDatos.Contrato objContrato = bbdd.Contrato.First(e => e.Numero == nroContrato);

                //Se borran los datos en la BD

                bbdd.Contrato.Remove(objContrato);
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Contrato> ReadAll()
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.Contrato> listaDatos = bbdd.Contrato.ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.Contrato a Contrato
                List<Contrato> listadoContratos = generarListado(listaDatos);

                return listadoContratos;
            }
            catch (Exception ex)
            {
                return new List<Contrato>();
            }
        }

        private List<Contrato> generarListado(List<ClienteDatos.Contrato> listaDatos)
        {
            List<Contrato> listadoClientes = new List<Contrato>();

            foreach (ClienteDatos.Contrato dato in listaDatos)
            {
                Contrato contrato = new Contrato();

                //Se sincroniza el dato de la lista con un objeto tipo Contrato
                //CommonBC.Syncronize(dato, contrato);
                contrato.Numero = dato.Numero;
                contrato.Creacion = dato.Creacion;
                contrato.Termino = dato.Termino;
                contrato.RutCliente = dato.RutCliente;
                contrato.IdModalidad = dato.IdModalidad;
                contrato.IdTipoEvento = dato.IdTipoEvento;
                contrato.FechaHoraInicio = dato.FechaHoraInicio;
                contrato.FechaHoraTermino = dato.FechaHoraTermino;
                contrato.Asistentes = dato.Asistentes;
                contrato.PersonalAdicional = dato.PersonalAdicional;
                contrato.Realizado = dato.Realizado;
                contrato.ValorTotalContrato = dato.ValorTotalContrato;
                contrato.Observaciones = dato.Observaciones;

                listadoClientes.Add(contrato);
            }

            return listadoClientes;
        }

        public List<Contrato> ReadAllByNumeroContrato(String nroContrato)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.Contrato> listaDatos = bbdd.Contrato.SqlQuery("select * from dbo.contrato where numero=@num", new SqlParameter("@num", nroContrato)).ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.Contrato a Contrato
                List<Contrato> listadoContratos = generarListado(listaDatos);

                return listadoContratos;
            }
            catch (Exception ex)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadAllByRutCliente(String rut)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.Contrato> listaDatos = bbdd.Contrato.SqlQuery("select * from dbo.contrato where rutcliente=@rut", new SqlParameter("@rut", rut)).ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.Contrato a Contrato
                List<Contrato> listadoContratos = generarListado(listaDatos);

                return listadoContratos;
            }
            catch (Exception ex)
            {
                return new List<Contrato>();
            }
        }

        public List<Contrato> ReadAllByTipo(int id)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.Contrato> listaDatos = bbdd.Contrato.SqlQuery("select * from dbo.contrato where idtipoevento=@id", new SqlParameter("@id", id)).ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.Contrato a Contrato
                List<Contrato> listadoContratos = generarListado(listaDatos);

                return listadoContratos;
            }
            catch (Exception ex)
            {
                return new List<Contrato>();
            }
        }

        public bool quitarVigencia(String nroContrato) {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se busca el primer resultado que coincida con el numero
                ClienteDatos.Contrato objContrato = bbdd.Contrato.First(e => e.Numero == nroContrato);

                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto contrato
                //CommonBC.Syncronize(this, objContrato);
                objContrato.Realizado = false;

                //Se guardan los cambios realizados
                bbdd.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public String toString() {
            String salida = "Numero:"+this.Numero+", Creacion:"+this.Creacion.ToString()+", Termino:"+this.Termino+", RutCliente:"+this.RutCliente+
                ", IdModalidad:"+this.IdModalidad+", IdTipoEvento:"+this.IdTipoEvento+", FechaHoraInicio:"+this.FechaHoraInicio+", FechaHoraTermino:"+
                this.FechaHoraTermino+", Asistentes:"+this.Asistentes+", PersonalAdicional:"+this.PersonalAdicional+", Realizado:"+this.Realizado.ToString()+
                ", ValorTotalContrato:"+this.ValorTotalContrato+", Observaciones:"+this.Observaciones;
            return salida;
        }
    }
}
