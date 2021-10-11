using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClienteDatos;

namespace BibliotecaCliente
{
    public class ActividadEmpresa
    {
        public int Id { get; set; }
        public String Descripcion { get; set; }

        public ActividadEmpresa()
        {
            Init();
        }

        private void Init() {
            Id = 0;
            Descripcion = String.Empty;
        }

        public bool Read(int id) {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();
            
            try
            {
                //Se busca el primer resultado que coincida con el id
                ClienteDatos.ActividadEmpresa objActividadEmpresa = bbdd.ActividadEmpresa.First(e => e.IdActividadEmpresa == id);

                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto ActividadEmpresa
                //CommonBC.Syncronize(this, objActividadEmpresa);
                this.Id = objActividadEmpresa.IdActividadEmpresa;
                this.Descripcion = objActividadEmpresa.Descripcion;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ActividadEmpresa> ReadAll()
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                //List<ClienteDatos.ActividadEmpresa> listaDatos2 = GetHoliday();
                List<ClienteDatos.ActividadEmpresa> listaDatos = bbdd.ActividadEmpresa.ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.ActividadEmpresa a ActividadEmpresa
                List<ActividadEmpresa> listadoActividadEmpresa = generarListado(listaDatos);

                return listadoActividadEmpresa;
            }
            catch (Exception ex)
            {
                return new List<ActividadEmpresa>();
            }
        }

        private List<ActividadEmpresa> generarListado(List<ClienteDatos.ActividadEmpresa> listaDatos)
        {
            List<ActividadEmpresa> listadoActividadEmpresa = new List<ActividadEmpresa>();

            foreach (ClienteDatos.ActividadEmpresa dato in listaDatos)
            {
                ActividadEmpresa actividadEmpresa = new ActividadEmpresa();

                //Se sincroniza el dato de la lista con un objeto tipo ActividadEmpresa
                CommonBC.Syncronize(dato,actividadEmpresa);

                listadoActividadEmpresa.Add(actividadEmpresa);
            }

            return listadoActividadEmpresa;
        }
    }
}
