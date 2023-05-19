using Modelo;
using ModeloDAO.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class SedexClienteControl
    {
        private SedexClienteDAO sedeClienteDao;

        public SedexClienteControl()
        {
            sedeClienteDao = new SedexClienteDAO();
        }

        public List<Sede> ListarTodosSedesOrd() { 
            return sedeClienteDao.ListarTodosSedesOrdDA();  
        }
        public List<SedeCliente> ListarTodosSedesCliente(string ruc)
        {
            return sedeClienteDao.ListarTodosSedeClienteDA(ruc);
        }

        public string Agregar(string ruc, int idSede)
        {
            return sedeClienteDao.MantenimientoDA(0 , ruc , idSede , 1);
        }

        public List<SedeClienteCons> ListarConsultaFiltro(int accion , string valor)
        {
            return sedeClienteDao.ListarConsultaFiltroDA(accion , valor);
        }
    }
}
