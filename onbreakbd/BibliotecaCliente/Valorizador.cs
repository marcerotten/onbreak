using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClienteDatos;

namespace BibliotecaCliente
{
    public class Valorizador
    {
        Cliente objCliente;

        public Valorizador()
        {
        }

        public Valorizador(Cliente objCliente)
        {
            this.objCliente = objCliente;
        }

        public double CalcularValorEvento(double valorBase, int personalAdicional, int asistentes) {
            int recargoAsistentes = 0;
            int recargoPersonal = 0;
            double valorEvento = 0;

            if (asistentes>=1 && asistentes<=20) {
                recargoAsistentes = 3;
            }
            if (asistentes >= 21 && asistentes <= 50) {
                recargoAsistentes = 5;
            }
            if (asistentes<50) {
                double recargo = asistentes / 10;
                recargoAsistentes =(int) Math.Round(recargo,0);
            }

            if (personalAdicional==2) {
                recargoPersonal = 2;
            }
            if (personalAdicional==3) {
                recargoPersonal = 3;
            }
            if (personalAdicional==4) {
                recargoPersonal =(int) Math.Round(3.5,0);
            }
            if (personalAdicional==5) {
                recargoPersonal = (int)Math.Round(3.5+(0.5*personalAdicional),0);
            }

            valorEvento = valorBase + recargoAsistentes + recargoPersonal;
            
            return valorEvento;
        }
    }
}
