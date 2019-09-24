using Comfandi_.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Comfandi_.Logica
{
    public class Consumo
    {
        public bool Autenticar(string idUsaurio, string contraseña) {
            try
            {
                //Instanciar Clase Conexion
                //Validar respuesta
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Respuesta ConsultarPreAprobado()
        {
            try
            {
                Thread.Sleep(5000);
                return new Respuesta() { Id = "102030", NombreUsuario = "Armando Casas", ValorConsulta = 20000M };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Respuesta ConsultarSaldo()
        {
            try
            {
                Thread.Sleep(5000);
                return new Respuesta() { Id = "102030", NombreUsuario = "Armando Casas", ValorConsulta = 20000M };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Respuesta ConsultarUltimoPago() {
            try
            {
                Thread.Sleep(5000);
                return new Respuesta() { Id="102030", NombreUsuario ="Armando Casas", Fecha="2019-01-12", ValorConsulta =20000M };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Respuesta ActualizarDatos()
        {
            try
            {
                Thread.Sleep(5000);
                return new Respuesta() { Id = "102030", NombreUsuario = "Armando Casas", Dirección = "Calle Falsa 123", Teléfono="1234567", Celular="3003487553"};
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
