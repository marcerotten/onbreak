using BibliotecaCliente;
using BibliotecaCliente.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClienteWPF.Servicios
{
    public class HolidayService : IHolidayService
    {
        public HolidayService()
        {}
        public async Task<List<TipoEmpresa>> GetHoliday(int id)
        {
            List<TipoEmpresa> response2 = new List<TipoEmpresa>();
            using (HttpClient client = new HttpClient())
            {
                //http://localhost:8080/api/{usuario}
                var response = await client.GetAsync($"https://www.feriadosapp.com/api/holidays.json");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    //message.Content = await response.Content.ReadAsStringAsync();
                    
                }
                else
                {
                    //message.Content = $"Server error code {response.StatusCode}";
                }
            }
            return response2;
        }
    }
}
