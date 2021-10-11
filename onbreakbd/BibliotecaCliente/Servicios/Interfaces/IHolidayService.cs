using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaCliente.Servicios.Interfaces
{
    public interface IHolidayService
    {
        Task<List<TipoEmpresa>> GetHoliday(int id);
    }
}
