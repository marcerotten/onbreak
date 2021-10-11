using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClienteDatos;

namespace BibliotecaCliente
{
    public class TipoEvento
    {
        public int Id { get; set; }
        public String Descripcion { get; set; }

        public TipoEvento()
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
                ClienteDatos.TipoEvento objTipoEvento = bbdd.TipoEvento.First(e => e.IdTipoEvento == id);

                //Sincroniza el objeto de origen y el objeto a copiar, guardando los datos de la BD en la instancia del objeto TipoEvento
                //CommonBC.Syncronize(this, objTipoEvento);
                this.Id = objTipoEvento.IdTipoEvento;
                this.Descripcion = objTipoEvento.Descripcion;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<TipoEvento> ReadAll()
        {
            //Se inicia la base de datos a traves de la clase OnbreakEntities
            OnBreakEntities bbdd = new OnBreakEntities();

            try
            {
                //Se obtienen los datos de la BD en una lista
                List<ClienteDatos.TipoEvento> listaDatos = bbdd.TipoEvento.ToList();

                //Se llama al metodo generarListado para convertir ClienteDatos.TipoEvento a TipoEvento
                List<TipoEvento> listadoTipoEvento = generarListado(listaDatos);

                return listadoTipoEvento;
            }
            catch (Exception ex)
            {
                return new List<TipoEvento>();
            }
        }

        private List<TipoEvento> generarListado(List<ClienteDatos.TipoEvento> listaDatos)
        {
            List<TipoEvento> listadoTipoEvento = new List<TipoEvento>();

            foreach (ClienteDatos.TipoEvento dato in listaDatos)
            {
                TipoEvento tipoEvento = new TipoEvento();

                //Se sincroniza el dato de la lista con un objeto tipo TipoEvento
                //CommonBC.Syncronize(tipoEvento, dato);
                tipoEvento.Id = dato.IdTipoEvento;
                tipoEvento.Descripcion = dato.Descripcion;

                listadoTipoEvento.Add(tipoEvento);
            }

            return listadoTipoEvento;
        }
    }
}
