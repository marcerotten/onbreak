using ClienteDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaCliente
{
    public class ModalidadServicio
    {
        public String Id { get; set; }
        public int TipoEvento { get; set; }
        public String Nombre { get; set; }
        public double ValorBase { get; set; }
        public int PersonalBase { get; set; }

        public ModalidadServicio()
        {
            Init();
        }

        private void Init() {
            Id = String.Empty;
            TipoEvento = 0;
            Nombre = String.Empty;
            ValorBase = 0;
            PersonalBase = 0;
        }

        public List<ModalidadServicio> Read(int id) {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se busca el primer resultado que coincida con la modalidad
                List<ClienteDatos.ModalidadServicio> listaDatos = bbdd.ModalidadServicio.SqlQuery("select * from dbo.ModalidadServicio where IdTipoEvento=@id",
                    new SqlParameter("@id",id)).ToList();

                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto ModalidadServicio
                //CommonBC.Syncronize(this, objModalidadServicio);
                List<ModalidadServicio> listaModalidad = generarListado(listaDatos);

                return listaModalidad;

            }
            catch (Exception ex)
            {
                return new List<ModalidadServicio>();
            }
        }

        public bool ReadById(String id) {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            { 
                ClienteDatos.ModalidadServicio modalidad = bbdd.ModalidadServicio.First(e => e.IdModalidad == id);

                this.Id = modalidad.IdModalidad;
                this.TipoEvento = modalidad.IdTipoEvento;
                this.Nombre = modalidad.Nombre;
                this.ValorBase = modalidad.ValorBase;
                this.PersonalBase = modalidad.PersonalBase;

                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        public List<ModalidadServicio> ReadAll() {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.ModalidadServicio> listaDatos = bbdd.ModalidadServicio.ToList();

                List <ModalidadServicio> listadoModalidadServicio = generarListado(listaDatos);

                return listadoModalidadServicio;
            }
            catch (Exception ex) {
                return new List<ModalidadServicio>();
            }

        }

        private List<ModalidadServicio> generarListado(List<ClienteDatos.ModalidadServicio> listaDatos)
        {
            List<ModalidadServicio> listadoModalidadServicio = new List<ModalidadServicio>();

            foreach (ClienteDatos.ModalidadServicio dato in listaDatos)
            {
                ModalidadServicio modalidad = new ModalidadServicio();

                //Se sincroniza el dato de la lista con un objeto tipo ModalidadServicio
                //CommonBC.Syncronize(dato, modalidad);
                modalidad.Id = dato.IdModalidad;
                modalidad.TipoEvento = dato.IdTipoEvento;
                modalidad.Nombre = dato.Nombre;
                modalidad.ValorBase = dato.ValorBase;
                modalidad.PersonalBase = dato.PersonalBase;

                listadoModalidadServicio.Add(modalidad);
            }

            return listadoModalidadServicio;
        }
    }
}
