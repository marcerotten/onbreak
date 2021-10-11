using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaCliente.Servicios.Interfaces;
using ClienteDatos;

namespace BibliotecaCliente
{
    public class TipoEmpresa
    {
        public int Id { get; set; }
        public String Descripcion { get; set; }

        public TipoEmpresa()
        {
            Init();
        }

        private void Init()
        {
            Id = 0;
            Descripcion = String.Empty;
        }

        public bool Read(int id)
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se busca el primer resultado que coincida con el id
                ClienteDatos.TipoEmpresa objTipoEmpresa = bbdd.TipoEmpresa.First(e => e.IdTipoEmpresa == id);

                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto TipoEmpresa
                //CommonBC.Syncronize(objTipoEmpresa,this);
                this.Id = objTipoEmpresa.IdTipoEmpresa;
                this.Descripcion = objTipoEmpresa.Descripcion;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TipoEmpresa> ReadAll()
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();
            
            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.TipoEmpresa> listaDatos = bbdd.TipoEmpresa.ToList();
                //Test service
                //List<ClienteDatos.TipoEmpresa> listaDatos2 = _holidayService.GetHoliday(1);
                //Se llama al metodo generarListado para convertir ClienteDatos.TipoEmpresa a TipoEmpresa
                List<TipoEmpresa> listadoTipoEmpresa = generarListado(listaDatos);

                return listadoTipoEmpresa;
            }
            catch (Exception ex)
            {
                return new List<TipoEmpresa>();
            }
        }

        private List<TipoEmpresa> generarListado(List<ClienteDatos.TipoEmpresa> listaDatos)
        {
            List<TipoEmpresa> listadoActividadEmpresa = new List<TipoEmpresa>();

            foreach (ClienteDatos.TipoEmpresa dato in listaDatos)
            {
                TipoEmpresa tipoEmpresa = new TipoEmpresa();

                //Se sincroniza el dato de la lista con un objeto tipo TipoEmpresa
                CommonBC.Syncronize(dato, tipoEmpresa);

                listadoActividadEmpresa.Add(tipoEmpresa);
            }

            return listadoActividadEmpresa;
        }
    }
}
